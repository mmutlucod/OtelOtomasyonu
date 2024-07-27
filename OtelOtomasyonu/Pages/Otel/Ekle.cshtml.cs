using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Otel_ProjeOdev.Pages.Clients
{
    public class EkleModel : PageModel
    {
        public OtelBilgi otelBilgi = new OtelBilgi();
        public List<ptSelectBilgi> ptList = new List<ptSelectBilgi>();
        public List<TsSelectBilgi> tsList = new List<TsSelectBilgi>();
        public List<PstSelectBilgi> pstList = new List<PstSelectBilgi>();
        public List<OtelBilgi> yntList = new List<OtelBilgi>();


        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=DESKTOP-9M6PTMV\\SQLEXPRESS;Initial Catalog=OTELDB0;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql1 = "Select PolitikaKod from Politika";
                    using (SqlCommand command = new SqlCommand(sql1, connection))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                ptSelectBilgi ptselect = new ptSelectBilgi();
                                ptselect.PolitikaKod = dr[0].ToString();

                                ptList.Add(ptselect);
                            }
                        }
                    }
                    String sql2 = "Select TesisKodu from Tesis";
                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                TsSelectBilgi tsSelect = new TsSelectBilgi();
                                tsSelect.TesisKodu = dr[0].ToString();

                                tsList.Add(tsSelect);
                            }
                        }
                    }
                    String sql3 = "Select LokasyonPostaKodu,LokasyonSehir from Lokasyon";
                    using (SqlCommand command = new SqlCommand(sql3, connection))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                PstSelectBilgi pstSelect = new PstSelectBilgi();
                                pstSelect.LokasyonPostaKodu = dr[0].ToString();
                                pstSelect.LokasyonSehir = dr[1].ToString();

                                pstList.Add(pstSelect);
                            }
                        }
                    }
                    String sql4 = "Select YoneticiID,YoneticiTCKN,YoneticiAdiSoyadi from Yonetici";
                    using (SqlCommand command = new SqlCommand(sql4, connection))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                OtelBilgi yntSelect = new OtelBilgi();
                                yntSelect.OtelYoneticiID = dr[0].ToString();
                                yntSelect.OtelYoneticiTCKN = dr[1].ToString();
                                yntSelect.OtelYoneticiAdiSoyadi = dr[2].ToString();

                                yntList.Add(yntSelect);
                            }
                        }
                    }





                }
            }
            catch(Exception ex)
            {

            }
        }
        public void OnPost()
        {
            String id = Request.Query["id"];
            otelBilgi.OtelAdi = Request.Form["OtelAdi"];
            otelBilgi.OtelSiralamasi = Request.Form["OtelSiralamasi"];
            otelBilgi.OtelPostaKodu = Request.Form["OtelPostaKodu"];
            otelBilgi.OtelPolitikaKodu = Request.Form["OtelPolitikaKodu"];
            otelBilgi.OtelTesisKodu = Request.Form["OtelTesisKodu"];
            otelBilgi.OtelYoneticiID = Request.Form["OtelYoneticiID"];

            if (otelBilgi.OtelAdi.Length == 0 || otelBilgi.OtelSiralamasi.Length == 0 ||
                otelBilgi.OtelPostaKodu.Length == 0 || otelBilgi.OtelPolitikaKodu.Length == 0 ||
                otelBilgi.OtelTesisKodu.Length == 0)
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
                    String sql = "insert into Otel "
                        + "(OtelAdi,OtelSiralamasi,OtelPostaKodu," +
                        "OtelPolitikaKodu,OtelTesisKodu,OtelYoneticiID) values "
                        + "(@OtelAdi, @OtelSiralamasi, @OtelPostaKodu, " +
                        "@OtelPolitikaKodu, @OtelTesisKodu, @OtelYoneticiID);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@OtelAdi", otelBilgi.OtelAdi);
                        command.Parameters.AddWithValue("@OtelSiralamasi", otelBilgi.OtelSiralamasi);
                        command.Parameters.AddWithValue("@OtelPostaKodu", otelBilgi.OtelPostaKodu);
                        command.Parameters.AddWithValue("@OtelPolitikaKodu", otelBilgi.OtelPolitikaKodu);
                        command.Parameters.AddWithValue("@OtelTesisKodu", otelBilgi.OtelTesisKodu);
                        command.Parameters.AddWithValue("@OtelYoneticiID", otelBilgi.OtelYoneticiID);

                        command.ExecuteNonQuery();

                    }





                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            otelBilgi.OtelAdi = "";
            otelBilgi.OtelSiralamasi = "";
            otelBilgi.OtelPostaKodu = "";
            otelBilgi.OtelPolitikaKodu = "";
            otelBilgi.OtelTesisKodu = "";
            otelBilgi.OtelYoneticiTCKN = "";
            successMessage = "Yeni Kayýt Eklendi";

            Response.Redirect("/Otel");


        }

        public void Selects()
        {
            String connectionString = "Data Source=UMUT;Initial Catalog=OTELDB0;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql4 = "Select YoneticiTCKN,YoneticiAdiSoyadi from Yonetici";
                using (SqlCommand command = new SqlCommand(sql4, connection))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            OtelBilgi yntSelect = new OtelBilgi();
                            yntSelect.OtelYoneticiTCKN = dr[0].ToString();
                            yntSelect.OtelYoneticiAdiSoyadi = dr[1].ToString();

                            yntList.Add(yntSelect);
                        }
                    }
                }




            }
    }

}}
