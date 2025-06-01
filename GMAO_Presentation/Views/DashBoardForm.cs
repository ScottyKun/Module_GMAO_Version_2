using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Heatmap;
using DevExpress.XtraTreeMap;
using GMAO_Presentation.Helpers;
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
    public partial class DashBoardForm : Form
    {
        private DashboardVM viewModel;
        public DashBoardForm()
        {
            InitializeComponent();
            viewModel = new DashboardVM();

            var dateDebut = new DateTime(DateTime.Today.Year, 1, 1);
            var dateFin = DateTime.Today;
            viewModel.LoadAll(dateDebut,dateFin);
        }

        private void DashBoardForm_Load(object sender, EventArgs e)
        {
            setupEquipementPage();
            setupEquipePage();
            setupResponsablePage();
        }

        private void setupResponsablePage()
        {
            chartClotureWO.Series.Clear();
            var series = new Series("Taux de clôture", ViewType.Bar);
            ((BarSeriesView)series.View).ColorEach = true;

            foreach (var item in viewModel.responsableVM.TauxClotureWo)
            {
                var point = new SeriesPoint(item.Responsable, item.TauxCloture); // en %

                // Couleur indicative
                if (item.TauxCloture >= 100)
                    point.Color = Color.Green;
                else if (item.TauxCloture >= 80)
                    point.Color = Color.Orange;
                else
                    point.Color = Color.Red;

                series.Points.Add(point);
            }

            chartClotureWO.Series.Add(series);

            if (chartClotureWO.Diagram is XYDiagram diagram)
            {
                diagram.Rotated = true;
                diagram.AxisY.Title.Text = "Taux (%)";
                diagram.AxisX.Title.Text = "Responsables";
                
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }

            chartTauxPlanif.Series.Clear();

            var series2 = new Series("Taux de réalisation", ViewType.Bar);
            ((BarSeriesView)series2.View).ColorEach = true;

            foreach (var item in viewModel.responsableVM.TauxPlanification)
            {
                var point = new SeriesPoint(item.Responsable, item.TauxRealisation); // en %

                // Couleur selon performance
                if (item.TauxRealisation >= 80)
                    point.Color = Color.Green;
                else if (item.TauxRealisation >= 50)
                    point.Color = Color.Orange;
                else
                    point.Color = Color.Red;

                series2.Points.Add(point);
            }

            chartTauxPlanif.Series.Add(series2);

            // Configuration du diagramme
            if (chartTauxPlanif.Diagram is XYDiagram diagram2)
            {
                diagram2.Rotated = true; // Barres horizontales
                diagram2.AxisY.Title.Text = "Taux de réalisation (%)";
                diagram2.AxisX.Title.Text = "Responsables";
                diagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram2.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;

                diagram2.AxisX.WholeRange.SetMinMaxValues(0, 100);
            }


        }

        private void setupEquipePage()
        {
            chartDonutReussite.DataSource = viewModel.equipeVM.TauxReussite;

            // Créer la série Doughnut
            var series = new Series("Taux de Réussite", ViewType.Doughnut)
            {
                ArgumentDataMember = "Equipe"
            };
            series.ValueDataMembers.AddRange("TauxReussite");

            // Activer les légendes avec le nom de chaque équipe
            series.LegendTextPattern = "{A}"; // A = Argument, donc le nom de l'équipe

            // Effacer l'existant et ajouter la nouvelle série
            chartDonutReussite.Series.Clear();
            chartDonutReussite.Series.Add(series);

            // Configurer la vue Doughnut
            var view = (DoughnutSeriesView)series.View;
            view.HoleRadiusPercent = 60;

            // Activer la légende
            chartDonutReussite.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            // (Optionnel) Ajouter les valeurs au centre
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series.Label.TextPattern = "{A}: {V:0.##}%"; // A = Équipe, V = Valeur



            chartTempsIntervention.Series.Clear();
            var series2 = new Series("Temps Moyen", ViewType.Bar);
            ((BarSeriesView)series2.View).ColorEach = true;

            foreach (var item in viewModel.equipeVM.TempsIntervention)
            {
                var point = new SeriesPoint(item.Equipe, item.TempsMoyenHeures);

                // Couleurs conditionnelles simulant la "chaleur"
                if (item.TempsMoyenHeures < 2)
                    point.Color = Color.Green;
                else if (item.TempsMoyenHeures < 4)
                    point.Color = Color.Gold;
                else
                    point.Color = Color.Red;

                series2.Points.Add(point);
            }

            chartTempsIntervention.Series.Add(series2);

            // Diagramme horizontal
            if (chartTempsIntervention.Diagram is XYDiagram diagram)
            {
                diagram.Rotated = true;
                diagram.AxisX.Label.Angle = -45;

                diagram.AxisY.Title.Text = "Temps moyen (h)";
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }


        }

        private void setupEquipementPage()
        {
            // Créer et configurer l'adapter
            var adapter = new TreeMapFlatDataAdapter()
            {
                DataSource = viewModel.equipementVM.ClassificationABC,
                LabelDataMember = "NomEquipement",
                ValueDataMember = "CoutTotal",
            };

            adapter.GroupDataMembers.Add("CategorieABC");
            // Assigner l’adapter au TreeMap
            treeMapABC.DataAdapter = adapter;

            TreeMapPaletteColorizer colorizer = new TreeMapPaletteColorizer
            {
                ColorizeGroups = true
            };

            
            // Assigner le coloriseur au TreeMap
            treeMapABC.Colorizer = colorizer;




            stackedBarPanne.DataSource = viewModel.equipementVM.TauxPanne;

            var series = new Series("Nombre de pannes", ViewType.StackedBar);
            series.ArgumentDataMember = "NomEquipement";
            series.ValueDataMembers.AddRange("NbPannes");

            stackedBarPanne.Series.Clear();
            stackedBarPanne.Series.Add(series);

            if (stackedBarPanne.Diagram is XYDiagram diagram)
            {
                diagram.AxisX.Label.Angle = -45;
                diagram.AxisY.Title.Text = "Nb pannes";
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }


            chartMTTR.DataSource = viewModel.equipementVM.Mttr;

            var series2 = new Series("MTTR", ViewType.Bar);
            series2.ArgumentDataMember = "NomEquipement";
            series2.ValueDataMembers.AddRange("MTTRenHeures");

            chartMTTR.Series.Clear();
            chartMTTR.Series.Add(series2);

            if (chartMTTR.Diagram is XYDiagram diag)
            {
                diag.Rotated = true;
                diag.AxisY.Title.Text = "Durée (h)";
                //diagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diag.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }

            rangeBarMTBF.DataSource = viewModel.equipementVM.Mtbf;

            var series3 = new Series("MTBF", ViewType.RangeBar);
            series3.ArgumentDataMember = "NomEquipement";                   // Axe Y (étiquette)
            series3.ValueDataMembers.AddRange("Debut", "MTBFenJours");      // Axe X (début, fin)

            rangeBarMTBF.Series.Clear();
            rangeBarMTBF.Series.Add(series3);

            // Affichage horizontal
            var view = (RangeBarSeriesView)series3.View;
            view.BarWidth = 0.5;
            view.Transparency = 50;


            if (rangeBarMTBF.Diagram is XYDiagram diag2)
            {
                diag2.Rotated = true;
                diag2.AxisY.Title.Text = "Durée (Jours)";
               // diag2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diag2.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }


        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            new Views.DashboardDesigner().ShowDialog();
        }

        private void btnTelecharger_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Choisissez le dossier où exporter les visuels";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string dossierExport = folderDialog.SelectedPath;

                    // Récupère la page courante sélectionnée du XtraTabControl (celle dont on veut exporter les visuels)
                    var tabPage = xtraTabControl1.SelectedTabPage;
                    if (tabPage == null)
                    {
                        MessageBox.Show("Aucune page sélectionnée pour l'export.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        ExportHelper.ExportVisuelsDeLaPage(tabPage, dossierExport);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de l'export : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
