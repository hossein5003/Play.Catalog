using Microsoft.AspNetCore.Mvc;
using play.catalog.service.Extentions;

namespace play.catalog.service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : Controller
    {
        private static readonly List<ItemDto> _items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow)
        };

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            return _items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto>? GetItemById(Guid id)
        {
            var item = _items.FirstOrDefault(item => item.Id == id);

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> CreareItem(CreateItemDto item)
        {
            var itemDto = item.toItemDto();
            _items.Add(itemDto);

            return CreatedAtAction(nameof(GetItemById), new { id = itemDto.Id }, itemDto);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            ItemDto? item=_items.Where(item=> item.Id == id).SingleOrDefault();

            if (item == null)
                return NotFound();

            _items.Remove(item);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutItem(Guid id,UpdateItemDto updatedItem)
        {
            var itemFromDatabase=_items.FirstOrDefault(item => item.Id == id);

            if (updatedItem == null)
                return NotFound();

            var newItem=updatedItem.toItemDto(itemFromDatabase);
            _items.Remove(itemFromDatabase);
            _items.Add(newItem);

            return NoContent();
        }




    }
}
