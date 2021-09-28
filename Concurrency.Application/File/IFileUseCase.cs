namespace Concurrency.Application.File
{
    using System;
    using System.Threading.Tasks;

    public interface IFileUseCase
    {
        Task<int> ProcessFileAsync(Guid observationId);
    }
}
