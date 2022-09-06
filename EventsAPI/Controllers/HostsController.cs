using EventsAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateHost(Models.Host host)
        {
            IGraph gr = new Graph();

            if (System.IO.File.Exists("Hosts.nt"))
            {
                NTriplesParser ntparser = new();
                ntparser.Load(gr, "Hosts.nt");
            }

            gr.NamespaceMap.AddNamespace("dc", new Uri("http://purl.org/dc/elements/1.1/"));
            gr.NamespaceMap.AddNamespace("dbp", new Uri("https://dbpedia.org/ontology/"));
            gr.NamespaceMap.AddNamespace("schema", new Uri("https://schema.org/"));
            gr.NamespaceMap.AddNamespace("org", new Uri("https://www.w3.org/TR/vocab-org/"));

            string guid = Guid.NewGuid().ToString();

            string hostLink = "https://localhost:4200/hosts/" + guid;
            host.Identifier = hostLink;

            IUriNode hostNode = gr.CreateUriNode(UriFactory.Create(host.Identifier));

            IUriNode hasName = gr.CreateUriNode(UriFactory.Create("schema:name"));
            ILiteralNode name = gr.CreateLiteralNode(host.Name);

            IUriNode hasSite = gr.CreateUriNode(UriFactory.Create("org:hasSite"));
            ILiteralNode site = gr.CreateLiteralNode(host.Page);

            IUriNode hasAddress = gr.CreateUriNode(UriFactory.Create("schema:address"));
            ILiteralNode address = gr.CreateLiteralNode(host.Address);

            gr.Assert(new Triple(hostNode, hasName, name));
            gr.Assert(new Triple(hostNode, hasSite, site));
            gr.Assert(new Triple(hostNode, hasAddress, address));

            NTriplesWriter ntwriter = new();
            ntwriter.Save(gr, "Hosts.nt");

            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<HostNameViewModel>> GetHosts()
        {

            IEnumerable<HostNameViewModel> hosts = new List<HostNameViewModel>();
            IGraph g = new Graph();
            NTriplesParser ntparser = new();
            ntparser.Load(g, "Hosts.nt");

            IEnumerable<Triple> results = g.GetTriplesWithPredicate(g.CreateUriNode(UriFactory.Create("schema:name")));

            foreach (Triple t in results)
            {
                HostNameViewModel hostNameViewModel = new();
                hostNameViewModel.Identifier = t.Subject.ToString();
                hostNameViewModel.Name = t.Object.ToString();
                hosts = hosts.Append(hostNameViewModel).ToList();
            }

            return hosts;
        }
    }
}
