using Domains;
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
        string cctring = "Data Source=LAPTOP-DU4GOUVH;Initial Catalog=win_form_saude;Integrated Security=True;Pooling=False";
        string sql_dash = @"SELECT 
                            Childs   =  (SELECT COUNT(ID) FROM [win_form_saude].[dbo].[Beneficiary] AS ben   WHERE FLOOR(DATEDIFF(day, CAST(ben.DataOfBirth As Date),GETDATE())/365.25) < 18 ) ,
                            Adults   =  (SELECT COUNT(ID) FROM [win_form_saude].[dbo].[Beneficiary] AS ben   WHERE FLOOR(DATEDIFF(day, CAST(ben.DataOfBirth As Date),GETDATE())/365.25) > 17 ) ,
                            Total    =  (SELECT COUNT(ID) FROM [win_form_saude].[dbo].[Beneficiary] AS ben   )																				   ,
                            Activist =  (SELECT COUNT(ID) FROM [win_form_saude].[dbo].Activist)   
                            ";


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


        public dashboardDTO Dashboard()
        {
            dashboardDTO data = new dashboardDTO();


            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(sql_dash, con);
            SqlDataReader myReader;

            try
            {
                con.Open();


                myReader = cmd.ExecuteReader();


                while (myReader.Read())
                {

                    data.Childs = int.Parse(myReader.GetValue(0).ToString());
                    data.Adults = int.Parse(myReader.GetValue(1).ToString());
                    data.Total = int.Parse(myReader.GetValue(2).ToString());
                    data.Activist = int.Parse(myReader.GetValue(3).ToString());

                }

            }
            catch (Exception x)
            {
                MessageBox.Show("Falha: " + x.ToString());

            }


            return data;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dashboardDTO data = Dashboard();

            label1.Text = "Crianças: " + data.Childs.ToString();
            label2.Text = "Adultos: " + data.Adults.ToString();
            label3.Text = "Beneficiários: " + data.Total.ToString();
            label4.Text = "Ativistas: " + data.Activist.ToString();
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

        private void button6_Click_1(object sender, EventArgs e)
        {

            openChildForm(new Home());

            hideSubmenus();
        }
    }
}
