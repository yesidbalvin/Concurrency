namespace Concurrency.Domain.Models
{
    using System;
    using System.Collections.Generic;

    public class File
    {
        public Guid Id { get; set; }
        public string FileType { get; set; }
        public ICollection<Metadata> FileMetadata { get; set; }
    }
}
