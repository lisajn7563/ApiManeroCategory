using System.ComponentModel.DataAnnotations;

namespace ApiManeroCategory.Entites;

public class CategoryEntity
{
    [Key]
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string? categoryTitle { get; set; }
    public string PartitionKey { get; set; } = "Categories";
}
