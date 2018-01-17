using System;
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
        private const string DocxMimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        private readonly IHostingEnvironment _env;

        private readonly EmailService emailService = new EmailService();

        public BriefController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public IActionResult GenerateDocument([FromBody] BriefInfo value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BriefExport export = new BriefExport(value);

            XElement data;
            using (var dataStream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(BriefExport));
                using (var writer = XmlDictionaryWriter.CreateTextWriter(dataStream, Encoding.UTF8, false))
                {
                    serializer.WriteObject(writer, export);
                }

                dataStream.Position = 0;
                data = XElement.Load(dataStream);
            }

            var templateDoc = new WmlDocument(_env.ContentRootFileProvider.GetFileInfo("briefTemplate.docx").PhysicalPath);
            var assembledDoc = DocumentAssembler.AssembleDocument(templateDoc, data, out bool isTemplateError);
            var id = Guid.NewGuid().ToString("N");
            assembledDoc.SaveAs(Path.Combine(_env.ContentRootPath, $"briefs/{id}.docx"));

            var result = new BriefGenerationResult
            {
                Id = id
            };

            return Created($"briefs/download/{id}.docx", result);
        }

        [HttpGet("download/{id}")]
        public IActionResult DownloadBrief(string id)
        {
            var briefFileInfo = _env.ContentRootFileProvider.GetFileInfo($"briefs/{id}.docx");
            if (briefFileInfo.Exists && !briefFileInfo.IsDirectory)
            {
                return PhysicalFile(briefFileInfo.PhysicalPath, DocxMimeType, "brief.docx");
            }

            return NotFound();
        }

        [HttpPost("email/{id}")]
        public IActionResult EmailBrief(string id, [FromBody] EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState.ValidationState);
            }

            var briefPath = Path.Combine(_env.ContentRootPath, $"briefs/{id}.docx");
            if (System.IO.File.Exists(briefPath))
            {
                emailService.SendEmail(emailRequest.Email, briefPath);
            }

            return NotFound();
        }
    }
}