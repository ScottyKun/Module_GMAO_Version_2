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
    public partial class GstStockAccueil : Form
    {
        private readonly GstStock1ViewModel _viewModel;
        public GstStockAccueil()
        {
            InitializeComponent();
            _viewModel = new GstStock1ViewModel();

            dataGridView1.DataSource = new BindingList<StockDTO>(_viewModel.Stocks);

            btnEnregistrer.Click += (s, e) =>
            {
                var addForm = new Views.GstStockCRUD();
                if (addForm.ShowDialog() == DialogResult.OK)
                    LoadData();
            };
        }
        private void LoadData()
        {
            _viewModel.LoadStocks();
            var stocks = _viewModel.Stocks;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = new BindingList<StockDTO>(stocks);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].DataBoundItem is StockDTO selected)
            {
                var updateForm = new Views.GstStockCRUD2(selected);
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
    }
}
