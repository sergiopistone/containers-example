using Microsoft.AspNetCore.Mvc;
using NLipsum.Core;

namespace LoremIpsum.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SentencesController : ControllerBase
    {
        [HttpGet]
        [Route("sentences/{count}")]
        public IEnumerable<string> Get(uint count)
        {
            var generator = new LipsumGenerator();
            return generator.GenerateSentences((int)count, new Sentence(20, 40));
        }
    }
}
