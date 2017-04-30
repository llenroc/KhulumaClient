using System;
using Xamarin.Forms;
using KhulumaClient.Droid;
using SQLite;
using System.IO;

[assembly:Dependency(typeof(SQLiteImplementation))]
namespace KhulumaClient.Droid
{
	public class SQLiteImplementation : ISQLite
	{
		public SQLiteImplementation()
		{
		}

		public SQLiteConnection GetConnection()
		{
			var sqliteFilename = "Khuluma_dx.db3";

			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, sqliteFilename);

			//Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			//Return the database connection
			return conn;

		}
	}
}
