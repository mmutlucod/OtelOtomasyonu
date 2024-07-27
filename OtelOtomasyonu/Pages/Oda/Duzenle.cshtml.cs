using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace Otel_ProjeOdev.Pages.Oda
{

    public class DuzenleModel : PageModel
    {
        public OdaBilgi odaBilgi = new OdaBilgi();

        public List<OtelSelect> otelSelectList = new List<OtelSelect>();

        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select OdaID,OdaNo,OdaTuru,OdaFiyat,OdaDolulukOrani,OtelAdi,OdaOtelID" +
                        " from Oda left join Otel on OtelID = OdaOtelID where OdaID=@OdaID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                            command.Parameters.AddWithValue("@OdaID", id);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {

                                odaBilgi.OdaID = "" + reader.GetInt32(0);
                                odaBilgi.OdaNo = reader.GetString(1);
                                odaBilgi.OdaTuru = reader[2].ToString();
                                odaBilgi.OdaFiyat = reader.GetString(3);
                                odaBilgi.OdaDolulukOrani = reader.GetInt32(4).ToString();
                                odaBilgi.OdaOtelAdi = reader.GetString(5);
                                odaBilgi.OdaOtelID = reader.GetInt32(6).ToString();
                                }
                            }
                    }
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
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            odaBilgi.OdaID = Request.Form["OdaID"];
            odaBilgi.OdaNo = Request.Form["OdaNo"];
            odaBilgi.OdaTuru = Request.Form["OdaTuru"];
            odaBilgi.OdaFiyat = Request.Form["OdaFiyat"];
            odaBilgi.OdaDolulukOrani = Request.Form["OdaDolulukOrani"];
            odaBilgi.OdaOtelID = Request.Form["OdaOtelID"];

            if (odaBilgi.OdaNo.Length == 0 || odaBilgi.OdaTuru.Length == 0 ||
                odaBilgi.OdaFiyat.Length == 0 || odaBilgi.OdaDolulukOrani.Length == 0
                || odaBilgi.OdaOtelID.Length == 0)
            {
                errorMessage = "Tüm Alanlar doldurulmalýdýr.";
                return;
            }

            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Oda Set OdaNo=@OdaNo, OdaTuru=@OdaTuru, OdaFiyat=@OdaFiyat," +
                        " OdaDolulukOrani=@OdaDolulukOrani, OdaOtelID = @OdaOtelID where OdaID = @OdaID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@OdaID", odaBilgi.OdaID);
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

            Response.Redirect("/Oda");

        }

    }
}
