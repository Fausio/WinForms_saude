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
    public partial class Home : Form
    {

        string cctring = "Data Source=LAPTOP-DU4GOUVH;Initial Catalog=win_form_saude;Integrated Security=True;Pooling=False";
        string sql_dash = @"SELECT 
                            Childs   =  (SELECT COUNT(ID) FROM [win_form_saude].[dbo].[Beneficiary] AS ben   WHERE FLOOR(DATEDIFF(day, CAST(ben.DataOfBirth As Date),GETDATE())/365.25) < 18 ) ,
                            Adults   =  (SELECT COUNT(ID) FROM [win_form_saude].[dbo].[Beneficiary] AS ben   WHERE FLOOR(DATEDIFF(day, CAST(ben.DataOfBirth As Date),GETDATE())/365.25) > 17 ) ,
                            Total    =  (SELECT COUNT(ID) FROM [win_form_saude].[dbo].[Beneficiary] AS ben   )																				   ,
                            Activist =  (SELECT COUNT(ID) FROM [win_form_saude].[dbo].Activist)   
                            ";

        public Home()
        {
            InitializeComponent();
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

        private void Home_Load(object sender, EventArgs e)
        {
            dashboardDTO data = Dashboard();

            label1.Text = "Crianças: " + data.Childs.ToString();
            label2.Text = "Adultos: " + data.Adults.ToString();
            label3.Text = "Beneficiários: " + data.Total.ToString();
            label4.Text = "Ativistas: " + data.Activist.ToString();
        }
    }
}
