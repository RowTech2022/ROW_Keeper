using System.Net;
using Bibliotekaen.Dto;
using Bibliotekaen.Sql;
using Row.Common1;
using Row.Common1.Client1;
using Row.Common1.Dto1;
using System.Text;
using Keeper.Client;

namespace Keeper.Core
{
    public partial class UserEngine
    {
        readonly ISqlFactory m_sql;
        readonly DtoComplex m_dto;
        readonly HashCalculator m_hashCalculator = new HashCalculator("#####");
        public ISendStrategy SendStrategy { get; set; }
        FileEngine m_fileEngine;

        readonly bool m_ignorePassword;
        public bool IgnoreCode { get; set; }

        LanguageService m_languageServices;

        public UserEngine(ISqlFactory sql, DtoComplex dto, FileEngine fileApi,
            bool ignorePassword, bool ignoreCode, ISendStrategy sendStrategy)
        {
            m_sql = sql;
            m_dto = dto;
            m_fileEngine = fileApi;
            m_ignorePassword = ignorePassword;

            IgnoreCode = ignoreCode;
            SendStrategy = sendStrategy;
        }

        public UserEngine InitLanguageServise(LanguageService languageService)
        {
            m_languageServices = languageService;
            return this;
        }

        public User Create(User.Create request, UserInfo userInfo)
        {
            var db = new Db.User.Create
            {
                ReqUserId = userInfo.UserId
            }.CopyFrom(request, m_dto);
            var userId = db.Exec(m_sql);

            var password = GeneratedPassword();
            UpdatePassword(userId, password);
            SendStrategy.SendLoginAndPasswordToPhone(request.Phone, request.Login, password);

            SetRole(new User.UserRoleAccess.Set(userId, UserRoles.ActivatedUser));

            return Get(userId);
        }

        public User Update(User.Update request)
        {
            request = m_dto.FixValue(request, nameof(request), x => x.NotEmpty().ValidateDto());

            var db = new Db.User.Update().CopyFrom(request, m_dto);
            db.SetDefaultUpdionlist();
            db.Exec(m_sql);

            return Get(request.Id);
        }

        private static string GeneratedPassword()
        {
            const int length = 7;
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string number = "1234567890";
            const string special = "!@#$%^&*_-=+";

            var _rand = new Random();

            // Get cryptographically random sequence of bytes
            var bytes = new byte[length];
            // Build up a string using random bytes and character classes
            var res = new StringBuilder();
            foreach (byte b in bytes)
            {
                // Randomly select a character class for each byte
                switch (_rand.Next(3))
                {
                    // In each case use mod to project byte b to the correct range
                    case 0:
                        res.Append(lower[b % lower.Length]);
                        break;
                    case 1:
                        res.Append(upper[b % upper.Length]);
                        break;
                    case 2:
                        res.Append(number[b % number.Length]);
                        break;
                    case 3:
                        res.Append(special[b % special.Length]);
                        break;
                }
            }

            return res.ToString();
        }

        public User.Search.Result Search(User.Search request)
        {
            var req = new Db.User.Search().CopyFrom(request, m_dto);
            var dbRes = req.Exec(m_sql);

            return new User.Search.Result
            {
                Items = dbRes.Select(x => new User.Search.Result.Item().CopyFrom(x, m_dto)).ToList(),
                Total = dbRes.Select(x => x.Total).FirstOrDefault()
            };
        }

        public User Get(int id)
        {
            var user = new Db.User.List(id).Exec(m_sql).FirstOrDefault();

            if (user == null)
                throw new RecordNotFoundApiException($"The User with {id} cannot be found");

            var result = new User().CopyFrom(user, m_dto);
            return result;
        }

        public void Delete(Delete delete)
        {
            var db = new Db.User.List(delete.Id).Exec(m_sql).FirstOrDefault();

            if (db == null)
                throw new RecordNotFoundApiException($"The User with id {delete.Id} cannot be found");

            var request = new Db.User.Update
            {
                Id = delete.Id,
                Active = false
            };
            request.UpdationList = new[] { nameof(request.Active) };
            request.Exec(m_sql);
        }

        public User.UserDetails GetUserInfo(UserInfo user)
        {
            var res = new User.UserDetails
            {
                Roles = GetUserRoles(user.UserId)
            }.CopyFrom(GetDbUser(user.UserId), m_dto);

            return res;
        }

        public Db.User.List.Result GetDbUser(int userId)
        {
            var user = new Db.User.List(userId).Exec(m_sql).FirstOrDefault();
            if (user == null)
                throw new RecordNotFoundApiException(m_languageServices.GetKey("RecordNotFound"));

            return user;
        }

        public void UpdatePassword(int userId, string password)
        {
            var update = new Db.User.Update
            {
                Id = userId,
                PasswordHash = m_hashCalculator.GetPasswordHash(userId, password)
            };
            update.UpdationList = [nameof(update.PasswordHash)];
            update.Exec(m_sql);
        }

        public void UpdatePassword(User.UpdatePassword password, UserInfo userInfo)
        {
            var user = new Db.User.CheckUser(userInfo.UserId).Exec(m_sql);

            if (user == null)
                throw new RecordNotFoundApiException("User not found.");

            var hashToCheck = m_hashCalculator.GetPasswordHash(user.UserId, password.OldPassWord);

            if (!HashHelper.EqualArrays(user.PasswordHash, hashToCheck))
                throw new ApiException("Incorrect password", HttpStatusCode.BadRequest);

            AuthCheckUser(user.Login, password.OldPassWord);


            var update = new Db.User.Update
            {
                Id = user.UserId,
                PasswordHash = m_hashCalculator.GetPasswordHash(user.UserId, password.NewPassWord)
            };
            update.UpdationList = [nameof(update.PasswordHash)];
            update.Exec(m_sql);
        }

        [Obsolete("Udalim esli budet ne nujen")]
        public void UpdatePasswordAndState(int userId, string password)
        {
            var update = new Db.User.Update()
            {
                Id = userId,
                PasswordHash = m_hashCalculator.GetPasswordHash(userId, password),
                // State = User.Status.Active
            };
            update.UpdationList = new[] { nameof(update.PasswordHash) };
            update.Exec(m_sql);
        }
    }
}