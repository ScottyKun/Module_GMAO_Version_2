using GMAO_Business.DTOs;
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
    public partial class GererUserUpForm : Form
    {
        private readonly GstUserUpVM viewModel;
        public UserDTO3 UtilisateurModifie { get; private set; }

        public GererUserUpForm(UserDTO3 user)
        {
            InitializeComponent();
          
            viewModel = new GstUserUpVM(user);

            txtNom.DataBindings.Add("Text", viewModel, "Nom");
            txtPrenom.DataBindings.Add("Text", viewModel, "Prenom");
            txtEmail.DataBindings.Add("Text", viewModel, "Email");
            txtTel.DataBindings.Add("Text", viewModel, "Tel");
            txtFonction.DataBindings.Add("Text", viewModel, "Fonction");
            txtUsername.DataBindings.Add("Text", viewModel, "Username");
            chkStatut.DataBindings.Add("Checked", viewModel, "Statut");

            //chkStatut.Enabled = false;

            btnModifier.Click += (s, e) =>
            {
                if (viewModel.ModifierCommand.CanExecute(null))
                    viewModel.ModifierCommand.Execute(null);
            };

            btnSupprimer.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer la suppression de cet utilisateur ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    viewModel.SupprimerCommand.Execute(null);
                    UtilisateurModifie = null;
                }
            };

            viewModel.OnClose += () =>
            {
                UtilisateurModifie = new UserDTO3 { 
                    IdUser=user.IdUser,
                    Nom=txtNom.Text,
                    Prenom=txtPrenom.Text,
                    Tel=txtTel.Text,
                    Email=txtEmail.Text,
                    Username=txtUsername.Text,
                    Fonction=txtFonction.Text,
                    Statut=chkStatut.Checked
                };
                MessageBox.Show("Opération effectuée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            viewModel.OnError += msg =>
            {
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }
     

    }
}
