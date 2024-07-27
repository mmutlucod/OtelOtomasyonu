using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;



namespace Otel_ProjeOdev.Pages.Politika
{

    public class DuzenleModel : PageModel
    {
        public PolitikaBilgi politikaBilgi = new PolitikaBilgi();


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
                    String sql = "Select * from Politika where PolitikaKod=@PolitikaKod";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                            command.Parameters.AddWithValue("@PolitikaKod", id);
                            using (SqlDataReader dr = command.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    politikaBilgi.PolitikaKod = "" + dr.GetInt32(0);
                                    politikaBilgi.PolitikaKayitZamani = dr.GetString(1);
                                    politikaBilgi.PolitikaAyrilmaZamani = dr.GetString(2);
                                    politikaBilgi.PolitikaIptalSuresi = "" + dr.GetInt32(3);
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
            politikaBilgi.PolitikaKod = Request.Form["PolitikaKod"];
            politikaBilgi.PolitikaKayitZamani = Request.Form["PolitikaKayitZamani"];
            politikaBilgi.PolitikaAyrilmaZamani = Request.Form["PolitikaAyrilmaZamani"];
            politikaBilgi.PolitikaIptalSuresi = Request.Form["PolitikaIptalSuresi"];
            

            if (politikaBilgi.PolitikaKayitZamani.Length == 0 ||
                politikaBilgi.PolitikaAyrilmaZamani.Length == 0 || politikaBilgi.PolitikaIptalSuresi.Length == 0 )
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
                    String sql = "UPDATE Politika Set " +
                        "PolitikaKayitZamani=@PolitikaKayitZamani," +
                        " PolitikaAyrilmaZamani=@PolitikaAyrilmaZamani, PolitikaIptalSuresi=@PolitikaIptalSuresi" +
                        " where PolitikaKod = @PolitikaKod";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PolitikaKod", politikaBilgi.PolitikaKod);
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

            Response.Redirect("/Politika");

        }

    }
}
