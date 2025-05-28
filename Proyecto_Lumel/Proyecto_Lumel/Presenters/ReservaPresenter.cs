using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Lumel.Interfaces;
using Proyecto_Lumel.Models;
using Proyecto_Lumel.Data;
using Proyecto_Lumel.Forms;

namespace Proyecto_Lumel.Presenters
{
    public class ReservaPresenter
    {
        private IReservaView view;
        private IReservaRepository repository;
        private BindingSource reservasBindingSource;
        private IEnumerable<Reserva> reservaList;

        public ReservaPresenter(IReservaView view, IReservaRepository repository)
        {
            this.reservasBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            // Suscribirse a eventos de la vista
            this.view.SearchEvent += SearchReserva;
            this.view.AddNewEvent += AddNewReserva;
            this.view.EditEvent += LoadSelectedReservaToEdit;
            this.view.DeleteEvent += DeleteSelectedReserva;
            this.view.SaveEvent += SaveReserva;
            this.view.CancelEvent += CancelAction;
            this.view.SeleccionarHuespedEvent += SeleccionarHuesped;
            this.view.SeleccionarHabitacionEvent += SeleccionarHabitacion;
            this.view.CalcularPrecioEvent += CalcularPrecio;
            this.view.FiltrarPorFechasEvent += FiltrarPorFechas;
            this.view.FiltrarPorEstadoEvent += FiltrarPorEstado;
            this.view.FiltrarPorHuespedEvent += FiltrarPorHuesped;
            this.view.FiltrarPorHabitacionEvent += FiltrarPorHabitacion;
            this.view.LimpiarFiltrosEvent += LimpiarFiltros;

            // Establecer binding source
            this.view.SetReservaListBindingSource(reservasBindingSource);

            // Cargar todas las reservas al iniciar
            LoadAllReservas();
        }

        private void LoadAllReservas()
        {
            reservaList = repository.GetAll();
            reservasBindingSource.DataSource = reservaList;
        }

        private void SearchReserva(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                reservaList = repository.GetByValue(this.view.SearchValue);
            else
                reservaList = repository.GetAll();
            reservasBindingSource.DataSource = reservaList;
        }

        private void AddNewReserva(object sender, EventArgs e)
        {
            view.IsEdit = false;
            view.IdReserva = "0";
            view.IdHuesped = "";
            view.NombreHuesped = "";
            view.IdHabitacion = "";
            view.NumeroHabitacion = "";
            view.TipoHabitacion = "";
            view.FechaEntrada = DateTime.Now;
            view.FechaSalida = DateTime.Now.AddDays(1);
            view.PrecioNoche = "0";
            view.PrecioTotal = "0";
            view.Estado = "Pendiente";
            view.Observaciones = "";
        }

        private void LoadSelectedReservaToEdit(object sender, EventArgs e)
        {
            var reserva = (Reserva)reservasBindingSource.Current;
            if (reserva != null)
            {
                view.IsEdit = true;
                view.IdReserva = reserva.IdReserva.ToString();
                view.IdHuesped = reserva.IdHuesped.ToString();
                view.NombreHuesped = reserva.NombreHuesped;
                view.IdHabitacion = reserva.IdHabitacion.ToString();
                view.NumeroHabitacion = reserva.NumeroHabitacion;
                view.TipoHabitacion = reserva.TipoHabitacion;
                view.FechaEntrada = reserva.FechaEntrada;
                view.FechaSalida = reserva.FechaSalida;
                view.PrecioNoche = reserva.PrecioNoche.ToString();
                view.PrecioTotal = reserva.PrecioTotal.ToString();
                view.Estado = reserva.Estado;
                view.Observaciones = reserva.Observaciones ?? "";
            }
        }

