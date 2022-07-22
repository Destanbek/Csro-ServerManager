using System.Runtime.InteropServices;
using System.Text;

namespace SilkroadServerManager
{
	internal class fonksiyonlar
	{
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		public static string connect_string(string sql)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			GetPrivateProfileString("entry0", "query", "", stringBuilder, 255, sql);
			sql = stringBuilder.ToString();
			sql = sql.Replace("DRIVER={SQL Server};", "").Replace("DSN=shard;", "").Replace("DSN=acc;", "");
			return sql;
		}
	}
}
