using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Politika
{
    public class EkleModel : PageModel
    {
        public PolitikaBilgi politikaBilgi = new PolitikaBilgi();



        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            String id = Request.Query["id"];
            politikaBilgi.PolitikaKayitZamani = Request.Form["PolitikaKayitZamani"];
            politikaBilgi.PolitikaAyrilmaZamani = Request.Form["PolitikaAyrilmaZamani"];
            politikaBilgi.PolitikaIptalSuresi = Request.Form["PolitikaIptalSuresi"];
            

            if (politikaBilgi.PolitikaKayitZamani.Length == 0 || politikaBilgi.PolitikaAyrilmaZamani.Length == 0 || politikaBilgi.PolitikaIptalSuresi.Length == 0)
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
                    String sql = "insert into Politika "
                        + "(PolitikaKayitZamani,PolitikaAyrilmaZamani,PolitikaIptalSuresi) values "
                        + "(@PolitikaKayitZamani, @PolitikaAyrilmaZamani, @PolitikaIptalSuresi);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PolitikaKayitZamani", politikaBilgi.PolitikaKayitZamani);
                        command.Parameters.AddWithValue("@PolitikaAyrilmaZamani", politikaBilgi.PolitikaAyrilmaZamani);
                        command.Parameters.AddWithValue("@PolitikaIptalSuresi", politikaBilgi.PolitikaIptalSuresi);

                        command.ExecuteNonQuery();

                    }





                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            politikaBilgi.PolitikaKayitZamani = "";
            politikaBilgi.PolitikaAyrilmaZamani = "";
            politikaBilgi.PolitikaIptalSuresi = "";

            successMessage = "Yeni Kayýt Eklendi";

            Response.Redirect("/Politika");


        }


    }

}
