namespace play.catalog.service
{
    public record ItemDto(Guid Id , String Name,String Description,decimal Price,DateTimeOffset CreatedDate);

    public record CreateItemDto(String Name,String Description,decimal Price);

    public record UpdateItemDto(String Name, String Description, decimal Price);
}
    