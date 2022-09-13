using VDS.RDF;

namespace EventsAPI.Models
{
    public class Artist
    {
        public Artist()
        {
            Songs = new List<IUriNode>();
        }
        public string? Identifier { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ActiveSince { get; set; }
        public IEnumerable<IUriNode> Songs { get; set; }

    }
}
