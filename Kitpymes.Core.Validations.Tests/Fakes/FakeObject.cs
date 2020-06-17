using System;
using System.Collections.Generic;

namespace Kitpymes.Core.Validations.Tests
{
    public class FakeObject
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public string? Hostname { get; set; }
        public int? Point { get; set; }
        public List<string>? Roles { get; set; }
	}
}
