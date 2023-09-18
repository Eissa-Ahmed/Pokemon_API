using Microsoft.AspNetCore.Http;

namespace Pokemon.DAL.Helper
{
    public static class FileManager
    {
        static public async Task<string> UploadImage(IFormFile file)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Images");
            var FileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
            var FinalPath = Path.Combine(FolderPath, FileName);
            using (var stream = new FileStream(FinalPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return FileName;
            }
        }
        static public bool RemoveImage(string ImageName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files/Images", ImageName);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
