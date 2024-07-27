using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;



namespace Otel_ProjeOdev.Pages.Yonetici
{

    public class DuzenleModel : PageModel
    {
        public YoneticiBilgi YoneticiBilgi = new YoneticiBilgi();

        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * from Yonetici where YoneticiID=@YoneticiID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                            command.Parameters.AddWithValue("@YoneticiID", id);
                            using (SqlDataReader dr = command.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    YoneticiBilgi.YoneticiID = "" + dr.GetInt32(0);
                                    YoneticiBilgi.YoneticiTCKN = dr.GetInt64(1).ToString();
                                    YoneticiBilgi.YoneticiAdiSoyadi = dr.GetString(2);
                                    YoneticiBilgi.YoneticiIseBaslamaTarihi = dr.GetString(3);
                                }
                            }
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            YoneticiBilgi.YoneticiID = Request.Form["YoneticiID"];
            YoneticiBilgi.YoneticiTCKN = Request.Form["YoneticiTCKN"];
            YoneticiBilgi.YoneticiAdiSoyadi = Request.Form["YoneticiAdiSoyadi"];
            YoneticiBilgi.YoneticiIseBaslamaTarihi = Request.Form["YoneticiIseBaslamaTarihi"];

            if (YoneticiBilgi.YoneticiTCKN.Length == 0 || YoneticiBilgi.YoneticiAdiSoyadi.Length == 0 ||
                YoneticiBilgi.YoneticiIseBaslamaTarihi.Length == 0 )
            {
                errorMessage = "Tüm Alanlar doldurulmalýdýr.";
                return;
            }

            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Yonetici Set " +
                        "YoneticiTCKN=@YoneticiTCKN, YoneticiAdiSoyadi=@YoneticiAdiSoyadi," +
                        " YoneticiIseBaslamaTarihi=@YoneticiIseBaslamaTarihi where YoneticiID = @YoneticiID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@YoneticiID", YoneticiBilgi.YoneticiID);
                        command.Parameters.AddWithValue("@YoneticiTCKN", YoneticiBilgi.YoneticiTCKN);
                        command.Parameters.AddWithValue("@YoneticiAdiSoyadi", YoneticiBilgi.YoneticiAdiSoyadi);
                        command.Parameters.AddWithValue("@YoneticiIseBaslamaTarihi", YoneticiBilgi.YoneticiIseBaslamaTarihi);
                        


                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Yonetici");

        }

    }
}
