using System;
using System.ComponentModel;
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
                Trace.WriteLine(data);
            }

            var templateDoc = new WmlDocument(Path.Combine(_hostingEnvironment.ContentRootPath, "briefTemplate.docx"));
            var assembledDoc = DocumentAssembler.AssembleDocument(templateDoc, data, out bool isTemplateError);
            var briefId = Guid.NewGuid().ToString("N");
            assembledDoc.SaveAs(Path.Combine(_hostingEnvironment.ContentRootPath, $"briefs/{briefId}.docx"));

            var result = new BriefGenerationResult
            {
                Id = briefId
            };

            return Created($"briefs/{briefId}.docx", result);
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