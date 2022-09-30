using EventsAPI.Models;
using EventsAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Builder;
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
            gr.NamespaceMap.AddNamespace("mo", new Uri("http://purl.org/ontology/mo/"));



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

            IUriNode hasPerformer = gr.CreateUriNode(UriFactory.Create("mo:performer"));
            IUriNode performer = gr.CreateUriNode(UriFactory.Create(@event.ArtistIdentifier));


            gr.Assert(new Triple(eventNode, hasTitle, title));
            gr.Assert(new Triple(eventNode, hasDescription, description));
            gr.Assert(new Triple(eventNode, onDate, date));
            gr.Assert(new Triple(eventNode, hasPrice, price));
            gr.Assert(new Triple(eventNode, hasHost, host));
            gr.Assert(new Triple(eventNode, hasPerformer, performer));

            NTriplesWriter ntwriter = new();
            ntwriter.Save(gr, "Events.nt");

            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<EventListViewModel>> GetEvents()
        {
            IList<EventListViewModel> events = new List<EventListViewModel>();

            IGraph g = new Graph();
            NTriplesParser ntparser = new();
            ntparser.Load(g, "Events.nt");

            IEnumerable<Triple> titles = g.GetTriplesWithPredicate(g.CreateUriNode(UriFactory.Create("dc:title")));
            IEnumerable<Triple> dates = g.GetTriplesWithPredicate(g.CreateUriNode(UriFactory.Create("dc:date")));
            IEnumerable<Triple> artistIdents = g.GetTriplesWithPredicate(g.CreateUriNode(UriFactory.Create("mo:performer")));
            IEnumerable<Triple> hostIdents = g.GetTriplesWithPredicate(g.CreateUriNode(UriFactory.Create("process-event:hasSourceHost")));

            for (int i = 0; i < titles.Count(); i++)
            {
                EventListViewModel eventListViewModel = new();

                eventListViewModel.Identifier = titles.ElementAt(i).Subject.ToString();
                eventListViewModel.Title = titles.ElementAt(i).Object.ToString();
                eventListViewModel.Date = FormatResult(dates.ElementAt(i).Object.ToString());
                eventListViewModel.ArtistIdentifier = artistIdents.ElementAt(i).Object.ToString();
                eventListViewModel.ArtistName = GetArtistNameByIdentifier(eventListViewModel.ArtistIdentifier);
                var hostIdentifier = hostIdents.ElementAt(i).Object.ToString();
                eventListViewModel.HostSite = GetHostSiteByIdentifier(hostIdentifier);
                eventListViewModel.HostName = GetHostNameByIdentifier(hostIdentifier);

                events.Add(eventListViewModel);
            }

            return events;
        }

        [HttpGet("{id}")]
        public async Task<EventDetailsViewModel> GetEvent(string id)
        {
            IGraph g = new Graph();
            NTriplesParser ntparser = new();
            ntparser.Load(g, "Events.nt");

            var eventsLink = "https://localhost:4200/events/" + id;


            IEnumerable<Triple> results = g.GetTriplesWithSubject(g.CreateUriNode(UriFactory.Create(eventsLink)));
            EventDetailsViewModel eventDetailsViewModel = new()
            {
                Title = results.ElementAt(0).Object.ToString(),
                Description = results.ElementAt(1).Object.ToString(),
                Price = FormatResult(results.ElementAt(3).Object.ToString())
        };

            return eventDetailsViewModel;
        }

    public static string GetArtistNameByIdentifier(string identifier)
    {
        string dbpediaPage = GetDBpediaPage(identifier);
        var name = GetArtistName(dbpediaPage);
        return name;
    }

    public static string GetHostNameByIdentifier(string identifier)
    {
        var prefixes = new NamespaceMapper(true);
        prefixes.AddNamespace("schema", new Uri("https://schema.org/"));

        string x = "x";
        var queryBuilder =
            QueryBuilder
            .Select(new string[] { x })
            .Where(
                (triplePatternBuilder) =>
                {
                    triplePatternBuilder
                        .Subject(new Uri(identifier))
                        .PredicateUri(new Uri("schema:name"))
                        .Object(x);
                });
        queryBuilder.Prefixes = prefixes;


        TripleStore tripleStore = new();
        tripleStore.LoadFromFile("Hosts.nt");

        object res = tripleStore.ExecuteQuery(queryBuilder.BuildQuery().ToString());

        if (res is SparqlResultSet rset && !rset.IsEmpty)
        {
            var result = rset.First().ToString();
            return FormatResult(result);
        }

        //TODO: throw an error if empty

        return "";
    }

    public static string GetHostSiteByIdentifier(string identifier)
    {
        var prefixes = new NamespaceMapper(true);
        prefixes.AddNamespace("org", new Uri("https://www.w3.org/TR/vocab-org/"));

        string x = "x";
        var queryBuilder =
            QueryBuilder
            .Select(new string[] { x })
            .Where(
                (triplePatternBuilder) =>
                {
                    triplePatternBuilder
                        .Subject(new Uri(identifier))
                        .PredicateUri(new Uri("org:hasSite"))
                        .Object(x);
                });
        queryBuilder.Prefixes = prefixes;


        TripleStore tripleStore = new();
        tripleStore.LoadFromFile("Hosts.nt");

        object res = tripleStore.ExecuteQuery(queryBuilder.BuildQuery().ToString());

        if (res is SparqlResultSet rset && !rset.IsEmpty)
        {
            var result = rset.First().ToString();
            return FormatResult(result);
        }

        //TODO: throw an error if empty

        return "";
    }

    private static string GetDBpediaPage(string identifier)
    {
        var prefixes = new NamespaceMapper(true);
        prefixes.AddNamespace("mo", new Uri("http://purl.org/ontology/mo/"));

        string x = "x";
        var queryBuilder =
            QueryBuilder
            .Select(new string[] { x })
            .Where(
                (triplePatternBuilder) =>
                {
                    triplePatternBuilder
                        .Subject(new Uri(identifier))
                        .PredicateUri(new Uri("mo:homePage"))
                        .Object(x);
                });
        queryBuilder.Prefixes = prefixes;


        TripleStore tripleStore = new();
        tripleStore.LoadFromFile("Artists.nt");

        object res = tripleStore.ExecuteQuery(queryBuilder.BuildQuery().ToString());

        if (res is SparqlResultSet rset && !rset.IsEmpty)
        {
            return rset.First().ToString();
        }
        //TODO: throw an error
        return "";
    }

    private static string GetArtistName(string resource)
    {
        return resource[(resource.LastIndexOf('/') + 1)..].Trim().Replace("_", " ");
    }

    private static string FormatResult(string x)
    {
        int startInd = x.IndexOf('=') + 1;
        if (x.Contains('^'))
        {
            int endInd = x.IndexOf('^');
            return x[startInd..endInd].Trim();
        }

        return x[startInd..].Trim();
    }


}
}
