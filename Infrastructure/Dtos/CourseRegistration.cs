using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos;

public class CourseRegistration
{
    [Required]
    public string Title { get; set; } = null!;
    public string? Author { get; set; }
    public string? Price { get; set; }
    public string? DiscountPrice { get; set; }
    public string? Hours { get; set; }
    public string? LikesInNumbers { get; set; }
    public string? LikesInProcent { get; set; }
    public bool IsDigital { get; set; }
    public bool IsBestSeller { get; set; }
    public string? ImageUrl { get; set; }

    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }

    public string CategoryName { get; set; } = null!;
    //Jag måste ha samma properties i min viewmodel som i min DTO på webapi för att få det att funka.Men det är okej att ha flera properties i min viewmodel som inte finns med i min DTO på webapiet
}
