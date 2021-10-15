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

    // queste operazioni devono essere gestite dal WCF.
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMainBusinessLayer _mainBL;

        public ClientsController(IMainBusinessLayer bl)
        {
            this._mainBL = bl;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            var result = this._mainBL.GetAllClients();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid client id");

            var result = this._mainBL.GetClientById(id);
            if (result == null)
                return NotFound("Client not found");

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddNewClient([FromBody]Client newClient)
        {
            if (newClient == null)
                return BadRequest("Invalid client received");

            if (this._mainBL.GetClientByCode(newClient.ClientCode) != null)
                return BadRequest("Invalid client code");

            if (!this._mainBL.AddNewClient(newClient))
                return BadRequest("Client not added");

            return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, newClient);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody]Client client)
        {
            if (id <= 0)
                return BadRequest("Invalid client id");

            if (client == null)
                return BadRequest("Invalid client received");

            if (id != client.Id)
                return BadRequest("Client ids not matching");

            if (!this._mainBL.UpdateClient(client))
                return BadRequest("Client not updated");

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid client id");

            if (this._mainBL.GetClientById(id) == null)
                return NotFound("Client not found");

            if (!this._mainBL.DeleteClientById(id))
                return BadRequest("Client not deleted");

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchClient(int id, [FromBody]JsonPatchDocument<Client> patchDoc)
        {
            if (id <= 0)
                return BadRequest("Invalid client id");

            if (patchDoc == null)
                return BadRequest("Invalid data received");

            var client = this._mainBL.GetClientById(id);

            if (client == null)
                return NotFound("Client not found");

            patchDoc.ApplyTo(client, ModelState);

            if (!ModelState.IsValid)
                return StatusCode(500, "Cannot patch client");

            if (this._mainBL.UpdateClient(client))
                return BadRequest("Client not updated");

            return Ok(client);
        }
    }
}
