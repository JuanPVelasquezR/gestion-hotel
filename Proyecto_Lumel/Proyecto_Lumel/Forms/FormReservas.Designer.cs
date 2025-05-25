namespace Proyecto_Lumel.Forms
{
    partial class FormReservas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageReservaList = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.btnFiltrarHabitacion = new System.Windows.Forms.Button();
            this.btnFiltrarHuesped = new System.Windows.Forms.Button();
            this.btnFiltrarFechas = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFiltroHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFiltroDesde = new System.Windows.Forms.DateTimePicker();
            this.cmbFiltroEstado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageReservaDetail = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCalcularPrecio = new System.Windows.Forms.Button();
            this.txtPrecioTotal = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPrecioNoche = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpFechaSalida = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFechaEntrada = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSeleccionarHabitacion = new System.Windows.Forms.Button();
            this.txtTipoHabitacion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNumeroHabitacion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIdHabitacion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSeleccionarHuesped = new System.Windows.Forms.Button();
            this.txtNombreHuesped = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIdHuesped = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtIdReserva = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(168, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = false;
            
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(62, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 1;
            
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buscar:";
            
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(524, 40);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = false;
            
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(605, 40);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = false;
            
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(686, 40);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = false;
            
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Estado:";
            
            // 
            // cmbFiltroEstado
            // 
            this.cmbFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroEstado.FormattingEnabled = true;
            this.cmbFiltroEstado.Location = new System.Drawing.Point(298, 16);
            this.cmbFiltroEstado.Name = "cmbFiltroEstado";
            this.cmbFiltroEstado.Size = new System.Drawing.Size(120, 21);
            this.cmbFiltroEstado.TabIndex = 7;
            
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Desde:";
            
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Hasta:";
            
            // 
            // dtpFiltroDesde
            // 
            this.dtpFiltroDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroDesde.Location = new System.Drawing.Point(62, 70);
            this.dtpFiltroDesde.Name = "dtpFiltroDesde";
            this.dtpFiltroDesde.Size = new System.Drawing.Size(100, 20);
            this.dtpFiltroDesde.TabIndex = 10;
            
            // 
            // dtpFiltroHasta
            // 
            this.dtpFiltroHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroHasta.Location = new System.Drawing.Point(212, 70);
            this.dtpFiltroHasta.Name = "dtpFiltroHasta";
            this.dtpFiltroHasta.Size = new System.Drawing.Size(100, 20);
            this.dtpFiltroHasta.TabIndex = 11;
            
            // 
            // btnFiltrarFechas
            // 
            this.btnFiltrarFechas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnFiltrarFechas.FlatAppearance.BorderSize = 0;
            this.btnFiltrarFechas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrarFechas.ForeColor = System.Drawing.Color.White;
            this.btnFiltrarFechas.Location = new System.Drawing.Point(318, 70);
            this.btnFiltrarFechas.Name = "btnFiltrarFechas";
            this.btnFiltrarFechas.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrarFechas.TabIndex = 12;
            this.btnFiltrarFechas.Text = "Filtrar";
            this.btnFiltrarFechas.UseVisualStyleBackColor = false;
            
            // 
            // btnFiltrarHuesped
            // 
            this.btnFiltrarHuesped.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnFiltrarHuesped.FlatAppearance.BorderSize = 0;
            this.btnFiltrarHuesped.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrarHuesped.ForeColor = System.Drawing.Color.White;
            this.btnFiltrarHuesped.Location = new System.Drawing.Point(399, 70);
            this.btnFiltrarHuesped.Name = "btnFiltrarHuesped";
            this.btnFiltrarHuesped.Size = new System.Drawing.Size(100, 23);
            this.btnFiltrarHuesped.TabIndex = 13;
            this.btnFiltrarHuesped.Text = "Filtrar Huésped";
            this.btnFiltrarHuesped.UseVisualStyleBackColor = false;
            
            // 
            // btnFiltrarHabitacion
            // 
            this.btnFiltrarHabitacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnFiltrarHabitacion.FlatAppearance.BorderSize = 0;
            this.btnFiltrarHabitacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrarHabitacion.ForeColor = System.Drawing.Color.White;
            this.btnFiltrarHabitacion.Location = new System.Drawing.Point(505, 70);
            this.btnFiltrarHabitacion.Name = "btnFiltrarHabitacion";
            this.btnFiltrarHabitacion.Size = new System.Drawing.Size(100, 23);
            this.btnFiltrarHabitacion.TabIndex = 14;
            this.btnFiltrarHabitacion.Text = "Filtrar Habitación";
            this.btnFiltrarHabitacion.UseVisualStyleBackColor = false;
            
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnLimpiarFiltros.FlatAppearance.BorderSize = 0;
            this.btnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarFiltros.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(611, 70);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(100, 23);
            this.btnLimpiarFiltros.TabIndex = 15;
            this.btnLimpiarFiltros.Text = "Limpiar Filtros";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = false;
            
            // 
            // txtIdReserva
            // 
            this.txtIdReserva.Location = new System.Drawing.Point(150, 20);
            this.txtIdReserva.Name = "txtIdReserva";
            this.txtIdReserva.ReadOnly = true;
            this.txtIdReserva.Size = new System.Drawing.Size(100, 20);
            this.txtIdReserva.TabIndex = 1;
            this.txtIdReserva.Visible = false;
            
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(20, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "ID Reserva";
            this.label16.Visible = false;
            
            // 
            // txtIdHuesped
            // 
            this.txtIdHuesped.Location = new System.Drawing.Point(150, 50);
            this.txtIdHuesped.Name = "txtIdHuesped";
            this.txtIdHuesped.ReadOnly = true;
            this.txtIdHuesped.Size = new System.Drawing.Size(100, 20);
            this.txtIdHuesped.TabIndex = 3;
            
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(20, 53);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "ID Huésped";
            
            // 
            // txtNombreHuesped
            // 
            this.txtNombreHuesped.Location = new System.Drawing.Point(150, 80);
            this.txtNombreHuesped.Name = "txtNombreHuesped";
            this.txtNombreHuesped.ReadOnly = true;
            this.txtNombreHuesped.Size = new System.Drawing.Size(200, 20);
            this.txtNombreHuesped.TabIndex = 5;
            
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Nombre Huésped";
            
            // 
            // btnSeleccionarHuesped
            // 
            this.btnSeleccionarHuesped.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnSeleccionarHuesped.FlatAppearance.BorderSize = 0;
            this.btnSeleccionarHuesped.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarHuesped.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarHuesped.Location = new System.Drawing.Point(356, 79);
            this.btnSeleccionarHuesped.Name = "btnSeleccionarHuesped";
            this.btnSeleccionarHuesped.Size = new System.Drawing.Size(75, 23);
            this.btnSeleccionarHuesped.TabIndex = 6;
            this.btnSeleccionarHuesped.Text = "Seleccionar";
            this.btnSeleccionarHuesped.UseVisualStyleBackColor = false;
            
            // 
            // txtIdHabitacion
            // 
            this.txtIdHabitacion.Location = new System.Drawing.Point(150, 110);
            this.txtIdHabitacion.Name = "txtIdHabitacion";
            this.txtIdHabitacion.ReadOnly = true;
            this.txtIdHabitacion.Size = new System.Drawing.Size(100, 20);
            this.txtIdHabitacion.TabIndex = 8;
            
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "ID Habitación";
            
            // 
            // txtNumeroHabitacion
            // 
            this.txtNumeroHabitacion.Location = new System.Drawing.Point(150, 140);
            this.txtNumeroHabitacion.Name = "txtNumeroHabitacion";
            this.txtNumeroHabitacion.ReadOnly = true;
            this.txtNumeroHabitacion.Size = new System.Drawing.Size(100, 20);
            this.txtNumeroHabitacion.TabIndex = 10;
            
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Número Habitación";
            
            // 
            // txtTipoHabitacion
            // 
            this.txtTipoHabitacion.Location = new System.Drawing.Point(150, 170);
            this.txtTipoHabitacion.Name = "txtTipoHabitacion";
            this.txtTipoHabitacion.ReadOnly = true;
            this.txtTipoHabitacion.Size = new System.Drawing.Size(200, 20);
            this.txtTipoHabitacion.TabIndex = 12;
            
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 173);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Tipo Habitación";
            
            // 
            // btnSeleccionarHabitacion
            // 
            this.btnSeleccionarHabitacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnSeleccionarHabitacion.FlatAppearance.BorderSize = 0;
            this.btnSeleccionarHabitacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarHabitacion.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarHabitacion.Location = new System.Drawing.Point(356, 169);
            this.btnSeleccionarHabitacion.Name = "btnSeleccionarHabitacion";
            this.btnSeleccionarHabitacion.Size = new System.Drawing.Size(75, 23);
            this.btnSeleccionarHabitacion.TabIndex = 13;
            this.btnSeleccionarHabitacion.Text = "Seleccionar";
            this.btnSeleccionarHabitacion.UseVisualStyleBackColor = false;
            
            // 
            // dtpFechaEntrada
            // 
            this.dtpFechaEntrada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEntrada.Location = new System.Drawing.Point(150, 200);
            this.dtpFechaEntrada.Name = "dtpFechaEntrada";
            this.dtpFechaEntrada.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaEntrada.TabIndex = 15;
            
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 203);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Fecha Entrada";
            
            // 
            // dtpFechaSalida
            // 
            this.dtpFechaSalida.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaSalida.Location = new System.Drawing.Point(150, 230);
            this.dtpFechaSalida.Name = "dtpFechaSalida";
            this.dtpFechaSalida.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaSalida.TabIndex = 17;
            
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 233);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Fecha Salida";
            
            // 
            // txtPrecioNoche
            // 
            this.txtPrecioNoche.Location = new System.Drawing.Point(150, 260);
            this.txtPrecioNoche.Name = "txtPrecioNoche";
            this.txtPrecioNoche.ReadOnly = true;
            this.txtPrecioNoche.Size = new System.Drawing.Size(100, 20);
            this.txtPrecioNoche.TabIndex = 19;
            
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 263);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Precio/Noche";
            
            // 
            // txtPrecioTotal
            // 
            this.txtPrecioTotal.Location = new System.Drawing.Point(150, 290);
            this.txtPrecioTotal.Name = "txtPrecioTotal";
            this.txtPrecioTotal.ReadOnly = true;
            this.txtPrecioTotal.Size = new System.Drawing.Size(100, 20);
            this.txtPrecioTotal.TabIndex = 21;
            
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 293);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Precio Total";
            
            // 
            // btnCalcularPrecio
            // 
            this.btnCalcularPrecio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnCalcularPrecio.FlatAppearance.BorderSize = 0;
            this.btnCalcularPrecio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalcularPrecio.ForeColor = System.Drawing.Color.White;
            this.btnCalcularPrecio.Location = new System.Drawing.Point(256, 289);
            this.btnCalcularPrecio.Name = "btnCalcularPrecio";
            this.btnCalcularPrecio.Size = new System.Drawing.Size(75, 23);
            this.btnCalcularPrecio.TabIndex = 22;
            this.btnCalcularPrecio.Text = "Calcular";
            this.btnCalcularPrecio.UseVisualStyleBackColor = false;
            
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(500, 50);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(150, 21);
            this.cmbEstado.TabIndex = 24;
            
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(450, 53);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Estado";
            
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(500, 80);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(250, 100);
            this.txtObservaciones.TabIndex = 26;
            
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(450, 83);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 13);
            this.label14.TabIndex = 25;
            this.label14.Text = "Observaciones";
            
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(605, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = false;
            
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(686, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;

            this.tabControl1.SuspendLayout();
            this.tabPageReservaList.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabPageReservaDetail.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();

            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageReservaList);
            this.tabControl1.Controls.Add(this.tabPageReservaDetail);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;

            // 
            // tabPageReservaList
            // 
            this.tabPageReservaList.Controls.Add(this.panel1);
            this.tabPageReservaList.Location = new System.Drawing.Point(4, 22);
            this.tabPageReservaList.Name = "tabPageReservaList";
            this.tabPageReservaList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReservaList.Size = new System.Drawing.Size(768, 400);
            this.tabPageReservaList.TabIndex = 0;
            this.tabPageReservaList.Text = "Lista de Reservas";
            this.tabPageReservaList.UseVisualStyleBackColor = true;

            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(762, 394);
            this.panel1.TabIndex = 0;

            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(762, 294);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnLimpiarFiltros);
            this.panel2.Controls.Add(this.btnFiltrarHabitacion);
            this.panel2.Controls.Add(this.btnFiltrarHuesped);
            this.panel2.Controls.Add(this.btnFiltrarFechas);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpFiltroHasta);
            this.panel2.Controls.Add(this.dtpFiltroDesde);
            this.panel2.Controls.Add(this.cmbFiltroEstado);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 294);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(762, 100);
            this.panel2.TabIndex = 0;

            // 
            // tabPageReservaDetail
            // 
            this.tabPageReservaDetail.Controls.Add(this.panel3);
            this.tabPageReservaDetail.Location = new System.Drawing.Point(4, 22);
            this.tabPageReservaDetail.Name = "tabPageReservaDetail";
            this.tabPageReservaDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReservaDetail.Size = new System.Drawing.Size(768, 400);
            this.tabPageReservaDetail.TabIndex = 1;
            this.tabPageReservaDetail.Text = "Detalle de Reserva";
            this.tabPageReservaDetail.UseVisualStyleBackColor = true;

            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtObservaciones);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.cmbEstado);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.btnCalcularPrecio);
            this.panel3.Controls.Add(this.txtPrecioTotal);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.txtPrecioNoche);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.dtpFechaSalida);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.dtpFechaEntrada);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.btnSeleccionarHabitacion);
            this.panel3.Controls.Add(this.txtTipoHabitacion);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtNumeroHabitacion);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtIdHabitacion);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.btnSeleccionarHuesped);
            this.panel3.Controls.Add(this.txtNombreHuesped);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtIdHuesped);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.txtIdReserva);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(762, 394);
            this.panel3.TabIndex = 0;
            this.panel3.AutoScroll = true;

            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnCancel);
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 344);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(762, 50);
            this.panel4.TabIndex = 0;

            // 
            // FormReservas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormReservas";
            this.Text = "Gestión de Reservas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl1.ResumeLayout(false);
            this.tabPageReservaList.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPageReservaDetail.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageReservaList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageReservaDetail;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtIdReserva;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtIdHuesped;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNombreHuesped;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSeleccionarHuesped;
        private System.Windows.Forms.TextBox txtIdHabitacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNumeroHabitacion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTipoHabitacion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSeleccionarHabitacion;
        private System.Windows.Forms.DateTimePicker dtpFechaEntrada;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpFechaSalida;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPrecioNoche;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPrecioTotal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnCalcularPrecio;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbFiltroEstado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFiltroDesde;
        private System.Windows.Forms.DateTimePicker dtpFiltroHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFiltrarFechas;
        private System.Windows.Forms.Button btnFiltrarHuesped;
        private System.Windows.Forms.Button btnFiltrarHabitacion;
        private System.Windows.Forms.Button btnLimpiarFiltros;
    }
}