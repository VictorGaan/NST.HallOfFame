using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NST.Test
{
    public class UnitTest
    {
        private readonly Mock<IPersonRepository> _personRepository;
        public UnitTest()
        {
            _personRepository = new Mock<IPersonRepository>();
        }
        [Fact]
        public async Task GetPeople_ReturnedAnyCollection()
        {
            _personRepository.Setup(x => x.GetPeople()).Returns(Task.Run(() => GetPeople()));
            var persons = await _personRepository.Object.GetPeople();
            Assert.True(persons.Any());
        }

        [Fact]
        public async Task GetPersonById_NotNullReturnedPerson()
        {
            _personRepository.Setup(x => x.GetPersonById(1)).Returns(Task.Run(() => GetPeople()[0]));
            var person = await _personRepository.Object.GetPersonById(1);
            Assert.NotNull(person);
        }

        [Fact]
        public async Task AddPerson_NotNullResponseReturned()
        {
            PersonRequest personRequest = new PersonRequest()
            {
                DisplayName = "Исаак",
                Name = "Исаак",
            };

            PersonResponse personResponse = new PersonResponse()
            {
                DisplayName = "Исаак",
                Name = "Исаак",
            };
            _personRepository.Setup(x => x.CreatePerson(personRequest)).Returns(Task.Run(() => personResponse));
            var person = await _personRepository.Object.CreatePerson(personRequest);
            Assert.NotNull(personResponse);
        }
        [Fact]
        public async Task UpdatePerson_NotNullResponseReturned()
        {
            PersonRequest personRequest = new PersonRequest()
            {
                DisplayName = "Исаак",
                Name = "Исаак",
                Skills = new List<SkillRequest>() { new SkillRequest() { Name = "Юморист", Level = 9 } }
            };


            PersonResponse personResponse = new PersonResponse()
            {
                DisplayName = "Исаак",
                Name = "Исаак",
                Skills = new List<SkillResponse>() { new SkillResponse() { Name = "Юморист", Level = 9 } }
            };

            _personRepository.Setup(x => x.UpdatePerson(1, personRequest)).Returns(Task.Run(() => personResponse));
            var person = await _personRepository.Object.UpdatePerson(1, personRequest);
            Assert.NotNull(person);
        }
        private List<PersonResponse> GetPeople() => new List<PersonResponse>
        {
            new PersonResponse() { Name = "Гарри", DisplayName = "Гарри", Skills = new List<SkillResponse>() { new SkillResponse() { Name = "Юморист", Level = 9 } } },
            new PersonResponse() { Name = "Соломон", DisplayName = "Соломон", Skills = new List<SkillResponse>() { new SkillResponse() { Name = "Шумахер", Level = 3 } } },
            new PersonResponse() { Name = "Самуил", DisplayName = "Самуил", Skills = new List<SkillResponse>() { new SkillResponse() { Name = "Повар", Level = 2 } } },
            new PersonResponse() { Name = "Евдоким", DisplayName = "Евдоким", Skills = new List<SkillResponse>() { new SkillResponse() { Name = "Певец", Level = 7 } } }
        };
    }
}