﻿using CatFactory.CodeFactory;
using CatFactory.Diagnostics;

namespace CatFactory.Tests.Models
{
    public class ProjectSettings : IProjectSettings
    {
        public virtual ValidationResult Validate()
            => null;
    }
}
