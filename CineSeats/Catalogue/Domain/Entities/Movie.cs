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
    
    public Movie(string title, int durationMinutes, DateOnly startDate,  DateOnly endDate)
    {
        ValidateMovieInfo(title, durationMinutes);
        ValidateDates(startDate, endDate);
        
        Id = Guid.NewGuid();
        Title = title;
        DurationMinutes = durationMinutes;
        StartDate = startDate;
        EndDate = endDate;
    }
    
    public void UpdateDetails(string title, int durationMinutes)
    {
        ValidateMovieInfo(title, durationMinutes);
        
        Title = title;
        DurationMinutes = durationMinutes;
    }

    public void Reschedule(DateOnly startDate, DateOnly endDate)
    {
        ValidateDates(startDate, endDate);
        
        if (this.StartDate <= _today)
            throw new ArgumentException("Movie exibition already started");
        
        StartDate = startDate;
        EndDate = endDate;
    }
    
    private void ValidateMovieInfo(string title, int durationMinutes)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title of movie cannot be null or empty.", nameof(title));

        if (durationMinutes < 60 || durationMinutes > 210)
            throw new ArgumentException("Duration cannot be less than 60 and more than 210 minutes.");
    }

    private void ValidateDates(DateOnly startDate, DateOnly endDate)
    {
        if (startDate <= _today)
            throw new ArgumentException("Start date cannot be in the past or today");
        
        if (startDate >= endDate)
            throw new ArgumentException("Start date cannot be greater than or equal to end date");
    }
}