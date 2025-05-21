using GMAO_Presentation.ViewModel;
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
    public partial class AuthentificationForm : Form
    {
        private readonly AuthentificationVM viewModel;
        public AuthentificationForm()
        {
            InitializeComponent();
            viewModel = new AuthentificationVM();



            txtUser.DataBindings.Add("Text", viewModel, "UserName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPassword.DataBindings.Add("Text", viewModel, "Password", false, DataSourceUpdateMode.OnPropertyChanged);

            btnConnexion.DataBindings.Add("Enabled", viewModel, "CanRegister", false, DataSourceUpdateMode.OnPropertyChanged);
            btnConnexion.Click += (s, e) => viewModel.AuthCommand.Execute(null);

            //gestion de nos evenements
            viewModel.OnShowNotification += ShowNotification;
            viewModel.OnNavigateToDashboard += OpenDashboard;
        }

        //redirection
        private void OpenDashboard(int id, string fonction)
        {
            this.Hide();

            if (fonction == "Technicien")
            {
                new Views.MainForm2().Show();
            }
            else if (fonction == "Responsable")
            {
                new Views.MainForm().Show();
            }
            else
            {
                new Views.MainFormAdmin().Show();
            }
        }

        //Appel et affichage de la notification
        public void ShowNotification(string title, string message)
        {
            MessageBox.Show($"{title}\n{message}", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;

        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;

        }

        private void lbClick_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Views.InscriptionForm().Show();
            this.Hide();
        }
    }
}

