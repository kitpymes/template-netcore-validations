using System;
using System.Collections.Generic;

namespace Kitpymes.Core.Validations.FluentValidation.Tests
{
    public static class FakeList
    {
        public static List<string>? Null => null;

        public static List<string> NotNullCountZero => new List<string>();

        public static List<string> NotNull(int count = 3)
        {
            var list = new List<string>(count);

            for (int i = 0; i < count; i++)
            {
                list.Add(Guid.NewGuid().ToString());
            }

            return list;
        }
     }
}
