using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Lumel.Interfaces;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel.Presenters
{
    public class HabitacionPresenter
    {
        private IHabitacionView view;
        private IHabitacionRepository repository;
        private BindingSource habitacionBindingSource;
        private IEnumerable<Habitacion> habitacionList;

        public HabitacionPresenter(IHabitacionView view, IHabitacionRepository repository)
        {
            this.habitacionBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            // Suscribirse a los eventos de la vista
            this.view.SearchEvent += SearchHabitacion;
            this.view.AddNewEvent += AddNewHabitacion;
            this.view.EditEvent += LoadSelectedHabitacionToEdit;
            this.view.DeleteEvent += DeleteSelectedHabitacion;
            this.view.SaveEvent += SaveHabitacion;
            this.view.CancelEvent += CancelAction;
            this.view.LoadAllEvent += LoadAllHabitaciones;

            // Establecer el binding source
            this.view.SetHabitacionListBindingSource(habitacionBindingSource);

            // Cargar la lista de habitaciones
            LoadAllHabitaciones(this, EventArgs.Empty);
        }

        private void LoadAllHabitaciones(object sender, EventArgs e)
        {
            try
            {
                habitacionList = repository.GetAll();
                habitacionBindingSource.DataSource = habitacionList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de habitaciones: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchHabitacion(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
            {
                habitacionList = repository.GetByValue(this.view.SearchValue);
            }
            else
            {
                habitacionList = repository.GetAll();
            }
            habitacionBindingSource.DataSource = habitacionList;
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveHabitacion(object sender, EventArgs e)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(view.Numero))
            {
                view.Message = "El número de habitación es obligatorio.";
                view.IsSuccessful = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(view.Tipo))
            {
                view.Message = "El tipo de habitación es obligatorio.";
                view.IsSuccessful = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(view.Estado))
            {
                view.Message = "El estado de la habitación es obligatorio.";
                view.IsSuccessful = false;
                return;
            }

            var habitacion = new Habitacion();
            habitacion.Numero = view.Numero.Trim();
            habitacion.Tipo = view.Tipo;
            habitacion.Descripcion = view.Descripcion;
            
            // Validar que la capacidad sea un número entero válido y positivo
            if (int.TryParse(view.Capacidad, out int capacidad) && capacidad > 0)
            {
                habitacion.Capacidad = capacidad;
            }
            else
            {
                view.Message = "La capacidad debe ser un número entero positivo.";
                view.IsSuccessful = false;
                return;
            }

            // Validar que el precio por noche sea un número decimal válido y positivo
            if (decimal.TryParse(view.PrecioNoche, out decimal precioNoche) && precioNoche >= 0)
            {
                habitacion.PrecioNoche = precioNoche;
            }
            else
            {
                view.Message = "El precio por noche debe ser un número decimal positivo.";
                view.IsSuccessful = false;
                return;
            }

            habitacion.Estado = view.Estado;

            try
            {
                // Verificar si ya existe una habitación con el mismo número
                bool numeroExistente = false;
                int idHabitacionActual = 0;
                
                // Si estamos editando, guardamos el ID actual
                if (view.IsEdit)
                {
                    idHabitacionActual = Convert.ToInt32(view.IdHabitacion);
                }
                
                // Verificar si el número ya existe en otra habitación
                foreach (var h in repository.GetAll())
                {
                    if (h.Numero.Equals(habitacion.Numero, StringComparison.OrdinalIgnoreCase) && 
                        h.IdHabitacion != idHabitacionActual)
                    {
                        numeroExistente = true;
                        break;
                    }
                }
                
                if (numeroExistente)
                {
                    view.Message = $"Ya existe una habitación con el número {habitacion.Numero}. Por favor, elija otro número.";
                    view.IsSuccessful = false;
                    return;
                }

                // Si estamos editando
                if (view.IsEdit)
                {
                    habitacion.IdHabitacion = idHabitacionActual;
                    repository.Edit(habitacion);
                    view.Message = "Habitación actualizada correctamente";
                }
                // Si estamos agregando
                else
                {
                    repository.Add(habitacion);
                    view.Message = "Habitación agregada correctamente";
                }
                view.IsSuccessful = true;
                LoadAllHabitaciones(this, EventArgs.Empty);
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Error al guardar la habitación: " + ex.Message;
            }
        }

        private void CleanViewFields()
        {
            view.IdHabitacion = "0";
            view.Numero = "";
            view.Tipo = "";
            view.Descripcion = "";
            view.Capacidad = "";
            view.PrecioNoche = "";
            view.Estado = "Disponible";
            view.IsEdit = false;
        }

        private void DeleteSelectedHabitacion(object sender, EventArgs e)
        {
            try
            {
                var habitacion = (Habitacion)habitacionBindingSource.Current;
                if (habitacion != null)
                {
                    DialogResult result = MessageBox.Show(
                        $"¿Está seguro de eliminar la habitación {habitacion.Numero}?",
                        "Advertencia",
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        repository.Delete(habitacion.IdHabitacion);
                        view.IsSuccessful = true;
                        view.Message = "Habitación eliminada correctamente";
                        LoadAllHabitaciones(this, EventArgs.Empty);
                    }
                }
                else
                {
                    view.IsSuccessful = false;
                    view.Message = "Debe seleccionar una habitación para eliminar";
                }
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void LoadSelectedHabitacionToEdit(object sender, EventArgs e)
        {
            var habitacion = (Habitacion)habitacionBindingSource.Current;
            if (habitacion != null)
            {
                view.IdHabitacion = habitacion.IdHabitacion.ToString();
                view.Numero = habitacion.Numero;
                view.Tipo = habitacion.Tipo;
                view.Descripcion = habitacion.Descripcion ?? "";
                view.Capacidad = habitacion.Capacidad.ToString();
                view.PrecioNoche = habitacion.PrecioNoche.ToString();
                view.Estado = habitacion.Estado;
                view.IsEdit = true;
            }
        }

        private void AddNewHabitacion(object sender, EventArgs e)
        {
            view.IsEdit = false;
            CleanViewFields();
        }
    }
}
