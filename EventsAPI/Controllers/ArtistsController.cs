using EventsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateArtist(Artist artistViewModel)
        {
            IGraph gr = new Graph();

            if (System.IO.File.Exists("Artists.nt"))
            {
                NTriplesParser ntparser = new();
                ntparser.Load(gr, "Artists.nt");
            }

            gr.NamespaceMap.AddNamespace("dc", new Uri("http://purl.org/dc/elements/1.1/"));
            gr.NamespaceMap.AddNamespace("foaf", new Uri("http://xmlns.com/foaf/0.1/"));

            IUriNode artistNode = gr.CreateUriNode(UriFactory.Create(artistViewModel.Identifier));
            


            return Ok();
        }
    }
}
