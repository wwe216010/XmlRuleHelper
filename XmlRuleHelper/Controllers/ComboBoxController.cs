using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace XmlRuleHelper.Controllers
{
    public class ComboBoxController : Controller
    {
        /// <summary>
        /// comboBox資料的進入點
        /// </summary>
        /// <param name="data">json的資料</param>
        /// <returns></returns>
        public ActionResult CommonQuery(string data)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<String, String> dict = jss.Deserialize<Dictionary<String, String>>(data);
            string str = jss.ToString();
            switch (dict["type"])
            {
                case "Code":
                    return Content(getOption_Type(dict));
                default:
                    return Content("");
            }
        }

        /// <summary>
        /// 設定加入類別
        /// </summary>
        /// <param name="srcDict"></param>
        /// <returns></returns>
        private String getOption_Type(Dictionary<String, String> srcDict)
        {
            String res = "[]";
            try
            {
                List<Dictionary<String, String>> list = new List<Dictionary<String, String>>();
                Dictionary<String, String> dict;

                dict = new Dictionary<String, String>();
                dict.Add("KEY", "ALIAS");
                dict.Add("TEXT","別名");                
                list.Add(dict);

                dict = new Dictionary<String, String>();
                dict.Add("KEY", "CONDITION");
                dict.Add("TEXT", "條件");
                list.Add(dict);

                JavaScriptSerializer jss = new JavaScriptSerializer();
                res = jss.Serialize(list);

            }
            catch (Exception)
            {

            }
            return res;
        }
    }
}