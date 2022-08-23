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

            string guid = Guid.NewGuid().ToString();

            string eventLink = "https://localhost:4200/events/" + guid;
            eventViewModel.Identifier = eventLink;

            IUriNode eventNode = gr.CreateUriNode(UriFactory.Create(eventLink));

            IUriNode hasTitle = gr.CreateUriNode(UriFactory.Create("dc:title"));
            ILiteralNode title = gr.CreateLiteralNode(eventViewModel.Title);

            IUriNode hasDescription = gr.CreateUriNode(UriFactory.Create("dc:description"));
            ILiteralNode description = gr.CreateLiteralNode(eventViewModel.Description);

            IUriNode onDate = gr.CreateUriNode(UriFactory.Create("dc:date"));
            ILiteralNode date = gr.CreateLiteralNode(eventViewModel.DateTime.ToString());


            //dodadi gi truples vo lista

            gr.Assert(new Triple(eventNode, hasTitle, title));
            gr.Assert(new Triple(eventNode, hasDescription, description));
            gr.Assert(new Triple(eventNode, onDate, date));

            NTriplesWriter ntwriter = new();
            ntwriter.Save(gr, "Events.nt");

            return Ok();
        }
    }
}
