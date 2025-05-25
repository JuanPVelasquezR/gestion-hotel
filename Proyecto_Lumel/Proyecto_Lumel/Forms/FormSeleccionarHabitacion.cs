using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Lumel.Models;
using Proyecto_Lumel.Data;

namespace Proyecto_Lumel.Forms
{
    public partial class FormSeleccionarHabitacion : Form
    {
        private HabitacionRepository repository;
        private Habitacion habitacionSeleccionada;

        public Habitacion HabitacionSeleccionada { get => habitacionSeleccionada; }

        public FormSeleccionarHabitacion()
        {
            InitializeComponent();
            repository = new HabitacionRepository();
            CargarHabitaciones();
        }

        private void CargarHabitaciones()
        {
            try
            {
                var habitaciones = repository.GetAll();
                // Filtrar solo habitaciones disponibles
                var habitacionesDisponibles = habitaciones.Where(h => h.Estado == "Disponible").ToList();
                dataGridView1.DataSource = habitacionesDisponibles;

                // Configurar columnas
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[0].Width = 50;

                    if (dataGridView1.Columns.Count > 1)
                    {
                        dataGridView1.Columns[1].HeaderText = "Número";
                        dataGridView1.Columns[1].Width = 80;
                    }

                    if (dataGridView1.Columns.Count > 2)
                    {
                        dataGridView1.Columns[2].HeaderText = "Tipo";
                        dataGridView1.Columns[2].Width = 120;
                    }

                    if (dataGridView1.Columns.Count > 3)
                    {
                        dataGridView1.Columns[3].HeaderText = "Precio/Noche";
                        dataGridView1.Columns[3].Width = 100;
                        dataGridView1.Columns[3].DefaultCellStyle.Format = "C2";
                    }

                    if (dataGridView1.Columns.Count > 4)
                    {
                        dataGridView1.Columns[4].HeaderText = "Estado";
                        dataGridView1.Columns[4].Width = 100;
                    }

                    if (dataGridView1.Columns.Count > 5)
                    {
                        dataGridView1.Columns[5].HeaderText = "Capacidad";
                        dataGridView1.Columns[5].Width = 80;
                    }

                    if (dataGridView1.Columns.Count > 6)
                    {
                        dataGridView1.Columns[6].HeaderText = "Descripción";
                        dataGridView1.Columns[6].Width = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar habitaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    var habitaciones = repository.GetByValue(txtBuscar.Text);
                    // Filtrar solo habitaciones disponibles
                    var habitacionesDisponibles = habitaciones.Where(h => h.Estado == "Disponible").ToList();
                    dataGridView1.DataSource = habitacionesDisponibles;
                }
                else
                {
                    CargarHabitaciones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar habitaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                habitacionSeleccionada = repository.GetById(id);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una habitación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tipoSeleccionado = cmbTipo.SelectedItem.ToString();
                
                if (tipoSeleccionado == "Todos")
                {
                    CargarHabitaciones();
                }
                else
                {
                    var habitaciones = repository.GetByTipo(tipoSeleccionado);
                    // Filtrar solo habitaciones disponibles
                    var habitacionesDisponibles = habitaciones.Where(h => h.Estado == "Disponible").ToList();
                    dataGridView1.DataSource = habitacionesDisponibles;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar por tipo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
