using Microsoft.AspNetCore.Hosting;

namespace Henry.Helpers
{
    /// <summary>
    /// Processes the uploaded file by generating a unique name and copying it to the BoatImgs folder
    /// </summary>
    /// <returns>File name as a string</returns>
    public static class FileHelpers
    {
        public static string ProcessUploadedFile(string path, IFormFile Photo, IWebHostEnvironment webHostEnvironment)
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, path);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
