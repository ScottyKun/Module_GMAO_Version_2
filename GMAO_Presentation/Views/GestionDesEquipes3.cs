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
    public partial class GestionDesEquipes3 : Form
    {
        private readonly GestionDesEquipes3ViewModel _viewModel;
        public GestionDesEquipes3()
        {
            InitializeComponent();
            _viewModel = new GestionDesEquipes3ViewModel();
            _viewModel.OnEquipeAjoutee += OnEquipeAjoutee;

            txtNomEquipe.DataBindings.Add("Text", _viewModel, "NomEquipe", false, DataSourceUpdateMode.OnPropertyChanged);
            chkStatut.DataBindings.Add("Checked", _viewModel, "Statut", false, DataSourceUpdateMode.OnPropertyChanged);

            clbMembres.DataSource = _viewModel.MembresDisponibles;
            clbMembres.DisplayMember = "nom";
            clbMembres.ValueMember = "idUser";

            cmbChefEquipe.DataBindings.Add("SelectedItem", _viewModel, "ChefSelectionne", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbChefEquipe.DataSource = _viewModel.ChefsDisponibles;
            cmbChefEquipe.DisplayMember = "nom";
            cmbChefEquipe.ValueMember = "idUser";


            //  btnAjouter.DataBindings.Add("Enabled", _viewModel, "CanExecute", false, DataSourceUpdateMode.OnPropertyChanged);
            btnAjouter.Click += (s, e) => _viewModel.AjouterEquipeCommand.Execute(null);
        }

        private void OnEquipeAjoutee()
        {
            MessageBox.Show("Équipe ajoutée avec succès !");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void clbMembres_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            var selectedIds = clbMembres.CheckedItems.Cast<UserDTO>().Select(u => u.idUser).ToList();
            if (e.NewValue == CheckState.Checked)
            {
                selectedIds.Add(((UserDTO)clbMembres.Items[e.Index]).idUser);
            }
            else
            {
                selectedIds.Remove(((UserDTO)clbMembres.Items[e.Index]).idUser);
            }

            _viewModel.MettreAJourMembresSelectionnes(selectedIds);
            cmbChefEquipe.DataSource = null;
            cmbChefEquipe.DataSource = _viewModel.ChefsDisponibles;
            cmbChefEquipe.DisplayMember = "nom";
            cmbChefEquipe.ValueMember = "idUser";
        }
    }
}
