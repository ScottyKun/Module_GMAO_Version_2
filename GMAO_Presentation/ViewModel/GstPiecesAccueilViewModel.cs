using GMAO_Business.DTOs;
using GMAO_Business.Services;
using GMAO_Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMAO_Presentation.ViewModel
{
    public class GstPiecesAccueilViewModel
    {
        private readonly PieceRechangeService _service;
        private const int SEUIL_CRITIQUE = 5; // Tu peux modifier selon ton besoin

        public BindingList<PieceDTO> ListePieces { get; private set; }
        public BindingList<PieceDTO> ListePiecesCritiques { get; private set; }

        public GstPiecesAccueilViewModel()
        {
            _service = new PieceRechangeService();
            Rafraichir();
        }

        public void Rafraichir()
        {
            ListePieces = new BindingList<PieceDTO>(_service.GetAllPieces());
            ListePiecesCritiques = new BindingList<PieceDTO>(_service.GetPiecesCritiques(SEUIL_CRITIQUE));
        }

        public void Commander(PieceDTO piece)
        {
            _service.CreateDemandeAchat(piece);
        }


        public void FiltrerParNomEquipement(string nomEquipement)
        {
            var equipementDto = _service.GetEquipementByNomApprox(nomEquipement.Trim());

            if (equipementDto != null)
            {
                var resultat = _service.GetByEquipement(equipementDto.Id);
                ListePieces = new BindingList<PieceDTO>(resultat);
            }
            else
            {
                ListePieces = new BindingList<PieceDTO>();
                MessageBox.Show("Aucun équipement ne correspond à votre recherche.", "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public List<string> GetEquipementsNoms()
        {
            return _service.GetEquipements()
                           .Where(e => !string.IsNullOrEmpty(e.Nom))
                           .Select(e => e.Nom)
                           .ToList();
        }

    }

}
