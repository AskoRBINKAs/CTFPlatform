using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTFPlatform.Models;

[Table("tasks")]
public class TaskModel
{
    [Key] public int Id { get; set; }
    [Column("name")] [Required] public string Name { get; set; } = string.Empty;

    [Column("desc")] [Required] public string Description { get; set; } = string.Empty;

    [Column("type")] [Required] public string Type { get; set; } = string.Empty;

    [Required] [Column("flag")] public string Flag { get; set; } = string.Empty;

    [Column("maxcost")] [Required] public int MaxCost { get; set; } = 1000;

    [Column("mincost")] [Required] public int MinCost { get; set; } = 100;

    [Column("cost")] [Required] public int Cost { get; set; } = 1000;

    [Required] [Column("visible")] public bool IsVisible { get; set; } = true;
    [Required] [Column("attachments")] public string Attachments { get; set; } = string.Empty;

    [Required] [Column("deployed_env")] public string DeployedEnvironment { get; set; } = string.Empty;
    [Required] [Column("solves")] public int Solves { get; set; } = 0;
}