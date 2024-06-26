﻿using Bibliotekaen.Dto;
using Keeper.Client;
using Row.Common1;

namespace Keeper.Core
{
    public partial class UserEngine
    {
        public User.Role.List Roles()
        {
            var request = new Db.User.Role.List().Exec(m_sql);

            return new User.Role.List
            {
                Items = request.Select(x => new User.Role.List.Item().CopyFrom(x, m_dto)).ToList()
            };
        } 
        
        public void SetRole(User.UserRoleAccess.Set request, UserInfo user)
        {
            var db = new Db.User.UserRoleAccess.Create().CopyFrom(request, m_dto);
            db.RegUserId = user.UserId;
            db.Exec(m_sql);
        }

        public void SetRole(User.UserRoleAccess.Set request)
        {
            var db = new Db.User.UserRoleAccess.Create
            {
                UserId = request.UserId,
                RoleId = request.RoleId
            }.CopyFrom(request, m_dto);
            db.Exec(m_sql);
        }

        public void RemoveRole(User.UserRoleAccess.Remove request, UserInfo user)
        {
            var db = new Db.User.UserRoleAccess.Remove().CopyFrom(request, m_dto);
            db.Exec(m_sql);
        }
    }
}