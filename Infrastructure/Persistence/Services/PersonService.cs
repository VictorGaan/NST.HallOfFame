using ApplicationCore.DTOs;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<IPersonService> _logger;
        public PersonService(IPersonRepository personRepository, ILogger<IPersonService> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }
        public async Task<ActionResult> CreatePerson(PersonRequest personRequest)
        {
            try
            {
                var person = await _personRepository.CreatePerson(personRequest);
                return new CreatedAtActionResult("Create", "Persons", null, person);
            }
            catch (ModelException ex)
            {
                var messages = ex.Errors.Select(x => x.ErrorMessage).ToList();
                messages.ForEach(x => _logger.LogError(x));
                return new BadRequestObjectResult(messages);
            }
            catch (DuplicateException ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }

        }

        public async Task<ActionResult> DeletePersonById(long id)
        {
            try
            {
                await _personRepository.DeletePersonById(id);
                return new NoContentResult();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return new NotFoundResult();
            }
        }

        public async Task<ActionResult> GetPeople() => new OkObjectResult(await _personRepository.GetPeople());

        public async Task<ActionResult> GetPersonById(long id)
        {
            try
            {
                var person = await _personRepository.GetPersonById(id);
                return new OkObjectResult(person);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return new NotFoundResult();
            }
        }

        public async Task<ActionResult> UpdatePerson(long id, PersonRequest personRequest)
        {
            try
            {
                var person = await _personRepository.UpdatePerson(id, personRequest);
                return new OkObjectResult(person);
            }
            catch (ModelException ex)
            {
                var messages = ex.Errors.Select(x => x.ErrorMessage).ToList();
                messages.ForEach(x => _logger.LogError(x));
                return new BadRequestObjectResult(messages);
            }
            catch (DuplicateException ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return new NotFoundResult();
            }
        }
    }
}
