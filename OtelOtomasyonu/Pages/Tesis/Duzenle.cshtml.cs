using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;



namespace Otel_ProjeOdev.Pages.Tesis
{

    public class DuzenleModel : PageModel
    {
        public TesisBilgi tesisBilgi = new TesisBilgi();

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
                    String sql = "Select * from Tesis where TesisKodu=@TesisKodu";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                            command.Parameters.AddWithValue("@TesisKodu", id);
                            using (SqlDataReader dr = command.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    tesisBilgi.TesisKodu = "" + dr.GetInt32(0);
                                    tesisBilgi.TesisSporSalonu = dr.GetString(1);
                                    tesisBilgi.TesisRestoran = dr.GetString(2);
                                    tesisBilgi.TesisKafe = dr.GetString(3);
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
            tesisBilgi.TesisKodu = Request.Form["TesisKodu"];
            tesisBilgi.TesisSporSalonu = Request.Form["TesisSporSalonu"];
            tesisBilgi.TesisRestoran = Request.Form["TesisRestoran"];
            tesisBilgi.TesisKafe = Request.Form["TesisKafe"];

            if (tesisBilgi.TesisKodu.Length == 0 || tesisBilgi.TesisSporSalonu.Length == 0 ||
                tesisBilgi.TesisRestoran.Length == 0 || tesisBilgi.TesisKafe.Length == 0)
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
                    String sql = "UPDATE Tesis Set " +
                        "TesisKodu=@TesisKodu, TesisSporSalonu=@TesisSporSalonu," +
                        " TesisRestoran=@TesisRestoran, TesisKafe=@TesisKafe where TesisKodu = @TesisKodu";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TesisKodu", tesisBilgi.TesisKodu);
                        command.Parameters.AddWithValue("@TesisSporSalonu", tesisBilgi.TesisSporSalonu);
                        command.Parameters.AddWithValue("@TesisRestoran", tesisBilgi.TesisRestoran);
                        command.Parameters.AddWithValue("@TesisKafe", tesisBilgi.TesisKafe);
                        


                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Tesis");

        }

    }
}
