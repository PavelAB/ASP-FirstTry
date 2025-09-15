using Demo_ASP_FirstTry.App.Models;
using Demo_ASP_FirstTry.App.Models.Form;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Demo_ASP_FirstTry.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactForm form)
        {
            const string PATTERN_MAIL = @"^[^@\s]+@[^@\s]+\.[^@\s]{1,6}$";

            string connectionString = "Data Source=GOS-VDI202\\TFTIC;Initial Catalog=SQL_EXERCISE_FORM;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";


            SqlConnection connection = new(connectionString);

            _logger.LogInformation("Connexion reussi!");



            if (form.Prenom is null || form.Prenom.Trim() == "" || form.Prenom.Trim() == string.Empty)
            {
                _logger.LogInformation($"Error : Prenom Vide {form.Prenom}");
                return RedirectToAction("Contact");
            }
            if(form.Nom is null || form.Nom.Trim() == "" || form.Nom.Trim() == string.Empty)
            {
                _logger.LogInformation($"Error : Nom Vide {form.Nom}");
                return RedirectToAction("Contact");
            }

            if (form.Raison < 1 || form.Raison > 3)
            {
                _logger.LogInformation($"Error : Raison n'est pas valide {form.Raison}");
                return RedirectToAction("Contact");
            }
            if(form.Email is null || !Regex.IsMatch(form.Email, PATTERN_MAIL))
            {
                _logger.LogInformation($"Error : Mail n'est pas valide {form.Email}");
                return RedirectToAction("Contact");
            }



            _logger.LogInformation($"Content : {CreateNewContactForm(connection, form)}");


            return RedirectToAction("Contact");

            int CreateNewContactForm(SqlConnection connection, ContactForm form)
            {
                int formCreatedId = -1;

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO DemandeDeContact (Nom, Prenom, Email, Raison, Message, Traiter) " +
                        "OUTPUT inserted.Id " +
                        "VALUES ( @Nom, @Prenom, @Email, @Raison, @Message, @Traiter)";
                    command.Parameters.AddWithValue("Nom", form.Nom);
                    command.Parameters.AddWithValue("Prenom", form.Prenom);
                    command.Parameters.AddWithValue("Email", form.Email);
                    command.Parameters.AddWithValue("Raison", form.Raison);
                    command.Parameters.AddWithValue("Message", form.Message);
                    command.Parameters.AddWithValue("Traiter", form.Traiter);

                    connection.Open();
                    formCreatedId = (int)command.ExecuteScalar();
                    connection.Close();

                }

                return formCreatedId;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
