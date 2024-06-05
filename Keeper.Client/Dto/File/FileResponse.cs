using System;
using System.Collections.Generic;
using System.Text;

namespace Keeper.Client
{
	public class FileResponse
	{
		public string Url { get; set; } = "";

		public FileResponse() { }
		public FileResponse(string url)
		{
			Url = url;
		}
	}
}
