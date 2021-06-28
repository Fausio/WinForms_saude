using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace WinForms_saude_modern_ui
{
    public partial class reports : Form
    {

		string cctring = "Data Source=LAPTOP-DU4GOUVH;Initial Catalog=win_form_saude;Integrated Security=True;Pooling=False";

		string sql_ben_with_date = @"SELECT 
										* 
							FROM		(

											SELECT  
													ben.Name [Nome do beneficiario],
													Genero = CASE WHEN ben.gender ='M' THEN 'Masculino' WHEN ben.gender ='F' THEN 'Feminino' ELSE '' END ,
													CONVERT(varchar,ben.DataOfBirth, 105)  AS [Data de nascimento],
													Idade =  FLOOR(DATEDIFF(day, CAST(ben.DataOfBirth As Date),GETDATE())/365.25),
													CASE WHEN hiv.[Description] IS NULL THEN '' ELSE hiv.[Description] END AS [Estado de HIV] ,
													CASE WHEN hh.hiv_data IS NULL THEN '' ELSE CONVERT(varchar,hh.hiv_data, 105) END AS [Data do estado de HIV] ,
													at.Name AS [Nome do Ativista]

											FROM		[win_form_saude].[dbo].[Beneficiary]  AS ben
											LEFT JOIN   [win_form_saude].[dbo].[Activist] as at ON AT.Id = BEN.ActivistId 
											LEFT JOIN   [win_form_saude].[dbo].[HIVstatusHistory] AS hh ON hh.beneficiary_id = ben.Id  
														AND hh.Id IN (
																		   SELECT Id FROM (
 
																								SELECT  row_number() OVER (PARTITION BY beneficiary_id ORDER BY hiv_data DESC ) AS _Row,  
																										*
																								FROM  [win_form_saude].[dbo].[HIVstatusHistory]

																			) AS lastHiv 
																			WHERE lastHiv._Row = 1
																		)
											LEFT JOIN   [win_form_saude].[dbo].[hiv]  ON hiv.Id = hh.hiv_id

										) AS benToreport

						WHERE [Data de nascimento] BETWEEN @startdate AND @enddate";

        string sql_abens = @"SELECT 
										* 
							FROM		(

											SELECT  
													ben.Name [Nome do beneficiario],
													Genero = CASE WHEN ben.gender ='M' THEN 'Masculino' WHEN ben.gender ='F' THEN 'Feminino' ELSE '' END ,
													CONVERT(varchar,ben.DataOfBirth, 105)  AS [Data de nascimento],
													Idade =  FLOOR(DATEDIFF(day, CAST(ben.DataOfBirth As Date),GETDATE())/365.25),
													CASE WHEN hiv.[Description] IS NULL THEN '' ELSE hiv.[Description] END AS [Estado de HIV] ,
													CASE WHEN hh.hiv_data IS NULL THEN '' ELSE CONVERT(varchar,hh.hiv_data, 105) END AS [Data do estado de HIV] ,
													at.Name AS [Nome do Ativista]

											FROM		[win_form_saude].[dbo].[Beneficiary]  AS ben
											LEFT JOIN   [win_form_saude].[dbo].[Activist] as at ON AT.Id = BEN.ActivistId 
											LEFT JOIN   [win_form_saude].[dbo].[HIVstatusHistory] AS hh ON hh.beneficiary_id = ben.Id  
														AND hh.Id IN (
																		   SELECT Id FROM (
 
																								SELECT  row_number() OVER (PARTITION BY beneficiary_id ORDER BY hiv_data DESC ) AS _Row,  
																										*
																								FROM  [win_form_saude].[dbo].[HIVstatusHistory]

																			) AS lastHiv 
																			WHERE lastHiv._Row = 1
																		)
											LEFT JOIN   [win_form_saude].[dbo].[hiv]  ON hiv.Id = hh.hiv_id

										) AS benToreport
							 ";

        string sql_adults = @"SELECT 
										* 
							FROM		(

											SELECT  
													ben.Name [Nome do beneficiario],
													Genero = CASE WHEN ben.gender ='M' THEN 'Masculino' WHEN ben.gender ='F' THEN 'Feminino' ELSE '' END ,
													CONVERT(varchar,ben.DataOfBirth, 105)  AS [Data de nascimento],
													Idade =  FLOOR(DATEDIFF(day, CAST(ben.DataOfBirth As Date),GETDATE())/365.25),
													CASE WHEN hiv.[Description] IS NULL THEN '' ELSE hiv.[Description] END AS [Estado de HIV] ,
													CASE WHEN hh.hiv_data IS NULL THEN '' ELSE CONVERT(varchar,hh.hiv_data, 105) END AS [Data do estado de HIV] ,
													at.Name AS [Nome do Ativista]

											FROM		[win_form_saude].[dbo].[Beneficiary]  AS ben
											LEFT JOIN   [win_form_saude].[dbo].[Activist] as at ON AT.Id = BEN.ActivistId 
											LEFT JOIN   [win_form_saude].[dbo].[HIVstatusHistory] AS hh ON hh.beneficiary_id = ben.Id  
														AND hh.Id IN (
																		   SELECT Id FROM (
 
																								SELECT  row_number() OVER (PARTITION BY beneficiary_id ORDER BY hiv_data DESC ) AS _Row,  
																										*
																								FROM  [win_form_saude].[dbo].[HIVstatusHistory]

																			) AS lastHiv 
																			WHERE lastHiv._Row = 1
																		)
											LEFT JOIN   [win_form_saude].[dbo].[hiv]  ON hiv.Id = hh.hiv_id

										) AS benToreport
							WHERE Idade < 18";

        string sql_kids = @"SELECT 
										* 
							FROM		(

											SELECT  
													ben.Name [Nome do beneficiario],
													Genero = CASE WHEN ben.gender ='M' THEN 'Masculino' WHEN ben.gender ='F' THEN 'Feminino' ELSE '' END ,
													CONVERT(varchar,ben.DataOfBirth, 105)  AS [Data de nascimento],
													Idade =  FLOOR(DATEDIFF(day, CAST(ben.DataOfBirth As Date),GETDATE())/365.25),
													CASE WHEN hiv.[Description] IS NULL THEN '' ELSE hiv.[Description] END AS [Estado de HIV] ,
													CASE WHEN hh.hiv_data IS NULL THEN '' ELSE CONVERT(varchar,hh.hiv_data, 105) END AS [Data do estado de HIV] ,
													at.Name AS [Nome do Ativista]

											FROM		[win_form_saude].[dbo].[Beneficiary]  AS ben
											LEFT JOIN   [win_form_saude].[dbo].[Activist] as at ON AT.Id = BEN.ActivistId 
											LEFT JOIN   [win_form_saude].[dbo].[HIVstatusHistory] AS hh ON hh.beneficiary_id = ben.Id  
														AND hh.Id IN (
																		   SELECT Id FROM (
 
																								SELECT  row_number() OVER (PARTITION BY beneficiary_id ORDER BY hiv_data DESC ) AS _Row,  
																										*
																								FROM  [win_form_saude].[dbo].[HIVstatusHistory]

																			) AS lastHiv 
																			WHERE lastHiv._Row = 1
																		)
											LEFT JOIN   [win_form_saude].[dbo].[hiv]  ON hiv.Id = hh.hiv_id

										) AS benToreport
							WHERE Idade > 17";
        public reports()
        {
            InitializeComponent();
        }

        private void reports_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = @"C:\app\Hiv_report.xlsx";

			criateSheet(path, sql_kids);

		}



        private   void criateSheet(string path, string sql)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();

            // sheet
            var workSheet = excel.Workbook.Worksheets.Add("Dados do sistema");

            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            workSheet.Row(2).Height = 20;
            workSheet.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(2).Style.Font.Bold = true;

			workSheet.Row(1).Height = 20;
			workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
			workSheet.Row(1).Style.Font.Bold = true;

			workSheet.Cells[1, 1].Value = "Emitido em: "+ DateTime.Now.Date.ToShortDateString();

			workSheet.Cells[2, 1].Value = "Nome do beneficiario";
            workSheet.Cells[2, 2].Value = "Genero";
            workSheet.Cells[2, 3].Value = "Data de nascimento";
            workSheet.Cells[2, 4].Value = "Idade";
            workSheet.Cells[2, 5].Value = "Estado de HIV";
            workSheet.Cells[2, 6].Value = "Data do estado de HIV";
            workSheet.Cells[2, 7].Value = "Nome do Ativista";

			int index = 3;


			#region pupulate

			DataTable dataTable = new DataTable();




			SqlConnection con = new SqlConnection(cctring);
			SqlCommand cmd = new SqlCommand(sql, con);
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


			#endregion



			//foreach (var item in collection)
			//{

			//}

			workSheet.Column(1).AutoFit();
			workSheet.Column(2).AutoFit();
			workSheet.Column(3).AutoFit();
			workSheet.Column(4).AutoFit();
			workSheet.Column(5).AutoFit();
			workSheet.Column(6).AutoFit();
			workSheet.Column(7).AutoFit();

            if (File.Exists(path))
            {
				File.Delete(path);
            }


			// create in HDD
			FileStream obejectStrm = File.Create(path);
			obejectStrm.Close();

			// write in the patch
			File.WriteAllBytes(path, excel.GetAsByteArray());

			//clore the file
			excel.Dispose();

			MessageBox.Show($"Planilha criada com sucesso em: {path}\n");

		}

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
