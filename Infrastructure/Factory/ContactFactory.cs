using Infrastructure.Dtos;
using Infrastructure.Entities;

namespace Infrastructure.Factory;

public class ContactFactory
{
    public static ContactEntity Create(ContactDto dto)
    {
        try
        {
            return new ContactEntity
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Service = dto.Service,
                Message = dto.Message,
            };
        }
        catch { }
        return null!;
    }
}
