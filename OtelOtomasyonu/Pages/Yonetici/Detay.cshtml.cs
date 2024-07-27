using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Otel_ProjeOdev.Pages.Clients;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Yonetici
{
    public class DetayModel : PageModel
    {
        public List<YoneticiBilgi> yoneticiListe = new List<YoneticiBilgi>();
        public void OnGet()
        {
            String id = Request.Query["id"];

            String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "Select * from Yonetici where YoneticiID = @YoneticiID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@YoneticiID", id);
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            YoneticiBilgi yb = new YoneticiBilgi();
                            yb.YoneticiID = dr[0].ToString();
                            yb.YoneticiTCKN = dr[1].ToString();
                            yb.YoneticiAdiSoyadi = dr[2].ToString();
                            yb.YoneticiIseBaslamaTarihi = dr[3].ToString();
                            

                            yoneticiListe.Add(yb);
                        }
                    }


                }
            }

        }
    }
}
