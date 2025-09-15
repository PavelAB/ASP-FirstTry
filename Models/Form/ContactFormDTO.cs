namespace Demo_ASP_FirstTry.App.Models.Form
{
    public class ContactFormDTO
    {

        public required int Id { get; set; }
        public required int Raison { get; set; }
        public required string Message { get; set; }
        public required bool Traiter { get; set; }
    }
}
