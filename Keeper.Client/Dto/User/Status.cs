namespace Keeper.Client
{
    public partial class User
	{
		public enum Status
		{
            Registered = 0,
            Active = 1,
			Disable = 2,
			Relieved = 3, // Освобожден от должности
			OnHoliday = 4,
			Absent = 5
		}
	}
}
