using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Otel_ProjeOdev.Pages.Tesis
{

    public class IndexModel : PageModel
    {
        public List<TesisBilgi> listele = new List<TesisBilgi>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * from Tesis" ;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            TesisBilgi tb = new TesisBilgi();

                            tb.TesisKodu = "" + reader.GetInt32(0);
                            tb.TesisSporSalonu = reader.GetString(1);
                            tb.TesisRestoran = reader.GetString(2);
                            tb.TesisKafe = reader.GetString(3);

                            listele.Add(tb);

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

    public class TesisBilgi
    {
        public String TesisKodu;
        public String TesisSporSalonu;
        public String TesisRestoran;
        public String TesisKafe;
    }
}
