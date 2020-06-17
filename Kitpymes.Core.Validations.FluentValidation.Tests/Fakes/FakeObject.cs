using System;
using System.Collections.Generic;

namespace Kitpymes.Core.Validations.FluentValidation.Tests
{
    public class FakeObject
    {
        public const string MESSAGE = "Esto es un mensaje";
        public const string FIELD_NAME = "FIELD_NAME";
        public const string COMPARE_FIELD_NAME = "COMPARE_FIELD_NAME";
        public const string RULE_FIELD_NAME = "RULE_FIELD_NAME";
        public const string CUSTOM_FIELD_NAME = "CUSTOM_FIELD_NAME";
        public const long MAX = long.MaxValue;
        public const long MIN = long.MinValue;

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public string? Subdomain { get; set; }
        public string? DirectoryPath { get; set; }
        public string? FilePath { get; set; }
        public string? FilePathExtension { get; set; }
        public int? Point { get; set; }
        public List<string>? Roles { get; set; }
        public List<string>? Permissions { get; set; }
    }
}
