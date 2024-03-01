using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Violazione()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Violazione> listaViolazioni = new List<Violazione>();

            try
            {
                conn.Open();
                string query = "SELECT * FROM TipoViolazione";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Violazione violazione = new Violazione();
                    violazione.ID_Violazione = Convert.ToInt32(reader["ID_Violazione"]);
                    violazione.Descrizione = reader["Descrizione"].ToString();
                    listaViolazioni.Add(violazione);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return View(listaViolazioni);

        }

        public ActionResult Verbale()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Verbale(Verbale verbale)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString.ToString(); 
            SqlConnection conn = new SqlConnection(connectionString);
          
            try
            {
                // Apro la connessione al db
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Verbale (DataViolazione, IndirizzoViolazione, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, ID_Violazione, ID_Anagrafica) VALUES (@DataViolazione, @IndirizzoViolazione, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @ID_Violazione, @ID_Anagrafica)", conn);
                cmd.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                cmd.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                cmd.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                cmd.Parameters.AddWithValue("@Importo", verbale.Importo);
                cmd.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                cmd.Parameters.AddWithValue("@ID_Violazione", verbale.ID_Violazione);
                cmd.Parameters.AddWithValue("@ID_Anagrafica", verbale.ID_Anagrafica);

                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close(); // Chiudo la connessione al db, NECESSARIO
            }
            return View();
        }

        public ActionResult Anagrafica()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Anagrafica(Anagrafica anagrafica)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                // Apro la connessione al db
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Citta, CAP, CodiceFiscale) VALUES (@Cognome, @Nome, @Indirizzo, @Citta, @CAP, @CodiceFiscale)", conn);
                cmd.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                cmd.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                cmd.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                cmd.Parameters.AddWithValue("@Citta", anagrafica.Citta);
                cmd.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                cmd.Parameters.AddWithValue("@CodiceFiscale", anagrafica.CodiceFiscale);

                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close(); // Chiudo la connessione al db, NECESSARIO
            }
            return View();
        }

        public ActionResult Nuovapagina()
        {
            return View();
        }

        public ActionResult Trasgressori()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Trasgressori> listaTrasgressori = new List<Trasgressori>();

            try
            {
                conn.Open();
                string query = @"SELECT Cognome, Nome AS COGNOME, COUNT(Anagrafica.ID_Anagrafica) AS TOTALEVERBALI FROM ANAGRAFICA JOIN  VERBALE  ON Anagrafica.ID_Anagrafica = Verbale.ID_Anagrafica GROUP BY COGNOME, NOME";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Trasgressori trasgressore = new Trasgressori();
                    trasgressore.Cognome = reader["Cognome"].ToString();
                    
                    trasgressore.TotaleVerbali = Convert.ToInt32(reader["TotaleVerbali"]);
                    listaTrasgressori.Add(trasgressore);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return View(listaTrasgressori);
            
        }
    }
}