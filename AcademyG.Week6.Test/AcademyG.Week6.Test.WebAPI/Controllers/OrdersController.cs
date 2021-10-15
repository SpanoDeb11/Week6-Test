using AcademyG.Week6.Test.Core.BL;
using AcademyG.Week6.Test.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyG.Week6.Test.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMainBusinessLayer _mainBL;

        public OrdersController(IMainBusinessLayer bl)
        {
            this._mainBL = bl;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var result = this._mainBL.GetAllOrders();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid order id");

            var result = this._mainBL.GetOrderById(id);
            if (result == null)
                return NotFound("Order not found");

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddNewOrder([FromBody] Order newOrder)
        {
            if (newOrder == null)
                return BadRequest("Invalid order received");

            if (this._mainBL.GetOrderByCode(newOrder.OrderCode) != null)
                return BadRequest("Invalid order code");

            if (!this._mainBL.AddNewOrder(newOrder))
                return BadRequest("Order not added");

            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order order)
        {
            if (id <= 0)
                return BadRequest("Invalid order id");

            if (order == null)
                return BadRequest("Invalid order received");

            if (id != order.Id)
                return BadRequest("Order ids not matching");

            if (!this._mainBL.UpdateOrder(order))
                return BadRequest("Order not updated");

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid order id");

            if (this._mainBL.GetOrderById(id) == null)
                return NotFound("Order not found");

            if (!this._mainBL.DeleteOrderById(id))
                return BadRequest("Order not deleted");

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchOrder(int id, [FromBody] JsonPatchDocument<Order> patchDoc)
        {
            if (id <= 0)
                return BadRequest("Invalid order id");

            if (patchDoc == null)
                return BadRequest("Invalid data received");

            var order = this._mainBL.GetOrderById(id);

            if (order == null)
                return NotFound("Order not found");

            patchDoc.ApplyTo(order, ModelState);

            if (!ModelState.IsValid)
                return StatusCode(500, "Cannot patch order");

            if (this._mainBL.UpdateOrder(order))
                return BadRequest("Order not updated");

            return Ok(order);
        }
    }
}
