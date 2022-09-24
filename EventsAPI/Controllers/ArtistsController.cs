using EventsAPI.Models;
using EventsAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Builder;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Writing;

namespace EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateArtist(CreateArtistViewModel artist)
        {
            IGraph gr = new Graph();

            if (System.IO.File.Exists("Artists.nt"))
            {
                NTriplesParser ntparser = new();
                ntparser.Load(gr, "Artists.nt");
            }

            artist.Name = artist.Name.Trim().Replace(" ", "_");

            gr.NamespaceMap.AddNamespace("mo", new Uri("http://purl.org/ontology/mo/"));
            string guid = Guid.NewGuid().ToString();

            string artistLink = "https://localhost:4200/artists/" + guid;

            IUriNode artistNode = gr.CreateUriNode(UriFactory.Create(artistLink));
            IUriNode hasHomePage = gr.CreateUriNode(UriFactory.Create("mo:homePage"));
            IUriNode homePage = gr.CreateUriNode(UriFactory.Create("http://dbpedia.org/resource/" + artist.Name));

            gr.Assert(new Triple(artistNode, hasHomePage, homePage));

            NTriplesWriter ntwriter = new();
            ntwriter.Save(gr, "Artists.nt");

            return Ok();
        }

        [HttpGet("{id}")]
        public Artist GetArtist(string id)
        {
            string dbpediaPage = GetDBpediaPage(id);
            dbpediaPage = FormatResult(dbpediaPage);
            string data = FormatToNtPage(dbpediaPage);

            return QueryDbpedia(dbpediaPage, data);
        }

        private Artist QueryDbpedia(string resource, string data)
        {
            Artist artist = new();

            var prefixes = new NamespaceMapper(true);
            prefixes.AddNamespace("dbr", new Uri("http://dbpedia.org/resource/"));
            prefixes.AddNamespace("dbp", new Uri("http://dbpedia.org/property/"));
            prefixes.AddNamespace("dbo", new Uri("http://dbpedia.org/ontology/"));
            string activeYearsStartYear = "activeYearsStartYear";
            var queryBuilder =
                QueryBuilder
                .Select(new string[] { activeYearsStartYear })
                .Where(
                    (triplePatternBuilder) =>
                    {
                        triplePatternBuilder
                            .Subject(new Uri(resource))
                            .PredicateUri(new Uri("http://dbpedia.org/property/yearsActive"))
                            .Object(activeYearsStartYear);
                    });
            queryBuilder.Prefixes = prefixes;

            TripleStore store = new();
            store.AddFromUri(new Uri(data));

            InMemoryDataset ds = new(store, new Uri(data));
            ISparqlQueryProcessor processor = new LeviathanQueryProcessor(ds);
            SparqlQueryParser sparqlparser = new();

            object results = processor.ProcessQuery(queryBuilder.BuildQuery());

            if (results is SparqlResultSet rset)
            {
                artist.ActiveSince = FormatResult(rset.First().ToString());
            }

            string x = "x";
            queryBuilder =
                QueryBuilder
                .Select(new string[] { x })
                .Where(
                    (triplePatternBuilder) =>
                    {
                        triplePatternBuilder
                        .Subject(new Uri(resource))
                        .PredicateUri(new Uri("http://dbpedia.org/ontology/abstract"))
                        .Object(x);
                    });
            //.Filter((builder) => builder.Variable(x));

            results = processor.ProcessQuery(queryBuilder.BuildQuery());

            if (results is SparqlResultSet rset2)
            {
                artist.Description = FormatResult(rset2.First().ToString());
            }

            queryBuilder =
                QueryBuilder
                .Select(new string[] { x })
                .Where(
                    (triplePatternBuilder) =>
                    {
                        triplePatternBuilder
                        .Subject(x)
                        .PredicateUri(new Uri("http://dbpedia.org/ontology/artist"))
                        .Object(new Uri(resource));
                    });

            results = processor.ProcessQuery(queryBuilder.BuildQuery());

            if(results is SparqlResultSet rset3)
            {
                foreach (SparqlResult result in rset3)
                {
                    string formated = FormatResult(result.ToString());
                    artist.Songs.Add(formated);
                }
            }

            return artist;
        }

        private static string GetDBpediaPage(string id)
        {
            //Get the artist from the Artist.nt file
            string artistLink = "https://localhost:4200/artists/" + id;

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
                            .Subject(new Uri(artistLink))
                            .PredicateUri(new Uri("mo:homePage"))
                            .Object(x);
                    });
            queryBuilder.Prefixes = prefixes;


            TripleStore tripleStore = new();
            tripleStore.LoadFromFile("Artists.nt");

            object res = tripleStore.ExecuteQuery(queryBuilder.BuildQuery().ToString());

            if (res is SparqlResultSet rset)
            {
                return rset.First().ToString();
            }
            //TODO: throw error
            return "";
        }

        private string FormatResult(string x)
        {
            int startInd = x.IndexOf('=') + 1;
            if (x.Contains('^'))
            {
                int endInd = x.IndexOf('^');
                return x[startInd..endInd].Trim();
            }

            return x[startInd..].Trim();
        }

        private static string FormatToNtPage(string x)
        {
            return x.Replace("resource", "data") + ".nt";
        }

    }
}
