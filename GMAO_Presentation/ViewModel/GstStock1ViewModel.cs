using GMAO_Business.DTOs;
using GMAO_Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class GstStock1ViewModel
    {
        public BindingList<StockDTO> Stocks { get; set; }
        private readonly StockService _service;

        public GstStock1ViewModel()
        {
            _service = new StockService();

            Stocks = new BindingList<StockDTO>(_service.GetAllStocks());
        }

        public void LoadStocks()
        {
            Stocks = new BindingList<StockDTO>(_service.GetAllStocks());
        }


    }
}
