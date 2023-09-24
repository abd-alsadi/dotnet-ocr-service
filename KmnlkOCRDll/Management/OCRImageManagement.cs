
using KmnlkCommon.Shareds;
using KmnlkOCRDll.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KmnlkCommon.Shareds.LoggerManagement;
using System.Drawing;
using KmnlkOCRDll.Constants;
using System.IO;
using KmnlkOCRDll.Exceptions;
using Tesseract;
//using Tesseract;

namespace KmnlkOCRDll.Management
{
    public class OCRImageManagement : IValidationOperations
    {
        private ILog logger;

        public OCRImageManagement(ILog logger)
        {
            this.logger = logger;
        }

        public string extractTextFromImage(string tessdataPath,Bitmap img,Rectangle rect, string lang = "eng")
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (!isValid(img))
                {
                    return null;
                }


                string result = "";

                var ocr = new TesseractEngine(tessdataPath, lang,EngineMode.Default);
                if (rect != null) {
                    Rect rectBox = new Rect(rect.X, rect.Y, rect.Width, rect.Height);
                    Page page = ocr.Process(img,rectBox);
                    result = page.GetText();
                }
                else
                {
                    Page page = ocr.Process(img);
                    result = page.GetText();
                }
         


                logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                return result;
            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return null;
            }
        }


        public bool isValid(object model)
        {
            bool result = true;
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);

            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
            return result;
        }
    }
}
