using EventsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateEvent(Event eventViewModel)
        {
            IGraph gr = new Graph();

            if (System.IO.File.Exists("Events.nt"))
            {
                NTriplesParser ntparser = new();
                ntparser.Load(gr, "Events.nt");
            }

            gr.NamespaceMap.AddNamespace("dc", new Uri("http://purl.org/dc/elements/1.1/"));
            gr.NamespaceMap.AddNamespace("dbp", new Uri("https://dbpedia.org/ontology/"));



            string guid = Guid.NewGuid().ToString();

            string eventLink = "https://localhost:4200/events/" + guid;
            eventViewModel.Identifier = eventLink;

            IUriNode eventNode = gr.CreateUriNode(UriFactory.Create(eventLink));

            IUriNode hasTitle = gr.CreateUriNode(UriFactory.Create("dc:title"));
            ILiteralNode title = gr.CreateLiteralNode(eventViewModel.Title);

            IUriNode hasDescription = gr.CreateUriNode(UriFactory.Create("dc:description"));
            ILiteralNode description = gr.CreateLiteralNode(eventViewModel.Description);

            //IUriNode onDate = gr.CreateUriNode(UriFactory.Create("dc:date"));
            //ILiteralNode date = gr.CreateLiteralNode(eventViewModel.DateTime.ToString(), new Uri(XmlSpecsHelper.XmlSchemaDataTypeDateTime));

            IUriNode hasPrice = gr.CreateUriNode(UriFactory.Create("dbp:price"));
            ILiteralNode price = gr.CreateLiteralNode(eventViewModel.Price.ToString(), new Uri(XmlSpecsHelper.XmlSchemaDataTypeDouble));


            gr.Assert(new Triple(eventNode, hasTitle, title));
            gr.Assert(new Triple(eventNode, hasDescription, description));
            //gr.Assert(new Triple(eventNode, onDate, date));
            gr.Assert(new Triple(eventNode, hasPrice, price));

            NTriplesWriter ntwriter = new();
            ntwriter.Save(gr, "Events.nt");

            return Ok();
        }
    }
}
