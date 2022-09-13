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
            ILiteralNode homePage = gr.CreateLiteralNode("http://dbpedia.org/resource/" + artist.Name);

            gr.Assert(new Triple(artistNode, hasHomePage, homePage));

            NTriplesWriter ntwriter = new();
            gr.BaseUri = new Uri("https://localhost:4200/artists/");
            ntwriter.Save(gr, "Artists.nt");

            return Ok();
        }

        [HttpGet("{id}")]
        public Task<Artist> GetArtist(string id)
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
                            .PredicateUri("mo:homePage")
                            .Object(x);
                    });
            queryBuilder.Prefixes = prefixes;

            TripleStore tripleStore = new();
            tripleStore.LoadFromFile("Artists.nt");

            InMemoryDataset ds = new(tripleStore);
            ISparqlQueryProcessor processor = new LeviathanQueryProcessor(ds);
            object res = processor.ProcessQuery(queryBuilder.BuildQuery());

            if (res is SparqlResultSet rset)
            {
                foreach (SparqlResult result in rset)
                {
                    Console.WriteLine(result.ToString());
                }
            }


            //query the web
            //var prefixes2 = new NamespaceMapper(true);
            //prefixes.AddNamespace("dbr", new Uri("http://dbpedia.org/resource/"));
            //prefixes.AddNamespace("dbp", new Uri("http://dbpedia.org/property/"));
            //prefixes.AddNamespace("dbo", new Uri("http://dbpedia.org/ontology/"));
            //string activeYearsStartYear = "activeYearsStartYear";
            //var queryBuilder2 =
            //    QueryBuilder
            //    .Select(new string[] { activeYearsStartYear })
            //    .Where(
            //        (triplePatternBuilder) =>
            //        {
            //            triplePatternBuilder
            //                .Subject(new Uri("http://dbpedia.org/resource/Coldplay"))
            //                .PredicateUri(new Uri("http://dbpedia.org/property/yearsActive"))
            //                .Object(activeYearsStartYear);
            //        });
            //queryBuilder.Prefixes = prefixes;

            //TripleStore store = new();
            //store.AddFromUri(new Uri("https://dbpedia.org/data/Coldplay.nt"));

            //InMemoryDataset ds = new(store, new Uri("https://dbpedia.org/data/Coldplay.nt"));
            //ISparqlQueryProcessor processor = new LeviathanQueryProcessor(ds);
            //SparqlQueryParser sparqlparser = new();

            //object results = processor.ProcessQuery(queryBuilder.BuildQuery());

            //if (results is SparqlResultSet rset2)
            //{
            //    foreach (SparqlResult result in rset2)
            //    {
            //        Console.WriteLine(result.ToString());

            //    }
            //}


            return null;

        }
    }
}
