namespace play.catalog.service.Extentions
{
    public static class DtoExtentions
    {
        public static ItemDto toItemDto(this CreateItemDto item)
            => new(Guid.NewGuid(),item.Name,item.Description,item.Price,DateTimeOffset.UtcNow);
           
        public static ItemDto toItemDto(this UpdateItemDto updatedItem ,ItemDto originalItem)
           =>originalItem with
           {
               Name = updatedItem.Name,
               Description = updatedItem.Description,
               Price = updatedItem.Price,               
           };
    }
}
