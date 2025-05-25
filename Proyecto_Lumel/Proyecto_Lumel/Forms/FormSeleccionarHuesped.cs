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
    public partial class FormSeleccionarHuesped : Form
    {
        private HuespedRepository repository;
        private Huesped huespedSeleccionado;

        public Huesped HuespedSeleccionado { get => huespedSeleccionado; }

        public FormSeleccionarHuesped()
        {
            InitializeComponent();
            repository = new HuespedRepository();
            CargarHuespedes();
        }

        private void CargarHuespedes()
        {
            try
            {
                var huespedes = repository.GetAll();
                dataGridView1.DataSource = huespedes;

                // Configurar columnas
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[0].Width = 50;

                    if (dataGridView1.Columns.Count > 1)
                    {
                        dataGridView1.Columns[1].HeaderText = "Nombre";
                        dataGridView1.Columns[1].Width = 150;
                    }

                    if (dataGridView1.Columns.Count > 2)
                    {
                        dataGridView1.Columns[2].HeaderText = "Apellidos";
                        dataGridView1.Columns[2].Width = 150;
                    }

                    if (dataGridView1.Columns.Count > 3)
                    {
                        dataGridView1.Columns[3].HeaderText = "Documento";
                        dataGridView1.Columns[3].Width = 100;
                    }

                    if (dataGridView1.Columns.Count > 4)
                    {
                        dataGridView1.Columns[4].HeaderText = "Email";
                        dataGridView1.Columns[4].Width = 150;
                    }

                    if (dataGridView1.Columns.Count > 5)
                    {
                        dataGridView1.Columns[5].HeaderText = "Teléfono";
                        dataGridView1.Columns[5].Width = 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar huéspedes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    var huespedes = repository.GetByValue(txtBuscar.Text);
                    dataGridView1.DataSource = huespedes;
                }
                else
                {
                    CargarHuespedes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar huéspedes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                huespedSeleccionado = repository.GetById(id);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un huésped", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
