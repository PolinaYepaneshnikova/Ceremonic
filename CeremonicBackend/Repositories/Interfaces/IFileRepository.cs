using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;

namespace CeremonicBackend.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Task<PhysicalFileResult> GetByName(string folderName, string fileName);

        Task<string> Add(string folderName, IFormFile file);

        Task Delete(string folderName, string fileName);
    }
}