        private void SaveReserva(object sender, EventArgs e)
        {
            var reserva = new Reserva();
            
            // Validar que se haya seleccionado un huésped
            if (string.IsNullOrEmpty(view.IdHuesped))
            {
                view.IsSuccessful = false;
                view.Message = "Debe seleccionar un huésped";
                return;
            }
            
            // Validar que se haya seleccionado una habitación
            if (string.IsNullOrEmpty(view.IdHabitacion))
            {
                view.IsSuccessful = false;
                view.Message = "Debe seleccionar una habitación";
                return;
            }
            
            // Validar fechas
            if (view.FechaEntrada >= view.FechaSalida)
            {
                view.IsSuccessful = false;
                view.Message = "La fecha de entrada debe ser anterior a la fecha de salida";
                return;
            }
            
            // Validar que la fecha de entrada no sea anterior a la fecha actual
            if (view.FechaEntrada.Date < DateTime.Today)
            {
                view.IsSuccessful = false;
                view.Message = "La fecha de entrada no puede ser anterior a la fecha actual";
                return;
            }
            
            // Validar que el período de reserva sea razonable (por ejemplo, máximo 30 días)
            TimeSpan duracion = view.FechaSalida - view.FechaEntrada;
            if (duracion.TotalDays > 30)
            {
                view.IsSuccessful = false;
                view.Message = "La duración de la reserva no puede exceder los 30 días";
                return;
            }
            
            // Validar precio
            decimal precioTotal;
            if (!decimal.TryParse(view.PrecioTotal, out precioTotal) || precioTotal <= 0)
            {
                view.IsSuccessful = false;
                view.Message = "El precio total debe ser un valor numérico mayor a cero";
                return;
            }
            
            // Asignar valores a la reserva
            reserva.IdHuesped = int.Parse(view.IdHuesped);
            reserva.IdHabitacion = int.Parse(view.IdHabitacion);
            reserva.FechaEntrada = view.FechaEntrada;
            reserva.FechaSalida = view.FechaSalida;
            reserva.PrecioTotal = precioTotal;
            reserva.Estado = view.Estado;
            reserva.Observaciones = view.Observaciones;
            
            try
            {
                // Nuevo registro
                if (view.IsEdit == false)
                {
                    repository.Add(reserva);
                    view.Message = "Reserva agregada correctamente";
                }
                // Actualizar registro existente
                else
                {
                    reserva.IdReserva = int.Parse(view.IdReserva);
                    repository.Edit(reserva);
                    view.Message = "Reserva actualizada correctamente";
                }
                view.IsSuccessful = true;
                LoadAllReservas();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void CleanViewFields()
        {
            view.IdReserva = "0";
            view.IdHuesped = "";
            view.NombreHuesped = "";
            view.IdHabitacion = "";
            view.NumeroHabitacion = "";
            view.TipoHabitacion = "";
            view.FechaEntrada = DateTime.Now;
            view.FechaSalida = DateTime.Now.AddDays(1);
            view.PrecioNoche = "0";
            view.PrecioTotal = "0";
            view.Estado = "Pendiente";
            view.Observaciones = "";
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void DeleteSelectedReserva(object sender, EventArgs e)
        {
            try
            {
                var reserva = (Reserva)reservasBindingSource.Current;
                if (reserva != null)
                {
                    DialogResult result = MessageBox.Show(
                        $"¿Está seguro de eliminar la reserva de {reserva.NombreHuesped}?",
                        "Advertencia",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    
                    if (result == DialogResult.Yes)
                    {
                        repository.Delete(reserva.IdReserva);
                        view.IsSuccessful = true;
                        view.Message = "Reserva eliminada correctamente";
                        LoadAllReservas();
                    }
                }
                else
                {
                    view.IsSuccessful = false;
                    view.Message = "No hay reserva seleccionada";
                }
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void SeleccionarHuesped(object sender, EventArgs e)
        {
            try
            {
                using (var form = new FormSeleccionarHuesped())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK && form.HuespedSeleccionado != null)
                    {
                        view.IdHuesped = form.HuespedSeleccionado.IdHuesped.ToString();
                        view.NombreHuesped = form.HuespedSeleccionado.Nombre + " " + form.HuespedSeleccionado.Apellido;
                    }
                }
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Error al seleccionar huésped: " + ex.Message;
                MessageBox.Show("Error al seleccionar huésped: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SeleccionarHabitacion(object sender, EventArgs e)
        {
            try
            {
                using (var form = new FormSeleccionarHabitacion())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK && form.HabitacionSeleccionada != null)
                    {
                        view.IdHabitacion = form.HabitacionSeleccionada.IdHabitacion.ToString();
                        view.NumeroHabitacion = form.HabitacionSeleccionada.Numero;
                        view.TipoHabitacion = form.HabitacionSeleccionada.Tipo;
                        view.PrecioNoche = form.HabitacionSeleccionada.PrecioNoche.ToString();
                        
                        // Calcular precio total automáticamente
                        CalcularPrecio(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Error al seleccionar habitación: " + ex.Message;
                MessageBox.Show("Error al seleccionar habitación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularPrecio(object sender, EventArgs e)
        {
            try
            {
                // Verificar que se haya seleccionado una habitación
                if (string.IsNullOrEmpty(view.IdHabitacion) || string.IsNullOrEmpty(view.PrecioNoche))
                {
                    view.IsSuccessful = false;
                    view.Message = "Debe seleccionar una habitación primero";
                    return;
                }
                
                // Verificar fechas válidas
                if (view.FechaEntrada >= view.FechaSalida)
                {
                    view.IsSuccessful = false;
                    view.Message = "La fecha de entrada debe ser anterior a la fecha de salida";
                    return;
                }
                
                // Calcular días de estancia
                int dias = (int)(view.FechaSalida - view.FechaEntrada).TotalDays;
                if (dias <= 0) dias = 1; // Mínimo un día
                
                // Calcular precio total
                decimal precioNoche = decimal.Parse(view.PrecioNoche);
                decimal precioTotal = dias * precioNoche;
                
                // Actualizar campo de precio total
                view.PrecioTotal = precioTotal.ToString();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Error al calcular precio: " + ex.Message;
            }
        }

        private void FiltrarPorFechas(object sender, EventArgs e)
        {
            try
            {
                // Obtener las fechas de los controles de filtro
                // Nota: Aquí asumimos que hay propiedades adicionales en la vista para las fechas de filtro
                // Si no existen, se deberían agregar a la interfaz IReservaView
                DateTime fechaDesde = DateTime.Now;
                DateTime fechaHasta = DateTime.Now;
                
                // Intentar obtener las fechas del control de filtro
                // Si hay un error, usar las fechas de los controles de edición como fallback
                try {
                    // Obtener las fechas de los controles de filtro mediante reflexión
                    var viewForm = (Form)view;
                    var dtpFiltroDesde = viewForm.Controls.Find("dtpFiltroDesde", true).FirstOrDefault() as DateTimePicker;
                    var dtpFiltroHasta = viewForm.Controls.Find("dtpFiltroHasta", true).FirstOrDefault() as DateTimePicker;
                    
                    if (dtpFiltroDesde != null && dtpFiltroHasta != null)
                    {
                        fechaDesde = dtpFiltroDesde.Value;
                        fechaHasta = dtpFiltroHasta.Value;
                    }
                    else
                    {
                        // Si no se encuentran los controles, usar las fechas de edición
                        fechaDesde = view.FechaEntrada;
                        fechaHasta = view.FechaSalida;
                    }
                }
                catch {
                    // Si hay un error, usar las fechas de los controles de edición
                    fechaDesde = view.FechaEntrada;
                    fechaHasta = view.FechaSalida;
                }
                
                // Asegurarse de que la fecha desde es anterior a la fecha hasta
                if (fechaDesde > fechaHasta)
                {
                    var temp = fechaDesde;
                    fechaDesde = fechaHasta;
                    fechaHasta = temp;
                }
                
                // Filtrar las reservas por las fechas
                reservaList = repository.GetByFechas(fechaDesde, fechaHasta);
                reservasBindingSource.DataSource = reservaList;
                
                view.IsSuccessful = true;
                view.Message = $"Se encontraron {reservaList.Count()} reservas entre {fechaDesde.ToShortDateString()} y {fechaHasta.ToShortDateString()}";
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Error al filtrar por fechas: " + ex.Message;
                MessageBox.Show("Error al filtrar por fechas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarPorEstado(object sender, EventArgs e)
        {
            try
            {
                // Obtener el estado seleccionado del ComboBox de filtro
                string estadoFiltro = ((ComboBox)sender).Text;
                
                // Si se selecciona "Todos", mostrar todas las reservas
                if (string.IsNullOrEmpty(estadoFiltro) || estadoFiltro == "Todos")
                {
                    LoadAllReservas();
                    return;
                }
                
                // Filtrar por el estado seleccionado
                reservaList = repository.GetByEstado(estadoFiltro);
                reservasBindingSource.DataSource = reservaList;
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Error al filtrar por estado: " + ex.Message;
                MessageBox.Show("Error al filtrar por estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarPorHuesped(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(view.IdHuesped))
                {
                    int idHuesped = int.Parse(view.IdHuesped);
                    reservaList = repository.GetByHuesped(idHuesped);
                    reservasBindingSource.DataSource = reservaList;
                }
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Error al filtrar por huésped: " + ex.Message;
            }
        }

        private void FiltrarPorHabitacion(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(view.IdHabitacion))
                {
                    int idHabitacion = int.Parse(view.IdHabitacion);
                    reservaList = repository.GetByHabitacion(idHabitacion);
                    reservasBindingSource.DataSource = reservaList;
                }
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Error al filtrar por habitación: " + ex.Message;
            }
        }

        private void LimpiarFiltros(object sender, EventArgs e)
        {
            try
            {
                // Limpiar el campo de búsqueda
                view.SearchValue = "";
                
                // Restablecer los controles de filtro a sus valores predeterminados
                var viewForm = (Form)view;
                
                // Restablecer el ComboBox de filtro de estado
                var cboFiltroEstado = viewForm.Controls.Find("cboFiltroEstado", true).FirstOrDefault() as ComboBox;
                if (cboFiltroEstado != null && cboFiltroEstado.Items.Count > 0)
                {
                    cboFiltroEstado.SelectedIndex = 0; // Seleccionar "Todos"
                }
                
                // Restablecer los DateTimePicker de filtro de fechas
                var dtpFiltroDesde = viewForm.Controls.Find("dtpFiltroDesde", true).FirstOrDefault() as DateTimePicker;
                var dtpFiltroHasta = viewForm.Controls.Find("dtpFiltroHasta", true).FirstOrDefault() as DateTimePicker;
                
                if (dtpFiltroDesde != null)
                {
                    dtpFiltroDesde.Value = DateTime.Now;
                }
                
                if (dtpFiltroHasta != null)
                {
                    dtpFiltroHasta.Value = DateTime.Now.AddDays(30);
                }
                
                // Cargar todas las reservas
                LoadAllReservas();
                
                view.IsSuccessful = true;
                view.Message = "Filtros limpiados correctamente";
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Error al limpiar filtros: " + ex.Message;
                MessageBox.Show("Error al limpiar filtros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
