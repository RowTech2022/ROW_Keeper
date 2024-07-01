using Keeper.Core;
using Keeper.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Row.Common1;
using Swashbuckle.AspNetCore.Annotations;

namespace Keeper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        readonly UserEngine m_engine;
        readonly RequestInfo m_requestInfo;

        public UserController(UserEngine engine, RequestInfo requestInfo, LanguageService languageService)
        {
            m_engine = engine.InitLanguageServise(languageService);
            m_requestInfo = requestInfo;
        }

        [Route("create")]
        [SwaggerOperation(Summary = "Для создание запис  (User)")]
        [HttpPost]
        [Authorize(Access.Admin)]
        public User Create(User.Create request)
        {
            var userInfo = m_requestInfo.GetUserInfoHelper(HttpContext);
            return m_engine.Create(request, userInfo);
        }

        [Route("update")]
        [SwaggerOperation(Summary = "Для изменение запис  (User)")]
        [HttpPost]
        [Authorize(Access.Admin)]
        public User Update(User.Update request)
        {
            var userInfo = m_requestInfo.GetUserInfo(HttpContext);
            return m_engine.Update(request);
        }

        [Route("search")]
        [SwaggerOperation(Summary = "Для поиска (User)")]
        [HttpPost]
        [Authorize(Access.Admin)]
        public User.Search.Result Search(User.Search request)
        {
            var userInfo = m_requestInfo.GetUserInfo(HttpContext);
            return m_engine.Search(request);
        }

        [Route("get/{id}")]
        [SwaggerOperation(Summary = "Для получение запис по Id (User)")]
        [HttpGet]
        [Authorize(Access.Admin)]
        public User Get(int id)
        {
            // var userInfo = m_requestInfo.GetUserInfo(HttpContext);
            return m_engine.Get(id);
        }

        [Route("delete")]
        [SwaggerOperation(Summary = "Для удаления запис по Id (User)")]
        [HttpPost]
        [Authorize(Access.Admin)]
        public void Delete(Delete delete)
        {
            m_requestInfo.GetUserInfo(HttpContext);
            m_engine.Delete(delete);
        }

        [Route("UserDetails")]
        [SwaggerOperation(Summary = "Для получение детали пользователя (User)")]
        [HttpGet]
        [Authorize(Access.User)]
        public User.UserDetails GetUserInfo()
        {
            var userInfo = m_requestInfo.GetUserInfo(HttpContext);
            return m_engine.GetUserInfo(userInfo);
        }

        [HttpPost("changePassword")]
        [Authorize(Access.User)]
        public void ChangePassWord(User.UpdatePassword update)
        {
            var userInfo = m_requestInfo.GetUserInfo(HttpContext);

            m_engine.UpdatePassword(update, userInfo);
        }

        [HttpGet("roles")]
        [Authorize(Access.User)]
        public User.Role.List Roles()
        {
            return m_engine.Roles();
        }
    }
}