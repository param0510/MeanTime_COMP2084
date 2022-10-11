namespace MeanTime.Data
{
    public class UtilityClass
    {
        // Copy it to the other controller it will not work for other controller due to location problem
        public static string UploadImage(IFormFile Image)
        {
            // Get the location of temp file upload
            var filePath = Path.GetTempFileName();

            // create a unique file name using GUID
            var fileName = Guid.NewGuid().ToString() + "-" + Image.FileName;

            // dynamically setting up the destination path for the image to be uploaded
            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\img\\genres\\" + fileName;

            // upload the file to the server using filestream
            using(var stream = new FileStream(uploadPath, FileMode.Create))
            {
                Image.CopyTo(stream);
            }
            // return the file name for saving it to the database
            return fileName;
        }
    }
}
