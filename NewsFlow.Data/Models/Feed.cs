using System.ComponentModel.DataAnnotations;

namespace NewsFlow.Data.Models
{
    public class Feed : BaseDbModel
    {
        [Required]
        public string Name { get; private set; }

        [Required]
        public string Link { get; private set; }

        [Required]
        public string Description { get; private set; }

        public DateTime TimestampAdded { get; private set; }

        private Feed(
            Guid id, string name, string link, string description)
        {
            Id = id;
            Name = name;
            Link = link;
            Description = description;
        }

        public static Feed Create(
            Guid id, string name, string link, string description)
        {
            return new Feed(id, name, link, description);
        }
    }
}
