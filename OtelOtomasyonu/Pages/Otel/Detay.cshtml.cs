using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Otel_ProjeOdev.Pages.Clients;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Otel
{
    public class DetayModel : PageModel
    {
        public List<OtelBilgi> otelListe = new List<OtelBilgi>();
        public void OnGet()
        {
            String id = Request.Query["id"];

            String connectionString = "Data Source=DESKTOP-9M6PTMV\\SQLEXPRESS;Initial Catalog=OTELDB0;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "Select OtelID,OtelAdi,OtelSiralamasi,OtelPostaKodu,OtelPolitikaKodu,OtelTesisKodu,YoneticiAdiSoyadi,OtelYoneticiID,YoneticiTCKN from Otel " +
                         "inner join Yonetici on OtelYoneticiID = YoneticiID where OtelID = @OtelID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OtelID", id);
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            OtelBilgi ob = new OtelBilgi();
                            ob.OtelID = dr[0].ToString();
                            ob.OtelAdi = dr[1].ToString();
                            ob.OtelSiralamasi = dr[2].ToString();
                            ob.OtelPostaKodu = dr[3].ToString();
                            ob.OtelPolitikaKodu = dr[4].ToString();
                            ob.OtelTesisKodu = dr[5].ToString();
                            ob.OtelYoneticiAdiSoyadi = dr[6].ToString();
                            ob.OtelYoneticiID = dr[7].ToString();
                            ob.OtelYoneticiTCKN = dr[8].ToString();

                            otelListe.Add(ob);
                        }
                    }


                }
            }

        }
    }
}
