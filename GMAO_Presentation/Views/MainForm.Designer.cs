
namespace GMAO_Presentation.Views
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imgExit = new System.Windows.Forms.PictureBox();
            this.imgVersProfil = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSideBar = new System.Windows.Forms.PictureBox();
            this.sideBar = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEquipement = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnIntervention = new System.Windows.Forms.Button();
            this.WO = new System.Windows.Forms.FlowLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnWorders = new System.Windows.Forms.Button();
            this.panel15 = new System.Windows.Forms.Panel();
            this.btnWOCO = new System.Windows.Forms.Button();
            this.panel16 = new System.Windows.Forms.Panel();
            this.btnWOI = new System.Windows.Forms.Button();
            this.menuContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnMaintenance = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnGstMaintenanceCo = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnGstMaintenancePlanifie = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.btnAlertes = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.btnGstStock = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btnGstEquipes = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnSuiviCB = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.panel14 = new System.Windows.Forms.Panel();
            this.btnConfig = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.menuTransition = new System.Windows.Forms.Timer(this.components);
            this.sideTransition = new System.Windows.Forms.Timer(this.components);
            this.woTransition = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVersProfil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSideBar)).BeginInit();
            this.sideBar.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.WO.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel16.SuspendLayout();
            this.menuContainer.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel14.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.imgExit);
            this.panel1.Controls.Add(this.imgVersProfil);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSideBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1284, 75);
            this.panel1.TabIndex = 1;
            // 
            // imgExit
            // 
            this.imgExit.Image = global::GMAO_Presentation.Properties.Resources.Emergency_Exit;
            this.imgExit.Location = new System.Drawing.Point(1222, 7);
            this.imgExit.Name = "imgExit";
            this.imgExit.Size = new System.Drawing.Size(50, 50);
            this.imgExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgExit.TabIndex = 4;
            this.imgExit.TabStop = false;
            this.imgExit.Click += new System.EventHandler(this.imgExit_Click);
            // 
            // imgVersProfil
            // 
            this.imgVersProfil.Image = global::GMAO_Presentation.Properties.Resources.User;
            this.imgVersProfil.Location = new System.Drawing.Point(656, 7);
            this.imgVersProfil.Name = "imgVersProfil";
            this.imgVersProfil.Size = new System.Drawing.Size(50, 50);
            this.imgVersProfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgVersProfil.TabIndex = 3;
            this.imgVersProfil.TabStop = false;
            this.imgVersProfil.Click += new System.EventHandler(this.imgVersProfil_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GMAO_Presentation.Properties.Resources.logo_Systematic;
            this.pictureBox1.Location = new System.Drawing.Point(921, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(207, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Myanmar Text", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(96, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 43);
            this.label1.TabIndex = 1;
            this.label1.Text = "MAINTENANCE | GMAO";
            // 
            // btnSideBar
            // 
            this.btnSideBar.Image = global::GMAO_Presentation.Properties.Resources.Menu2;
            this.btnSideBar.Location = new System.Drawing.Point(9, 12);
            this.btnSideBar.Name = "btnSideBar";
            this.btnSideBar.Size = new System.Drawing.Size(50, 44);
            this.btnSideBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnSideBar.TabIndex = 0;
            this.btnSideBar.TabStop = false;
            this.btnSideBar.Click += new System.EventHandler(this.btnSideBar_Click_1);
            // 
            // sideBar
            // 
            this.sideBar.BackColor = System.Drawing.Color.DimGray;
            this.sideBar.Controls.Add(this.panel2);
            this.sideBar.Controls.Add(this.panel3);
            this.sideBar.Controls.Add(this.WO);
            this.sideBar.Controls.Add(this.menuContainer);
            this.sideBar.Controls.Add(this.panel11);
            this.sideBar.Controls.Add(this.panel13);
            this.sideBar.Controls.Add(this.panel12);
            this.sideBar.Controls.Add(this.panel9);
            this.sideBar.Controls.Add(this.panel10);
            this.sideBar.Controls.Add(this.panel14);
            this.sideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sideBar.Location = new System.Drawing.Point(0, 75);
            this.sideBar.Name = "sideBar";
            this.sideBar.Size = new System.Drawing.Size(215, 676);
            this.sideBar.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.btnEquipement);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(186, 53);
            this.panel2.TabIndex = 3;
            // 
            // btnEquipement
            // 
            this.btnEquipement.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnEquipement.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEquipement.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnEquipement.Image = global::GMAO_Presentation.Properties.Resources.Gears;
            this.btnEquipement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEquipement.Location = new System.Drawing.Point(3, -6);
            this.btnEquipement.Name = "btnEquipement";
            this.btnEquipement.Size = new System.Drawing.Size(190, 59);
            this.btnEquipement.TabIndex = 2;
            this.btnEquipement.Text = "       Equipement";
            this.btnEquipement.UseVisualStyleBackColor = false;
            this.btnEquipement.Click += new System.EventHandler(this.btnEquipement_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.btnIntervention);
            this.panel3.Location = new System.Drawing.Point(3, 62);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(186, 53);
            this.panel3.TabIndex = 4;
            // 
            // btnIntervention
            // 
            this.btnIntervention.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnIntervention.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIntervention.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnIntervention.Image = global::GMAO_Presentation.Properties.Resources.Order_Completed;
            this.btnIntervention.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIntervention.Location = new System.Drawing.Point(3, -6);
            this.btnIntervention.Name = "btnIntervention";
            this.btnIntervention.Size = new System.Drawing.Size(190, 59);
            this.btnIntervention.TabIndex = 2;
            this.btnIntervention.Text = "        Intervention";
            this.btnIntervention.UseVisualStyleBackColor = false;
            this.btnIntervention.Click += new System.EventHandler(this.btnIntervention_Click);
            // 
            // WO
            // 
            this.WO.BackColor = System.Drawing.Color.Transparent;
            this.WO.Controls.Add(this.panel8);
            this.WO.Controls.Add(this.panel15);
            this.WO.Controls.Add(this.panel16);
            this.WO.Location = new System.Drawing.Point(3, 121);
            this.WO.Name = "WO";
            this.WO.Size = new System.Drawing.Size(211, 58);
            this.WO.TabIndex = 7;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnWorders);
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(186, 53);
            this.panel8.TabIndex = 7;
            // 
            // btnWorders
            // 
            this.btnWorders.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnWorders.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWorders.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnWorders.Image = global::GMAO_Presentation.Properties.Resources.Business;
            this.btnWorders.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWorders.Location = new System.Drawing.Point(3, -5);
            this.btnWorders.Name = "btnWorders";
            this.btnWorders.Size = new System.Drawing.Size(190, 58);
            this.btnWorders.TabIndex = 2;
            this.btnWorders.Text = "       WorkOrders";
            this.btnWorders.UseVisualStyleBackColor = false;
            this.btnWorders.Click += new System.EventHandler(this.btnWorders_Click);
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.btnWOCO);
            this.panel15.Location = new System.Drawing.Point(20, 62);
            this.panel15.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(186, 53);
            this.panel15.TabIndex = 7;
            // 
            // btnWOCO
            // 
            this.btnWOCO.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnWOCO.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWOCO.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnWOCO.Image = global::GMAO_Presentation.Properties.Resources.Maintenance;
            this.btnWOCO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWOCO.Location = new System.Drawing.Point(3, -6);
            this.btnWOCO.Name = "btnWOCO";
            this.btnWOCO.Size = new System.Drawing.Size(190, 59);
            this.btnWOCO.TabIndex = 2;
            this.btnWOCO.Text = "      Corrective";
            this.btnWOCO.UseVisualStyleBackColor = false;
            this.btnWOCO.Click += new System.EventHandler(this.btnWOCO_Click_1);
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.btnWOI);
            this.panel16.Location = new System.Drawing.Point(20, 121);
            this.panel16.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(186, 53);
            this.panel16.TabIndex = 8;
            // 
            // btnWOI
            // 
            this.btnWOI.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnWOI.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWOI.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnWOI.Image = global::GMAO_Presentation.Properties.Resources.Maintenance;
            this.btnWOI.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWOI.Location = new System.Drawing.Point(3, -6);
            this.btnWOI.Name = "btnWOI";
            this.btnWOI.Size = new System.Drawing.Size(190, 59);
            this.btnWOI.TabIndex = 2;
            this.btnWOI.Text = "        Systématique";
            this.btnWOI.UseVisualStyleBackColor = false;
            this.btnWOI.Click += new System.EventHandler(this.btnWOI_Click_1);
            // 
            // menuContainer
            // 
            this.menuContainer.BackColor = System.Drawing.Color.Transparent;
            this.menuContainer.Controls.Add(this.panel5);
            this.menuContainer.Controls.Add(this.panel6);
            this.menuContainer.Controls.Add(this.panel7);
            this.menuContainer.Location = new System.Drawing.Point(3, 185);
            this.menuContainer.Name = "menuContainer";
            this.menuContainer.Size = new System.Drawing.Size(211, 58);
            this.menuContainer.TabIndex = 6;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnMaintenance);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(186, 53);
            this.panel5.TabIndex = 7;
            // 
            // btnMaintenance
            // 
            this.btnMaintenance.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnMaintenance.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaintenance.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnMaintenance.Image = global::GMAO_Presentation.Properties.Resources.Service;
            this.btnMaintenance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaintenance.Location = new System.Drawing.Point(3, -5);
            this.btnMaintenance.Name = "btnMaintenance";
            this.btnMaintenance.Size = new System.Drawing.Size(190, 58);
            this.btnMaintenance.TabIndex = 2;
            this.btnMaintenance.Text = "       Maintenance";
            this.btnMaintenance.UseVisualStyleBackColor = false;
            this.btnMaintenance.Click += new System.EventHandler(this.btnMaintenance_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnGstMaintenanceCo);
            this.panel6.Location = new System.Drawing.Point(20, 62);
            this.panel6.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(186, 53);
            this.panel6.TabIndex = 7;
            // 
            // btnGstMaintenanceCo
            // 
            this.btnGstMaintenanceCo.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnGstMaintenanceCo.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGstMaintenanceCo.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnGstMaintenanceCo.Image = global::GMAO_Presentation.Properties.Resources.Maintenance;
            this.btnGstMaintenanceCo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGstMaintenanceCo.Location = new System.Drawing.Point(3, -6);
            this.btnGstMaintenanceCo.Name = "btnGstMaintenanceCo";
            this.btnGstMaintenanceCo.Size = new System.Drawing.Size(190, 59);
            this.btnGstMaintenanceCo.TabIndex = 2;
            this.btnGstMaintenanceCo.Text = "      Corrective";
            this.btnGstMaintenanceCo.UseVisualStyleBackColor = false;
            this.btnGstMaintenanceCo.Click += new System.EventHandler(this.btnGstMaintenanceCo_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnGstMaintenancePlanifie);
            this.panel7.Location = new System.Drawing.Point(20, 121);
            this.panel7.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(186, 53);
            this.panel7.TabIndex = 8;
            // 
            // btnGstMaintenancePlanifie
            // 
            this.btnGstMaintenancePlanifie.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnGstMaintenancePlanifie.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGstMaintenancePlanifie.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnGstMaintenancePlanifie.Image = global::GMAO_Presentation.Properties.Resources.Request_Service;
            this.btnGstMaintenancePlanifie.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGstMaintenancePlanifie.Location = new System.Drawing.Point(3, -6);
            this.btnGstMaintenancePlanifie.Name = "btnGstMaintenancePlanifie";
            this.btnGstMaintenancePlanifie.Size = new System.Drawing.Size(190, 59);
            this.btnGstMaintenancePlanifie.TabIndex = 2;
            this.btnGstMaintenancePlanifie.Text = "        Systématique";
            this.btnGstMaintenancePlanifie.UseVisualStyleBackColor = false;
            this.btnGstMaintenancePlanifie.Click += new System.EventHandler(this.btnGstMaintenancePlanifie_Click);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.Controls.Add(this.btnAlertes);
            this.panel11.Location = new System.Drawing.Point(3, 249);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(186, 53);
            this.panel11.TabIndex = 8;
            // 
            // btnAlertes
            // 
            this.btnAlertes.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnAlertes.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlertes.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnAlertes.Image = global::GMAO_Presentation.Properties.Resources.Brake_Warning;
            this.btnAlertes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlertes.Location = new System.Drawing.Point(3, -5);
            this.btnAlertes.Name = "btnAlertes";
            this.btnAlertes.Size = new System.Drawing.Size(190, 58);
            this.btnAlertes.TabIndex = 2;
            this.btnAlertes.Text = "Alertes";
            this.btnAlertes.UseVisualStyleBackColor = false;
            this.btnAlertes.Click += new System.EventHandler(this.btnAlertes_Click);
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.Transparent;
            this.panel13.Controls.Add(this.btnGstStock);
            this.panel13.Location = new System.Drawing.Point(3, 308);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(186, 53);
            this.panel13.TabIndex = 8;
            // 
            // btnGstStock
            // 
            this.btnGstStock.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnGstStock.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGstStock.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnGstStock.Image = global::GMAO_Presentation.Properties.Resources.Service;
            this.btnGstStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGstStock.Location = new System.Drawing.Point(3, -5);
            this.btnGstStock.Name = "btnGstStock";
            this.btnGstStock.Size = new System.Drawing.Size(190, 58);
            this.btnGstStock.TabIndex = 2;
            this.btnGstStock.Text = "Stock";
            this.btnGstStock.UseVisualStyleBackColor = false;
            this.btnGstStock.Click += new System.EventHandler(this.btnGstStock_Click);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.Controls.Add(this.btnGstEquipes);
            this.panel12.Location = new System.Drawing.Point(3, 367);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(186, 53);
            this.panel12.TabIndex = 9;
            // 
            // btnGstEquipes
            // 
            this.btnGstEquipes.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnGstEquipes.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGstEquipes.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnGstEquipes.Image = global::GMAO_Presentation.Properties.Resources.People;
            this.btnGstEquipes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGstEquipes.Location = new System.Drawing.Point(3, -5);
            this.btnGstEquipes.Name = "btnGstEquipes";
            this.btnGstEquipes.Size = new System.Drawing.Size(190, 58);
            this.btnGstEquipes.TabIndex = 2;
            this.btnGstEquipes.Text = "   Equipes";
            this.btnGstEquipes.UseVisualStyleBackColor = false;
            this.btnGstEquipes.Click += new System.EventHandler(this.btnGstEquipes_Click);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.Controls.Add(this.btnSuiviCB);
            this.panel9.Location = new System.Drawing.Point(3, 426);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(186, 53);
            this.panel9.TabIndex = 6;
            // 
            // btnSuiviCB
            // 
            this.btnSuiviCB.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnSuiviCB.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuiviCB.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnSuiviCB.Image = global::GMAO_Presentation.Properties.Resources.Profitability;
            this.btnSuiviCB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSuiviCB.Location = new System.Drawing.Point(3, -5);
            this.btnSuiviCB.Name = "btnSuiviCB";
            this.btnSuiviCB.Size = new System.Drawing.Size(190, 58);
            this.btnSuiviCB.TabIndex = 2;
            this.btnSuiviCB.Text = "      Suivi coûts";
            this.btnSuiviCB.UseVisualStyleBackColor = false;
            this.btnSuiviCB.Click += new System.EventHandler(this.btnSuiviCB_Click);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.Controls.Add(this.button8);
            this.panel10.Location = new System.Drawing.Point(3, 485);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(186, 53);
            this.panel10.TabIndex = 7;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.button8.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button8.Image = global::GMAO_Presentation.Properties.Resources.Control_Panel;
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(3, -5);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(190, 58);
            this.button8.TabIndex = 2;
            this.button8.Text = "    Reporting";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.Transparent;
            this.panel14.Controls.Add(this.btnConfig);
            this.panel14.Location = new System.Drawing.Point(3, 544);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(186, 53);
            this.panel14.TabIndex = 8;
            // 
            // btnConfig
            // 
            this.btnConfig.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnConfig.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfig.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnConfig.Image = global::GMAO_Presentation.Properties.Resources.Settings2;
            this.btnConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfig.Location = new System.Drawing.Point(3, -5);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(190, 58);
            this.btnConfig.TabIndex = 2;
            this.btnConfig.Text = "       Configration";
            this.btnConfig.UseVisualStyleBackColor = false;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.AutoScroll = true;
            this.panelContainer.BackColor = System.Drawing.Color.LightGray;
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(215, 75);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1069, 676);
            this.panelContainer.TabIndex = 4;
            // 
            // menuTransition
            // 
            this.menuTransition.Interval = 10;
            this.menuTransition.Tick += new System.EventHandler(this.menuTransition_Tick_1);
            // 
            // sideTransition
            // 
            this.sideTransition.Interval = 10;
            this.sideTransition.Tick += new System.EventHandler(this.sideTransition_Tick_1);
            // 
            // woTransition
            // 
            this.woTransition.Interval = 10;
            this.woTransition.Tick += new System.EventHandler(this.woTransition_Tick_1);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1284, 751);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.sideBar);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVersProfil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSideBar)).EndInit();
            this.sideBar.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.WO.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.menuContainer.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox imgExit;
        private System.Windows.Forms.PictureBox imgVersProfil;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnSideBar;
        private System.Windows.Forms.FlowLayoutPanel sideBar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnEquipement;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnIntervention;
        private System.Windows.Forms.FlowLayoutPanel WO;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnWorders;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Button btnWOCO;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button btnWOI;
        private System.Windows.Forms.FlowLayoutPanel menuContainer;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnMaintenance;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnGstMaintenanceCo;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnGstMaintenancePlanifie;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnAlertes;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Button btnGstStock;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Button btnGstEquipes;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnSuiviCB;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Timer menuTransition;
        private System.Windows.Forms.Timer sideTransition;
        private System.Windows.Forms.Timer woTransition;
    }
}