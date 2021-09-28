namespace Concurrency.Application.Services
{
    using System;
    using System.Threading.Tasks;

    public interface ILinkObservationFileService
    {
        Task ProcessAsync(Guid observationId);
    }
}
