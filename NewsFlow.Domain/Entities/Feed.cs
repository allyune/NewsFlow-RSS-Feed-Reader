using System;
using System.Text.RegularExpressions;
using NewsFlow.Domain.Base;
using NewsFlow.Domain.Exceptions;

namespace NewsFlow.Domain.Entities
{
	public class Feed : BaseEntity
	{
		public string Name { get; private set; }
		public string Link { get; private set; }
		public string Description { get; private set; }
		public DateTime? Added { get; private set; }


		private Feed(
			Guid id,
			string name,
			string link,
			string description,
			DateTime? added)
		{
			Id = id;
			Name = name;
			Link = link;
			Description = description;
			Added = added;
		}

		public static Feed Create(
            string name,
            string link,
            string description,
            Guid id = new Guid(),
			DateTime? added = null)
		{
			if (name.Length == 0)
			{
                throw new InvalidFeedNameException(
                    $"Name can not be empty");
            }

			return new Feed(id, name, link, description, added);

		}
	}
}
			