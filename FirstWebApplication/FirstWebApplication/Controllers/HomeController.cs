using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FirstWebApplication.Models;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace FirstWebApplication.Controllers
{
    public class HomeController : Controller
    {
        
        [HttpGet]
        public IActionResult Index()
        {
            var model = new XmlModel
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now
            };
                   
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(XmlModel xmlModel)
        {
            #region My code - My Business

            try
            {
                var Submit = new XmlSerializer(typeof(XmlModel));
                var xml = string.Empty;

                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        Submit.Serialize(writer, xmlModel);
                        xml = sww.ToString();
                    }
                }

                var data = Encoding.UTF8.GetBytes(xml);

                var path = @"C:\Users\hhraesha\Downloads\Example.xml";

                using (var stream = System.IO.File.Create(path))
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                var exModel = new ErrorViewModel();
                exModel.Explain = ex.Message;
            }

            #endregion

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
