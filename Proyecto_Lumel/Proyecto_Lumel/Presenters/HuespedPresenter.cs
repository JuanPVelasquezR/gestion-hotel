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
    public class HuespedPresenter
    {
        private IHuespedView view;
        private IHuespedRepository repository;
        private BindingSource huespedBindingSource;
        private IEnumerable<Huesped> huespedList;

        public HuespedPresenter(IHuespedView view, IHuespedRepository repository)
        {
            this.huespedBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            // Suscribirse a los eventos de la vista
            this.view.SearchEvent += SearchHuesped;
            this.view.AddNewEvent += AddNewHuesped;
            this.view.EditEvent += LoadSelectedHuespedToEdit;
            this.view.DeleteEvent += DeleteSelectedHuesped;
            this.view.SaveEvent += SaveHuesped;
            this.view.CancelEvent += CancelAction;
            this.view.LoadAllEvent += LoadAllHuespedes;

            // Establecer el binding source
            this.view.SetHuespedListBindingSource(huespedBindingSource);

            // Cargar la lista de huéspedes
            LoadAllHuespedes();
        }

        // Método para el evento LoadAllEvent
        private void LoadAllHuespedes(object sender, EventArgs e)
        {
            LoadAllHuespedes();
        }

        // Método para llamadas directas
        private void LoadAllHuespedes()
        {
            try
            {
                huespedList = repository.GetAll();
                huespedBindingSource.DataSource = huespedList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de huéspedes: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchHuesped(object sender, EventArgs e)
        {
            try
            {
                bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
                if (emptyValue)
                    huespedList = repository.GetAll();
                else
                    huespedList = repository.GetByFilter(this.view.SearchValue);

                huespedBindingSource.DataSource = huespedList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar huéspedes: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewHuesped(object sender, EventArgs e)
        {
            view.IsEdit = false;
            view.IdHuesped = "0";
            view.Nombre = "";
            view.Apellido = "";
            view.TipoDocumento = "";
            view.NumeroDocumento = "";
            view.Telefono = "";
            view.Correo = "";
            view.Direccion = "";
        }

        private void LoadSelectedHuespedToEdit(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay elementos en la lista y si hay un elemento actual seleccionado
                if (huespedBindingSource != null && huespedBindingSource.Current != null)
                {
                    var huesped = (Huesped)huespedBindingSource.Current;
                    
                    // Verificar que el huésped tenga un ID válido
                    if (huesped.IdHuesped <= 0)
                    {
                        MessageBox.Show("Por favor, seleccione un huésped válido para editar.", "Advertencia", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    view.IdHuesped = huesped.IdHuesped.ToString();
                    view.Nombre = huesped.Nombre ?? string.Empty;
                    view.Apellido = huesped.Apellido ?? string.Empty;
                    view.TipoDocumento = huesped.TipoDocumento ?? string.Empty;
                    view.NumeroDocumento = huesped.NumeroDocumento ?? string.Empty;
                    view.Telefono = huesped.Telefono ?? string.Empty;
                    view.Correo = huesped.Correo ?? string.Empty;
                    view.Direccion = huesped.Direccion ?? string.Empty;
                    view.IsEdit = true;
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un huésped para editar.", "Advertencia", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al cargar los datos del huésped: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveHuesped(object sender, EventArgs e)
        {
            try
            {
                // Validar que los datos básicos estén presentes
                if (string.IsNullOrWhiteSpace(view.Nombre) || 
                    string.IsNullOrWhiteSpace(view.Apellido) || 
                    string.IsNullOrWhiteSpace(view.NumeroDocumento))
                {
                    view.IsSuccessful = false;
                    view.Message = "Los campos Nombre, Apellido y Número de Documento son obligatorios.";
                    return;
                }
                
                // Validar el formato del ID
                if (!int.TryParse(view.IdHuesped, out int idHuesped))
                {
                    view.IsSuccessful = false;
                    view.Message = "El ID del huésped no es válido.";
                    return;
                }
                
                var huesped = new Huesped
                {
                    IdHuesped = idHuesped,
                    Nombre = view.Nombre,
                    Apellido = view.Apellido,
                    TipoDocumento = view.TipoDocumento,
                    NumeroDocumento = view.NumeroDocumento,
                    Telefono = string.IsNullOrWhiteSpace(view.Telefono) ? null : view.Telefono,
                    Correo = string.IsNullOrWhiteSpace(view.Correo) ? null : view.Correo,
                    Direccion = string.IsNullOrWhiteSpace(view.Direccion) ? null : view.Direccion
                };

                if (view.IsEdit) // Editar huésped
                {
                    // Verificar que exista el huésped a editar
                    if (idHuesped <= 0)
                    {
                        view.IsSuccessful = false;
                        view.Message = "No se puede editar un huésped sin ID válido.";
                        return;
                    }
                    
                    repository.Edit(huesped);
                    view.Message = "Huésped actualizado correctamente";
                }
                else // Nuevo huésped
                {
                    repository.Add(huesped);
                    view.Message = "Huésped agregado correctamente";
                }
                
                view.IsSuccessful = true;
                LoadAllHuespedes();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = $"Error al guardar el huésped: {ex.Message}";
            }
        }

        private void CleanViewFields()
        {
            view.IdHuesped = "0";
            view.Nombre = "";
            view.Apellido = "";
            view.TipoDocumento = "";
            view.NumeroDocumento = "";
            view.Telefono = "";
            view.Correo = "";
            view.Direccion = "";
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void DeleteSelectedHuesped(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay elementos en la lista y si hay un elemento actual seleccionado
                if (huespedBindingSource != null && huespedBindingSource.Current != null)
                {
                    var huesped = (Huesped)huespedBindingSource.Current;
                    
                    // Verificar que el huésped tenga un ID válido
                    if (huesped.IdHuesped <= 0)
                    {
                        view.IsSuccessful = false;
                        view.Message = "Por favor, seleccione un huésped válido para eliminar.";
                        return;
                    }
                    
                    repository.Delete(huesped.IdHuesped);
                    view.IsSuccessful = true;
                    view.Message = "Huésped eliminado correctamente";
                    LoadAllHuespedes();
                    CleanViewFields();
                }
                else
                {
                    view.IsSuccessful = false;
                    view.Message = "Por favor, seleccione un huésped para eliminar.";
                }
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = $"Error al eliminar el huésped: {ex.Message}";
            }
        }
    }
} 