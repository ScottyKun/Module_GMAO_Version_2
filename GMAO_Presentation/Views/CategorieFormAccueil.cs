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
        public CategorieFormAccueil()
        {
            InitializeComponent();
            _viewModel = new CategoryAccueilViewModel();

            dataGridView1.DataSource = new BindingList<CategoryDTO>(_viewModel.Categories);

            btnEnregistrer.Click += (s, e) =>
            {
                var addForm = new Views.CategorieFormAdd();
                if (addForm.ShowDialog() == DialogResult.OK)
                    LoadData();
            };
        }

        private void LoadData()
        {
            _viewModel.LoadCategories();
            var categories = _viewModel.Categories;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new BindingList<CategoryDTO>(categories);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].DataBoundItem is CategoryDTO selected)
            {
                var updateForm = new CategorieFormUpdate(selected);
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
    }
}
