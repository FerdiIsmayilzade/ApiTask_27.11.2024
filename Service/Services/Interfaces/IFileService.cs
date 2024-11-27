using Microsoft.AspNetCore.Http;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IFileService
    {
        Task<UploadResponse> UploadAsync(IFormFile file);
        void DeleteFile(string path);
    }
}
