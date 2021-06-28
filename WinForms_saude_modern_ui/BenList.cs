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
        string sql_populate_ben = "SELECT * FROM  [win_form_saude].[dbo].[Beneficiary]  WHERE ID = @SelectedID";
        string sql_get_activist = @"SELECT * FROM   [win_form_saude].[dbo].[Activist] WHERE   [ActivistTypeId] = 1 ";
        string sql_single_partner_name = @"SELECT [name] FROM [win_form_saude].[dbo].[Activist] WHERE id  = @thisID";
        string sql_single_partner_id   = @"SELECT * FROM [win_form_saude].[dbo].[Activist] WHERE [name]  = @thisID";
        string sql_update = @"
                                UPDATE [win_form_saude].[dbo].[Beneficiary] 
                                SET	   Name = @ThisName,
	                                   ActivistId =  @thisActivistId,
	                                   DataOfBirth = @DataOfBirth,
	                                   Gender = @Gender
                                WHERE Id = @thisId
                             ";

         
        int benID = 0;
        public BenList()
        {

            InitializeComponent();
        }

        public int activisId(string Name)
        {
            int id_name = 0;

            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(sql_single_partner_id, con);
            cmd.Parameters.AddWithValue("@thisID", Name);
            SqlDataReader myReader;

            try
            {
                con.Open();
                myReader = cmd.ExecuteReader();
 
                    while (myReader.Read())
                    {
                        id_name = myReader.GetInt32("ID");
                    }
                 

            }
            catch (Exception)
            {

                throw;
            }

            return id_name;
        }
        public string activistName(int activistId)
        {
            string name = "";

            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(sql_single_partner_name, con);
            cmd.Parameters.AddWithValue("@thisID", activistId);
            SqlDataReader myReader;



            try
            {
                con.Open();
                myReader = cmd.ExecuteReader();

                if (activistId != null)
                {
                    while (myReader.Read())
                    {

                        name = myReader.GetString("Name");
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return name;
        }

        public void FillCombo()
        {


            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(sql_get_activist, con);
            SqlDataReader myReader;



            try
            {
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string ActivistTypesDescription = myReader.GetString("Name");
                    comboBox2.Items.Add(ActivistTypesDescription);
                }
            }
            catch (Exception)
            {

                throw;
            }
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

        private Beneficiary Getben(string Id)
        {
            Beneficiary ben = new Beneficiary();

            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(sql_populate_ben, con);
            cmd.Parameters.AddWithValue("@SelectedID", Id);

            SqlDataReader myReader;



            try
            {
                con.Open();
                myReader = cmd.ExecuteReader();
                if (Id != null)
                {
                    while (myReader.Read())
                    {

                        ben.Id = myReader.GetInt32("id");
                        ben.Name = myReader.GetString("Name");
                        ben.DateOfBirth = myReader.GetDateTime("DataOfBirth");
                        ben.Gender = myReader.GetString("Gender");
                        ben.ActivistId = myReader.GetInt32("ActivistId");

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return ben;
        }


        private void BenList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Getbens();

            DateTime dt = DateTime.Now;
            textBox3.Text = dt.ToString("yyyy/MM/dd");

            FillCombo();
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
         


        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(sql_update, con);
            List<RadioButton> gender = new List<RadioButton>()
                {
                    radioButton1,
                    radioButton2
                };


            cmd.Parameters.AddWithValue("@ThisName", textBox1.Text);
            cmd.Parameters.AddWithValue("@thisActivistId", activisId(comboBox2.SelectedItem.ToString()));
            cmd.Parameters.AddWithValue("@DataOfBirth", textBox2.Text);
            cmd.Parameters.AddWithValue("@Gender", gender.Where(x => x.Checked).Select(x => x.Text).FirstOrDefault());
            cmd.Parameters.AddWithValue("@thisId", benID);
             

            try
            {
                con.Open();

                

                cmd.ExecuteNonQuery();


                MessageBox.Show("Operação realizada com sucesso!");
                dataGridView1.DataSource = Getbens();
            }
            catch (Exception x)
            {

                MessageBox.Show("Falha: " + x.ToString());
            }

        }




        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var BenId = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            Beneficiary selectedBen = Getben(BenId);

            benID = selectedBen.Id;

            textBox1.Text = selectedBen.Name;
            textBox2.Text = (selectedBen.DateOfBirth).ToString("yyyy/MM/dd"); ; 
            comboBox2.Text = activistName(selectedBen.ActivistId);

            if (selectedBen.Gender == "F")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var BenId = dataGridView1.CurrentRow.Cells[0].Value;
        }
    }
}
