using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CourseModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Price { get; set; }
    public string? DiscountPrice { get; set; }
    public string? Hours { get; set; }
    public bool IsBestSeller { get; set; }
    public string? LikesInNumbers { get; set; }
    public string? LikesInPoints { get; set; }
    public string? Author { get; set; }
    public string? ImageUrl { get; set; }

    public static implicit operator CourseModel(CourseEntity courseEntity)
    {
        return new CourseModel
        {
            Id = courseEntity.Id,
            Title = courseEntity.Title,
            Price = courseEntity.Price,
            DiscountPrice = courseEntity.DiscountPrice,
            Hours = courseEntity.Hours,
            IsBestSeller = courseEntity.IsBestSeller,
            LikesInNumbers = courseEntity.LikesInNumbers,
            LikesInPoints = courseEntity.LikesInPoints,
            Author = courseEntity.Author,
            ImageUrl = courseEntity.ImageUrl,
        };
    }
}
