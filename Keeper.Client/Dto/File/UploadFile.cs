using System.IO;

namespace Keeper.Client
{
	public class UploadFile
    {
        public UploadFile(string name, Stream file)
        {
            Name = name;
            File = file;
        }

        public string Name { get; set; }
        public Stream File { get; set; }

		public FileResponse ExecTest(KeeperApiClient client)
		{
			var req = client.PostRequest($"api/file/UploadFile");

			using (MemoryStream ms = new MemoryStream())
			{
				File.CopyTo(ms);

                req.Files.Add(new Row.Common1.Client1.File()
                {
                    Name = "filename",
                    FileName = Name,
                    Data = ms.ToArray()
                });
			}

			return client.ExecuteWithHttp<FileResponse>(req);
		}
	}
}
