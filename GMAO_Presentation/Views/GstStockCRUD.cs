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
    public partial class GstStockCRUD : Form
    {
        private readonly GstStock2ViewModel _viewModel;
        public GstStockCRUD()
        {
            InitializeComponent();
            _viewModel = new GstStock2ViewModel();

            txtNom.DataBindings.Add("Text", _viewModel, "Nom", false, DataSourceUpdateMode.OnPropertyChanged);

            btnAjouter.Click += (s, e) =>
            {
                if (_viewModel.EnregistrerCommand.CanExecute(null))
                {
                    _viewModel.EnregistrerCommand.Execute(null);
                    MessageBox.Show("Stock ajoutée avec succès.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Veuillez entrer un nom valide.");
                }
            };
        }
    }
}
