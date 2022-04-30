using Entites;

namespace play.catalog.service.Extentions
{
    public static class DtoExtentions
    {
        public static ItemDto toItemDto(this CreateItemDto item)
            => new(Guid.NewGuid(),item.Name,item.Description,item.Price,DateTimeOffset.UtcNow);

        public static ItemDto itemDto(this Item item)
            => new ItemDto(item.Id,item.Name,item.Description,item.Price,item.CreatedDate);

        public static Item toItem(this CreateItemDto item)
            => new()
            {
                Id=Guid.NewGuid(),
                Name=item.Name,
                Description=item.Description,
                Price=item.Price,
                CreatedDate=DateTimeOffset.UtcNow
            };
    }
}
