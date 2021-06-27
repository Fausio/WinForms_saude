using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_saude_modern_ui
{
    public partial class benInsert : Form
    {

        string cctring = "Data Source=LAPTOP-DU4GOUVH;Initial Catalog=win_form_saude;Integrated Security=True;Pooling=False";
        string sql_get_activist = @"SELECT * FROM   [win_form_saude].[dbo].[Activist] WHERE   [ActivistTypeId] = 1 ";

        string sql_insert_ben = @"
                                    INSERT INTO  [win_form_saude].[dbo].[Beneficiary]   (Name,ActivistId,DataOfBirth,Gender)
                                    SELECT @theName,(SELECT TOP 1 Id FROM [win_form_saude].[dbo].[Activist] WHERE Name = @theAtivistName ), CONVERT(date,@theDate ), @theGender                                        
";
        string sql_hava_inferior = "SELECT COUNT(ID) FROM [win_form_saude].[dbo].[Activist] WHERE ID = @SelectedID";


        public benInsert()
        {
            InitializeComponent();
        }




        private void benInsert_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            textBox2.Text = dt.ToString("yyyy/MM/dd");
            FillCombo();
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

        private void btn_insert_ativist_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cctring);
            SqlCommand cmd = new SqlCommand(sql_insert_ben, con);
            List<RadioButton> gender = new List<RadioButton>()
                {
                    radioButton1,
                    radioButton2
                };
            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@theName", textBox1.Text);
                cmd.Parameters.AddWithValue("@theAtivistName", comboBox2.SelectedItem);
                cmd.Parameters.AddWithValue("@theDate", textBox2.Text);
                cmd.Parameters.AddWithValue("@theGender",gender.Where(x => x.Checked).Select(x => x.Text).FirstOrDefault());


 

                cmd.ExecuteNonQuery();


                MessageBox.Show("Operação realizada com sucesso!");
                textBox1.Clear(); 
                this.Refresh();
            }
            catch (Exception x)
            {

                MessageBox.Show("Falha: " + x.ToString());
            }
        }
    }
}
