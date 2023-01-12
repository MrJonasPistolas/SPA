using EXM.Common.Models;

namespace EXM.Services.Contracts
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}
