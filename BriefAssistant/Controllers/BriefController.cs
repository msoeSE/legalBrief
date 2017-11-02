using System;
using System.ComponentModel;
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

        //TODO: JACOB ADD READONLY VARIABLE WITH EMAIL SERVICE HERE

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

            value.AppellateCase = new AppellateCase
            {
                District = GetDistrictFromCounty(value.CircuitCourtCase.County)
            };

            value.Date = DateTime.Now.ToShortDateString();

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

        private District GetDistrictFromCounty(County county)
        {
            switch (county)
            {
                case County.Milwaukee:
                    return District.One;
                case County.Calumet:
                case County.FondDuLac:
                case County.GreenLake:
                case County.Kenosha:
                case County.Manitowoc:
                case County.Ozaukee:
                case County.Racine:
                case County.Sheboygan:
                case County.Walworth:
                case County.Washington:
                case County.Waukesha:
                case County.Winnebago:
                    return District.Two;
                case County.Ashland:
                case County.Barron:
                case County.Bayfield:
                case County.Brown:
                case County.Buffalo:
                case County.Burnett:
                case County.Chippewa:
                case County.Door:
                case County.Douglas:
                case County.Dunn:
                case County.EauClaire:
                case County.Florence:
                case County.Forest:
                case County.Iron:
                case County.Kewaunee:
                case County.Langlade:
                case County.Lincoln:
                case County.Marathon:
                case County.Marinette:
                case County.Menominee:
                case County.Oconto:
                case County.Oneida:
                case County.Outagamie:
                case County.Pepin:
                case County.Pierce:
                case County.Polk:
                case County.Price:
                case County.Rusk:
                case County.Sawyer:
                case County.Shawano:
                case County.StCroix:
                case County.Taylor:
                case County.Trempealeau:
                case County.Vilas:
                case County.Washburn:
                    return District.Three;
                case County.Adams:
                case County.Clark:
                case County.Columbia:
                case County.Crawford:
                case County.Dane:
                case County.Dodge:
                case County.Grant:
                case County.Green:
                case County.Iowa:
                case County.Jackson:
                case County.Jefferson:
                case County.Juneau:
                case County.LaCrosse:
                case County.Lafayette:
                case County.Marquette:
                case County.Monroe:
                case County.Portage:
                case County.Richland:
                case County.Rock:
                case County.Sauk:
                case County.Vernon:
                case County.Waupaca:
                case County.Waushara:
                case County.Wood:
                    return District.Four;
                default:
                    throw new InvalidEnumArgumentException(nameof(county), (int)county, typeof(County));
            }
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
                //TODO: JACOB ADD METHOD FOR EMAIL HERE
            }

            return NotFound();
        }
    }
}