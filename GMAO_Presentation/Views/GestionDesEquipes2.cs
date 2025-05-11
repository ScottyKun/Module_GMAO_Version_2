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
    public partial class GestionDesEquipes2 : Form
    {
        private readonly GestionDesEquipes2ViewModel _viewModel;
        public GestionDesEquipes2(int id)
        {
            InitializeComponent();
            _viewModel = new GestionDesEquipes2ViewModel(id);
            _viewModel.OnEquipeModifiee += OnEquipeModifiee;

            txtNomEquipe.DataBindings.Add("Text", _viewModel, "NomEquipe", false, DataSourceUpdateMode.OnPropertyChanged);
            chkStatut.DataBindings.Add("Checked", _viewModel, "Statut", false, DataSourceUpdateMode.OnPropertyChanged);

            clbMembres.DataSource = _viewModel.MembresDisponibles;
            clbMembres.DisplayMember = "nom";
            clbMembres.ValueMember = "idUser";

            cmbChefEquipe.DataSource = _viewModel.ChefsDisponibles;
            cmbChefEquipe.DisplayMember = "nom";
            cmbChefEquipe.ValueMember = "idUser";


            cmbChefEquipe.SelectedIndexChanged += (s, e) =>
            {
                if (cmbChefEquipe.SelectedItem is UserDTO2 selectedChef)
                {
                    _viewModel.ChefSelectionne = selectedChef;
                }
            };


            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = _viewModel.MembresEquipe;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            /* foreach (var membre in _viewModel.MembresEquipe)
             {
                 Debug.WriteLine($"ID: {membre.idUser}, DateAjout: {membre.dateAjout}");
             }*/



            btnModifier.Click += (s, e) => _viewModel.ModifierEquipeCommand.Execute(null);

            btnDeleteItem.Click += (s, e) => {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int idMembre = (int)dataGridView1.SelectedRows[0].Cells["idUser"].Value;
                    _viewModel.SupprimerMembreCommand.Execute(idMembre);
                    _viewModel.MembresSelectionnes.Remove(idMembre);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _viewModel.MembresEquipe;
                    RafraichirCheckedListBox();
                }
            };

            btnSupprimer.Click += (s, e) => _viewModel.SupprimerEquipeCommand.Execute(id);
        }

        private void RafraichirCheckedListBox()
        {
            clbMembres.DataSource = null;
            clbMembres.DataSource = _viewModel.MembresDisponibles;
            clbMembres.DisplayMember = "nom";
            clbMembres.ValueMember = "idUser";

            for (int i = 0; i < clbMembres.Items.Count; i++)
            {
                var user = (UserDTO2)clbMembres.Items[i];
                clbMembres.SetItemChecked(i, _viewModel.MembresSelectionnes.Contains(user.idUser));
            }
        }

        private void OnEquipeModifiee()
        {
            MessageBox.Show("Opération réussie.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

      
        public GestionDesEquipes2ViewModel ViewModel { get; set; }

      
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "dateAjout")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime date))
                {
                    e.Value = date == DateTime.MinValue ? "N/A" : date.ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = "N/A";
                    e.FormattingApplied = true;
                }
            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Activer le bouton "Retirer"
                btnDeleteItem.Enabled = true;
            }
            else
            {
                // Désactiver le bouton "Retirer" si aucune ligne n'est sélectionnée
                btnDeleteItem.Enabled = false;
            }
        }

        private void GestionDesEquipes2_Load_1(object sender, EventArgs e)
        {

            if (ViewModel != null)
            {
                // Définir la DataSource
                clbMembres.DataSource = null;
                clbMembres.DataSource = ViewModel.MembresDisponibles;
                clbMembres.DisplayMember = "nom";
                clbMembres.ValueMember = "idUser";

                // Parcourir les membres et cocher ceux qui sont déjà dans l'équipe
                for (int i = 0; i < clbMembres.Items.Count; i++)
                {
                    var user = (UserDTO2)clbMembres.Items[i];
                    if (ViewModel.MembresSelectionnes.Contains(user.idUser))
                    {
                        clbMembres.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void clbMembres_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var selectedIds = clbMembres.CheckedItems.Cast<UserDTO2>().Select(u => u.idUser).ToList();

            var clickedUserId = ((UserDTO2)clbMembres.Items[e.Index]).idUser;

            if (e.NewValue == CheckState.Checked)
            {
                selectedIds.Add(clickedUserId);
            }
            else
            {
                selectedIds.Remove(clickedUserId);
            }


            _viewModel.MembresSelectionnes = selectedIds;

            cmbChefEquipe.DataSource = null;
            cmbChefEquipe.DataSource = _viewModel.ChefsDisponibles;
            cmbChefEquipe.DisplayMember = "nom";
            cmbChefEquipe.ValueMember = "idUser";
        }
    }
}
