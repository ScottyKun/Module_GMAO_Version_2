using GMAO_Business.DTOs;
using GMAO_Business.Services;
using GMAO_Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMAO_Presentation.ViewModel
{
    public class GestionDesEquipes2ViewModel : INotifyPropertyChanged
    {
        private readonly EquipeService _equipeService;

        private readonly int _idEquipe;
        private string _nomEquipe;
        private int _chefEquipeId;
        private bool _statut;

        public List<UserDTO2> MembresDisponibles { get; set; }
        // public ObservableCollection<UserDTO2> MembresDisponibles { get; set; } = new ObservableCollection<UserDTO2>();
        public List<int> MembresSelectionnes { get; set; } = new List<int>(); // Liste des ID des membres sélectionnés

        public List<UserDTO2> MembresEquipe { get; set; }
        public List<UserDTO2> ChefsDisponibles
        {
            get
            {
                var listeChefs = MembresDisponibles
                    .Where(u => MembresSelectionnes.Contains(u.idUser))
                    .ToList();

                // S'assurer que le chef actuel est en premier
                var chef = listeChefs.FirstOrDefault(u => u.idUser == ChefEquipeId);
                if (chef != null)
                {
                    listeChefs.Remove(chef);
                    listeChefs.Insert(0, chef);
                }

                return listeChefs;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnEquipeModifiee;

        //liaison des champs
        public string NomEquipe
        {
            get => _nomEquipe;
            set { _nomEquipe = value; OnPropertyChanged(); }
        }


        public int ChefEquipeId
        {
            get => _chefEquipeId;
            set { _chefEquipeId = value; OnPropertyChanged(); }
        }

        public bool Statut
        {
            get => _statut;
            set { _statut = value; OnPropertyChanged(); }
        }

        private UserDTO2 _chefSelectionne;
        public UserDTO2 ChefSelectionne
        {
            get => _chefSelectionne;
            set
            {
                _chefSelectionne = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChefSelectionne)));
            }
        }

        public RelayCommand ModifierEquipeCommand { get; }
        public RelayParam<int> SupprimerMembreCommand { get; }

        public RelayParam<int> SupprimerEquipeCommand { get; }

        public GestionDesEquipes2ViewModel(int idE)
        {
            _idEquipe = idE;
            _equipeService = new EquipeService();

            ChargerEquipe();
            ModifierEquipeCommand = new RelayCommand(ModifierEquipe);
            SupprimerMembreCommand = new RelayParam<int>(SupprimerMembre);
            SupprimerEquipeCommand = new RelayParam<int>(SupprimerEquipe);
        }

        private void SupprimerEquipe(int obj)
        {
            if (MessageBox.Show("Voulez-vous vraiment supprimer cette équipe ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _equipeService.SupprimerEquipe(obj);
                   
                    OnEquipeModifiee?.Invoke();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la suppression : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ChargerEquipe()
        {
            var equipe = _equipeService.GetEquipeById(_idEquipe);
            if (equipe != null)
            {
                NomEquipe = equipe.NomEquipe;
                Statut = equipe.Statut;
                MembresDisponibles = _equipeService.GetAllUtilisateursDate(equipe.IdEquipe);

                // Stocker les ID des membres qui appartiennent déjà à l'équipe
                MembresSelectionnes = equipe.Membres.Select(m => m.idUser).ToList();


                MembresEquipe = MembresDisponibles
                     .Where(u => equipe.Membres.Select(m => m.idUser).Contains(u.idUser))
                     .ToList();

                ChefSelectionne = MembresEquipe.FirstOrDefault(m => m.idUser == equipe.IdChefEquipe);

                ChefEquipeId = equipe.IdChefEquipe;

                OnPropertyChanged(nameof(NomEquipe));
                OnPropertyChanged(nameof(Statut));
                OnPropertyChanged(nameof(MembresDisponibles));
                OnPropertyChanged(nameof(MembresEquipe));
                OnPropertyChanged(nameof(MembresSelectionnes)); // Pour que la Vue puisse récupérer les membres déjà cochés
                OnPropertyChanged(nameof(ChefSelectionne));
                OnPropertyChanged(nameof(ChefEquipeId)); // ✅ Ajouté
                OnPropertyChanged(nameof(ChefsDisponibles));
            }
        }

        private void ModifierEquipe()
        {
            if (string.IsNullOrWhiteSpace(NomEquipe) || !MembresSelectionnes.Any())
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ChefSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un chef d'équipe.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Voulez-vous vraiment modifier cette équipe ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {

                    //_equipeService.ModifierEquipe(_idEquipe, NomEquipe, ChefSelectionne.idUser, Statut, MembresEquipe.Select(u => u.idUser).ToList());
                    //ChefEquipeId = ChefSelectionne.idUser;
                    //ChargerEquipe();

                    _equipeService.ModifierEquipe(_idEquipe, NomEquipe, ChefSelectionne.idUser, Statut, MembresSelectionnes);
                    OnEquipeModifiee?.Invoke();


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la modification : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void SupprimerMembre(int obj)
        {
            if (MessageBox.Show("Voulez-vous vraiment supprimer ce membre ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _equipeService.SupprimerMembreEquipe(_idEquipe, obj);
                    // MembresEquipe.RemoveAll(m => m.idUser == obj);
                    //  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MembresEquipe)));
                    MessageBox.Show("Membre supprimée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Rafraîchir la liste après suppression
                    ChargerEquipe();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la suppression : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
