using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Otel_ProjeOdev.Pages.Clients
{

    public class IndexModel : PageModel
    {
        public List<OtelBilgi> listele = new List<OtelBilgi>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-9M6PTMV\\SQLEXPRESS;Initial Catalog=OTELDB0;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select OtelID,OtelAdi,OtelSiralamasi,OtelPostaKodu,OtelPolitikaKodu,OtelTesisKodu,YoneticiAdiSoyadi,OtelYoneticiID,LokasyonSehir,YoneticiTCKN from Otel " +
                        "left join Yonetici on OtelYoneticiID = YoneticiID " +
                        "left join Lokasyon on LokasyonPostaKodu = OtelPostaKodu";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            OtelBilgi otelBilgi = new OtelBilgi();

                            otelBilgi.OtelID = "" + reader.GetInt32(0);
                            otelBilgi.OtelAdi = reader.GetString(1);
                            otelBilgi.OtelSiralamasi = reader[2].ToString();
                            otelBilgi.OtelPostaKodu = reader[3].ToString();
                            otelBilgi.OtelPolitikaKodu = reader[4].ToString();
                            otelBilgi.OtelTesisKodu = reader[5].ToString();
                            otelBilgi.OtelYoneticiAdiSoyadi = reader[6].ToString();
                            otelBilgi.OtelYoneticiID = reader[7].ToString();
                            otelBilgi.LokasyonSehirOtel = reader[8].ToString();
                            otelBilgi.OtelYoneticiTCKN = reader[9].ToString();

                            listele.Add(otelBilgi);

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

    public class OtelBilgi
    {
        public String OtelID;
        public String OtelAdi;
        public String OtelSiralamasi;
        public String OtelPostaKodu;
        public String OtelPolitikaKodu;
        public String OtelTesisKodu;
        public String OtelYoneticiTCKN;
        public String OtelYoneticiID;
        public String OtelYoneticiAdiSoyadi;
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
