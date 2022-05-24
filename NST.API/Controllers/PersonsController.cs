using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NST.API.Contracts.V1;

namespace NST.API.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpGet(ApiRoutes.Person.GetAll)]
        public async Task<ActionResult> GetPeople() => await _personService.GetPeople();


        [HttpGet(ApiRoutes.Person.Get)]
        public async Task<IActionResult> GetPersonById([FromRoute] long id) => await _personService.GetPersonById(id);


        [HttpPut(ApiRoutes.Person.Update)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] PersonRequest personRequest) => await _personService.UpdatePerson(id, personRequest);


        [HttpDelete(ApiRoutes.Person.Delete)]
        public async Task<IActionResult> Delete([FromRoute] long id) => await _personService.DeletePersonById(id);


        [HttpPost(ApiRoutes.Person.Create)]
        public async Task<IActionResult> Create([FromBody] PersonRequest personRequest) => await _personService.CreatePerson(personRequest);
    }
}
