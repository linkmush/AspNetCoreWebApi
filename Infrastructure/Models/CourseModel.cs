using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CourseModel
{
    public int Id { get; set; }
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
    public string CategoryName { get; set; } = null!;

    public static implicit operator CourseModel(CourseEntity courseEntity)
    {
        return new CourseModel
        {
            Id = courseEntity.Id,
            Title = courseEntity.Title,
            Author = courseEntity.Author,
            Price = courseEntity.Price,
            DiscountPrice = courseEntity.DiscountPrice,
            Hours = courseEntity.Hours,
            LikesInNumbers = courseEntity.LikesInNumbers,
            LikesInProcent = courseEntity.LikesInProcent,
            IsBestSeller = courseEntity.IsBestSeller,
            IsDigital = courseEntity.IsDigital,
            ImageUrl = courseEntity.ImageUrl,
            CategoryName = courseEntity.Category!.CategoryName
        };
    }
}
