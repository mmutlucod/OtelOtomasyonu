using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Otel_ProjeOdev.Pages.Clients;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Tesis
{
    public class DetayModel : PageModel
    {
        public List<TesisBilgi> tesisListe = new List<TesisBilgi>();
        public void OnGet()
        {
            String id = Request.Query["id"];

            String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "Select * from Tesis where TesisKodu = @TesisKodu";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TesisKodu", id);
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            TesisBilgi tb = new TesisBilgi();
                            tb.TesisKodu = dr[0].ToString();
                            tb.TesisSporSalonu = dr[1].ToString();
                            tb.TesisRestoran = dr[2].ToString();
                            tb.TesisKafe = dr[3].ToString();

                            tesisListe.Add(tb);
                        }
                    }


                }
            }

        }
    }
}
