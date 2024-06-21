namespace test.DTO;

public class CharacterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public List<BackpackItemDto> BackpackItems { get; set; }
    public List<TitleDto> Titles { get; set; }
}

public class BackpackItemDto
{
    public string ItemName { get; set; }
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class TitleDto
{
    public string Title { get; set; }
    public DateTime AcquiredAt { get; set; }
}
