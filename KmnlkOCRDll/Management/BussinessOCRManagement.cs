using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using static KmnlkCommon.Shareds.LoggerManagement;

namespace KmnlkOCRDll.Management
{
    public class BussinessOCRManagement
    {
        private OCRImageManagement TEM;
        public ILog logger;
        public BussinessOCRManagement(ILog logger)
        {
            this.logger = logger;
            this.TEM = new OCRImageManagement(logger);
        }
        public string extractTextFromImage(string tessdataPath,Bitmap img, Rectangle rect, string lang)
        {
            return TEM.extractTextFromImage(tessdataPath,img, rect,lang) ;
        }
    }
}