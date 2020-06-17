using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidatorController : ControllerBase
    {
        [HttpPost("AddPerson")]
        public async Task AddPerson(PersonAddDto dto)
        {
            await Task.Delay(1);
        }

        [HttpPost("ChangeName/{id}")]
        public async Task ChangeName(string id, string invalidName)
        {
            var person = GetPerson(id);

            person.ChangeName(invalidName);

            await Task.Delay(1);
        }

        private Person GetPerson(string id) 
            => new Person(20, "Pedro", "pedro@gmail.com");
    }
}
