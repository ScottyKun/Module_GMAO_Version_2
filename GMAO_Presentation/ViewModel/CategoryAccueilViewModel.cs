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
    public class CategoryAccueilViewModel : INotifyPropertyChanged
    {
        public BindingList<CategoryDTO> Categories { get; set; }
        private readonly CategoryService _service;

        public event PropertyChangedEventHandler PropertyChanged;


        public CategoryAccueilViewModel()
        {
            _service = new CategoryService();

            Categories = new BindingList<CategoryDTO>(_service.GetAll());
        }

        public void LoadCategories()
        {
            Categories = new BindingList<CategoryDTO>(_service.GetAll());
        }

    }
}
