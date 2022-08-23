using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateHost(Models.Host hostViewModel)
        {
            IGraph gr = new Graph();

            if (System.IO.File.Exists("Hosts.nt"))
            {
                NTriplesParser ntparser = new();
                ntparser.Load(gr, "Hosts.nt");
            }

            gr.NamespaceMap.AddNamespace("dc", new Uri("http://purl.org/dc/elements/1.1/"));

            IUriNode hostNode = gr.CreateUriNode(UriFactory.Create(hostViewModel.Identifier));



            return Ok();
        }
    }
}
