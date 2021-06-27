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
    public partial class ListingActivist : Form
    {
        string cctring = "Data Source=LAPTOP-DU4GOUVH;Initial Catalog=win_form_saude;Integrated Security=True;Pooling=False";
        string ativist= @"SELECT Activist.Id AS    [Id do ativista],
		                    Activist.Name AS [Nome do ativista],
		                    Supervisor.Name AS [Nome do supervisor],
		                    [type].[description] AS [Tipo do ativista]

                    FROM   [win_form_saude].[dbo].[Activist]   
                    LEFT JOIN  [win_form_saude].[dbo].[Activist] AS Supervisor ON Supervisor.Id = Activist.SuperiorId
                    LEFT JOIN  [win_form_saude].[dbo].[ActivistType] AS [type] ON [type].Id     = Activist.ActivistTypeId
                        ";
     
        public ListingActivist()
        {
            InitializeComponent();
        }

        private void ListingActivist_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetActivistis();
        }

        private DataTable GetActivistis()
        {
            DataTable dataTable = new DataTable();




            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(ativist, con);
            SqlDataReader myReader;



            try
            {
                con.Open();
                myReader = cmd.ExecuteReader();
                dataTable.Load(myReader);



            }
            catch (Exception)
            {

                throw;
            }







            return dataTable;
        }
    }
}
