using System.ComponentModel.DataAnnotations;

namespace NewsFlow.Data.Models
{
    public class Feed : BaseDatabaseModel
    {
        [Required]
        public string Name { get; private set; }

        [Required]
        public string Link { get; private set; }

        [Required]
        public string Description { get; private set; }

        public DateTime TimestampAdded { get; private set; }

        private Feed(
            string name, string link, string description)
        {
            Name = name;
            Link = link;
            Description = description;
        }

        public static Feed Create(
            string name, string link, string description)
        {
            return new Feed(name, link, description);
        }
    }
}
