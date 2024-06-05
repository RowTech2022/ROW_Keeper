using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Core.Context.Models;

[Index(nameof(UserId))]
public class UserAuthCode : BaseModel
{
	public int Id { get; set; }

	public int UserId { get; set; }
		
	public int? Code { get; set; }
		
	[StringLength(20)]
	public string? PhoneNumber { get; set; }
}