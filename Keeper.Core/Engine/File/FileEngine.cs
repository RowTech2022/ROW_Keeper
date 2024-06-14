using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using Keeper.Client;

namespace Keeper.Core
{
	public class FileEngine
	{
		private static string hashKeyNew = "b14ca5898a4e4133bbce2ea2315a1916";

		string m_publicPath;

		public FileEngine(string publicPath)
		{
			m_publicPath = publicPath;
		}

		public async Task<FileResponse> CopyFileToTempV2(IFormCollection req)
		{
			if (!Directory.Exists(Path.Combine(m_publicPath, Helper.FileTempName.Substring(1))))
				Directory.CreateDirectory(Path.Combine(m_publicPath, Helper.FileTempName.Substring(1)));

			var fileName = Path.GetFileNameWithoutExtension(req.Files.Select(x => x.FileName).FirstOrDefault());
			var fileExtension = Path.GetExtension(req.Files.Select(x => x.FileName).FirstOrDefault());

			var prevPath = $"{new Random().Next(1, 999)}_{(fileName.Length > 20 ? fileName.Substring(0, 20) : fileName)}_{DateTime.Now.ToString("FFFFFF")}";

			var fileN = $"{fileName}{fileExtension}";
			var filePath1 = Path.Combine(m_publicPath, Helper.FileTempName.Substring(1), prevPath);
			if (!Directory.Exists(filePath1))
				Directory.CreateDirectory(filePath1);

			var filePath = Path.Combine(filePath1, fileN);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await req.Files[0].CopyToAsync(fileStream);
			}

			var tempPath = Path.Combine(Helper.FilePublicPath, Helper.FileTempName.Substring(1), prevPath, fileN);

			var str = GetFileWithHashWithFileName(tempPath);

			return new FileResponse(str);
		}

		public async Task<FileResponse> CopyFileToContent(IFormCollection req)
		{
			if (!Directory.Exists(Path.Combine(m_publicPath, nameof(FileType.Content))))
				Directory.CreateDirectory(Path.Combine(m_publicPath, nameof(FileType.Content)));

			var fileName = Path.GetFileNameWithoutExtension(req.Files.Select(x => x.FileName).FirstOrDefault());
			var fileExtension = Path.GetExtension(req.Files.Select(x => x.FileName).FirstOrDefault());

			var prevPath = $"{new Random().Next(1, 999)}_{(fileName.Length > 20 ? fileName.Substring(0, 20) : fileName)}_{DateTime.Now.ToString("FFFFFF")}";

			var fileN = $"{fileName}{fileExtension}";
			var filePath1 = Path.Combine(m_publicPath, nameof(FileType.Content), prevPath);
			if (!Directory.Exists(filePath1))
				Directory.CreateDirectory(filePath1);
			var filePath = Path.Combine(filePath1, fileN);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await req.Files[0].CopyToAsync(fileStream);
			}

			var tempPath = Path.Combine(Helper.FilePublicPath, nameof(FileType.Content), prevPath, fileN);

			var str = GetFileWithHashWithFileName(tempPath);

