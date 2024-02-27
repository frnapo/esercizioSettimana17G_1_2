using MVC1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["MVC1"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Dipendenti> dipendenti = new List<Dipendenti>();

            try
            {
                conn.Open();
                string query = "SELECT * FROM Dipendenti";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())

                {
                    Dipendenti dipendente = new Dipendenti();
                    dipendente.Id = Convert.ToInt32(reader["idDipendente"]);
                    dipendente.Nome = reader["Nome"].ToString();
                    dipendente.Cognome = reader["Cognome"].ToString();
                    dipendente.Indirizzo = reader["Indirizzo"].ToString();
                    dipendente.CodFiscale = reader["CodFiscale"].ToString();
                    dipendente.Mansione = reader["Mansione"].ToString();
                    dipendente.Coniugato = Convert.ToBoolean(reader["Coniugato"]);
                    dipendente.FigliACarico = Convert.ToInt32(reader["FigliACarico"]);
                    dipendenti.Add(dipendente);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);

            }
            finally
            {
                conn.Close();
            }

            return View(dipendenti);
        }


        public ActionResult CreateDipendenti() { return View(); }
        [HttpPost]
        public ActionResult CreateDipendenti(Dipendenti dipendente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MVC1"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO Dipendenti (Nome, Cognome, Indirizzo, CodFiscale, Coniugato, FigliACarico,  Mansione)" +
                    " VALUES (@Nome, @Cognome, @Indirizzo, @CodFiscale, @Coniugato, @FigliACarico, @Mansione)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", dipendente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", dipendente.Cognome);
                cmd.Parameters.AddWithValue("@Indirizzo", dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@CodFiscale", dipendente.CodFiscale);
                cmd.Parameters.AddWithValue("@Coniugato", dipendente.Coniugato);
                cmd.Parameters.AddWithValue("@FigliACarico", dipendente.FigliACarico);
                cmd.Parameters.AddWithValue("@Mansione", dipendente.Mansione);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);

            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        List<Pagamenti> pagamenti = new List<Pagamenti>();
        public ActionResult CreatePayment() { return View(); }
        [HttpPost]
        public ActionResult CreatePayment(Pagamenti pagamento)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MVC1"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO Pagamenti (FK_idDipendente, PeriodoPagamento, AmmontarePagamento, Tipo)" +
                    " VALUES (@FK_idDipendente, @PeriodoPagamento, @AmmontarePagamento, @Tipo)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FK_idDipendente", pagamento.IdDipendente);
                cmd.Parameters.AddWithValue("@PeriodoPagamento", pagamento.DataPagamento);
                cmd.Parameters.AddWithValue("@AmmontarePagamento", pagamento.Ammontare);
                cmd.Parameters.AddWithValue("@Tipo", pagamento.TipoPagamento);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);

            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MVC1"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = "SELECT * FROM Pagamenti";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())

                {
                    Pagamenti pagamento = new Pagamenti();
                    pagamento.IdDipendente = Convert.ToInt32(reader["FK_idDipendente"]);
                    pagamento.DataPagamento = Convert.ToDateTime(reader["PeriodoPagamento"]);
                    pagamento.Ammontare = Convert.ToDecimal(reader["AmmontarePagamento"]);
                    pagamento.TipoPagamento = Convert.ToInt32(reader["Tipo"]);

                    pagamenti.Add(pagamento);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);

            }
            finally
            {
                conn.Close();
            }

            return View(pagamenti);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}