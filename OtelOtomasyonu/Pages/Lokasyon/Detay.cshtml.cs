using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Otel_ProjeOdev.Pages.Clients;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Lokasyon
{
    public class DetayModel : PageModel
    {
        public List<LokasyonBilgi> lokasyonListe = new List<LokasyonBilgi>();
        public void OnGet()
        {
            String id = Request.Query["id"];

            String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "Select * from Lokasyon where LokasyonPostaKodu = @LokasyonPostaKodu ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@LokasyonPostaKodu", id);
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            LokasyonBilgi lb = new LokasyonBilgi();
                            lb.LokasyonPostaKodu = dr[0].ToString();
                            lb.LokasyonSehir = dr[1].ToString();
                            lb.LokasyonCadde = dr[2].ToString();
                            lb.LokasyonUzaklik = dr[3].ToString();

                            lokasyonListe.Add(lb);
                        }
                    }


                }
            }

        }
    }
}
