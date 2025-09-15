using Demo_ASP_FirstTry.App.Data;
using Demo_ASP_FirstTry.App.Models.Form;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Demo_ASP_FirstTry.App.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SQLConnectionFactory _connectionFactory;
        private readonly ContactRepository _contactRepository;
        public ContactController(
            ILogger<HomeController> logger, 
            SQLConnectionFactory connectionFactory, 
            ContactRepository contactRepository)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
            _contactRepository = contactRepository;
        }

        public IActionResult Index()
        {
            List<ContactFormDTO> data = _contactRepository.GetContactForms();

            return View(data);

            
        }
        public IActionResult Details(int id)
        {
            string connectionString = "Data Source=GOS-VDI202\\TFTIC;Initial Catalog=SQL_EXERCISE_FORM;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";


            SqlConnection connection = new(connectionString);

            _logger.LogInformation("Connexion reussi!");

            ContactForm? data = GetContactFormById(connection);

            if (data is not null)
                return View(data);
            else
            {
                return RedirectToAction("Index");
            }

            ContactForm? GetContactFormById(SqlConnection connection)
            {
                ContactForm? form = null;

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * " +
                        "FROM DemandeDeContact " +
                        "WHERE Id = @Id";
                    command.Parameters.AddWithValue("Id", id);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ContactForm contactForm = new ContactForm
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nom = reader.GetString(reader.GetOrdinal("Nom")),
                                Prenom = reader.GetString(reader.GetOrdinal("Prenom")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Raison = reader.GetInt32(reader.GetOrdinal("Raison")),
                                Message = reader.GetString(reader.GetOrdinal("Message")),
                                Traiter = reader.GetBoolean(reader.GetOrdinal("Traiter"))
                            };


                            form = contactForm;

                        }

                    }

                    connection.Close();

                    return form;
                }


            }
        }

        [HttpPost]
        public IActionResult Details(int id, bool Traiter)
        {

            string connectionString = "Data Source=GOS-VDI202\\TFTIC;Initial Catalog=SQL_EXERCISE_FORM;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";


            SqlConnection connection = new(connectionString);

            _logger.LogInformation("Connexion reussi!");

            int formActionResult = -1;
            int idTest = int.Parse(id.ToString());
           

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE DemandeDeContact " +
                    "SET Traiter = @Traiter " +
                    "WHERE Id = @Id ";
                command.Parameters.AddWithValue("Id", idTest);
                command.Parameters.AddWithValue("Traiter", Traiter);

                connection.Open();
                formActionResult = command.ExecuteNonQuery();
                connection.Close();
            }

            if (formActionResult < 0)
                _logger.LogInformation("Update failled!");
            else
                _logger.LogInformation($"Update done! Id = {idTest}");



            return RedirectToAction("Index");

        }

    }
}
