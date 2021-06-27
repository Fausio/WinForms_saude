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
    public partial class BenList : Form
    {

        string cctring = "Data Source=LAPTOP-DU4GOUVH;Initial Catalog=win_form_saude;Integrated Security=True;Pooling=False";
        string sql_datagrid = @"SELECT  BEN.Id [Id do beneficiario],
		                                ben.Name [Nome do beneficiario],
		                                Genero = CASE WHEN ben.gender ='M' THEN 'Masculino' WHEN ben.gender ='F' THEN 'Feminino' ELSE '' END ,
		                                ben.DataOfBirth AS [Data de nascimento],
		                                Idade =  FLOOR(DATEDIFF(day, CAST(ben.DataOfBirth As Date),GETDATE())/365.25),
		                                at.Name AS [Nome do Ativista]

                                FROM		[win_form_saude].[dbo].[Beneficiary]  AS ben
                                LEFT JOIN   [win_form_saude].[dbo].Activist as at ON AT.Id = BEN.ActivistId ";

        string sql_delete = "DELETE [win_form_saude].[dbo].[Beneficiary]  WHERE ID = @SelectedID";

        public BenList()
        {
            InitializeComponent(); 
        }

        private DataTable Getbens()
        {
            DataTable dataTable = new DataTable();




            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(sql_datagrid, con);
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

        private void BenList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Getbens();
        }

        private void btn_insert_ativist_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(sql_delete, con);




            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SelectedID", dataGridView1.CurrentRow.Cells[0].Value);
                cmd.ExecuteNonQuery();


                MessageBox.Show("Operação realizada com sucesso!");

                dataGridView1.DataSource = Getbens();
                dataGridView1.Refresh();

            }
            catch (Exception x)
            {

                MessageBox.Show("Falha: " + x.ToString());
            }
        }
    }
}
