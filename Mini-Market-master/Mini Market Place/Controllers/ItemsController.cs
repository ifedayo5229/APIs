using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Market_Place.Services;
using Mini_Market_Place.ViewModels;

namespace Mini_Market_Place.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody]CreateItemViewModel model)
        {
            var res = _itemService.CreateItem(model);
            if (res.ErrorMessages.Any())
            {
                return Ok(res.ErrorMessages);
            }

            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetAllItems()
        {
            var res = _itemService.GetAllItems();
            if (res.ErrorMessages.Any())
            {
                return Ok(res.ErrorMessages);
            }

            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var res = _itemService.GetById(id);
            if (res.ErrorMessages.Any())
            {
                return Ok(res.ErrorMessages);
            }

            return Ok(res);
        }

        [HttpPost]
        public IActionResult UpdateItem([FromBody]ItemViewModel model)
        {
            var res = _itemService.UpdateItem(model);
            if (res.ErrorMessages.Any())
            {
                return Ok(res.ErrorMessages);
            }

            return Ok(res);
        }

        [HttpPost]
        public IActionResult PurchaseItem(Guid itemId)
        {
            var res = _itemService.PurchaseItem(itemId);
            if (res.ErrorMessages.Any())
            {
                return Ok(res.ErrorMessages);
            }

            return Ok(res);
        }
    }
}