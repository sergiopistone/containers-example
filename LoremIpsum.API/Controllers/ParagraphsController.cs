using Microsoft.AspNetCore.Mvc;
using NLipsum.Core;

namespace LoremIpsum.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParagraphsController : ControllerBase
    {
        [HttpGet]
        [Route("{count}")]
        public IEnumerable<string> Get(uint count)
        {
            var generator = new LipsumGenerator();
            return generator.GenerateParagraphs((int)count, new Paragraph(1, 10));
        }
    }
}
