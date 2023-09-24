using KmnlkOCRApi.Constants;
using KmnlkOCRApi.Exceptions;
using KmnlkOCRApi.Management;
using KmnlkOCRApi.Models;
using KmnlkCommon.Shareds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static KmnlkCommon.Shareds.LoggerManagement;

namespace KmnlkOCRApi.Controllers
{
    public class OCRImageController : ApiController
    {
        private PackageManagement package = null;

        public OCRImageController(PackageManagement repo)
        {
            package = repo;
        }


        [HttpPost]
        [ActionName("GetTextFromImage")]
        public async Task<HttpResponseMessage> GetTextFromImage([FromUri]int x=0, [FromUri]int y=0, [FromUri]int width=0, [FromUri]int height=0,[FromUri]string lang="eng")
        {
            package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstants.MSG_SUCCESS);
            string startTime = DateTime.Now.ToString("hh:mm:ss");
            string endTime = "";
            HttpResponseMessage res;
            try
            {
                string dataFolder = SettingsManagement.getSetting(SettingsManagement.KEY_DataFolder).ToString();
                dataFolder=Path.Combine(dataFolder, "TempFolder");
                var provider = new MultipartFormDataStreamProvider(dataFolder);
                await Request.Content.ReadAsMultipartAsync(provider);
                string text = package.extractTextFromImage(provider, new Rectangle(x,y,width,height), lang);
                endTime = DateTime.Now.ToString("hh:mm:ss");
                var response = new ResponseModel(text, modConstants.MSG_SUCCESS, HttpStatusCode.OK, startTime, endTime);
                res = Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
                package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstants.MSG_SUCCESS);
                return res;
            }
            catch (Exception e)
            {
                new ApiException(package.logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                var response = new ResponseModel(null, e.Message, HttpStatusCode.BadRequest, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }

        }

        [NonAction]
        public bool isValid(string uid)
        {
            return true;
        }
    }
}
