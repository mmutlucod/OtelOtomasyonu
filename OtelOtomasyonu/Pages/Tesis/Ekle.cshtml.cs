using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Otel_ProjeOdev.Pages.Tesis;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Tesis
{
    public class EkleModel : PageModel
    {
        public TesisBilgi tesisBilgi = new TesisBilgi();



        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            
        }
        public void OnPost()
        {
            String id = Request.Query["id"];
            tesisBilgi.TesisKodu = Request.Form["TesisKodu"];
            tesisBilgi.TesisSporSalonu = Request.Form["TesisSporSalonu"];
            tesisBilgi.TesisRestoran = Request.Form["TesisRestoran"];
            tesisBilgi.TesisKafe = Request.Form["TesisKafe"];

            if (tesisBilgi.TesisKodu.Length == 0 || tesisBilgi.TesisSporSalonu.Length == 0 ||
                tesisBilgi.TesisRestoran.Length == 0 || tesisBilgi.TesisKafe.Length == 0 )
            {
                errorMessage = "T�m Alanlar� Doldurun.";
                return;
            }

            //Veritaban�na eklendi�inde kay�t i�lemi

            try
            {
                String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "insert into Tesis "
                        + "(TesisKodu,TesisSporSalonu,TesisRestoran,TesisKafe) values " +
                        "(@TesisKodu, @TesisSporSalonu, @TesisRestoran,@TesisKafe);";

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

            tesisBilgi.TesisKodu = "";
            tesisBilgi.TesisSporSalonu = "";
            tesisBilgi.TesisRestoran = "";
            tesisBilgi.TesisKafe = "";

            successMessage = "Yeni Kay�t Eklendi";

            Response.Redirect("/Tesis");


        }
    }
}
