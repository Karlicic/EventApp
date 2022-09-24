using VDS.RDF;

namespace EventsAPI.Models
{
    public class Artist
    {
        public Artist()
        {
            Songs = new List<string>();
        }
        public string? Identifier { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ActiveSince { get; set; }
        public IList<string> Songs { get; set; }

    }
}
