namespace test.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Weight { get; set; }
    public ICollection<Backpacks> Editions { get; set; } = new
        HashSet<Backpacks>();
}