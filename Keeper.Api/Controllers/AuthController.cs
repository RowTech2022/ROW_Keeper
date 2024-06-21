using Keeper.Core;
using Keeper.Client;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;

namespace Keeper.Api.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		AuthEngine m_authEngine;
		RequestInfo m_requestInfo;
		private LanguageService m_languageService;

		public AuthController(AuthEngine authEngine, IDataProtectionProvider protectedProvider, RequestInfo requestInfo, LanguageService languageService)
		{
			m_authEngine = authEngine.InitLanguageServise(languageService);
			m_requestInfo = requestInfo;
			m_languageService = languageService;
		}

		//[Route("token")]
		//[HttpPost]
		//public TokenInfo Login()
		//{
		//	var result = m_authEngine2.Login2(HttpContext);
		//	return result;
		//}

		[Route("Login")]
		[HttpPost]
		public TokenInfo Login(Login request)
		{
			var result = m_authEngine.Login(request);
			return result;
		}

		//[Route("refresh")]
		//[HttpPost]
		//public TokenInfo GetRefreshToken(Refresh request)
		//{
		//	var result = m_authEngine2.Refresh(request);
		//	return result;
		//}

		[Route("getcode")]
		[HttpPost]
		public void GetCode(User.UserCode request)
		{
			//var userTokenImfo = new UserTokenInfo();
			//userTokenImfo.CertExpiry = Request.Headers["CertExpiry"];
			//userTokenImfo.CertId = Request.Headers["CertId"];
			//userTokenImfo.CertStartDate = Request.Headers["CertStartDate"];
			//userTokenImfo.UserLogin = Request.Headers["UserLogin"];

			//m_restoreEngine.CheckUserSertGetCode(userTokenImfo, request);

			m_authEngine.SendCode(request);
		}

        //[Route("checkCode")]
        //[HttpPost]
        ////[EnableCors(Startup.MyAllowSpecificOrigins)]
        //public bool CheckCode(User.CheckCode request)
        //{
        //	//var userTokenImfo = new UserTokenInfo();
        //	//userTokenImfo.CertExpiry = Request.Headers["CertExpiry"];
        //	//userTokenImfo.CertId = Request.Headers["CertId"];
        //	//userTokenImfo.CertStartDate = Request.Headers["CertStartDate"];
        //	//userTokenImfo.UserLogin = Request.Headers["UserLogin"];

        //	//m_restoreEngine.CheckUserSertGetCode(userTokenImfo, request);

        //	return m_authEngine2.CheckCode(request);
        //}
    }
}
