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
    public partial class CategorieFormUpdate : Form
    {
        private readonly CategoryUpdateViewModel _viewModel;
        public CategoryDTO CategorieModifiee { get; private set; }
        public CategorieFormUpdate(CategoryDTO categorie)
        {
            InitializeComponent();

            _viewModel = new CategoryUpdateViewModel(categorie);

            txtNomEquipe.DataBindings.Add("Text", _viewModel, "NouveauNom", false, DataSourceUpdateMode.OnPropertyChanged);
            txtNomEquipe.Text = _viewModel.NouveauNom;

            //btnModifier.DataBindings.Add("Enabled", _viewModel, "CanModifier", false, DataSourceUpdateMode.OnPropertyChanged);
            btnModifier.Click += (s, e) =>
            {

                _viewModel.ModifierCommand.Execute(null);


                var updated = new CategoryDTO
                { 
                    id = categorie.id,
                    nom = txtNomEquipe.Text

                };
                CategorieModifiee = updated;
                MessageBox.Show("Catégorie modifiée avec succès.");

                
                this.DialogResult = DialogResult.OK;
                this.Close();


            };

            btnSupprimer.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Confirmer la suppression ?", "Suppression", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _viewModel.SupprimerCommand.Execute(null);
                    MessageBox.Show("Catégorie supprimée.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            };

        }
    }
}