			return new FileResponse(str);
		}

		public SaveResponse SaveFileToProdDir(SaveToOrigin request)
		{
			var loc_path = Path.Combine(m_publicPath, request.Type.ToString(), request.Partion.ToString());
			try
			{
				if (!Directory.Exists(loc_path))
					Directory.CreateDirectory(loc_path);
			}
			catch (Exception ex)
			{
				throw new Exception($"cant create Directory{ex}");
			}

			var result = new SaveResponse();
			foreach (var item in request.Files)
			{
				try
				{
					var fileInfo = GetFileNameV4FromUrl(item.Url);
					if (IsProdStorageFile(item, request.Type, request.Partion))
					{
						var tempPath1 = Path.Combine(Helper.FilePublicPath, request.Type.ToString(), request.Partion.ToString(), fileInfo.filePath, fileInfo.fileName);
						result.FilePathInfo[fileInfo.fileName] = tempPath1;
						continue;
					}

					// need to add log  (if file not exsist)
					var filePath = Path.Combine(m_publicPath, Helper.FileTempName.Substring(1), fileInfo.filePath, fileInfo.fileName);
					var savePathPrev = Path.Combine(loc_path, fileInfo.filePath);
					if (!Directory.Exists(savePathPrev))
						Directory.CreateDirectory(savePathPrev);
					var savePath = Path.Combine(savePathPrev, fileInfo.fileName);

					if (request.Type == FileType.UserPicture)
					{
						if (File.Exists(savePath))
							File.Delete(savePath);

						using var img = Image.FromFile(filePath);
						Image i = Helper.ResizeImage(img, new Size(100, 100));
						i.Save(savePath);
					}
					else
						File.Copy(filePath, savePath, true);

					File.Delete(filePath);

					var tempPath = Path.Combine(Helper.FilePublicPath, request.Type.ToString(), request.Partion.ToString(), fileInfo.filePath, fileInfo.fileName);
					result.FilePathInfo[fileInfo.fileName] = tempPath;
				}
				catch (Exception ex)
				{
					throw new Exception($"Could not found file-(Save-method)  {ex}");
				}
			}

			return result;
		}

		public string SaveFile(string? pathFile, int docId, FileType type)
		{
			var res = "";
			if (String.IsNullOrWhiteSpace(pathFile)) return res;
			var file = new FileResponse(pathFile);
			var toSave = new SaveToOrigin
			{
				Partion = docId,
				Type = type,
				Files = [file]
			};
			var files = SaveFileToProdDir(toSave);

			var fileName = FileEngine.GetFileNameV4FromUrl(file.Url).fileName;
			if (files.FilePathInfo.ContainsKey(fileName))
				res = files.FilePathInfo[fileName];
			return res;
		}

		public static (string fileName, string filePath) GetFileNameV4FromUrl(string url)
		{
			var ind = url.LastIndexOf("?SignKey");
			var str = (ind == -1) ? url : url.Substring(0, ind);
			var info = str?.Replace("\\", "/")?.Replace("//", "/").Split('/');

			///FileStorage\FilesTemp\727_492373\22222222.jpg
			/// 0 - '' 1 - FileStorage 2 - FilesTemp 3 - nameGenrate 4-name
			/// 0 - '' 1 - FileStorage 2 - Type 3 - partioal 4-nameGenrate 5-name
			switch (info.Length)
			{
				case 5:
					return (GetFileName(info[4]), info[3]);
				case 6:
					return (GetFileName(info[5]), info[4]);
				default:
					throw new Exception($"Url имеет не корректный формат url {url} (Split 5 | 6)");
			}
			//1-public 2-type 3-id 4-name  - prod
			//1-public 2-fileTemp 3-folder 4-name - sesion
		}

		public string GetFilePathOrigin(string item)
		{
			var fileInfo = GetFileNameV4FromUrl(item);
			return Path.Combine(m_publicPath, Helper.FileTempName.Substring(1), fileInfo.filePath, fileInfo.fileName);
		}

		public static bool IsProdStorageFile(FileResponse file, FileType type, int partion)
		{
			var ind = file.Url.LastIndexOf("?SignKey");
			var str = (ind == -1) ? file.Url : file.Url.Substring(0, ind);
			var info = str?.Replace("\\", "/")?.Replace("//", "/").Split('/');

			//0-'' 1-public 2-type 3-id 4-fodler 5-name  - prod
			//0-'' 1-public 2-fileTemp 3-folder 4-name - sesion

			switch (info.Length)
			{
				case 5:
					return false;
				case 6:
					if (Enum.TryParse<FileType>(info[2], out FileType type1))
					{
						if (int.TryParse(info[3], out int id))
							if (id == partion)
								return true;
						return false;
					}
					else
						return false;
				default:
					throw new Exception($"Url имеет не корректный формат url {file.Url} (Split 5 | 6)");
			}
		}


		public static string GetFileName(string pathWithSign)
		{
			var index2 = pathWithSign.IndexOf('?');
			if (index2 != -1)
				return pathWithSign.Substring(0, index2);
			return pathWithSign;
		}

		public static string? GetFileWithHashWithFileName(string? fileUrl)
		{
			if (String.IsNullOrWhiteSpace(fileUrl))
				return null;

			string text = fileUrl;
			DateTime clearText = DateTime.Now.AddMinutes(10.0);
			string text2 = EncryptNew(clearText);
			string text3 = "";
			int num = fileUrl.LastIndexOf("\\");
			if (num != -1)
			{
				string text4 = fileUrl.Substring(num + 1);
				int num2 = text4.IndexOf('?');
				if (num2 != -1)
				{
					text3 = text4.Substring(0, num2);
				}

				text3 = text4;
			}
			else
			{
				text3 = "unknowmfileName";
			}

			return text + "?SignKey=" + text2 + "&expiredTime=" + clearText.ToString("yyyy-MM-dd hh:mm:ss") + "&fileName=" + text3;
		}

		public static string GetFileWithHashWithFileNameContent(string fileUrl)
		{
			string text = fileUrl;
			string fileName = "";
			int num = fileUrl.LastIndexOf("\\");
			if (num != -1)
			{
				string text4 = fileUrl.Substring(num + 1);
				int num2 = text4.IndexOf('?');
				if (num2 != -1)
				{
					fileName = text4.Substring(0, num2);
				}

				fileName = text4;
			}
			else
			{
				fileName = "unknowmfileName";
			}

			return text + "?fileName=" + fileName;
		}

		public static string EncryptNew<T>(T clearText)
		{
			string value = JsonConvert.SerializeObject(clearText);
			byte[] iV = new byte[16];
			byte[] inArray;
			using (Aes aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(hashKeyNew);
				aes.IV = iV;
				using MemoryStream memoryStream = new MemoryStream();
				using CryptoStream stream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
				using (StreamWriter streamWriter = new StreamWriter(stream))
				{
					streamWriter.Write(value);
				}

				inArray = memoryStream.ToArray();
			}

			return Convert.ToBase64String(inArray);
		}

		public static T DecryptNew<T>(string cipherText)
		{
			cipherText = cipherText.Replace(" ", "+");
			string value = "";
			byte[] iV = new byte[16];
			byte[] buffer = Convert.FromBase64String(cipherText);
			using (Aes aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(hashKeyNew);
				aes.IV = iV;
				ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
				using MemoryStream stream = new MemoryStream(buffer);
				using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
				using StreamReader streamReader = new StreamReader(stream2);
				value = streamReader.ReadToEnd();
			}

			return JsonConvert.DeserializeObject<T>(value);
		}
	}
}
