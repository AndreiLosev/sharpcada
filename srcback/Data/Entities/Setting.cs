using System.ComponentModel.DataAnnotations;

namespace sharpcada.Data.Entities;

public class Setting
{
    [Key]
    public string Key { set; get; } = null!;
    public string Value { set; get; } = null!;
}
