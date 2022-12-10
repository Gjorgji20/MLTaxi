using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MLHousePrice.BLL;
using MLHousePrice.DAL;

namespace MLHousePrice
{
    public partial class FrmTaxi : Form
    {

        Dal_Taxi t = new Dal_Taxi();
        public FrmTaxi()
        {
            InitializeComponent();

            t.TaxiModel();
        }
    }
}
