using System.ComponentModel.DataAnnotations;

namespace Demo_ASP_FirstTry.App.Models.Form
{
    public class ContactForm
    {
        public int? Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required string Email { get; set; }
        public required int Raison { get; set; }
        public required string Message { get; set; }
        public required bool Traiter { get; set; } = false;
    }
}
