using ApplicationCore.Domain;
using ApplicationCore.DTOs;
using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using RecursiveDataAnnotationsValidation;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PersonRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PersonResponse> CreatePerson(PersonRequest personRequest)
        {
            var validator = new RecursiveDataAnnotationValidator();
            var results = new List<ValidationResult>();
            if (!validator.TryValidateObjectRecursive(personRequest, results))
                throw new ModelException(results);

            if (personRequest.Skills.Select(x => x.Name).HasDuplicates())
                throw new DuplicateException("Duplicate entries in the model");

            var person = _mapper.Map<Person>(personRequest);

            await _context.AddAsync(person);
            await _context.SaveChangesAsync();
            return _mapper.Map<PersonResponse>(person);
        }

        public async Task DeletePersonById(long id)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(p => p.Id == id);
            if (person == null)
                throw new NotFoundException("Person not found");
            _context.Remove(person);
            await _context.SaveChangesAsync();

        }

        public Task<List<PersonResponse>> GetPeople()
        {
            return _context.Persons.Select(p => _mapper.Map<PersonResponse>(p)).ToListAsync();
        }

        public async Task<PersonResponse> GetPersonById(long id)
        {
            var person = await _context.Persons.SingleOrDefaultAsync(p => p.Id == id);
            if (person == null)
                throw new NotFoundException("Person not found");
            return _mapper.Map<PersonResponse>(person);
        }

        public async Task<PersonResponse> UpdatePerson(long id, PersonRequest personRequest)
        {
            var validator = new RecursiveDataAnnotationValidator();
            var results = new List<ValidationResult>();
            if (!validator.TryValidateObjectRecursive(personRequest, results))
                throw new ModelException(results);

            if (personRequest.Skills.Select(x => x.Name).HasDuplicates())
                throw new DuplicateException("Duplicate entries in the model");
            var person = await _context.Persons.SingleOrDefaultAsync(p => p.Id == id);
            if (person == null)
                throw new NotFoundException("Person not found");
            person.DisplayName = personRequest.DisplayName;
            person.Name = personRequest.Name;
            person.Skills.Clear();
            person.Skills = personRequest.Skills
                .Select(x => new Skill() { Name = x.Name, Level = x.Level })
                .ToList();
            _context.Update(person);
            await _context.SaveChangesAsync();
            return _mapper.Map<PersonResponse>(person);
        }
    }
}
