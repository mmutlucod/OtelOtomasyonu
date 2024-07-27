using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Lokasyon
{
    public class EkleModel : PageModel
    {
        public LokasyonBilgi lokasyonBilgi = new LokasyonBilgi();


        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            String id = Request.Query["id"];
            lokasyonBilgi.LokasyonPostaKodu = Request.Form["LokasyonPostaKodu"];
            lokasyonBilgi.LokasyonSehir = Request.Form["LokasyonSehir"];
            lokasyonBilgi.LokasyonCadde = Request.Form["LokasyonCadde"];
            lokasyonBilgi.LokasyonUzaklik = Request.Form["LokasyonUzaklik"];

            if (lokasyonBilgi.LokasyonPostaKodu.Length == 0 || lokasyonBilgi.LokasyonSehir.Length == 0 ||
                lokasyonBilgi.LokasyonCadde.Length == 0 || lokasyonBilgi.LokasyonUzaklik.Length == 0 )
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
                    String sql = "insert into Lokasyon "
                        + "(LokasyonPostaKodu,LokasyonSehir,LokasyonCadde,LokasyonUzaklik) values "
                        + "(@LokasyonPostaKodu, @LokasyonSehir, @LokasyonCadde, @LokasyonUzaklik);";

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

            lokasyonBilgi.LokasyonPostaKodu = "";
            lokasyonBilgi.LokasyonSehir = "";
            lokasyonBilgi.LokasyonCadde = "";
            lokasyonBilgi.LokasyonUzaklik = "";

            successMessage = "Yeni Kayýt Eklendi";

            Response.Redirect("/Lokasyon");


        }
    }

}

