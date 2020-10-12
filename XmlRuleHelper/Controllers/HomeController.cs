using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;
//using LinqKit;
//using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

using System.Configuration;
using System.Threading.Tasks;
using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Azure.Storage.Sas;
using Microsoft.Azure.Storage.Blob;

namespace XmlRuleHelper.Controllers
{
    public class HomeController : Controller
    {
        static XElement purchaseOrder;

        public ActionResult Index()
        {
            try
            {
                var purchaseOrderFilepath = Path.Combine(Server.MapPath("~\\App_Data\\"), "HELLCAT13_TGL_U_X00_MB_20191227.xml");//"C:\\xmlHelper\\xmlHelper\\xmlHelper\\bin\\Debug\\HELLCAT13_TGL_U_X00_MB_20191227.xml";//Path.Combine(currentDirectory, "HELLCAT13_TGL_U_X00_MB_20191227.xml");//改手動匯入
                purchaseOrder = XElement.Load(purchaseOrderFilepath);

                List<SelectListItem> inputElement = new List<SelectListItem>();
                string sourceElement = string.Empty;
                sourceElement += "<select class = \"form - control\">";
                foreach (var item in purchaseOrder.Elements().OfType<XElement>().Select(c => c.Name).Distinct())
                {
                    //inputElement.Add(new SelectListItem() { Text = item.ToString(), Value = item.ToString() });
                    sourceElement += string.Format("<option value=\"{0}\">{1}</option>", item.ToString(), item.ToString());
                }
                sourceElement += "</select>";
                ViewBag.sourceElement = HttpUtility.HtmlDecode(sourceElement);//inputElement;
                return View();
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }

            ////Dowload XML From Azure File Storage(Has file access deny Issue!!!)
            //string connectionString = @"DefaultEndpointsProtocol=https;AccountName=kikikokoazurestorage;AccountKey=jinDMgNi4n3x5TI0nOFcmSYr/cSdgUwPMQz86WwA6Z1XnNgTTLnzQG2rL5v1Qb1B03oL4xLbAYB1CQjKw47KIQ==;EndpointSuffix=core.windows.net";
            //string fileName = "HELLCAT13_TGL_U_X00_MB_20191227.xml";
            //string localPath = string.Format("D:\\WistronXmlRuleHelper\\xmls");
            //string fullFileName = string.Empty;
            //ShareClient share = new ShareClient(connectionString, "wistronfiles");
            //share.CreateIfNotExistsAsync();
            //if (share.Exists())
            //{
            //    ShareDirectoryClient cloudDirectory = share.GetDirectoryClient("xmls");
            //    ShareFileClient file = cloudDirectory.GetFileClient(fileName);
            //    if (file.Exists())
            //    {
            //        try
            //        {
            //            ShareFileDownloadInfo download = file.Download();
            //            //Save the data to a local file, overwrite if the file already exists
            //            if (!Directory.Exists(localPath))
            //            {
            //                try
            //                {
            //                    Directory.CreateDirectory(localPath);
            //                }
            //                catch (Exception e)
            //                {
            //                    return Content(e.ToString());
            //                }
            //            }
            //            fullFileName = localPath + fileName;
            //            using (FileStream stream = System.IO.File.OpenWrite(fullFileName))
            //            {
            //                download.Content.CopyTo(stream);
            //                stream.Flush();
            //                stream.Close();

            //                // Display where the file was saved
            //                Console.WriteLine($"File downloaded: {stream.Name}");
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            return Content(e.ToString());
            //        }
            //    }
            //}
        }

        [HttpPost]
        public ActionResult GetAttributeData(/*tring element*/)
        {
            string resJson = string.Empty;
            IEnumerable<XElement> data = from c in purchaseOrder.Descendants() select c;
            var qq = data.OfType<XElement>().Attributes().Select(c => c.Name.LocalName).OrderBy(c => c.ToString());
            resJson += "<select class = \"form-control sel-attribute\" style=\"width:auto;\">";
            resJson += "<option value=\"TagName\">節點名稱</option>";
            foreach (var itemAttr in qq.Distinct())
            {
                resJson += string.Format("<option value=\"{0}\">{1}</option>", itemAttr, itemAttr);
            }
            resJson += "</select>";
            return Content(resJson);
        }

        /// <summary>
        /// 設定加入類別
        /// </summary>
        /// <param name="srcDict"></param>
        /// <returns></returns>
        [HttpPost]
        public String getSourceElement(Dictionary<String, String> srcDict)
        {
            string sourceElement = "";
            try
            {
                sourceElement += "<select class = \"form-control sel-source-element\" style=\"width:auto;\">";
                foreach (var item in purchaseOrder.Elements().OfType<XElement>().Select(x => x.Name).Where(c => c.LocalName != "Profile").Distinct())
                {
                    //inputElement.Add(new SelectListItem() { Text = item.ToString(), Value = item.ToString() });
                    sourceElement += string.Format("<option value=\"{0}\">{1}</option>", item.ToString(), item.ToString());
                }
                sourceElement += "</select>";
            }
            catch (Exception)
            {

            }
            return sourceElement;
        }
    }
}