using GMAO_Business.Services;
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
    public partial class InscriptionForm : Form
    {
        private readonly InscriptionViewModel viewModel;

        public InscriptionForm()
        {
            InitializeComponent();
            var insService = new InscriptionService();
            viewModel = new InscriptionViewModel(insService);

            txtUser.DataBindings.Add("Text", viewModel, "Username", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPassword.DataBindings.Add("Text", viewModel, "Password", false, DataSourceUpdateMode.OnPropertyChanged);
            txtEmail.DataBindings.Add("Text", viewModel, "Email", false, DataSourceUpdateMode.OnPropertyChanged);

            cbRole.DataSource = viewModel.Roles;
            cbRole.DataBindings.Add("SelectedItem", viewModel, "Role", false, DataSourceUpdateMode.OnPropertyChanged);


            btnInscription.DataBindings.Add("Enabled", viewModel, "CanRegister", false, DataSourceUpdateMode.OnPropertyChanged);
            btnInscription.Click += (s, e) => viewModel.Inscomand.Execute(null);

            viewModel.OnNavigate += OnInscriptionSuccess;
            viewModel.OnShowNotification += ShowNotification;



        }

        private void OnInscriptionSuccess()
        {
            this.Invoke((MethodInvoker)(() =>
            {
                this.Hide();
                new Views.AuthentificationForm().Show();
            }));
        }

        public void ShowNotification(string title, string message)
        {
            MessageBox.Show($"{title}\n{message}", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void lbClick_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new Views.AuthentificationForm().Show();

        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;

        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;

        }
    }
}
