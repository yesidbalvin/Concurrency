namespace Concurrency.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public class Observation
    {
        public Guid Id { get; set; }
        public ICollection<File> Files { get; set; }
    }
}
