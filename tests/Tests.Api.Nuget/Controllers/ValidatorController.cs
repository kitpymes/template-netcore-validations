using Tests.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Tests.Api.Nuget.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidatorController : ControllerBase
    {
        [HttpPost("AddPerson")]
        public async Task AddPerson(PersonAddDto dto)
        => await Task.Delay(1);

        [HttpPost("ChangeName/{age}")]
        public async Task ChangeName(int age, string invalidName)
        {
            var person = new Person(age, "Pedro", "pedro@gmail.com");

            person.ChangeName(invalidName);

            await Task.Delay(1);
        }
    }
}
