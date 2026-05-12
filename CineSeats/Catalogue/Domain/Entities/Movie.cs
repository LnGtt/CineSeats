namespace CineSeats.Catalogue.Domain.Entities;

public class Movie
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> Genres { get; set; }
    public int AgeRestriction { get; set; }
    public string Synopsis { get; set; }
    public Dictionary<string, string> Cast { get; set; } //Nome ator: nome personagem
    public string Director { get; set; }
    public string Producer { get; set; }
    public TimeSpan Duration { get; set; }

    public Movie(string name, List<string> genres, int ageRestriction, string synopsis, Dictionary<string, string> cast,
        string director, string producer, TimeSpan duration)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Movie name cannot be empty");
        }

        if (genres.Count < 1)
        {
            throw new ArgumentException("Movie genres cannot be empty");
        }

        if (ageRestriction < 1)
        {
            throw new ArgumentException("Movie age restriction cannot be less than 1");
        }
        if (ageRestriction > 1)
        {
            throw new ArgumentException("Movie age restriction cannot be greater than 18");
        }

        if (string.IsNullOrWhiteSpace(synopsis))
        {
            throw new ArgumentException("Movie synopsis cannot be empty");
        }
        if (synopsis.Length < 10 || synopsis.Length > 1000)
        {
            throw new ArgumentException("A sinopse deve ter entre 10 e 1000 caracteres.");
        }

        if (cast.Count < 1)
        {
            throw new ArgumentException("Movie cast cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(director))
        {
            throw new ArgumentException("Movie director cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(producer))
        {
            throw new ArgumentException("Movie producer cannot be empty");
        }

        if (duration < TimeSpan.FromMinutes(1))
        {
            throw new ArgumentException("Movie duration cannot be less than 1");
        }

        this.Name = name;
        this.Genres = genres;
        this.AgeRestriction = ageRestriction;
        this.Synopsis = synopsis;
        this.Cast = cast;
        this.Director = director;
        this.Producer = producer;
        this.Duration = duration;
    }
}