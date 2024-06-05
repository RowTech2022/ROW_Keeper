using System.Security.Cryptography;
using System.Text;

namespace Keeper.Core
{
    public class HashCalculator
	{
		string m_passwordSeparator;

		public HashCalculator(string passwordSeparator)
		{
			m_passwordSeparator = passwordSeparator;
		}

		public byte[] GetPasswordHash(int userId, string? password)
		{
			return GetHash($"{userId}{m_passwordSeparator}{password}");
		}

		public byte[] GetHash(string value)
		{
			var hashStr = Encoding.UTF8.GetBytes(value);
			var hash = SHA512.Create().ComputeHash(hashStr);
			return hash;
		}
	}
}
