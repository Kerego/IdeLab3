using System;
using Refractored.Xam.Settings.Abstractions;
using Refractored.Xam.Settings;

namespace CrossWeather
{
	public static class LocationSettings
	{
		private const string LocationKey = "username_key";
		private static readonly string LocationDefault = "Chisinau";

		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		public static string Location
		{
			get { return AppSettings.GetValueOrDefault(LocationKey, LocationDefault); }
			set { AppSettings.AddOrUpdateValue(LocationKey, value); }
		}

	}
}

