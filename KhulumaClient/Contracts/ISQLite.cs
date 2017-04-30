using System;
using SQLite;

namespace KhulumaClient
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}
