using EXM.Base.Requests;

namespace EXM.Base.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}
