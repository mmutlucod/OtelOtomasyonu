using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;



namespace Otel_ProjeOdev.Pages.Lokasyon
{

    public class DuzenleModel : PageModel
    {
        public LokasyonBilgi lokasyonBilgi = new LokasyonBilgi();
        public List<ptSelectBilgi> ptList = new List<ptSelectBilgi>();
        public List<TsSelectBilgi> tsList = new List<TsSelectBilgi>();
        public List<PstSelectBilgi> pstList = new List<PstSelectBilgi>();
        public List<LokasyonBilgi> yntList = new List<LokasyonBilgi>();

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
                    String sql = "Select * from Lokasyon where LokasyonPostaKodu=@LokasyonPostaKodu";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                            command.Parameters.AddWithValue("@LokasyonPostaKodu", id);
                            using (SqlDataReader dr = command.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    lokasyonBilgi.LokasyonPostaKodu = "" + dr.GetInt32(0);
                                    lokasyonBilgi.LokasyonSehir = dr.GetString(1);
                                    lokasyonBilgi.LokasyonCadde = "" + dr.GetString(2);
                                    lokasyonBilgi.LokasyonUzaklik = "" + dr.GetString(3);
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
            lokasyonBilgi.LokasyonPostaKodu = Request.Form["LokasyonPostaKodu"];
            lokasyonBilgi.LokasyonSehir = Request.Form["LokasyonSehir"];
            lokasyonBilgi.LokasyonCadde = Request.Form["LokasyonCadde"];
            lokasyonBilgi.LokasyonUzaklik = Request.Form["LokasyonUzaklik"];

            if (lokasyonBilgi.LokasyonPostaKodu.Length == 0 || lokasyonBilgi.LokasyonSehir.Length == 0 ||
                lokasyonBilgi.LokasyonCadde.Length == 0 || lokasyonBilgi.LokasyonUzaklik.Length == 0 )
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
                    String sql = "UPDATE Lokasyon Set " +
                        "LokasyonPostaKodu=@LokasyonPostaKodu, LokasyonSehir=@LokasyonSehir," +
                        " LokasyonCadde=@LokasyonCadde, LokasyonUzaklik=@LokasyonUzaklik " +
                        " where LokasyonPostaKodu = @LokasyonPostaKodu";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@LokasyonPostaKodu", lokasyonBilgi.LokasyonPostaKodu);
                        command.Parameters.AddWithValue("@LokasyonSehir", lokasyonBilgi.LokasyonSehir);
                        command.Parameters.AddWithValue("@LokasyonCadde", lokasyonBilgi.LokasyonCadde);
                        command.Parameters.AddWithValue("@LokasyonUzaklik", lokasyonBilgi.LokasyonUzaklik);
                        


                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Lokasyon");

        }

    }
}
