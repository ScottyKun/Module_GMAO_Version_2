
namespace GMAO_Presentation.Views
{
    partial class MainForm2
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnIntervention = new System.Windows.Forms.Button();
            this.WO = new System.Windows.Forms.FlowLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnWorders = new System.Windows.Forms.Button();
            this.panel15 = new System.Windows.Forms.Panel();
            this.btnWOCO = new System.Windows.Forms.Button();
            this.panel16 = new System.Windows.Forms.Panel();
            this.btnWOI = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.btnAlertes = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.sideTransition = new System.Windows.Forms.Timer(this.components);
            this.woTransition = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVersProfil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSideBar)).BeginInit();
            this.sideBar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.WO.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel11.SuspendLayout();
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
            this.panel1.TabIndex = 2;
            // 
            // imgExit
            // 
            this.imgExit.Image = global::GMAO_Presentation.Properties.Resources.Emergency_Exit;
            this.imgExit.Location = new System.Drawing.Point(1222, 7);
            this.imgExit.Name = "imgExit";
            this.imgExit.Size = new System.Drawing.Size(50, 50);
            this.imgExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgExit.TabIndex = 5;
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
            this.btnSideBar.Click += new System.EventHandler(this.btnSideBar_Click);
            // 
            // sideBar
            // 
            this.sideBar.BackColor = System.Drawing.Color.DimGray;
            this.sideBar.Controls.Add(this.panel3);
            this.sideBar.Controls.Add(this.WO);
            this.sideBar.Controls.Add(this.panel11);
            this.sideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sideBar.Location = new System.Drawing.Point(0, 75);
            this.sideBar.Name = "sideBar";
            this.sideBar.Size = new System.Drawing.Size(215, 676);
            this.sideBar.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.btnIntervention);
            this.panel3.Location = new System.Drawing.Point(3, 3);
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
            this.WO.Location = new System.Drawing.Point(3, 62);
            this.WO.Name = "WO";
            this.WO.Size = new System.Drawing.Size(211, 58);
            this.WO.TabIndex = 8;
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
            this.btnWOCO.Click += new System.EventHandler(this.btnWOCO_Click);
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
            this.btnWOI.Click += new System.EventHandler(this.btnWOI_Click);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.Controls.Add(this.btnAlertes);
            this.panel11.Location = new System.Drawing.Point(3, 126);
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
            // panelContainer
            // 
            this.panelContainer.AutoScroll = true;
            this.panelContainer.BackColor = System.Drawing.Color.LightGray;
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(215, 75);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1069, 676);
            this.panelContainer.TabIndex = 5;
            // 
            // sideTransition
            // 
            this.sideTransition.Interval = 10;
            this.sideTransition.Tick += new System.EventHandler(this.sideTransition_Tick);
            // 
            // woTransition
            // 
            this.woTransition.Interval = 10;
            this.woTransition.Tick += new System.EventHandler(this.woTransition_Tick);
            // 
            // MainForm2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1284, 751);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.sideBar);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm2";
            this.Load += new System.EventHandler(this.MainForm2_Load);
            this.Shown += new System.EventHandler(this.MainForm2_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVersProfil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSideBar)).EndInit();
            this.sideBar.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.WO.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnIntervention;
        private System.Windows.Forms.FlowLayoutPanel WO;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnWorders;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Button btnWOCO;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button btnWOI;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnAlertes;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Timer sideTransition;
        private System.Windows.Forms.Timer woTransition;
    }
}