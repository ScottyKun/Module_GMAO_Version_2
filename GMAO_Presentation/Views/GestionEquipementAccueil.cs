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
    public partial class GestionEquipementAccueil : Form
    {
        private EquipementAccueilViewModel _viewModel;
        private BindingList<EquipementDTO> _equipements;

        public GestionEquipementAccueil()
        {
            InitializeComponent();
            _viewModel = new EquipementAccueilViewModel();
            _viewModel.ChargerEquipements();
            _equipements = new BindingList<EquipementDTO>(_viewModel.Equipements);

            gridControlEquipement.DataSource = _equipements;

            gridViewEquipements.OptionsBehavior.Editable = false;

            gridViewEquipements.DoubleClick += (s, e) =>
            {
                if (gridViewEquipements.GetFocusedRow() is EquipementDTO selected)
                {
                    var modifForm = new Views.GestionEquipementUpdate(selected.Id);
                    if (modifForm.ShowDialog() == DialogResult.OK)
                    {
                        var updatedEquipement = modifForm.EquipementModifie;
                        if (updatedEquipement != null)
                        {
                            int index = _viewModel.Equipements.IndexOf(selected);
                            if (index >= 0)
                            {
                                _viewModel.Equipements[index] = updatedEquipement;
                                gridViewEquipements.RefreshData();
                            }
                        }
                        else // si supprimé
                        {
                            _viewModel.Equipements.Remove(selected);
                            _equipements.Remove(selected); // aussi dans la liste liée au GridControl
                            gridViewEquipements.RefreshData();
                        }

                    }
                }
            };

        }


        private void LoadData()
        {
            _viewModel.ChargerEquipements();
            _equipements = new BindingList<EquipementDTO>(_viewModel.Equipements);

            gridControlEquipement.DataSource = null;
            gridControlEquipement.DataSource = _equipements;
            gridViewEquipements.RefreshData();
        }


        private void btnAjouter_Click_1(object sender, EventArgs e)
        {
            var ajoutForm = new Views.GestionEquipementAjout();
            if (ajoutForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

    }
}
