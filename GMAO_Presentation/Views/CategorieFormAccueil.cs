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
    public partial class CategorieFormAccueil : Form
    {
        private readonly CategoryAccueilViewModel _viewModel;
        private BindingList<CategoryDTO> _bindedCategories;
        public CategorieFormAccueil()
        {
            InitializeComponent();
            _viewModel = new CategoryAccueilViewModel();

            LoadData();

            btnEnregistrer.Click += (s, e) =>
            {
                var addForm = new Views.CategorieFormAdd();
                if (addForm.ShowDialog() == DialogResult.OK)
                    LoadData();
            };


            gridViewCategories.DoubleClick += (s, e) =>
            {
                var selected = gridViewCategories.GetFocusedRow() as CategoryDTO;
                if (selected != null)
                {
                    var updateForm = new CategorieFormUpdate(selected);
                    if (updateForm.ShowDialog() == DialogResult.OK)
                    {
                        var updated = updateForm.CategorieModifiee;
                        if (updated != null)
                        {
                            int index = _bindedCategories.IndexOf(selected);
                            if (index >= 0)
                            {
                                _bindedCategories[index] = updated;
                                gridViewCategories.RefreshData(); // Redraw
                            }
                        }
                        else // suppression
                        {
                            _bindedCategories.Remove(selected);
                            gridViewCategories.RefreshData();
                        }
                    }

                }
            };
        }

        private void LoadData()
        {
            _viewModel.LoadCategories();

            gridViewCategories.OptionsBehavior.Editable = false;
            gridViewCategories.OptionsView.ShowGroupPanel = false;
            gridViewCategories.BestFitColumns();
            // On rafraîchit complètement la source du GridControl

            gridControlCategories.DataSource = null;
            _bindedCategories = new BindingList<CategoryDTO>(_viewModel.Categories);
            gridControlCategories.DataSource = _bindedCategories;

            // Force le rafraîchissement visuel
            gridViewCategories.RefreshData();
        }
    }
}
