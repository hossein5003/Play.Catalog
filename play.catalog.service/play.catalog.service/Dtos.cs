using System.ComponentModel.DataAnnotations;

namespace play.catalog.service
{
    public record ItemDto(Guid Id , String Name,String Description,decimal Price,DateTimeOffset CreatedDate);

    public record CreateItemDto([Required] String Name, String Description,[Range(0,1000)] decimal Price);

    public record UpdateItemDto([Required] String Name, String Description, [Range(0, 1000)] decimal Price);
}
    