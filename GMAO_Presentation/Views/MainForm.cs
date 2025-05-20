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
    public partial class MainForm : Form
    {
        private Form activeForm;

        //id de user passe en parametres
        private int id;

        public MainForm()
        {
            InitializeComponent();
            //this.id = id;
        }

        

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
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

        private void btnEquipement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.GestionEquipementAccueil(), sender);
        }

        private void imgVersProfil_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.GestionProfilForm(UserContext.IdUser), sender);
        }

        private void btnGstEquipes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.GestionDesEquipes(), sender);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.ConfigurationForm(), sender);
        }

        private void btnGstStock_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.GstPieceAcceuil(), sender);
        }

        private void btnGstMaintenanceCo_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.MaintenanceCoFormAccueil(UserContext.IdUser), sender);
        }

        private void btnGstMaintenancePlanifie_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.MaintenancePlanAccueilForm(UserContext.IdUser), sender);
        }

        private void btnIntervention_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.InterventionsAccueilForm(), sender);
        }

        private void btnAlertes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.AlertesForm(), sender);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                var planningService = new PlanningService();

                // Nettoyage des anciennes alertes
                planningService.NettoyerAnciennesAlertes();

                // Planification des interventions et génération d’alertes pour le responsable courant
                planningService.PlanifierPourResponsable(UserContext.IdUser);

                //stock
                planningService.VerifierStockEtGenererAlertes();

                //WOCO
                planningService.VerifierMaintenancesCorrectivesSansWO(id);

                // Récupération des alertes récentes
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
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la planification des interventions :\n" + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void MainForm_Shown(object sender, EventArgs e)
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

        private void btnSuiviCB_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.SuiviBudgetCoutForm(), sender);
        }

        //gestion du deroulement du menu
        bool expand = false;
        private void menuTransition_Tick_1(object sender, EventArgs e)
        {
            if (expand == false)
            {
                menuContainer.Height += 15;
                if (menuContainer.Height >= 176)
                {
                    menuTransition.Stop();
                    expand = true;
                }
            }
            else
            {
                menuContainer.Height -= 15;
                if (menuContainer.Height <= 58)
                {
                    menuTransition.Stop();
                    expand = false;
                }
            }
        }

        //gestion du 2e menu
        bool expand2 = false;
        private void woTransition_Tick_1(object sender, EventArgs e)
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
        private void sideTransition_Tick_1(object sender, EventArgs e)
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

        private void btnSideBar_Click_1(object sender, EventArgs e)
        {
            sideTransition.Start();
        }

        private void btnWOCO_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Views.WorkOrderCoAccueilForm(), sender);
        }

        private void btnWOI_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Views.WOIAccueilForm(), sender);
        }

        private void btnWorders_Click(object sender, EventArgs e)
        {
            woTransition.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Views.DashBoardForm(), sender);
        }
    }
}
