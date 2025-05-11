using DevExpress.XtraGrid.Views.Grid;
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
    public partial class GestionDesEquipes : Form
    {
        private readonly GestionDesEquipes1ViewModel viewModel;

        public GestionDesEquipes()
        {
            InitializeComponent();
            viewModel = new GestionDesEquipes1ViewModel();

            ConfigureGridView();

            // Lier les données
            gridControlEquipes.DataSource = viewModel.Equipes;

            // Abonner l’événement double-clic
            gridViewEquipes.DoubleClick += GridViewEquipes_DoubleClick;

        }


        private void loadData()
        {
            viewModel.RafraichirEquipes();
            gridControlEquipes.DataSource = null;
            gridControlEquipes.DataSource = new List<TeamInfo>(viewModel.Equipes);

            // Ajustement après chargement
            ((GridView)gridControlEquipes.MainView).BestFitColumns();
        }

        private void btnAjouter_Click_1(object sender, EventArgs e)
        {
            var form = new Views.GestionDesEquipes3();
            if (form.ShowDialog() == DialogResult.OK)
            {
                loadData();

            }

        }

       
        private void ConfigureGridView()
        {
            GridView gridView = (GridView)gridControlEquipes.MainView;

            // Lecture seule
            gridView.OptionsBehavior.Editable = false;

            // Ajustement automatique des colonnes
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.BestFitColumns();

            
            gridView.OptionsSelection.EnableAppearanceFocusedRow = true;
        }

        private void GridViewEquipes_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewEquipes.FocusedRowHandle >= 0)
            {
                int equipeId = (int)gridViewEquipes.GetRowCellValue(gridViewEquipes.FocusedRowHandle, "Id");

                var _viewModel = new GestionDesEquipes2ViewModel(equipeId);
                var form = new GestionDesEquipes2(equipeId) { ViewModel = _viewModel };

                if (form.ShowDialog() == DialogResult.OK)
                {
                    loadData();
                }
            }
        }
    }
}
