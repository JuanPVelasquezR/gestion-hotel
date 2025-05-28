namespace Proyecto_Lumel
{
    partial class FormMenu
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnUsuarios = new FontAwesome.Sharp.IconButton();
            this.btnHuéspedes = new FontAwesome.Sharp.IconButton();
            this.btnHabitaciones = new FontAwesome.Sharp.IconButton();
            this.btnReservas = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.PictureBox();
            this.iconSplitButton1 = new FontAwesome.Sharp.IconSplitButton();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.btnExit = new FontAwesome.Sharp.IconButton();
            this.btnMaximize = new FontAwesome.Sharp.IconButton();
            this.btnMinimize = new FontAwesome.Sharp.IconButton();
            this.lblTitleChildForm = new System.Windows.Forms.Label();
            this.iconCurrentChildForm = new FontAwesome.Sharp.IconPictureBox();
            this.panelShadow = new System.Windows.Forms.Panel();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).BeginInit();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconCurrentChildForm)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelMenu.Controls.Add(this.btnUsuarios);
            this.panelMenu.Controls.Add(this.btnHuéspedes);
            this.panelMenu.Controls.Add(this.btnHabitaciones);
            this.panelMenu.Controls.Add(this.btnReservas);
            this.panelMenu.Controls.Add(this.panel1);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 574);
            this.panelMenu.TabIndex = 0;
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsuarios.FlatAppearance.BorderSize = 0;
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnUsuarios.IconChar = FontAwesome.Sharp.IconChar.UserCircle;
            this.btnUsuarios.IconColor = System.Drawing.Color.Gainsboro;
            this.btnUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.Location = new System.Drawing.Point(0, 320);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnUsuarios.Size = new System.Drawing.Size(220, 60);
            this.btnUsuarios.TabIndex = 5;
            this.btnUsuarios.Text = "Usuarios";
            this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnHuéspedes
            // 
            this.btnHuéspedes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHuéspedes.FlatAppearance.BorderSize = 0;
            this.btnHuéspedes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuéspedes.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnHuéspedes.IconChar = FontAwesome.Sharp.IconChar.UserAlt;
            this.btnHuéspedes.IconColor = System.Drawing.Color.Gainsboro;
            this.btnHuéspedes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHuéspedes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHuéspedes.Location = new System.Drawing.Point(0, 260);
            this.btnHuéspedes.Name = "btnHuéspedes";
            this.btnHuéspedes.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnHuéspedes.Size = new System.Drawing.Size(220, 60);
            this.btnHuéspedes.TabIndex = 4;
            this.btnHuéspedes.Text = "Huéspedes";
            this.btnHuéspedes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHuéspedes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuéspedes.UseVisualStyleBackColor = true;
            this.btnHuéspedes.Click += new System.EventHandler(this.btnHuéspedes_Click);
            // 
            // btnHabitaciones
            // 
            this.btnHabitaciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHabitaciones.FlatAppearance.BorderSize = 0;
            this.btnHabitaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHabitaciones.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnHabitaciones.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            this.btnHabitaciones.IconColor = System.Drawing.Color.Gainsboro;
            this.btnHabitaciones.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHabitaciones.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHabitaciones.Location = new System.Drawing.Point(0, 200);
            this.btnHabitaciones.Name = "btnHabitaciones";
            this.btnHabitaciones.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnHabitaciones.Size = new System.Drawing.Size(220, 60);
            this.btnHabitaciones.TabIndex = 3;
            this.btnHabitaciones.Text = "Habitaciones";
            this.btnHabitaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHabitaciones.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHabitaciones.UseVisualStyleBackColor = true;
            this.btnHabitaciones.Click += new System.EventHandler(this.btnHabitaciones_Click);
            // 
            // btnReservas
            // 
            this.btnReservas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReservas.FlatAppearance.BorderSize = 0;
            this.btnReservas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReservas.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnReservas.IconChar = FontAwesome.Sharp.IconChar.Calendar;
            this.btnReservas.IconColor = System.Drawing.Color.Gainsboro;
            this.btnReservas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReservas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReservas.Location = new System.Drawing.Point(0, 140);
            this.btnReservas.Name = "btnReservas";
            this.btnReservas.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnReservas.Size = new System.Drawing.Size(220, 60);
            this.btnReservas.TabIndex = 1;
            this.btnReservas.Text = "Reservas";
            this.btnReservas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReservas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReservas.UseVisualStyleBackColor = true;
            this.btnReservas.Click += new System.EventHandler(this.btnReservas_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnHome);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 140);
            this.panel1.TabIndex = 0;
            // 
            // btnHome
            // 
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnHome.Image = global::Proyecto_Lumel.Properties.Resources.Screenshot_2025_05_11_205258;
            this.btnHome.Location = new System.Drawing.Point(21, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(173, 134);
            this.btnHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHome.TabIndex = 0;
            this.btnHome.TabStop = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // iconSplitButton1
            // 
            this.iconSplitButton1.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.iconSplitButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconSplitButton1.IconColor = System.Drawing.Color.Black;
            this.iconSplitButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconSplitButton1.IconSize = 48;
            this.iconSplitButton1.Name = "iconSplitButton1";
            this.iconSplitButton1.Rotation = 0D;
            this.iconSplitButton1.Size = new System.Drawing.Size(23, 23);
            this.iconSplitButton1.Text = "iconSplitButton1";
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.panelTitleBar.Controls.Add(this.btnExit);
            this.panelTitleBar.Controls.Add(this.btnMaximize);
            this.panelTitleBar.Controls.Add(this.btnMinimize);
            this.panelTitleBar.Controls.Add(this.lblTitleChildForm);
            this.panelTitleBar.Controls.Add(this.iconCurrentChildForm);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(220, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(719, 75);
            this.panelTitleBar.TabIndex = 1;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnExit.IconChar = FontAwesome.Sharp.IconChar.X;
            this.btnExit.IconColor = System.Drawing.Color.Gainsboro;
            this.btnExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExit.Location = new System.Drawing.Point(688, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(25, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.btnMaximize.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
            this.btnMaximize.IconColor = System.Drawing.Color.Gainsboro;
            this.btnMaximize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMaximize.IconSize = 20;
            this.btnMaximize.Location = new System.Drawing.Point(651, 3);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(28, 23);
            this.btnMaximize.TabIndex = 4;
            this.btnMaximize.UseVisualStyleBackColor = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMinimize.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.btnMinimize.IconColor = System.Drawing.Color.Gainsboro;
            this.btnMinimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMinimize.IconSize = 20;
            this.btnMinimize.Location = new System.Drawing.Point(620, 3);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(25, 23);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // lblTitleChildForm
            // 
            this.lblTitleChildForm.AutoSize = true;
            this.lblTitleChildForm.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblTitleChildForm.Location = new System.Drawing.Point(55, 30);
            this.lblTitleChildForm.Name = "lblTitleChildForm";
            this.lblTitleChildForm.Size = new System.Drawing.Size(32, 13);
            this.lblTitleChildForm.TabIndex = 1;
            this.lblTitleChildForm.Text = "Inicio";
            // 
            // iconCurrentChildForm
            // 
            this.iconCurrentChildForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.iconCurrentChildForm.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.iconCurrentChildForm.IconChar = FontAwesome.Sharp.IconChar.HouseChimneyUser;
            this.iconCurrentChildForm.IconColor = System.Drawing.Color.MediumSlateBlue;
            this.iconCurrentChildForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconCurrentChildForm.Location = new System.Drawing.Point(17, 21);
            this.iconCurrentChildForm.Name = "iconCurrentChildForm";
            this.iconCurrentChildForm.Size = new System.Drawing.Size(32, 32);
            this.iconCurrentChildForm.TabIndex = 0;
            this.iconCurrentChildForm.TabStop = false;
            // 
            // panelShadow
            // 
            this.panelShadow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(24)))), ((int)(((byte)(58)))));
            this.panelShadow.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelShadow.Location = new System.Drawing.Point(220, 75);
            this.panelShadow.Name = "panelShadow";
            this.panelShadow.Size = new System.Drawing.Size(719, 9);
            this.panelShadow.TabIndex = 2;
            // 
            // panelDesktop
            // 
            this.panelDesktop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(220, 84);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(719, 490);
            this.panelDesktop.TabIndex = 3;
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 574);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelShadow);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.Name = "FormMenu";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).EndInit();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconCurrentChildForm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private FontAwesome.Sharp.IconButton btnReservas;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnUsuarios;
        private FontAwesome.Sharp.IconButton btnHuéspedes;
        private FontAwesome.Sharp.IconButton btnHabitaciones;
        private FontAwesome.Sharp.IconSplitButton iconSplitButton1;
        private System.Windows.Forms.PictureBox btnHome;
        private System.Windows.Forms.Panel panelTitleBar;
        private FontAwesome.Sharp.IconPictureBox iconCurrentChildForm;
        private System.Windows.Forms.Label lblTitleChildForm;
        private System.Windows.Forms.Panel panelShadow;
        private System.Windows.Forms.Panel panelDesktop;
        private FontAwesome.Sharp.IconButton btnMinimize;
        private FontAwesome.Sharp.IconButton btnExit;
        private FontAwesome.Sharp.IconButton btnMaximize;
    }
}

