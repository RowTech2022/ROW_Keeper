using Bibliotekaen.Dto;
using Keeper.Client;
using Row.Common.Dto1;

namespace Keeper.Core
{
	[DtoContainer]
	public static class MainDto
	{
		public interface IImage
		{
			string? Image { get; set; }
		}

		public interface IEmail
		{
			string? Email { get; set; }
		}

		public interface IAddress
		{
			string? Address { get; set; }
		}

		public interface ICategoryDb
		{
			int CategoryId { get; set; }
		}

        public interface IUserType
        {
            UserType UserType { get; set; }
        }

        public interface IState
        {
            User.Status State { get; set; }
        }

        public interface ICategoryDbNull
		{
			int? CategoryId { get; set; }
		}

		public interface ICategoryDbWithName : ICategoryDb
		{
			string CategoryName { get; set; }
		}
		public interface ICategoryDbNullWithName : ICategoryDbNull
		{
			string CategoryName { get; set; }
		}

		public interface ICategoryApi
		{
			Information Category { get; set; }
		}

		#region ICategoryId
		[DtoConvert]
		static void Convert(ICategoryApi dst, ICategoryDbWithName src)
		{
			dst.Category = new Information(src.CategoryId, src.CategoryName);
		}
		[DtoConvert]
		static void Convert(ICategoryApi dst, ICategoryDbNullWithName src)
		{
			if (src.CategoryId != null)
				dst.Category = new Information(src.CategoryId.Value, src.CategoryName);
		}

		[DtoConvert]
		static void Convert(ICategoryDb dst, ICategoryApi src)
		{
			if (src?.Category?.Id != null)
				dst.CategoryId = src.Category?.Id ?? 0;
		}

		[DtoConvert]
		static void Convert(ICategoryDbNull dst, ICategoryApi src)
		{
			dst.CategoryId = src?.Category?.Id;
		}


		#endregion

		public interface IId
		{
			int Id { get; set; }
		}

		public interface IUserId
		{
			int UserId { get; set; }
		}

		public interface IReqUserId
		{
			int ReqUserId { get; set; }
		}

		public interface IOwnerId
		{
			int OwnerId { get; set; }
		}

		public interface IOrgId
		{
			int OrgId { get; set; }
		}
		
		public interface IBranchId
		{
			int BranchId { get; set; }
		}
		
		public interface IActive
		{
			bool Active { get; set; }
		}
		
		public interface ILangType
		{
			int LangType { get; set; }
		}

		public interface IIds
		{
			int[]? Ids { get; set; }
		}

		public interface IInn
		{
			string Inn { get; set; }
		}

        public interface IInnNullable
        {
            string? Inn { get; set; }
        }

        public interface IPhone
		{
			string Phone { get; set; }
		}

        public interface ILogin
        {
            string Login { get; set; }
        }

        public interface IAdress
		{
			string Address { get; set; }
		}


		public interface IIdNullable
		{
			int? Id { get; set; }
		}

		#region Id

		[DtoConvert]
		static void Convert(IIdNullable dst, IId src)
		{
			dst.Id = src.Id;
		}

		[DtoConvert]
		static void Convert(IId dst, IIdNullable src)
		{
			dst.Id = src.Id ?? 0;
		}
		#endregion

		public interface ITimestamp
		{
			byte[] Timestamp { get; set; }
		}

		public interface IUpdationList
		{
			string[] UpdationList { get; set; }
		}

		public interface ILife
		{
			DateTimeOffset CreatedAt { get; set; }
			DateTimeOffset UpdatedAt { get; set; }
		}

		public interface IPageInfoSource
		{
			PageInfo PageInfo { get; set; }
		}

        public interface IPageInfoSource_Public
        {
            PageInfo PageInfo { get; set; }
        }

        [DtoConvert]
        static void Convert(IPageInfoDb dst, IPageInfoSource_Public src, DtoComplex dto)
        {
            dst.CopyFrom(src.PageInfo, dto);
        }

        [DtoConvert]
		static void Convert(IPageInfoDb dst, IPageInfoSource src, DtoComplex dto)
		{
			dst.CopyFrom(src.PageInfo, dto);
		}

		public interface IPageInfo
		{
			int PageNumber { get; set; }
			int PageSize { get; set; }
		}

		public interface IPageInfoDb
		{
			int? Start { get; set; }
			int? Count { get; set; }
		}

		#region Result

		[DtoConvert]
		static void Convert(IPageInfoDb dst, IPageInfo src)
		{
			if (src != null)
			{
				if (src.PageNumber == 0)
					src.PageNumber = 1;
				if (src.PageSize == 0)
					src.PageSize = 10;

				dst.Start = (src.PageNumber - 1) * src.PageSize;
				dst.Count = src.PageSize;
			}
			else
			{
				dst.Start = 0;
				dst.Count = 10;
			}
		}

		#endregion

		public class Client_IPageInfo : PageInfo, IPageInfo { }
	}
}
