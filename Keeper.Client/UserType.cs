using System.Collections.Generic;

namespace Keeper.Client
{
    public enum UserType
	{
		Admin = 1,
        Manager = 2,
		Cashier = 3
    }

	public class InformationWithAvatart : Information
    {
		public InformationWithAvatart(int id, string name, string avatar) : base(id, name)
		{
			Avatar = avatar;
		}

		public string Avatar { get; set; }
	}

	public class InforamtionWithFlag : Information
	{
		public InforamtionWithFlag() { }
		public InforamtionWithFlag(int id, string name, bool include = false) : base(id, name)
		{
			Include = include;
		}

		public bool Include { get; set; }
	}

	public class Information
	{
		public int Id { get; set; }
		public string Value { get; set; } = "";

		public Information() { }
		public Information(int id)
		{
			Id = id;
			Value = "Тест";
		}
		public Information(int id, string value)
		{
			Id = id;
			Value = value;
		}
	}

	public class IncomeTransition : Transition
	{
		public  bool FirstLevel { get; set; }
	}

	public class Transition
	{
		public Dictionary<string, Setting> FieldSettings { get; set; } = new Dictionary<string, Setting>();
		public Dictionary<string, Setting> ButtonSettings { get; set; } = new Dictionary<string, Setting>();
		public Dictionary<int, List<string>> ButtonInfo { get; set; } = new Dictionary<int, List<string>>();

		public class Setting
		{
			public bool ReadOnly { get; set; }
			public bool Hide { get; set; }

			public Setting(bool readOnly = false, bool hide = false)
			{
				ReadOnly = readOnly;
				Hide = hide;
			}
		}
	}
}
