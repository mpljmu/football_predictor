namespace FootballPredictor.Models.People
{
    public interface IPassword
    {
        string Hash { get; set; }
        string Salt { get; set; }
        string TextPassword { get; set; }
    }
}