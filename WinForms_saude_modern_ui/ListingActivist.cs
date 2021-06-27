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
        string ativist = @"SELECT Activist.Id AS    [Id do ativista],
		                    Activist.Name AS [Nome do ativista],
		                    Supervisor.Name AS [Nome do supervisor],
		                    [type].[description] AS [Tipo do ativista]

                    FROM   [win_form_saude].[dbo].[Activist]   
                    LEFT JOIN  [win_form_saude].[dbo].[Activist] AS Supervisor ON Supervisor.Id = Activist.SuperiorId
                    LEFT JOIN  [win_form_saude].[dbo].[ActivistType] AS [type] ON [type].Id     = Activist.ActivistTypeId
                        ";

        string sql_delete_activist = "DELETE [win_form_saude].[dbo].[Activist]  WHERE ID = @SelectedID";
        string sql_hava_inferior = "SELECT COUNT(ID) FROM [win_form_saude].[dbo].[Activist] WHERE ID = @SelectedID";


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

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
          
        }

        private void btn_insert_ativist_Click(object sender, EventArgs e)
        {
         
            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(sql_delete_activist, con);




            try
            {
                con.Open(); 
                cmd.Parameters.AddWithValue("@SelectedID", dataGridView1.CurrentRow.Cells[0].Value);   
                cmd.ExecuteNonQuery();


                MessageBox.Show("Operação realizada com sucesso!");

                dataGridView1.DataSource = GetActivistis();
                dataGridView1.Refresh();

            }
            catch (Exception x)
            {

                MessageBox.Show("Falha: " + x.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
