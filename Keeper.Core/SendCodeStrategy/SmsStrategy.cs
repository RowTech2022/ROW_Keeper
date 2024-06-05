﻿using Keeper.Client;

namespace Keeper.Core
{
    public class SmsStrategy : ISendStrategy
    {
		string m_messageText;
		string m_messageUrl;

		public SmsStrategy(string messgeUrl, string messageText)
		{
			m_messageText = messageText;
			m_messageUrl = messgeUrl;
		}

		public async void SendCodeToPhone(User.UserCode request, string phone, string Code, string UserName)
		{
			var i = m_messageText.IndexOf(")");
			var newText = m_messageText.Insert(i + 1, $" {UserName},\n");
			var TextCode = $"{newText} \n{Code}";
			using var httpClient = new HttpClient();
			var response = await httpClient.GetAsync($"{m_messageUrl}={phone}&mess={TextCode}");
		}

		public async void SendLoginAndPasswordToPhone(string phone, string login, string pass)
		{
			var i = m_messageText.IndexOf(")");
			var newText = m_messageText.Insert(i + 1, $" {login},\n");
			var TextCode = $"{newText} \n{pass}";
			using var httpClient = new HttpClient();
			var response = await httpClient.GetAsync($"{m_messageUrl}={phone}&mess={TextCode}");
		}
	}
}
