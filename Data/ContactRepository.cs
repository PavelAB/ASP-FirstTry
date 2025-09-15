using Demo_ASP_FirstTry.App.Controllers;
using Demo_ASP_FirstTry.App.Models.Form;
using Microsoft.Data.SqlClient;

namespace Demo_ASP_FirstTry.App.Data
{
    public class ContactRepository
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SQLConnectionFactory _connectionFactory;

        public ContactRepository(ILogger<HomeController> logger, SQLConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }


        public List<ContactFormDTO> GetContactForms()
        {
            SqlConnection connectionT = _connectionFactory.CreateConnection();


            List<ContactFormDTO> formCreated = new List<ContactFormDTO>();

            using (SqlCommand command = connectionT.CreateCommand())
            {
                command.CommandText = "SELECT Id, Raison, Message, Traiter " +
                    "FROM DemandeDeContact ";
                connectionT.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ContactFormDTO contactDTO = new ContactFormDTO
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Raison = reader.GetInt32(reader.GetOrdinal("Raison")),
                            Message = reader.GetString(reader.GetOrdinal("Message")),
                            Traiter = reader.GetBoolean(reader.GetOrdinal("Traiter"))
                        };

                        formCreated.Add(contactDTO);
                    }
                }

                connectionT.Close();

            }

            return formCreated;
        }

    }
}
