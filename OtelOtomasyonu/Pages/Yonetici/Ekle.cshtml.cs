using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Yonetici
{
    public class EkleModel : PageModel
    {
        public YoneticiBilgi yoneticiBilgi = new YoneticiBilgi();


        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            String id = Request.Query["id"];
            yoneticiBilgi.YoneticiTCKN = Request.Form["YoneticiTCKN"];
            yoneticiBilgi.YoneticiAdiSoyadi = Request.Form["YoneticiAdiSoyadi"];
            yoneticiBilgi.YoneticiIseBaslamaTarihi = Request.Form["YoneticiIseBaslamaTarihi"];

            if (yoneticiBilgi.YoneticiTCKN.Length == 0 || yoneticiBilgi.YoneticiAdiSoyadi.Length == 0 ||
                yoneticiBilgi.YoneticiIseBaslamaTarihi.Length == 0 )
            {
                errorMessage = "Tüm Alanlarý Doldurun.";
                return;
            }

            //Veritabanýna eklendiðinde kayýt iþlemi

            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "insert into Yonetici "
                        + "(YoneticiTCKN,YoneticiAdiSoyadi,YoneticiIseBaslamaTarihi) values "
                        + "(@YoneticiTCKN, @YoneticiAdiSoyadi, @YoneticiIseBaslamaTarihi);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@YoneticiTCKN", yoneticiBilgi.YoneticiTCKN);
                        command.Parameters.AddWithValue("@YoneticiAdiSoyadi", yoneticiBilgi.YoneticiAdiSoyadi);
                        command.Parameters.AddWithValue("@YoneticiIseBaslamaTarihi", yoneticiBilgi.YoneticiIseBaslamaTarihi);

                        command.ExecuteNonQuery();

                    }





                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            yoneticiBilgi.YoneticiTCKN = "";
            yoneticiBilgi.YoneticiAdiSoyadi = "";
            yoneticiBilgi.YoneticiIseBaslamaTarihi = "";

            successMessage = "Yeni Kayýt Eklendi";

            Response.Redirect("/Yonetici");


        }
    }
}
