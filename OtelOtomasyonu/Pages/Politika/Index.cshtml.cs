using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Otel_ProjeOdev.Pages.Politika
{

    public class IndexModel : PageModel
    {
        public List<PolitikaBilgi> listele = new List<PolitikaBilgi>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * from Politika ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            PolitikaBilgi politikaBilgi = new PolitikaBilgi();

                            politikaBilgi.PolitikaKod = "" + reader.GetInt32(0);
                            politikaBilgi.PolitikaKayitZamani = reader[1].ToString();
                            politikaBilgi.PolitikaAyrilmaZamani = reader[2].ToString();
                            politikaBilgi.PolitikaIptalSuresi = reader[3].ToString();

                            listele.Add(politikaBilgi);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }
    }

    public class PolitikaBilgi
    {
        public String PolitikaKod;
        public String PolitikaKayitZamani;
        public String PolitikaAyrilmaZamani;
        public String PolitikaIptalSuresi;
    }
}
