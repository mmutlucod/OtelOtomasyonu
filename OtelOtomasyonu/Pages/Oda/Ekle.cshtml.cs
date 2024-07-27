using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Oda
{
    public class EkleModel : PageModel
    {
        public OdaBilgi odaBilgi = new OdaBilgi();
        public List<OtelSelect> otelSelectList = new List<OtelSelect>();


        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql1 = "Select OtelID,OtelAdi from Otel";
                using (SqlCommand command = new SqlCommand(sql1, connection))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            OtelSelect os = new OtelSelect();
                            os.OtelID = dr[0].ToString();
                            os.OtelAdi = dr[1].ToString();

                            otelSelectList.Add(os);
                        }
                    }
                }
            }
        }
        public void OnPost()
        {
            String id = Request.Query["id"];
            odaBilgi.OdaNo = Request.Form["OdaNo"];
            odaBilgi.OdaTuru = Request.Form["OdaTuru"];
            odaBilgi.OdaFiyat = Request.Form["OdaFiyat"];
            odaBilgi.OdaDolulukOrani = Request.Form["OdaDolulukOrani"];
            odaBilgi.OdaOtelID = Request.Form["OdaOtelID"];

            if (odaBilgi.OdaNo.Length == 0 || odaBilgi.OdaTuru.Length == 0 ||
                odaBilgi.OdaFiyat.Length == 0 || odaBilgi.OdaDolulukOrani.Length == 0)
            {
                errorMessage = "Tüm Alanlarý Doldurun.";
                return;
            }

            //Veritabanýna eklendiðinde kayýt iþlemi

            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "insert into Oda "
                        + "(OdaNo,OdaTuru,OdaFiyat,OdaDolulukOrani,OdaOtelID) values "
                        + "(@OdaNo, @OdaTuru, @OdaFiyat,@OdaDolulukOrani,@OdaOtelID);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@OdaNo", odaBilgi.OdaNo);
                        command.Parameters.AddWithValue("@OdaTuru", odaBilgi.OdaTuru);
                        command.Parameters.AddWithValue("@OdaFiyat", odaBilgi.OdaFiyat);
                        command.Parameters.AddWithValue("@OdaDolulukOrani", odaBilgi.OdaDolulukOrani);
                        command.Parameters.AddWithValue("@OdaOtelID", odaBilgi.OdaOtelID);

                        command.ExecuteNonQuery();

                    }





                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            odaBilgi.OdaNo = "";
            odaBilgi.OdaTuru = "";
            odaBilgi.OdaFiyat = "";
            odaBilgi.OdaFiyat = "";

            successMessage = "Yeni Kayýt Eklendi";

            Response.Redirect("/Oda");
        }
    }
}
