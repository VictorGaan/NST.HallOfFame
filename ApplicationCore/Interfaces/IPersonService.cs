using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces
{
    public interface IPersonService
    {
        Task<ActionResult> GetPeople();
        Task<ActionResult> GetPersonById(long id);
        Task<ActionResult> DeletePersonById(long id);
        Task<ActionResult> CreatePerson(PersonRequest personRequest);
        Task<ActionResult> UpdatePerson(long id, PersonRequest personRequest);
    }
}
