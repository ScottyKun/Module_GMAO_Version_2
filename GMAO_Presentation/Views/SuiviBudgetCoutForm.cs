using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
    public partial class SuiviBudgetCoutForm : Form
    {
        private readonly KpiDashboardVM _viewModel;
        public SuiviBudgetCoutForm()
        {
            InitializeComponent();

            _viewModel = new KpiDashboardVM();

            _viewModel.LoadAll();
        }

        private void SuiviBudgetCoutForm_Load(object sender, EventArgs e)
        {
            SetupTopPage();
            SetupGlobalPage();
            SetupBudgetPage();
            SetupEvolutionPage();
        }

        private void SetupTopPage()
        {
            // TopEquipements
            chartTopEquipements.DataSource = _viewModel.TopVM.TopEquipements;
            var seriesEquipements = chartTopEquipements.Series[0];

            seriesEquipements.ArgumentDataMember = "Label";
            seriesEquipements.ValueDataMembers.AddRange("Value");
            seriesEquipements.LegendText = "Équipements les plus coûteux";

            // Configuration Diagramme
            if (chartTopEquipements.Diagram is XYDiagram diagramEquipements)
            {
                diagramEquipements.Rotated = true; // Rotation horizontale
                diagramEquipements.AxisX.Label.Angle = -45;
                diagramEquipements.AxisY.WholeRange.AlwaysShowZeroLevel = false;

                diagramEquipements.AxisY.Title.Text = "Coût total";
                diagramEquipements.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagramEquipements.AxisY.Title.Alignment = StringAlignment.Center;
            }

            // Configuration Vue Barres
            if (seriesEquipements.View is BarSeriesView barViewEquipements)
            {
                barViewEquipements.BarWidth = 0.6;
                barViewEquipements.FillStyle.FillMode = FillMode.Solid;
            }
            else
            {
                seriesEquipements.ChangeView(ViewType.Bar);
            }

            // Top pieces
            chartTopPieces.DataSource = _viewModel.TopVM.TopPieces;
            var seriesPieces = chartTopPieces.Series[0];

            seriesPieces.ArgumentDataMember = "Label";
            seriesPieces.ValueDataMembers.AddRange("Value");
            seriesPieces.LegendText = "Pièces les plus utilisées";

            // Configuration Diagramme
            if (chartTopPieces.Diagram is XYDiagram diagramPieces)
            {
                diagramPieces.Rotated = true; // Rotation horizontale
                diagramPieces.AxisX.Label.Angle = -45;
                diagramPieces.AxisY.WholeRange.Auto = true;

                diagramPieces.AxisY.Title.Text = "Quantité utilisée";
                diagramPieces.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagramPieces.AxisY.Title.Alignment = StringAlignment.Center;

            }

            // Configuration Vue Barres
            if (seriesPieces.View is BarSeriesView barViewPieces)
            {
                barViewPieces.BarWidth = 0.6;
                barViewPieces.FillStyle.FillMode = FillMode.Gradient;
                barViewPieces.ColorEach = true;
            }
            else
            {
                seriesPieces.ChangeView(ViewType.Bar);
            }

        }

        private void SetupGlobalPage()
        {
            // Valeurs globales
            biCardCoutTotal.ValueText = _viewModel.GlobalVM.CoutTotalGlobal.ToString();
            biCardCoutTotal.Title = "Coût Total";

            biCardCoutCorrective.ValueText = _viewModel.GlobalVM.CoutTotalCorrective.ToString();
            biCardCoutCorrective.Title = "Corrective";

            biCardCoutPlanifiee.ValueText = _viewModel.GlobalVM.CoutTotalPlanifiee.ToString();
            biCardCoutPlanifiee.Title = "Planifiée";

            biCardCoutMoyenCo.ValueText = _viewModel.GlobalVM.CoutMoyenCorrective.ToString();
            biCardCoutMoyenCo.Title = "Moyen Corrective";

            biCardCoutMoyenPlan.ValueText = _viewModel.GlobalVM.CoutMoyenPlanifiee.ToString();
            biCardCoutMoyenPlan.Title = "Moyen Planifiée";


            // Coût par équipement
            chartCoutParEquipement.DataSource = _viewModel.GlobalVM.CoutParEquipement;

            var seriesCout = chartCoutParEquipement.Series[0];
            seriesCout.Name = "Coût total par équipement";
            seriesCout.ArgumentDataMember = "Label";
            seriesCout.ValueDataMembers.Clear(); // au cas où
            seriesCout.ValueDataMembers.AddRange("Value");

            // Affiche la légende
            chartCoutParEquipement.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            if (chartCoutParEquipement.Diagram is XYDiagram diagram)
            {
               
                diagram.AxisY.Title.Text = "Coût";
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
            }

            // Répartition
            chartRepartition.DataSource = _viewModel.GlobalVM.RepartitionMaintenance;

            var seriesRepartition = chartRepartition.Series[0];
            seriesRepartition.Name = "Répartition des maintenances";
            seriesRepartition.ArgumentDataMember = "Label";
            seriesRepartition.ValueDataMembers.Clear();
            seriesRepartition.ValueDataMembers.AddRange("Value");

            // Affiche les pourcentages dans les labels si tu veux :
            seriesRepartition.Label.TextPattern = "{A}: {V} ({VP:P1})";

            // Active la légende
            chartRepartition.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

        }

        private void SetupBudgetPage()
        {
            chartPrevuReel.DataSource = _viewModel.BudgetVM.CoutPrevuVsReel;
            var corrective = chartPrevuReel.Series[0];
            corrective.ArgumentDataMember = "Mois";
            corrective.ValueDataMembers.AddRange("CoutPrevu");
            corrective.LegendText = "Cout Prevu";

            var planifiee = chartPrevuReel.Series[1];
            planifiee.ArgumentDataMember = "Mois";
            planifiee.ValueDataMembers.AddRange("CoutReel");
            planifiee.LegendText = "Cout Reel";


            if (chartPrevuReel.Diagram is XYDiagram diagram)
            {
                diagram.AxisX.Title.Text = "Mois";
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Alignment = StringAlignment.Center;

                diagram.AxisY.Title.Text = "Coût";
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
            }

            chartDepenseMensuelle.DataSource = _viewModel.BudgetVM.DepensesMensuelles;
            // Série budget mensuel (barres)
            var seriesBudget = chartDepenseMensuelle.Series[0];
            seriesBudget.ArgumentDataMember = "Mois";
            seriesBudget.ValueDataMembers.AddRange("BudgetLisse");
            seriesBudget.LegendText = "Budget mensuel";

            // Série coûts réels cumulés (lignes)
            var seriesCout = chartDepenseMensuelle.Series[1];
            seriesCout.ArgumentDataMember = "Mois";
            seriesCout.ValueDataMembers.AddRange("CoutReel");
            seriesCout.LegendText = "Coût réel cumulé";

            if (chartDepenseMensuelle.Diagram is XYDiagram diagram2)
            {
                diagram2.AxisX.Title.Text = "Mois";
                diagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram2.AxisX.Title.Alignment = StringAlignment.Center;

                diagram2.AxisY.Title.Text = "Montant";
                diagram2.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram2.AxisY.Title.Alignment = StringAlignment.Center;
            }


            gridViewEcart.DataSource = _viewModel.BudgetVM.EcartBudgets;
           

            // Format des colonnes
            var view = gridViewEcart.MainView as DevExpress.XtraGrid.Views.Grid.GridView;

            view.OptionsBehavior.Editable = false;

            view.Columns["BudgetPrevu"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
           // view.Columns["BudgetPrevu"].DisplayFormat.FormatString = "c0";
            view.Columns["CoutReel"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
           // view.Columns["CoutReel"].DisplayFormat.FormatString = "c0";
            view.Columns["Ecart"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
           // view.Columns["Ecart"].DisplayFormat.FormatString = "c0";

            // Mise en forme conditionnelle pour Ecart
            StyleFormatCondition conditionNegative = new StyleFormatCondition();
            conditionNegative.Column = view.Columns["Ecart"];
            conditionNegative.Condition = FormatConditionEnum.Less;
            conditionNegative.Value1 = 0;
            conditionNegative.Appearance.BackColor = Color.LightCoral;
            conditionNegative.Appearance.ForeColor = Color.White;
            conditionNegative.Appearance.Options.UseBackColor = true;
            conditionNegative.Appearance.Options.UseForeColor = true;

            StyleFormatCondition conditionPositive = new StyleFormatCondition();
            conditionPositive.Column = view.Columns["Ecart"];
            conditionPositive.Condition = FormatConditionEnum.GreaterOrEqual;
            conditionPositive.Value1 = 0;
            conditionPositive.Appearance.BackColor = Color.LightGreen;
            conditionPositive.Appearance.ForeColor = Color.Black;
            conditionPositive.Appearance.Options.UseBackColor = true;
            conditionPositive.Appearance.Options.UseForeColor = true;

            view.FormatConditions.AddRange(new StyleFormatCondition[] {
                conditionNegative, conditionPositive
            });

            //
            // Assigne la source
            gridTauxDepassement.DataSource = _viewModel.BudgetVM.Depassement;

            // Récupère la GridView
            var gridView = gridTauxDepassement.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gridView == null) return;

          
            gridView.OptionsView.ShowGroupPanel = false;

            gridView.OptionsBehavior.Editable = false;

            gridView.BestFitColumns();

            // --- Orange si Ecart < 0 ---
            var ruleNegatif = new GridFormatRule();
            var conditionNegatif = new FormatConditionRuleValue();
            ruleNegatif.Column = gridView.Columns["Ecart"];
            ruleNegatif.ColumnApplyTo = gridView.Columns["Ecart"];
            conditionNegatif.Condition = FormatCondition.Less;
            conditionNegatif.Value1 = 0;
            conditionNegatif.Appearance.BackColor = Color.Orange;
            conditionNegatif.Appearance.ForeColor = Color.Black;
            conditionNegatif.Appearance.Options.UseBackColor = true;
            conditionNegatif.Appearance.Options.UseForeColor = true;
            ruleNegatif.Rule = conditionNegatif;
            gridView.FormatRules.Add(ruleNegatif);

            // --- Beige si Ecart == 0 ---
            var ruleZero = new GridFormatRule();
            var conditionZero = new FormatConditionRuleValue();
            ruleZero.Column = gridView.Columns["Ecart"];
            ruleZero.ColumnApplyTo = gridView.Columns["Ecart"];
            conditionZero.Condition = FormatCondition.Equal;
            conditionZero.Value1 = 0;
            conditionZero.Appearance.BackColor = Color.Beige;
            conditionZero.Appearance.ForeColor = Color.Black;
            conditionZero.Appearance.Options.UseBackColor = true;
            conditionZero.Appearance.Options.UseForeColor = true;
            ruleZero.Rule = conditionZero;
            gridView.FormatRules.Add(ruleZero);



            gaugeBudget.Value = (double)_viewModel.BudgetVM.PourcentageUtilisationBudget;
            if (gaugeBudget.Value > 100)
            {
                gaugeBudget.GaugeColor = Color.IndianRed;
            }
            else if (gaugeBudget.Value > 80)
            {
                gaugeBudget.GaugeColor = Color.Orange;
            }
            else
            {
                gaugeBudget.GaugeColor = Color.SeaGreen;
            }


        }

        private void SetupEvolutionPage()
        {
            chartEvolution.DataSource = _viewModel.EvolutionVM.EvolutionMensuelle;
            var corrective = chartEvolution.Series[0];
            corrective.ArgumentDataMember = "Mois";
            corrective.ValueDataMembers.AddRange("CoutCorrective");
            corrective.LegendText = "Maintenance Corrective";

            var planifiee = chartEvolution.Series[1];
            planifiee.ArgumentDataMember = "Mois";
            planifiee.ValueDataMembers.AddRange("CoutPlanifiee");
            planifiee.LegendText = "Maintenance Planifiée";

            if (chartEvolution.Diagram is XYDiagram diagram)
            {
                diagram.AxisX.Title.Text = "Mois";
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Title.Alignment = StringAlignment.Center;

                diagram.AxisY.Title.Text = "Coût";
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Title.Alignment = StringAlignment.Center;
            }
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
                    var tabPage = Dashboard.SelectedTabPage;
                    if (tabPage == null)
                    {
                        MessageBox.Show("Aucune page sélectionnée pour l'export.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        GMAO_Presentation.Helpers.ExportHelper.ExportVisuelsDeLaPage(tabPage, dossierExport);
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
