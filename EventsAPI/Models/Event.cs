using VDS.RDF;

namespace EventsAPI.Models
{
    public class Event
    {
        public string? Identifier { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DateTime { get; set; }
        //public IUriNode? Artist { get; set; }
        //public IUriNode? Host { get; set; }
        //public string? Location { get; set; }
        public double? Price { get; set; }
        //public int? Duration { get; set; }
    }
}
