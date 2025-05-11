using GMAO_Business.DTOs;
using GMAO_Business.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GMAO_Presentation.ViewModel
{
    public class GestionDesEquipes1ViewModel : INotifyPropertyChanged
    {
        private readonly EquipeService service;
        private List<TeamInfo> _equipes;

        public event Action<int> OnNavigate;
        public event PropertyChangedEventHandler PropertyChanged;

        public List<TeamInfo> Equipes
        {
            get => _equipes;
            set { _equipes = value; OnPropertyChanged(); }
        }


        public GestionDesEquipes1ViewModel()
        {
            service = new EquipeService();
            Equipes = new List<TeamInfo>(service.GetAllEquipes());

        }

       
        public void RafraichirEquipes()
        {
            Equipes = new List<TeamInfo>(service.GetAllEquipes());
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
