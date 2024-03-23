using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos;

public class SubscriberDto
{
    public bool DailyNewsletter { get; set; }

    public bool AdvertisingUpdates { get; set; }

    public bool WeekInReview { get; set; }

    public bool EventUpdates { get; set; }

    public bool StartupsWeekly { get; set; }

    //Jag måste ha samma properties i min viewmodel som i min DTO på webapi för att få det att funka.Men det är okej att ha flera properties i min viewmodel som inte finns med i min DTO på webapiet

    public bool Podcasts { get; set; }

    [Required]
    public string Email { get; set; } = null!;
}
