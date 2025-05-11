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
    public partial class AlertesForm : Form
    {
        private readonly AlerteAccVM viewModel;
        public AlertesForm()
        {
            InitializeComponent();

            viewModel = new AlerteAccVM();

            gridControlAlertes.DataSource = viewModel.Alertes;

            // Accès à la vue principale
            var view = gridViewAlertes;
            view.Columns.Clear();

            // Colonne Libellé
            view.Columns.AddVisible("Libelle", "Libellé");

            // Colonne Priorité
            view.Columns.AddVisible("Priorite", "Priorité");

            // Colonne Date
            var dateCol = view.Columns.AddVisible("DateCreation", "Date");
            dateCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateCol.DisplayFormat.FormatString = "dd/MM/yyyy";

            // Colonne Lue (checkbox)
            var checkCol = view.Columns.AddVisible("Terminee", "Lue");
            checkCol.ColumnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();

            // Double-clic pour voir les détails
            view.DoubleClick += (s, e) =>
            {
                var selected = view.GetFocusedRow() as AlerteDTO;
                if (selected != null)
                {
                    var form = new AlerteDetailForm(selected.Id);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        viewModel.RefreshCommand.Execute(null); // actualisation
                        gridViewAlertes.RefreshData(); // pour être sûr
                    }
                }
            };

        }
    }
}
