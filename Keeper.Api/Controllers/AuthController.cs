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
		AuthEngine _mAuthEngine;
		RequestInfo m_requestInfo;
		private LanguageService m_languageService;

		public AuthController(AuthEngine authEngine, IDataProtectionProvider protectedProvider, RequestInfo requestInfo, LanguageService languageService)
		{
			_mAuthEngine = authEngine.InitLanguageServise(languageService);
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
			var result = _mAuthEngine.Login(request);
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

			_mAuthEngine.SendCode(request);
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
