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
    public partial class MainForm2 : Form
    {
        private Form activeForm;
        // private int id;
        public MainForm2()
        {
            InitializeComponent();
            //this.id = id;
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

        private void imgVersProfil_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.GestionProfilForm(UserContext.IdUser), sender);
        }

        private void btnWorders_Click(object sender, EventArgs e)
        {
            woTransition.Start();
        }

        private void btnIntervention_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.InterventionsAccueilForm(), sender);
        }

        private void btnWOCO_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.WorkOrderCoAccueilForm(), sender);
        }

        private void btnWOI_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.WOIAccueilForm(), sender);
        }

        private void btnAlertes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.AlertesForm(), sender);
        }

        private void btnSideBar_Click(object sender, EventArgs e)
        {
            sideTransition.Start();
        }

        //gestion du deroulement du menu
        bool expand2 = false;

        private void woTransition_Tick(object sender, EventArgs e)
        {

            if (expand2 == false)
            {
                WO.Height += 15;
                if (WO.Height >= 177)
                {
                    woTransition.Stop();
                    expand2 = true;
                }
            }
            else
            {
                WO.Height -= 15;
                if (WO.Height <= 58)
                {
                    woTransition.Stop();
                    expand2 = false;
                }
            }
        }
        //gestion de la barre de navigation
        bool expandSide = true;

        private void sideTransition_Tick(object sender, EventArgs e)
        {

            if (expandSide)
            {
                sideBar.Width -= 15;
                if (sideBar.Width <= 70)
                {
                    expandSide = false;
                    sideTransition.Stop();
                }
            }
            else
            {
                sideBar.Width += 15;
                if (sideBar.Width >= 215)
                {
                    expandSide = true;
                    sideTransition.Stop();
                }
            }
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

        private void MainForm2_Load(object sender, EventArgs e)
        {
            var alertes = new AlerteService().GetRecentesEtNonTraitees();

            if (alertes.Any())
            {
                var confirm = MessageBox.Show(
                            $"Vous avez {alertes.Count} alerte(s) récente(s) ou importantes. Souhaitez-vous les consulter ?",
                               "Alertes de maintenance",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning
                );

                if (confirm == DialogResult.Yes)
                {
                    OpenChildForm(new Views.AlertesForm(), sender);
                }
            }

        }

        private void MainForm2_Shown(object sender, EventArgs e)
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
    }
}
