using System;
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
        /// <summary>
        /// /
        /// </summary>
        /// <param name="image"></param>
        /// <param name="UploadedImage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveImages(ImageUploadTable image, HttpPostedFileBase uploadedimage)
        {
            if (uploadedimage != null && uploadedimage.ContentLength > 0)
            {
                try
                {
                    UploadImageEntities DbCOntext = new UploadImageEntities();
                    // Byte Conversion
                    image.ByteValue = new byte[uploadedimage.ContentLength];
                    uploadedimage.InputStream.Read(image.ByteValue, 0, uploadedimage.ContentLength);
                    image.ImageFileName = uploadedimage.FileName;
                    //Check if byte value already exists in the table and assign the value to filecheck
                    var filecheck = DbCOntext.ImageUploadTables.Where(i => i.ByteValue == image.ByteValue).FirstOrDefault();
                    if (filecheck != null)
                    {
                        TempData["ErrorMessage"] = "Image Already Exists";
                        TempData["SuccessMessage"] = "";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "";
                        //Save image into database
                        DbCOntext.ImageUploadTables.Add(image);
                        DbCOntext.SaveChanges();
                        TempData["SuccessMessage"] = "Uploaded Successfully";

                    }
                }
                catch (Exception e)
                {

                }
            }

            return RedirectToAction("SaveImages");
        }

        public ActionResult Delete(int id)
        {
            UploadImageEntities DbCOntext = new UploadImageEntities();
            ImageUploadTable image = DbCOntext.ImageUploadTables.Where(x => x.Id == id).FirstOrDefault();
            DbCOntext.ImageUploadTables.Remove(image);
            DbCOntext.SaveChanges();
            return View("SaveImages");
        }
    }
}
