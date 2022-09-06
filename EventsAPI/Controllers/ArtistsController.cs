using EventsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateArtist(Artist artist)
        {
            IGraph gr = new Graph();

            if (System.IO.File.Exists("Artists.nt"))
            {
                NTriplesParser ntparser = new();
                ntparser.Load(gr, "Artists.nt");
            }

            gr.NamespaceMap.AddNamespace("dc", new Uri("http://purl.org/dc/elements/1.1/"));
            gr.NamespaceMap.AddNamespace("foaf", new Uri("http://xmlns.com/foaf/0.1/"));

            string guid = Guid.NewGuid().ToString();

            string artistLink = "https://localhost:4200/hosts/" + guid;
            artist.Identifier = artistLink;

            IUriNode artistNode = gr.CreateUriNode(UriFactory.Create(artist.Identifier));

            //try querying
            IGraph gr2 = new Graph();
            UriLoader.Load(gr2, new Uri("https://dbpedia.org/page/Coldplay.nt"));
            NTriplesWriter ntwriter2 = new();
            ntwriter2.Save(gr2, "DBpedia.nt");


            return Ok();
        }
    }
}
