﻿using System.Configuration;
using System.Globalization;

namespace Module4
{
	public class WatcherSettings : ConfigurationSection
	{
		[ConfigurationProperty("Directories", IsRequired = true)]
		public DirectoryCollection Directories =>
		   (DirectoryCollection)this["Directories"];


		[ConfigurationProperty("Rules", IsRequired = true)]
		public RuleCollection Rules =>
			(RuleCollection)this["Rules"];


		[ConfigurationProperty("CultureInfo", IsRequired = true)]
		public CultureInfo CultureInfo =>
			new CultureInfo(base["CultureInfo"].ToString());
	}
}
