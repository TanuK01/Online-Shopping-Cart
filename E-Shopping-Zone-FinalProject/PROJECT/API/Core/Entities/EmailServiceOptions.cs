namespace Core.Entities
{
    public class EmailServiceOptions
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
