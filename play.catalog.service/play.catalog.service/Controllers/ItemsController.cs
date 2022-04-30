   using Microsoft.AspNetCore.Mvc;
using play.catalog.service.Extentions;
using Repositories;

namespace play.catalog.service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : Controller
    {
        private readonly IItemRepository _itemRepository;
        
        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return items.Select(item=>item.itemDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>>? GetItemByIdAsync(Guid id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            if (item == null)
                return NotFound();

            return item.itemDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreareItemAsync(CreateItemDto createdItem)
        {
            await _itemRepository.CreateAsync(createdItem.toItem());

            var itemDto = createdItem.toItemDto();

            return CreatedAtAction(nameof(GetItemByIdAsync),
                new { id = itemDto.Id },itemDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            if (item == null)
                return NotFound();

            await _itemRepository.DeleteByIdAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemAsync(Guid id,UpdateItemDto updatedItem)
        {
            var itemFromDatabase=await _itemRepository.GetByIdAsync(id);

            if (itemFromDatabase == null)
                return NotFound();

            itemFromDatabase.Description =  updatedItem.Description;
            itemFromDatabase.Name=updatedItem.Name;
            itemFromDatabase.Price=updatedItem.Price;

            await _itemRepository.UpdateItemAsync(id, itemFromDatabase);

            return NoContent();
        }
    }
}
