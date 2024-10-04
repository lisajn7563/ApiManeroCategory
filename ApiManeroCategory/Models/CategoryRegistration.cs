using System.ComponentModel.DataAnnotations;

namespace ApiManeroCategory.Models;

public class CategoryRegistration
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string categoryTitle { get; set; } = null!;
}
