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
    public partial class GestionEquipementAjout : Form
    {
        private readonly EquipementAjoutViewModel viewModel;
        public GestionEquipementAjout()
        {
            InitializeComponent();
            viewModel = new EquipementAjoutViewModel();

            comboCategorie.DataSource = viewModel.Categories;
            comboCategorie.DisplayMember = "nom";
            comboCategorie.ValueMember = "id";
            comboCategorie.DataBindings.Add("SelectedItem", viewModel, "SelectedCategorie", false, DataSourceUpdateMode.OnPropertyChanged);

            comboResponsable.DataSource = viewModel.Responsables;
            comboResponsable.DisplayMember = "nom";
            comboResponsable.ValueMember = "idUser";
            comboResponsable.DataBindings.Add("SelectedItem", viewModel, "SelectedResponsable", false, DataSourceUpdateMode.OnPropertyChanged);

            //comboResponsable.Enabled = false;

            comboEquipe.DataSource = viewModel.Equipes;
            comboEquipe.DisplayMember = "Nom";
            comboEquipe.ValueMember = "Id";
            comboEquipe.DataBindings.Add("SelectedItem", viewModel, "SelectedEquipe", false, DataSourceUpdateMode.OnPropertyChanged);

            // Champs texte et dates
            txtNom.DataBindings.Add("Text", viewModel, "Nom", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCommentaires.DataBindings.Add("Text", viewModel, "Commentaires", false, DataSourceUpdateMode.OnPropertyChanged);
            dtAchat.DataBindings.Add("Value", viewModel, "DateAchat", false, DataSourceUpdateMode.OnPropertyChanged);
            dtGarantie.DataBindings.Add("Value", viewModel, "DateFinGarantie", false, DataSourceUpdateMode.OnPropertyChanged);
            chkStatut.DataBindings.Add("Checked", viewModel, "Statut", false, DataSourceUpdateMode.OnPropertyChanged);

            //btnAjouter.DataBindings.Add("Enabled", viewModel, "PeutAjouter", false, DataSourceUpdateMode.OnPropertyChanged);
            btnAjouter.Click += (s, e) =>
            {
                HighlightEmptyFields();
                if (!viewModel.EnregistrerCommand.CanExecute(null))
                {
                    MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                viewModel.EnregistrerCommand.Execute(null);
                MessageBox.Show("Équipement ajouté avec succès !", "Ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            };

        }
        private void HighlightEmptyFields()
        {
            comboCategorie.BackColor = viewModel.CategorieId.HasValue ? Color.White : Color.MistyRose;
            comboResponsable.BackColor = viewModel.ResponsableId.HasValue ? Color.White : Color.MistyRose;
            comboEquipe.BackColor = viewModel.MaintenanceTeamId.HasValue ? Color.White : Color.MistyRose;
            txtNom.BackColor = !string.IsNullOrWhiteSpace(txtNom.Text) ? Color.White : Color.MistyRose;
            txtCommentaires.BackColor = !string.IsNullOrWhiteSpace(txtCommentaires.Text) ? Color.White : Color.MistyRose;
        }



    }
}

