using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;



namespace Otel_ProjeOdev.Pages.Clients
{

    public class DuzenleModel : PageModel
    {
        public OtelBilgi OtelBilgi = new OtelBilgi();
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
                    String sql = "Select * from Otel where OtelID=@OtelID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                            command.Parameters.AddWithValue("@OtelID", id);
                            using (SqlDataReader dr = command.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    OtelBilgi.OtelID = "" + dr.GetInt32(0);
                                    OtelBilgi.OtelAdi = dr.GetString(1);
                                    OtelBilgi.OtelSiralamasi = "" + dr.GetDouble(2);
                                    OtelBilgi.OtelPostaKodu = dr[3].ToString();
                                    OtelBilgi.OtelPolitikaKodu = "" + dr.GetInt32(4);
                                    OtelBilgi.OtelTesisKodu = "" + dr.GetInt32(5);
                                    OtelBilgi.OtelYoneticiID = dr[6].ToString();
                                }
                            }
                    }
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
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            OtelBilgi.OtelID = Request.Form["OtelID"];
            OtelBilgi.OtelAdi = Request.Form["OtelAdi"];
            OtelBilgi.OtelSiralamasi = Request.Form["OtelSiralamasi"];
            OtelBilgi.OtelPostaKodu = Request.Form["OtelPostaKodu"];
            OtelBilgi.OtelPolitikaKodu = Request.Form["OtelPolitikaKodu"];
            OtelBilgi.OtelTesisKodu = Request.Form["OtelTesisKodu"];
            OtelBilgi.OtelYoneticiID = Request.Form["OtelYoneticiID"];

            if (OtelBilgi.OtelID.Length == 0 || OtelBilgi.OtelAdi.Length == 0 ||
                OtelBilgi.OtelSiralamasi.Length == 0 || OtelBilgi.OtelPostaKodu.Length == 0 ||
                OtelBilgi.OtelPolitikaKodu.Length == 0 || OtelBilgi.OtelTesisKodu.Length == 0 ||
                OtelBilgi.OtelYoneticiID.Length == 0)
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
                    String sql = "UPDATE Otel Set " +
                        "OtelAdi=@OtelAdi, OtelSiralamasi=@OtelSiralamasi," +
                        " OtelPostaKodu=@OtelPostaKodu, OtelPolitikaKodu=@OtelPolitikaKodu," +
                        "OtelTesisKodu=@OtelTesisKodu,OtelYoneticiID=@OtelYoneticiID" +
                        " where OtelID = @OtelID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@OtelID", OtelBilgi.OtelID);
                        command.Parameters.AddWithValue("@OtelAdi", OtelBilgi.OtelAdi);
                        command.Parameters.AddWithValue("@OtelSiralamasi", OtelBilgi.OtelSiralamasi);
                        command.Parameters.AddWithValue("@OtelPostaKodu", OtelBilgi.OtelPostaKodu);
                        command.Parameters.AddWithValue("@OtelPolitikaKodu", OtelBilgi.OtelPolitikaKodu);
                        command.Parameters.AddWithValue("@OtelTesisKodu", OtelBilgi.OtelTesisKodu);
                        command.Parameters.AddWithValue("@OtelYoneticiID", OtelBilgi.OtelYoneticiID);
                        


                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Otel");

        }

    }
}
