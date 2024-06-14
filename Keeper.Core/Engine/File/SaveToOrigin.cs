using Common.Dto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Keeper.Client;

namespace Keeper.Core
{
	public class SaveToOrigin
	{
		public SaveToOrigin() { }
		public SaveToOrigin(List<FileResponse> files, FileType type)
		{
			Files = files;
			Type = type;
		}

		public List<FileResponse> Files { get; set; } = [];

		public FileType Type { get; set; }

		public int Partion { get; set; }
	}
}
