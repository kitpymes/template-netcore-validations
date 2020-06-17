using Kitpymes.Core.Validations;
using System;

namespace Api.Models
{
    public class Person
    {
        public Person(int age, string name, string email)
        {
            Validator
                .AddRule(age, x => x.IsMin(17).IsMax(51).WithRuleFieldName("Edad"))
                .AddRule(name, x => x.IsName("Nombre"))
                .AddRule(email, x => x.IsEmailWithMessage("El correo eléctronico tiene un formato incorrecto."))
                .Throw();

            Id = Guid.NewGuid();
            Age = age;
            Name = name;
            Email = email;
        }

        public Person ChangeName(string name)
        {
            Validator.AddRule(name, x => x.IsName("Nombre")).Throw();

            Name = name;

            return this;
        }

        public Guid Id { get; private set; }
        public int Age { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
    }
}
