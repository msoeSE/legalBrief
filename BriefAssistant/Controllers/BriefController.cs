using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BriefAssistant.Models;
using Microsoft.AspNetCore.Mvc;
using OpenXmlPowerTools;

namespace BriefAssistant.Controllers
{
    [Route("api/[controller]")]
    public class BriefController : Controller
    {
        private const string TeplatePath = "./briefTemplate.docx";
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

                Trace.Write(Encoding.UTF8.GetString(dataStream.ToArray()));

                data = XElement.Load(dataStream);
            }

            var templateDoc = new WmlDocument(TeplatePath);
            var assembledDoc = DocumentAssembler.AssembleDocument(templateDoc, data, out bool isTemplateError);
            var briefId = Guid.NewGuid().ToString("N");
            assembledDoc.SaveAs($"./briefs{briefId}.docx");

            var result = new BriefGenerationResult
            {
                Id = briefId
            };

            return Created($"/briefs/{briefId}.docx", result);
        }

        [HttpGet("download/{id}")]
        public IActionResult Download(string id)
        {
            if (System.IO.File.Exists($"./briefs/{id}.docx"))
            {
                try
                {
                    return PhysicalFile($"./briefs/{id}.docx", "application/octet-stream");
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
