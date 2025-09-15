namespace Demo_ASP_FirstTry.App.Models.Request
{
    public class PersonRequest
    {

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime BirthDate { get; set; }
    }
}
