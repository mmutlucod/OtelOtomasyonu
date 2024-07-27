using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Otel_ProjeOdev.Pages.Clients;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Oda
{
    public class DetayModel : PageModel
    {
        public List<OdaBilgi> odaListe = new List<OdaBilgi>();
        public void OnGet()
        {
            String id = Request.Query["id"];

            String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "select OdaID,OdaNo,OdaTuru,OdaFiyat,OdaDolulukOrani,OtelAdi,OdaOtelID" +
                        " from Oda left join Otel on OtelID = OdaOtelID where OdaID = @OdaID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OdaID", id);
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            OdaBilgi ob = new OdaBilgi();
                            ob.OdaID = dr[0].ToString();
                            ob.OdaNo = dr[1].ToString();
                            ob.OdaTuru = dr[2].ToString();
                            ob.OdaFiyat = dr[3].ToString();
                            ob.OdaDolulukOrani = dr[4].ToString();
                            ob.OdaOtelAdi = dr[5].ToString();

                            odaListe.Add(ob);
                        }
                    }


                }
            }

        }
    }
}
