using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<SubscriberEntity> Subscribers { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ContactEntity> Contacts { get; set; }

}
