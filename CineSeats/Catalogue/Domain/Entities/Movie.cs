using System.Collections.ObjectModel;

namespace CineSeats.Catalogue.Domain.Entities;

public class Movie
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public int DurationMinutes { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    
    private readonly DateOnly _today = DateOnly.FromDateTime(DateTime.Today);
    
    /*
    public Guid CinemaId { get; private set; }
    public string AgeRestriction { get; private set; }
    public string Synopsis { get; private set; }
    public string Director { get; private set; }
    public string Producer { get; private set; }
    */
    //public DateOnly StartDate { get; private set; }
    //public DateOnly EndDate { get; private set; }
    /*
    private readonly List<string> _genres; 
    public IReadOnlyCollection<string> Genres => _genres.AsReadOnly();
    
    private readonly Dictionary<string, string> _cast;
    public IReadOnlyDictionary<string, string> Cast => new ReadOnlyDictionary<string, string>(_cast); //Nome ator: nome personagem
    
    private static readonly string[] _restrictions = { "L", "10", "12", "14", "16", "18" };
    
    protected Movie() //exigência de MongoDB
    {
        _genres = new List<string>();
        _cast = new Dictionary<string, string>();
    }
    */
    public Movie(string title, int durationMinutes, DateOnly startDate,  DateOnly endDate)
    {
        /*if (cinemaId == Guid.Empty)
            throw new ArgumentException("CinemaId cannot be empty");*/
        
        /*var genresList = genres?.ToList() ?? new List<string>();
        var castDictionary = cast != null ? new Dictionary<string, string>(cast) : new Dictionary<string, string>();*/
        
        ValidateMovieInfo(title, durationMinutes);
        ValidateDates(startDate, endDate);
        
        Id = Guid.NewGuid();
        Title = title;
        DurationMinutes = durationMinutes;
        StartDate = startDate;
        EndDate = endDate;
        //CinemaId = cinemaId;
        //_genres = genresList;
        //AgeRestriction = ageRestriction;
        //Synopsis = synopsis;
        //_cast = castDictionary;
        //Director = director;
        //Producer = producer;
    }
    
    public void UpdateDetails(string title, int durationMinutes)
    {
        ValidateMovieInfo(title, durationMinutes);
        
        /*var genresList = genres?.ToList() ?? new List<string>();
        var castDictionary = cast != null ? new Dictionary<string, string>(cast) : new Dictionary<string, string>();*/
        
        Title = title;
        DurationMinutes = durationMinutes;
        // AgeRestriction = ageRestriction;
        // Synopsis = synopsis;
        // Director = director;
        // Producer = producer;

    
        // _genres.Clear();
        // _genres.AddRange(genresList);

        // _cast.Clear();
        // foreach (var kvp in castDictionary)
        // {
        //     _cast[kvp.Key] = kvp.Value;
        // }
    }

    public void Reschedule(DateOnly startDate, DateOnly endDate)
    {
        ValidateDates(startDate, endDate);
        
        if (this.StartDate <= _today)
            throw new ArgumentException("Movie exibition already started");
        
        StartDate = startDate;
        EndDate = endDate;
    }

    /*public void AddGenre(string genre)
    {
        if (string.IsNullOrWhiteSpace(genre))
            throw new ArgumentException("O gênero não pode ser vazio.", nameof(genre));
        
        if (!_genres.Contains(genre))
        {
            _genres.Add(genre);
        }
    }*/

    /*public void RemoveGenre(string genre)
    {
        if (_genres.Count == 1 && _genres.Contains(genre))
            throw new InvalidOperationException("O filme deve ter pelo menos um gênero. Adicione outro antes de remover este.");

        _genres.Remove(genre);
    }*/

    /*public void AddOrUpdateCastMember(string actor, string character)
    {
        if (string.IsNullOrWhiteSpace(actor))
            throw new ArgumentException("O nome do ator não pode ser vazio.", nameof(actor));

        if (string.IsNullOrWhiteSpace(character))
            throw new ArgumentException("O nome do personagem não pode ser vazio.", nameof(character));
        
        _cast[actor] = character;
    }*/

    /*public void RemoveCastMember(string actor)
    {
        if (_cast.Count == 1 && _cast.ContainsKey(actor))
            throw new InvalidOperationException("O filme deve ter pelo menos um membro no elenco.");

        _cast.Remove(actor);
    }*/
    
    private void ValidateMovieInfo(string title, int durationMinutes)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title of movie cannot be null or empty.", nameof(title));

        if (durationMinutes < 60 || durationMinutes > 210)
            throw new ArgumentException("Duration cannot be less than 60 and more than 210 minutes.");
        
        /*if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Movie name cannot be empty");

        if (genresList.Count < 1)
            throw new ArgumentException("Movie genres cannot be empty", nameof(genresList));

        if (string.IsNullOrWhiteSpace(ageRestriction) || !_restrictions.Contains(ageRestriction))
            throw new ArgumentException("Movie age restriction is invalid or empty");

        if (string.IsNullOrWhiteSpace(synopsis))
            throw new ArgumentException("Movie synopsis cannot be empty");
    
        if (synopsis.Length < 10 || synopsis.Length > 1000)
            throw new ArgumentException("A sinopse deve ter entre 10 e 1000 caracteres.");

        if (castDictionary.Count < 1)
            throw new ArgumentException("Movie cast cannot be empty", nameof(castDictionary));

        if (string.IsNullOrWhiteSpace(director))
            throw new ArgumentException("Movie director cannot be empty");

        if (string.IsNullOrWhiteSpace(producer))
            throw new ArgumentException("Movie producer cannot be empty");

        if (duration < TimeSpan.FromMinutes(1))
            throw new ArgumentException("Movie duration cannot be less than 1 minute");*/
    }

    private void ValidateDates(DateOnly startDate, DateOnly endDate)
    {
        if (startDate <= _today)
            throw new ArgumentException("Start date cannot be in the past or today");
        
        if (startDate >= endDate)
            throw new ArgumentException("Start date cannot be greater than or equal to end date");
    }
}