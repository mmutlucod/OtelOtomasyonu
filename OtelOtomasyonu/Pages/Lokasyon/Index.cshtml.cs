using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Otel_ProjeOdev.Pages.Lokasyon
{

    public class IndexModel : PageModel
    {
        public List<LokasyonBilgi> listele = new List<LokasyonBilgi>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select LokasyonPostaKodu,LokasyonSehir,LokasyonCadde,LokasyonUzaklik from Lokasyon";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            LokasyonBilgi lokasyonBilgi = new LokasyonBilgi();

                            lokasyonBilgi.LokasyonPostaKodu = "" + reader.GetInt32(0);
                            lokasyonBilgi.LokasyonSehir = reader.GetString(1);
                            lokasyonBilgi.LokasyonCadde = reader.GetString(2);
                            lokasyonBilgi.LokasyonUzaklik = reader.GetString(3);

                            listele.Add(lokasyonBilgi);

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

    public class LokasyonBilgi
    {
        public String LokasyonPostaKodu;
        public String LokasyonSehir;
        public String LokasyonCadde;
        public String LokasyonUzaklik;
        public String LokasyonSehirOtel;
    }
    public class ptSelectBilgi
    {
        public String PolitikaKod;
    }
    public class TsSelectBilgi
    {
        public String TesisKodu;
    }
    public class PstSelectBilgi
    {
        public String LokasyonPostaKodu;
        public String LokasyonSehir;
    }
}
