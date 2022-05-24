using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<PersonResponse>> GetPeople();
        Task<PersonResponse> GetPersonById(long id);
        Task DeletePersonById(long id);
        Task<PersonResponse> CreatePerson(PersonRequest personRequest);
        Task<PersonResponse> UpdatePerson(long id, PersonRequest personRequest);
    }
}
