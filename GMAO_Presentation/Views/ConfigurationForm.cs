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
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
        {
            InitializeComponent();
        }

        private void btnGstCategorie_Click_1(object sender, EventArgs e)
        {
            new Views.CategorieFormAccueil().ShowDialog();
        }

        private void btnGstStock_Click_1(object sender, EventArgs e)
        {
            new Views.GstStockAccueil().ShowDialog();
        }

        private void btnGstBudget_Click_1(object sender, EventArgs e)
        {
            new Views.BudgetAccForm().ShowDialog();
        }
    }
}
