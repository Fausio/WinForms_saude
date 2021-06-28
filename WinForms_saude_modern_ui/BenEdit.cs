using Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_saude_modern_ui
{
    public partial class BenEdit : Form
    {
        private Beneficiary Beneficiary { get; set; }
        public BenEdit(Beneficiary beneficiary )
        {
            InitializeComponent();
            this.Beneficiary = beneficiary;
        }

        public BenEdit()
        {
            InitializeComponent();
            this.Show();
        }

        private void BenEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
