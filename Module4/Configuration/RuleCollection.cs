using System;
using System.Configuration;

namespace Module4
{
	[ConfigurationCollection(typeof(Rule), AddItemName = "Rule")]
	public class RuleCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement() => new Rule();

		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException(nameof(element));
			}

			return ((Rule)element).FilePattern;
		}
	}

	public class Rule : ConfigurationElement
	{
		[ConfigurationProperty("FilePattern", IsRequired = true, IsKey = true)]
		public string FilePattern => (string)base["FilePattern"];

		[ConfigurationProperty("DestinationFolder", IsRequired = false, DefaultValue = @"C:\Github\Module1\DefaultFolder")]
		public string DestinationFolder => (string)base["DestinationFolder"];

		[ConfigurationProperty("FileNameChanged", IsRequired = true)]
		public FileNameChanged FileNameChanged =>
			(FileNameChanged)Enum.Parse(
				typeof(FileNameChanged),
				base["FileNameChanged"].ToString());
	}
}
