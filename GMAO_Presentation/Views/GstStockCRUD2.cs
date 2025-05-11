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
    public partial class GstStockCRUD2 : Form
    {
        private readonly GstStock3ViewModel _viewModel;
        public StockDTO StockModifie { get; private set; }
        public GstStockCRUD2(StockDTO stock)
        {
            InitializeComponent();
            _viewModel = new GstStock3ViewModel(stock);

            txtNom.DataBindings.Add("Text", _viewModel, "NouveauNom", false, DataSourceUpdateMode.OnPropertyChanged);
            //txtNomEquipe.Text = _viewModel.NouveauNom;

            //btnModifier.DataBindings.Add("Enabled", _viewModel, "CanModifier", false, DataSourceUpdateMode.OnPropertyChanged);
            btnModifier.Click += (s, e) =>
            {

                _viewModel.ModifierCommand.Execute(null);

                StockModifie = new StockDTO { 
                    Id=stock.Id,
                    Nom=txtNom.Text
                };

                MessageBox.Show("Stock modifié avec succès.", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.OK;
                this.Close();


            };

            btnSupprimer.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer la suppression ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    _viewModel.SupprimerCommand.Execute(null);
                    StockModifie = null;
                    MessageBox.Show("Stock supprimé.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            };

        }
    }
}
