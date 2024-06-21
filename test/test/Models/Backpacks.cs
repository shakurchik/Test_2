using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

[Table("Backpacks")]
[PrimaryKey(nameof(CharacterId), nameof(ItemId))]
public class Backpacks
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }

    [ForeignKey(nameof(CharacterId))]
    public Characters Character { get; set; } = null!;

    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; } = null!;
}
    
