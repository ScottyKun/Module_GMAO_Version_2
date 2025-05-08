
namespace GMAO_Presentation.Views
{
    partial class ConfigurationForm
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
            this.btnGstBudget = new System.Windows.Forms.Button();
            this.btnGstStock = new System.Windows.Forms.Button();
            this.btnGstCategorie = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGstBudget
            // 
            this.btnGstBudget.BackColor = System.Drawing.Color.OliveDrab;
            this.btnGstBudget.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGstBudget.Location = new System.Drawing.Point(262, 366);
            this.btnGstBudget.Name = "btnGstBudget";
            this.btnGstBudget.Size = new System.Drawing.Size(633, 60);
            this.btnGstBudget.TabIndex = 15;
            this.btnGstBudget.Text = "Gérer budget";
            this.btnGstBudget.UseVisualStyleBackColor = false;
            // 
            // btnGstStock
            // 
            this.btnGstStock.BackColor = System.Drawing.Color.OliveDrab;
            this.btnGstStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGstStock.Location = new System.Drawing.Point(262, 261);
            this.btnGstStock.Name = "btnGstStock";
            this.btnGstStock.Size = new System.Drawing.Size(633, 60);
            this.btnGstStock.TabIndex = 14;
            this.btnGstStock.Text = "Gérer stock";
            this.btnGstStock.UseVisualStyleBackColor = false;
            // 
            // btnGstCategorie
            // 
            this.btnGstCategorie.BackColor = System.Drawing.Color.OliveDrab;
            this.btnGstCategorie.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGstCategorie.Location = new System.Drawing.Point(262, 159);
            this.btnGstCategorie.Name = "btnGstCategorie";
            this.btnGstCategorie.Size = new System.Drawing.Size(633, 60);
            this.btnGstCategorie.TabIndex = 13;
            this.btnGstCategorie.Text = "Gérer catégorie";
            this.btnGstCategorie.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GMAO_Presentation.Properties.Resources.Settings2;
            this.pictureBox1.Location = new System.Drawing.Point(159, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Script MT Bold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(255, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 39);
            this.label4.TabIndex = 11;
            this.label4.Text = "Vos configurations";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(155, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 24);
            this.label3.TabIndex = 10;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1051, 450);
            this.Controls.Add(this.btnGstBudget);
            this.Controls.Add(this.btnGstStock);
            this.Controls.Add(this.btnGstCategorie);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "ConfigurationForm";
            this.Text = "ConfigurationForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGstBudget;
        private System.Windows.Forms.Button btnGstStock;
        private System.Windows.Forms.Button btnGstCategorie;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}