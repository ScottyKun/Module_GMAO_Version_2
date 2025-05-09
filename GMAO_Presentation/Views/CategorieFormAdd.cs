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
    public partial class CategorieFormAdd : Form
    {
        private readonly CategoryAddViewModel _viewModel;
        public CategorieFormAdd()
        {
            InitializeComponent();
            _viewModel = new CategoryAddViewModel();

            txtNomEquipe.DataBindings.Add("Text", _viewModel, "Nom", false, DataSourceUpdateMode.OnPropertyChanged);

            btnAjouter.Click += (s, e) =>
            {
                if (_viewModel.EnregistrerCommand.CanExecute(null))
                {
                    _viewModel.EnregistrerCommand.Execute(null);
                    MessageBox.Show("Catégorie ajoutée avec succès.");
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
