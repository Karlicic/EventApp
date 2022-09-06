using EventsAPI.Models;
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
        public IActionResult CreateEvent(Event @event)
        {
            IGraph gr = new Graph();

            if (System.IO.File.Exists("Events.nt"))
            {
                NTriplesParser ntparser = new();
                ntparser.Load(gr, "Events.nt");
            }

            gr.NamespaceMap.AddNamespace("dc", new Uri("http://purl.org/dc/elements/1.1/"));
            gr.NamespaceMap.AddNamespace("dbp", new Uri("https://dbpedia.org/ontology/"));
            gr.NamespaceMap.AddNamespace("process-event", new Uri("http://w3id.org/sepses/vocab/event/process-event#"));



            string guid = Guid.NewGuid().ToString();

            string eventLink = "https://localhost:4200/events/" + guid;
            @event.Identifier = eventLink;

            IUriNode eventNode = gr.CreateUriNode(UriFactory.Create(eventLink));

            IUriNode hasTitle = gr.CreateUriNode(UriFactory.Create("dc:title"));
            ILiteralNode title = gr.CreateLiteralNode(@event.Title);

            IUriNode hasDescription = gr.CreateUriNode(UriFactory.Create("dc:description"));
            ILiteralNode description = gr.CreateLiteralNode(@event.Description);

            IUriNode onDate = gr.CreateUriNode(UriFactory.Create("dc:date"));
            ILiteralNode date = gr.CreateLiteralNode(@event.Date.ToString(), new Uri(XmlSpecsHelper.XmlSchemaDataTypeDateTime));

            IUriNode hasPrice = gr.CreateUriNode(UriFactory.Create("dbp:price"));
            ILiteralNode price = gr.CreateLiteralNode(@event.Price.ToString(), new Uri(XmlSpecsHelper.XmlSchemaDataTypeDouble));

            IUriNode hasHost = gr.CreateUriNode(UriFactory.Create("process-event:hasSourceHost"));
            IUriNode host = gr.CreateUriNode(UriFactory.Create(@event.HostIdentifier));


            gr.Assert(new Triple(eventNode, hasTitle, title));
            gr.Assert(new Triple(eventNode, hasDescription, description));
            gr.Assert(new Triple(eventNode, onDate, date));
            gr.Assert(new Triple(eventNode, hasPrice, price));
            gr.Assert(new Triple(eventNode, hasHost, host));

            NTriplesWriter ntwriter = new();
            ntwriter.Save(gr, "Events.nt");

            return Ok();
        }
    

    }
}
