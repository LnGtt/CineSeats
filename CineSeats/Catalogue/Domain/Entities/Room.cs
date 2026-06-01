using CineSeats.Catalogue.ValueObject;

namespace CineSeats.Catalogue.Domain.Entities;

public class Room
{
    public Guid Id { get; private set; }
    public int RoomNumber { get; private set; }
    private readonly List<RowMap> _layout;
    public IReadOnlyCollection<RowMap> Layout => _layout.AsReadOnly();
    public int TotalCapacity => _layout.Sum(row => row.NumberOfSeats);
    
    protected Room() 
    {
        _layout = new List<RowMap>();
    }
    
    public Room(int roomNumber, IEnumerable<RowMap> layout)
    {
        
        if (roomNumber <= 0)
            throw new ArgumentException("Room number cannot be zero or negative");

        var layoutList = layout?.ToList() ?? new List<RowMap>();
        if (layoutList.Count == 0)
            throw new ArgumentException("Room layout must have at least one row");

        Id = Guid.NewGuid();
        RoomNumber = roomNumber;
        _layout = layoutList;
    }
    
    public void UpdateDetails(int roomNumber, IEnumerable<RowMap> newLayout)
    {
        if (roomNumber <= 0)
            throw new ArgumentException("Room number cannot be zero or negative");

        var layoutList = newLayout?.ToList() ?? new List<RowMap>();
        if (layoutList.Count == 0)
            throw new ArgumentException("Room layout must have at least one row");

        RoomNumber = roomNumber;
    
        _layout.Clear();
        _layout.AddRange(layoutList);
    }
}