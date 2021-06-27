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
    public partial class createActivistForm : Form
    {
        string cctring = "Data Source=LAPTOP-DU4GOUVH;Initial Catalog=win_form_saude;Integrated Security=True;Pooling=False";
        string ativist_type = "SELECT *   FROM [win_form_saude].[dbo].[ActivistType]";
        string Supervisor_combo = "SELECT *  FROM [win_form_saude].[dbo].[Activist]   WHERE [ActivistTypeId] = (SELECT Id + 1 FROM [win_form_saude].[dbo].[ActivistType] WHERE [Description] =  @ActivisTypeId )";


        string InsertSQL = @"
 
                                        INSERT INTO  [win_form_saude].[dbo].[Activist] ([Name],[ActivistTypeId] ,[SuperiorId])  
                                        SELECT
                                          @Name,
                                        (SELECT Id FROM [win_form_saude].[dbo].[ActivistType] WHERE [Description] = @Description),
                                        (SELECT Id FROM [win_form_saude].[dbo].[Activist]     WHERE  [Name] = @NameActivist)

                            ";
            

         

        public createActivistForm()
        {
            InitializeComponent();
            FillCombo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void FillCombo()
        {
            

            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(ativist_type, con);
            SqlDataReader myReader;



            try
            {
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    string ActivistTypesDescription = myReader.GetString("description");
                    comboBox1.Items.Add(ActivistTypesDescription);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void createActivistForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(InsertSQL, con);
          

           

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Description", comboBox1.SelectedItem);

                if (comboBox2.SelectedItem != null)
                {
                    cmd.Parameters.AddWithValue("@NameActivist", comboBox2.SelectedItem);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NameActivist", "");
                }
             
                cmd.ExecuteNonQuery();

               
                MessageBox.Show("Operação realizada com sucesso!");
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                this.Refresh();
            }
            catch (Exception x)
            {

                MessageBox.Show("Falha: "+ x.ToString());
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();

            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(Supervisor_combo, con); 
            cmd.Parameters.AddWithValue("@ActivisTypeId", comboBox1.SelectedItem);

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

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            comboBox2.Items.Clear();
            comboBox2.Text = "";
        }
    }
}
