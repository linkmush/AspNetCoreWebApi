using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factory;

public class CourseFactory
{
    public static CoursesModel Create(CourseEntity entity)
    {
        try
        {
            return new CoursesModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Author = entity.Author,
                Price = entity.Price,
                DiscountPrice = entity.DiscountPrice,
                Hours = entity.Hours,
                LikesInNumbers = entity.LikesInNumbers,
                LikesInProcent = entity.LikesInProcent,
                IsDigital = entity.IsDigital,
                IsBestSeller = entity.IsBestSeller,
                ImageUrl = entity.ImageUrl,
                CategoryName = entity.Category!.CategoryName
            };
        }
        catch { }
        return null!;
    }

    public static IEnumerable<CoursesModel> Create(List<CourseEntity> entities)
    {
        List<CoursesModel> courses = [];

        try
        {
            foreach (var entity in entities)
            {
                courses.Add(Create(entity));
            }

        }
        catch { }

        return courses;
    }
}
