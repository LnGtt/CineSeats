namespace CineSeats.Catalogue.ValueObject;

public class RowMap
{
    public string RowLetter { get; set; }
    public int NumberOfSeats { get; set; }

    public RowMap(string rowLetter, int numberOfSeats)
    {
        if (string.IsNullOrWhiteSpace(rowLetter))
            throw new ArgumentException("A letra da fileira não pode ser vazia.");
        
        if (numberOfSeats <= 0)
            throw new ArgumentException("Uma fileira deve ter pelo menos 1 cadeira.");
        
        RowLetter = rowLetter.ToUpper();
        NumberOfSeats = numberOfSeats;
    }
}