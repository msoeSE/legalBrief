using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BriefAssistant.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OpenXmlPowerTools;

namespace BriefAssistant.Controllers
{
    [Route("api/[controller]")]
    public class BriefController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public BriefController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult Post([FromBody]BriefInfo value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            XElement data;
            using (var dataStream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(BriefInfo));
                using (var writer = XmlDictionaryWriter.CreateTextWriter(dataStream, Encoding.UTF8, false))
                {
                    serializer.WriteObject(writer, value);
                }

                dataStream.Position = 0;
                data = XElement.Load(dataStream);
            }

            var templateDoc = new WmlDocument(Path.Combine(_hostingEnvironment.ContentRootPath, "briefInfo.docx"));
            var assembledDoc = DocumentAssembler.AssembleDocument(templateDoc, data, out bool isTemplateError);
            var briefId = Guid.NewGuid().ToString("N");
            assembledDoc.SaveAs(Path.Combine(_hostingEnvironment.ContentRootPath, $"briefs/{briefId}.docx"));

            var result = new BriefGenerationResult
            {
                Id = briefId
            };

            return Created($"briefs/{briefId}.docx", result);
        }

        [HttpGet("download/{id}")]
        public IActionResult Download(string id)
        {
            var briefPath = Path.Combine(_hostingEnvironment.ContentRootPath, $"briefs/{id}.docx");
            if (System.IO.File.Exists(briefPath))
            {
                try
                {
                    return PhysicalFile(briefPath, "application/octet-stream");
                }
                catch (ArgumentException)
                {
                    return BadRequest();
                }
            }

            return NotFound();
        }
    }
}
