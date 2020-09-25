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


namespace XmlRuleHelper.Controllers
{
    public class HomeController : Controller
    {
        static XElement purchaseOrder;

        public ActionResult Index()
        {
            //Get XML Content
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            var purchaseOrderFilepath = "C:\\xmlHelper\\xmlHelper\\xmlHelper\\bin\\Debug\\HELLCAT13_TGL_U_X00_MB_20191227.xml";//Path.Combine(currentDirectory, "HELLCAT13_TGL_U_X00_MB_20191227.xml");//改手動匯入
            purchaseOrder = XElement.Load(purchaseOrderFilepath);

            List<SelectListItem> inputElement = new List<SelectListItem>();
            string sourceElement = string.Empty;
            sourceElement += "<select class = \"form - control\">";
            foreach (var item in purchaseOrder.Elements().OfType<XElement>().Select(x => x.Name).Distinct())
            {
                //inputElement.Add(new SelectListItem() { Text = item.ToString(), Value = item.ToString() });
                sourceElement += string.Format("<option value=\"{0}\">{1}</option>", item.ToString(), item.ToString());
            }
            sourceElement += "</select>";
            ViewBag.sourceElement = HttpUtility.HtmlDecode(sourceElement);//inputElement;
            return View();
        }

        [HttpPost]
        public ActionResult GetAttributeData(/*tring element*/)
        {
            string resJson = string.Empty;
            IEnumerable<XElement> data = from c in purchaseOrder.Descendants() select c;
            var qq = data.OfType<XElement>().Attributes().Select(c => c.Name.LocalName).OrderBy(c => c.ToString());
            resJson += "<select id = \"inputAttribute\" class = \"form-control\">";
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
                //List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();                
                //Dictionary<string, string> dic = null;
                //foreach (var item in purchaseOrder.Elements().OfType<XElement>().Select(x => x.Name).Distinct())
                //{
                //    dic = new Dictionary<string, string>();
                //    dic.Add("KEY", item.ToString());
                //    dic.Add("TEXT", item.ToString());
                //    dicList.Add(dic);
                //}
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //res = jss.Serialize(dicList);
                //return res;
                sourceElement += "<select class = \"form-control\">";
                foreach (var item in purchaseOrder.Elements().OfType<XElement>().Select(x => x.Name).Distinct())
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