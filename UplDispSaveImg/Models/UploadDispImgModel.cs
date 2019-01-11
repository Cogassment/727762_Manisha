using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UplDispSaveImg.Models
{
    public class UploadDispImgModel
    {
        public int Id { get; set; }
        public byte[] ByteValue  { get; set; }
        public string ImageFileName { get; set; }
    }
}