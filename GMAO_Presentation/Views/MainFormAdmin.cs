using GMAO_Business.Entities;
using GMAO_Business.Services;
using GMAO_Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMAO_Presentation.Views
{
    public partial class MainFormAdmin : Form
    {
        private Form activeForm;
        public MainFormAdmin()
        {
            InitializeComponent();
        }

        private void imgExit_Click(object sender, EventArgs e)
        {

            var confirm = MessageBox.Show(
                                 $"Souhaitez vous déconnectez?",
                                    "Déconnexion",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning
                     );

            if (confirm == DialogResult.Yes)
            {
                this.Hide(); // cache la fenêtre actuelle
                var loginForm = new Views.AuthentificationForm();
                loginForm.Show();
                UserContext.IdUser = 0;
                UserContext.Role = null;
                this.Close(); // ferme la fenêtre actuelle après retour


            }
        }

        private void OpenChildForm(Form child, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = child;
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            this.panelContainer.Controls.Add(child);
            this.panelContainer.Tag = child;
            child.BringToFront();
            child.Show();
        }

        private void btnGstUsers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.GererUserAccForm(), sender);
        }

        private void btnSuiviCout_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.SuiviBudgetCoutForm(), sender);
        }

        private void imgVersProfil_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.GestionProfilForm(UserContext.IdUser), sender);
        }


        private void MainFormAdmin_Shown(object sender, EventArgs e)
        {
            if (new UserService().ProfilIncomplet(UserContext.IdUser))
            {
                var result = MessageBox.Show("Votre profil est incomplet. Voulez-vous le compléter maintenant ?",
                                             "Profil Incomplet", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    OpenChildForm(new Views.GestionProfilForm(UserContext.IdUser), sender);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.DashBoardForm(), sender);
        }
    }
}
