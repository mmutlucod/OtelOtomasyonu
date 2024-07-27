using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Otel_ProjeOdev.Pages.Clients;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Politika
{
    public class DetayModel : PageModel
    {
        public List<PolitikaBilgi> politikaListe = new List<PolitikaBilgi>();
        public void OnGet()
        {
            String id = Request.Query["id"];

            String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "Select * from Politika where PolitikaKod = @PolitikaKod";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@PolitikaKod", id);
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            PolitikaBilgi pb = new PolitikaBilgi();
                            pb.PolitikaKod = dr[0].ToString();
                            pb.PolitikaKayitZamani = dr[1].ToString();
                            pb.PolitikaAyrilmaZamani = dr[2].ToString();
                            pb.PolitikaIptalSuresi = dr[3].ToString();

                            politikaListe.Add(pb);
                        }
                    }


                }
            }

        }
    }
}
