using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_saude_modern_ui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            customizedDesigng();
            hideSubmenus();
        }

        private Form activeForm = null;

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
             

            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel4.Controls.Add(childForm);
            panel4.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void customizedDesigng()
        {
            //Menu_panel.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;


        }

        private void hideSubmenus()
        {
            if (panel1.Visible)
            {
                panel1.Visible = false;
            }

            if (panel2.Visible)
            {
                panel2.Visible = false;
            }

            if (panel3.Visible)
            {
                panel3.Visible = false;
            }
        }

        private void showSubmenus(Panel submenu)
        {

            if (!submenu.Visible)
            {
                hideSubmenus();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ///
            hideSubmenus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            showSubmenus(panel1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new reports());
            hideSubmenus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new benInsert());
            hideSubmenus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new BenList()); hideSubmenus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showSubmenus(panel2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            showSubmenus(panel3);
        }

        private void button9_Click(object sender, EventArgs e)
        {
           


            openChildForm(new createActivistForm());

            hideSubmenus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openChildForm(new ListingActivist());
            hideSubmenus();
        }
    }
}
