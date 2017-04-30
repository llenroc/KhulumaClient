using System;
using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhulumaClient
{
	public class KhulumaDatabase
	{
		private SQLiteConnection _connection;
		public List<ChatModel> Chats;

		public KhulumaDatabase()
		{
			_connection = DependencyService.Get<ISQLite>().GetConnection();
		}




	}
}
