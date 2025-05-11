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

            gridControlStock.DataSource = new BindingList<StockDTO>(_viewModel.Stocks);

            btnEnregistrer.Click += (s, e) =>
            {
                var addForm = new Views.GstStockCRUD();
                if (addForm.ShowDialog() == DialogResult.OK)
                    LoadData();
            };

            gridViewStock.OptionsBehavior.Editable = false;

            gridViewStock.DoubleClick += (s, e) =>
            {
                if (gridViewStock.GetFocusedRow() is StockDTO selected)
                {
                    var updateForm = new Views.GstStockCRUD2(selected);
                    if (updateForm.ShowDialog() == DialogResult.OK)
                    {
                        var updatedStock = updateForm.StockModifie;

                        if (updatedStock == null)
                        {
                            LoadData();
                        }
                        else
                        {
                            int index = _viewModel.Stocks.IndexOf(selected);
                            if (index >= 0)
                            {
                                _viewModel.Stocks[index] = updatedStock;
                                LoadData();
                            }
                        }
                    }

                }
            };
        }
        private void LoadData()
        {
            _viewModel.LoadStocks();
            var stocks = _viewModel.Stocks;
            gridControlStock.DataSource = null;
            gridControlStock.DataSource = new BindingList<StockDTO>(stocks);
            gridViewStock.RefreshData();
        }
    }
}
