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
    public partial class GestionProfilForm : Form
    {
        private readonly GestionProfilViewModel gpViewModel;
        private bool _isClosing = false;
        public GestionProfilForm(int id)
        {
            InitializeComponent();
            gpViewModel = new GestionProfilViewModel(id);

            //methode pour lier les elts au viewModel #nouvelle approche
            BindData();

            this.FormClosing += AlertMessage;

            gpViewModel.OnShowNotification += ShowNotification;

        }

        public void ShowNotification(string title, string message)
        {
            MessageBox.Show($"{title}\n{message}", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //methode qui alerte user quand les changements ne sont pas enregistres et il veut fermer la fenetre
        private void AlertMessage(object sender, FormClosingEventArgs e)
        {
            if (_isClosing) return;

            var result = MessageBox.Show("Vous avez des modifications non enregistrées. Voulez-vous enregistrer avant de quitter ?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                gpViewModel.UpdateCommand.Execute(null);
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void BindData()
        {
            txtNom.DataBindings.Add("Text", gpViewModel, "Nom", false, DataSourceUpdateMode.OnPropertyChanged);
            txtPrenom.DataBindings.Add("Text", gpViewModel, "Prenom", false, DataSourceUpdateMode.OnPropertyChanged);
            txtTelephone.DataBindings.Add("Text", gpViewModel, "Telephone", false, DataSourceUpdateMode.OnPropertyChanged);
            txtEmail.DataBindings.Add("Text", gpViewModel, "Email", false, DataSourceUpdateMode.OnPropertyChanged);
            txtUsername.DataBindings.Add("Text", gpViewModel, "Username", false, DataSourceUpdateMode.OnPropertyChanged);
            txtFonction.DataBindings.Add("Text", gpViewModel, "Fonction", false, DataSourceUpdateMode.OnPropertyChanged);
            txtFonction.Enabled = false;

            btnModifier.DataBindings.Add("Enabled", gpViewModel, "CanRegister", false, DataSourceUpdateMode.OnPropertyChanged);
            btnModifier.Click += (s, e) => {
                gpViewModel.UpdateCommand.Execute(null);
                _isClosing = true;
            };
        }
    }
}
