
namespace GMAO_Presentation.Views
{
    partial class GererUserAccForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnModPWD = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRecherche = new System.Windows.Forms.TextBox();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imgActualiser = new System.Windows.Forms.PictureBox();
            this.imgREchercher = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgActualiser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgREchercher)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnModPWD);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 119);
            this.panel1.TabIndex = 14;
            // 
            // btnModPWD
            // 
            this.btnModPWD.BackColor = System.Drawing.Color.OliveDrab;
            this.btnModPWD.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnModPWD.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModPWD.Image = global::GMAO_Presentation.Properties.Resources.Refresh2;
            this.btnModPWD.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnModPWD.Location = new System.Drawing.Point(910, 26);
            this.btnModPWD.Name = "btnModPWD";
            this.btnModPWD.Size = new System.Drawing.Size(127, 76);
            this.btnModPWD.TabIndex = 8;
            this.btnModPWD.Text = "PwdUpdate";
            this.btnModPWD.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnModPWD.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Mistral", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(623, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = ">>>>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Script MT Bold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(151, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(395, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gestion des Utilisateurs";
            // 
            // txtRecherche
            // 
            this.txtRecherche.Location = new System.Drawing.Point(595, 243);
            this.txtRecherche.Name = "txtRecherche";
            this.txtRecherche.Size = new System.Drawing.Size(284, 22);
            this.txtRecherche.TabIndex = 18;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(146, 311);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.RowTemplate.Height = 24;
            this.dgvUsers.Size = new System.Drawing.Size(776, 255);
            this.dgvUsers.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GMAO_Presentation.Properties.Resources.Management;
            this.pictureBox1.Location = new System.Drawing.Point(11, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 63);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // imgActualiser
            // 
            this.imgActualiser.Image = global::GMAO_Presentation.Properties.Resources.Refresh;
            this.imgActualiser.Location = new System.Drawing.Point(97, 311);
            this.imgActualiser.Name = "imgActualiser";
            this.imgActualiser.Size = new System.Drawing.Size(34, 35);
            this.imgActualiser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgActualiser.TabIndex = 16;
            this.imgActualiser.TabStop = false;
            // 
            // imgREchercher
            // 
            this.imgREchercher.Image = global::GMAO_Presentation.Properties.Resources.Search;
            this.imgREchercher.Location = new System.Drawing.Point(885, 243);
            this.imgREchercher.Name = "imgREchercher";
            this.imgREchercher.Size = new System.Drawing.Size(37, 34);
            this.imgREchercher.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgREchercher.TabIndex = 17;
            this.imgREchercher.TabStop = false;
            // 
            // GererUserAccForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1051, 629);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imgActualiser);
            this.Controls.Add(this.txtRecherche);
            this.Controls.Add(this.imgREchercher);
            this.Controls.Add(this.dgvUsers);
            this.Name = "GererUserAccForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgActualiser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgREchercher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnModPWD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox imgActualiser;
        private System.Windows.Forms.TextBox txtRecherche;
        private System.Windows.Forms.PictureBox imgREchercher;
        private System.Windows.Forms.DataGridView dgvUsers;
    }
}