
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using static KmnlkOCRApi.Constants.Enums;
using static KmnlkCommon.Shareds.LoggerManagement;
using KmnlkOCRDll.Management;

namespace KmnlkOCRApi.Management
{
    public class PackageManagement
    {
        private BussinessOCRManagement manager;
        public ILog logger;

        public PackageManagement()
        {
            string pathLog = SettingsManagement.getSetting(SettingsManagement.KEY_PathLog).ToString();
            string typeLog = SettingsManagement.getSetting(SettingsManagement.KEY_TypeLog).ToString();
           
            switch (typeLog.ToLower())
            {
                case "file":
                    logger = new FileLogger(pathLog);
                    break;
                case "db":
                    logger = new DBLogger(pathLog);
                    break;
                default:
                    logger = new FileLogger(pathLog);
                    break;
            }
            manager = new BussinessOCRManagement(logger);
        }
        public string extractTextFromImage(MultipartFormDataStreamProvider provider, Rectangle rect, string lang)
        {
            Guid guid = Guid.NewGuid();
            string newId = guid.ToString();
            string tessDataPath = SettingsManagement.getSetting(SettingsManagement.KEY_TessDataPath).ToString().ToLower();
            string dataFolder = SettingsManagement.getSetting(SettingsManagement.KEY_DataFolder).ToString();
            dataFolder = Path.Combine(dataFolder, "UploadFolder");
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
            dataFolder = Path.Combine(dataFolder, newId);
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
            string result = "";
            foreach (var file in provider.FileData)
            {
                var name = file.Headers.ContentDisposition.FileName;
                name = name.Trim('"');
                var locationFileName = file.LocalFileName;
                var filePath = Path.Combine(dataFolder, newId+Path.GetExtension(name));

                File.Copy(locationFileName, filePath);
                Bitmap img = new Bitmap(filePath);
                result += manager.extractTextFromImage(tessDataPath, img, rect, lang);
            }
            return result;

          
        }
       
    }
}