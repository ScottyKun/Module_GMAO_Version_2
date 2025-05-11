using GMAO_Business.DTOs;
using GMAO_Business.Services;
using GMAO_Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class CategoryUpdateViewModel : INotifyPropertyChanged
    {
        private readonly CategoryService _service;
        public CategoryDTO Categorie { get; set; }

        public RelayCommand ModifierCommand { get; }
        public RelayCommand SupprimerCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _nouveauNom;
        public string NouveauNom
        {
            get => _nouveauNom;
            set
            {
                _nouveauNom = value;
                OnPropertyChanged(nameof(NouveauNom));
                //ModifierCommand.EvaluateCanExecute();
            }
        }

        public CategoryUpdateViewModel(CategoryDTO categorie)
        {
            _service = new CategoryService();
            Categorie = categorie;
            NouveauNom = categorie.nom;

            ModifierCommand = new RelayCommand(Modifier);
            SupprimerCommand = new RelayCommand(Supprimer);
        }

        private void Modifier()
        {
            Categorie.nom = NouveauNom;
            _service.Update(Categorie);
        }

        // private bool CanModifier => !string.IsNullOrWhiteSpace(NouveauNom);


        private void Supprimer()
        {
            _service.Delete(Categorie.id);
        }

        protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
