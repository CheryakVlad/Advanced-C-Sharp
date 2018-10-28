namespace Module4
{
	public class RuleModel
	{
		public string FilePattern
		{
			get; set;
		}

		public string DestinationFolder
		{
			get; set;
		}

		public FileNameChanged FileNameChanged
		{
			get; set;
		}

		public long Counter
		{
			get; set;
		}
	}
}
