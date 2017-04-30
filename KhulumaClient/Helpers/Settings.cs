// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace KhulumaClient.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }


		/*
					Helpers.Settings.id = responseAppUser.ID;
					Helpers.Settings.Username = responseAppUser.Username;
					Helpers.Settings.Name = responseAppUser.Name;
					Helpers.Settings.Surname = responseAppUser.Surname;
					Helpers.Settings.Age = responseAppUser.Age;
					Helpers.Settings.Gender = responseAppUser.Gender;
					Helpers.Settings.Email = responseAppUser.Email;
					Helpers.Settings.PhoneNumber = responseAppUser.PhoneNumber;
					Helpers.Settings.HomeAddress = responseAppUser.HomeAddress;
					Helpers.Settings.LocationId = responseAppUser.LocationId;
					//Helpers.Settings.GroupId = responseAppUser.GroupId;

					Helpers.Settings.isRegistered = true;

		*/


    #region Setting Constants

    private const string SettingsKey = "settings_key";
    private static readonly string SettingsDefault = string.Empty;

		private const string Id_key = "id_key";
		private static readonly int IdDefault = 0;

		private const string Username_key = "Username_key";
		private static readonly string UsernameDefault = string.Empty;

		private const string Name_key = "Name_key";
		private static readonly string NameDefault = string.Empty;

		private const string Surname_key = "Surname_key";
		private static readonly string SurnameDefault = string.Empty;

		private const string Age_key = "Age_key";
		private static readonly int AgeDefault = 0;

		private const string Gender_key = "Gender_key";
		private static readonly string GenderDefault = string.Empty;

		private const string Email_key = "Email_key";
		private static readonly string EmailDefault = string.Empty;

		private const string PhoneNumber_key = "PhoneNumber_key";
		private static readonly string PhoneNumberDefault = string.Empty;

		private const string HomeAddress_key = "HomeAddress_key";
		private static readonly string HomeAddressDefault = string.Empty;

		private const string LocationId_key = "LocationId_key";
		private static readonly int LocationIdDefault = 0;

		private const string GroupId_key = "GroupId_key";
		private static readonly int GroupIdDefault = 0;

		private const string isRegistered_key = "isRegistered_key";
		private static readonly bool isRegisteredDefault = false;



    #endregion

		//Int values
		public static int id
		{
			get { return AppSettings.GetValueOrDefault<int>(Id_key, IdDefault); }
			set { AppSettings.AddOrUpdateValue<int>(Id_key, value); }
		}

		public static int Age
		{
			get { return AppSettings.GetValueOrDefault<int>(Age_key, AgeDefault); }
			set { AppSettings.AddOrUpdateValue<int>(Age_key, value); }
		}

		public static int LocationId
		{
			get { return AppSettings.GetValueOrDefault<int>(LocationId_key, LocationIdDefault); }
			set { AppSettings.AddOrUpdateValue<int>(LocationId_key, value); }
		}

		public static int GroupId
		{
			get { return AppSettings.GetValueOrDefault<int>(GroupId_key, GroupIdDefault); }
			set { AppSettings.AddOrUpdateValue<int>(GroupId_key, value); }
		}


		//String values
		public static string Username
		{
			get { return AppSettings.GetValueOrDefault<string>(Username_key, UsernameDefault); }
			set { AppSettings.AddOrUpdateValue<string>(Username_key, value); }
		}

		public static string Name
		{
			get { return AppSettings.GetValueOrDefault<string>(Name_key, NameDefault); }
			set { AppSettings.AddOrUpdateValue<string>(Name_key, value); }
		}

		public static string Surname
		{
			get { return AppSettings.GetValueOrDefault<string>(Surname_key, SurnameDefault); }
			set { AppSettings.AddOrUpdateValue<string>(Surname_key, value); }
		}

		public static string Gender
		{
			get { return AppSettings.GetValueOrDefault<string>(Gender_key, GenderDefault); }
			set { AppSettings.AddOrUpdateValue<string>(Gender_key, value); }
		}

		public static string Email
		{
			get { return AppSettings.GetValueOrDefault<string>(Email_key, EmailDefault); }
			set { AppSettings.AddOrUpdateValue<string>(Email_key, value); }
		}

		public static string PhoneNumber
		{
			get { return AppSettings.GetValueOrDefault<string>(PhoneNumber_key, PhoneNumberDefault); }
			set { AppSettings.AddOrUpdateValue<string>(PhoneNumber_key, value); }
		}

		public static string HomeAddress
		{
			get { return AppSettings.GetValueOrDefault<string>(HomeAddress_key, HomeAddressDefault); }
			set { AppSettings.AddOrUpdateValue<string>(HomeAddress_key, value); }
		}

		public static bool isRegistered
		{
			get { return AppSettings.GetValueOrDefault<bool>(isRegistered_key, isRegisteredDefault); }
			set { AppSettings.AddOrUpdateValue<bool>(isRegistered_key, value); }
		}


    public static string GeneralSettings
    {
      get
      {
        return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
      }
    }



  }
}