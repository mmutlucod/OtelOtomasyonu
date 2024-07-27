using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;

namespace Otel_ProjeOdev.Pages.Oda
{

    public class IndexModel : PageModel
    {
        public List<OdaBilgi> listele = new List<OdaBilgi>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select OdaID,OdaNo,OdaTuru,OdaFiyat,OdaDolulukOrani,OtelAdi,OdaOtelID" +
                        " from Oda left join Otel on OtelID = OdaOtelID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            OdaBilgi odaBilgi = new OdaBilgi();

                            odaBilgi.OdaID = "" + reader.GetInt32(0);
                            odaBilgi.OdaNo = reader[1].ToString();
                            odaBilgi.OdaTuru = reader[2].ToString();
                            odaBilgi.OdaFiyat = reader[3].ToString();
                            odaBilgi.OdaDolulukOrani = reader[4].ToString();
                            odaBilgi.OdaOtelAdi = reader[5].ToString();
                            odaBilgi.OdaOtelID = reader[6].ToString();

                            listele.Add(odaBilgi);

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

    public class OdaBilgi
    {
        public String OdaID;
        public String OdaNo;
        public String OdaTuru;
        public String OdaFiyat;
        public String OdaDolulukOrani;
        public String OdaOtelAdi;
        public String OdaOtelID;
       
    }
    public class OtelSelect
    {
        public String OtelID;
        public String OtelAdi;
    }
}
