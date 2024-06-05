using System.Net;
using Keeper.Core;

namespace Keeper.Api
{
    public class StaticMiddleware
    {
        private readonly RequestDelegate _next;
        private List<string> AllowWithoutKey = GetTypeSithoutKey();

        static List<string> GetTypeSithoutKey()
        {
            var accum = new List<string>();

            accum.Add(nameof(FileType.UserPicture));
            accum.Add(nameof(FileType.Content));
            accum.Add(nameof(FileType.ContentMain));
            accum.Add(nameof(FileType.InfoPresident));
            accum.Add(nameof(FileType.Menu));
            accum.Add(nameof(FileType.UsefulLink));
            accum.Add(nameof(FileType.Sites));
            accum.Add(nameof(FileType.Public));

            return accum;
        }

        public StaticMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var baseUrl = httpContext.Request.Path.Value;
            if (string.IsNullOrWhiteSpace(baseUrl)) return;
            
            if (baseUrl.StartsWith(Helper.FilePublicPath))
            {
                //FileStorage//UserPicture//2//292_photo_234516.png
                var url = baseUrl.Substring(Helper.FilePublicPath.Length).Replace("//", "/");
                if (!AllowWithoutKey.Any(x => url.StartsWith($"/{x}")))
                {
                    var qs = httpContext.Request.Query["SignKey"];
                    if (!IsAuthenticatedByQueryString(qs))
                    {
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await httpContext.Response.CompleteAsync();
                        return;
                    }
                }
            }
            await _next(httpContext);
        }

        private bool IsAuthenticatedByQueryString(string str)
        {
            var info = FileEngine.DecryptNew<DateTime>(str);

            if (info < DateTime.Now)
                return false;

            //  implement code here to check qs value
            //  probably against a DB or cache of tokens
            return true;
        }


        public static void AddFiles(string path)
        {
            var local = System.IO.Directory.GetCurrentDirectory();

            var locDir = Path.Combine(path, nameof(FileType.Public));
            if (!Directory.Exists(locDir))
                Directory.CreateDirectory(locDir);

            foreach (var lan in new List<string>() { "tg", "ru", "en" })
            {
                var copyTo = Path.Combine(locDir, $"{lan}.png");
                var filePath1 = Path.Combine(local, "Icons", $"{lan}.png");

                if (File.Exists(filePath1))
                {
                    File.Copy(filePath1, copyTo, true);
                }
            }
        }
    }
}
