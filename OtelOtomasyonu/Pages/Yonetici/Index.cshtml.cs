using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Otel_ProjeOdev.Pages.Yonetici
{

    public class IndexModel : PageModel
    {
        public List<YoneticiBilgi> listele = new List<YoneticiBilgi>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * from Yonetici";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            YoneticiBilgi ytBilgi = new YoneticiBilgi();

                            ytBilgi.YoneticiID = reader[0].ToString();
                            ytBilgi.YoneticiTCKN = "" + reader.GetInt64(1);
                            ytBilgi.YoneticiAdiSoyadi = reader.GetString(2);
                            ytBilgi.YoneticiIseBaslamaTarihi = reader.GetString(3);

                            listele.Add(ytBilgi);

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

    public class YoneticiBilgi
    {
        public String YoneticiID;
        public String YoneticiTCKN;
        public String YoneticiAdiSoyadi;
        public String YoneticiIseBaslamaTarihi;
    }

}
