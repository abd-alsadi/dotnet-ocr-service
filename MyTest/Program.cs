
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KmnlkCommon.Shareds.LoggerManagement;

namespace MyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DBLogger log = new DBLogger(@"E:\Pro\KmnlkBarcode\KmnlkBarcodeApi\Output\Log\Local\");

            //BussinessOCRManagement mnager = new BussinessOCRManagement(log);
            //Bitmap img = new Bitmap(@"F:\Capture.PNG");
            //string tt = mnager.extractTextFromImage(img,new Rectangle(),"eng");
            //string xxxx;
        }
    }
}
