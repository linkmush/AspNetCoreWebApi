﻿namespace Infrastructure.Models;

public class CoursesModel
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
}
