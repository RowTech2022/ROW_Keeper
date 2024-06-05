using System;

namespace Keeper.Client
{
    public class EnumNameAttribute : Attribute
	{
		public string Name { get; set; }

		public EnumNameAttribute(string name)
		{
			Name = name;
		}
	}

	public class EnumMapStatusNameAttribute : Attribute
	{
		public Type Status { get; set; }

		public EnumMapStatusNameAttribute(Type sts)
		{
			Status = sts;
		}
	}
}
