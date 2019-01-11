using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UplDispSaveImg.Controllers
{
    public class UploadController : Controller
    {
        [HttpGet]
        public ActionResult SaveImages()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveImages(ImageUploadTable table, HttpPostedFileBase UploadedImage)
        {
            if (UploadedImage != null && UploadedImage.ContentLength > 0)
            {
                UploadImageEntities DbCOntext = new UploadImageEntities();
                // Byte Conversion
                table.ByteValue = new byte[UploadedImage.ContentLength];
                UploadedImage.InputStream.Read(table.ByteValue, 0, UploadedImage.ContentLength);
                table.ImageFileName = UploadedImage.FileName;
                //Check if byte value already exists in the table and assign the value to filecheck
                var filecheck = DbCOntext.ImageUploadTables.Where(i => i.ByteValue == table.ByteValue).FirstOrDefault();
                if (filecheck != null)
                {
                    ViewBag.ErrorMessage = "Image Already Exists";
                    ViewBag.SuccessMessage = "";
                }
                else
                {
                    ViewBag.ErrorMessage = "";
                    //Save image into database
                    DbCOntext.ImageUploadTables.Add(table);
                    DbCOntext.SaveChanges();
                    ViewBag.SuccessMessage = "Uploaded Successfully";

                }
            }

            return View("SaveImages");
        }
    }
}
