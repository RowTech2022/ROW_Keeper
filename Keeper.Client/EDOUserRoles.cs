namespace Keeper.Client
{
	public enum EDOUserRoles
	{
		Admin = 1,
		Kurator = 2,
		Manager = 3,

		Client = 4,
		ActivatedUser = 7,
		DisabledUser = 8,


		Minister = 9,
		FirstDeputyMinister = 10,
		DeputyMinister = 11,
		Chancery = 12,						//Концелярия						

		Bukhgalter  = 13,
		Rukovoditel = 14,
		KuratorPodgotovki = 15,
		NachKuratorPodgotovki =16,
		KuratorGosDokhod = 17,
		KuratorIspolnenie = 18,
		NachKuratorIspolnenie = 19,
		RukovoditelFinUpravlenie = 20,
		KuratorBankKaznachey = 21,
		NachalnikGUCK = 22,
		NachalnikBudgetUpravlenie = 23,
		NachalnikNalogUpravlenie = 24,

		NachMestnogo = 25,

		TFMIS_Sardor = 26  // max value
	}
}