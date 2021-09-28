namespace Concurrency.Application.File
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Models;
    using Domain.RepositoryInterfaces;
    using Services;

    public class FileUseCase : IFileUseCase
    {
        private readonly IFile _fileRepository;
        private readonly ILinkObservationFileService _linkObservationFileService;

        public FileUseCase(IFile file, ILinkObservationFileService linkObservationFileService)
        {
            _fileRepository = file;
            _linkObservationFileService = linkObservationFileService;
            Console.WriteLine("New instance of FileUSeCase");
        }

        public async Task<int> ProcessFileAsync(Guid observationId)
        {
            var fileResult = 0;
            while(true)
            {
                var file = new File
                {
                        FileType = "LicensePlateImage",
                        FileMetadata = new List<Metadata>
                        {
                                new()
                                {
                                        Id = Guid.NewGuid(), Key = "ObservationId", Value = observationId.ToString()
                                }
                        }
                };
                fileResult = await _fileRepository.SaveAsync(file);
                await _linkObservationFileService.ProcessAsync(observationId);
            }
            
            return fileResult;
        }
    }
}
