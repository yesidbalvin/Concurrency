namespace Concurrency.Domain.RepositoryInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IFile
    {
        Task<int> SaveAsync(File file);
        Task<IEnumerable<File>> GetFileByObservationId(Guid observationId);
        Task UpdateFileAsync(Guid observationId);
    }
}
