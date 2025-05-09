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
    public partial class GestionEquipementUpdate : Form
    {
        private readonly EquipementModifViewModel viewModel;
        public GestionEquipementUpdate(int id)
        {
            InitializeComponent();
            viewModel = new EquipementModifViewModel(id);

            comboCategorie.DataSource = viewModel.Categories;
            comboCategorie.DisplayMember = "nom";
            comboCategorie.ValueMember = "id";
            comboCategorie.DataBindings.Add("SelectedValue", viewModel.Equipement, "CategorieId", false, DataSourceUpdateMode.OnPropertyChanged);

            comboResponsable.DataSource = viewModel.Responsables;
            comboResponsable.DisplayMember = "nom";
            comboResponsable.ValueMember = "idUser";
            comboResponsable.DataBindings.Add("SelectedValue", viewModel.Equipement, "ResponsableId", false, DataSourceUpdateMode.OnPropertyChanged);

            comboEquipe.DataSource = viewModel.Equipes;
            comboEquipe.DisplayMember = "Nom";
            comboEquipe.ValueMember = "Id";
            comboEquipe.DataBindings.Add("SelectedValue", viewModel.Equipement, "MaintenanceTeamId", false, DataSourceUpdateMode.OnPropertyChanged);

            txtNom.DataBindings.Add("Text", viewModel.Equipement, "Nom", false, DataSourceUpdateMode.OnPropertyChanged);
            txtCommentaires.DataBindings.Add("Text", viewModel.Equipement, "Commentaires", false, DataSourceUpdateMode.OnPropertyChanged);
            dtAchat.DataBindings.Add("Value", viewModel.Equipement, "DateAchat", false, DataSourceUpdateMode.OnPropertyChanged);
            dtGarantie.DataBindings.Add("Value", viewModel.Equipement, "DateFinGarantie", false, DataSourceUpdateMode.OnPropertyChanged);
            chkStatut.DataBindings.Add("Checked", viewModel.Equipement, "Statut", false, DataSourceUpdateMode.OnPropertyChanged);

            btnModifier.Click += (s, e) =>
            {
                viewModel.ModifierCommand.Execute(null);
                MessageBox.Show("Équipement modifié avec succès !", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            };

            btnSupprimer.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Voulez-vous vraiment supprimer cet équipement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    viewModel.SupprimerCommand.Execute(null);
                    MessageBox.Show("Équipement supprimé !", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
        }

    }
}
