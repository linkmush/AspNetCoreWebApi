namespace Infrastructure.Models;

public class CourseResult
{
    public bool Succeeded { get; set; }
    public IEnumerable<CoursesModel>? Courses { get; set; }
}
