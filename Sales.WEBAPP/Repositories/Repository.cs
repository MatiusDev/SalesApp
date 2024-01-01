
using System.Text;
using System.Text.Json;

namespace Sales.WEBAPP.Repositories
{
	public class Repository : IRepository
	{
		#region Attributes
		private readonly HttpClient _httpClient;

		private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
		};
		#endregion

		#region Constructor
		public Repository(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		#endregion

		#region Methods
		private async Task<T> UnserializeAnswer<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
		{
			var strResponse = await httpResponse.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<T>(strResponse, jsonSerializerOptions);
		}
		public async Task<HttpResponseWrapper<T>> Get<T>(string url)
		{
			var responseHttp = await _httpClient.GetAsync(url);
			if (responseHttp.IsSuccessStatusCode)
			{
				var response = await UnserializeAnswer<T>(responseHttp, _jsonDefaultOptions);
				return new HttpResponseWrapper<T>(response, false, responseHttp);
			}

			return new HttpResponseWrapper<T>(default, true, responseHttp);
		}		

		public async Task<HttpResponseWrapper<object>> Post<T>(string url, T model)
		{
			var messageJSON = JsonSerializer.Serialize(model);
			var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
			var httpResponse = await _httpClient.PostAsync(url,messageContent);
			return new HttpResponseWrapper<object>(null, !httpResponse.IsSuccessStatusCode, httpResponse);
		}

		public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T model)
		{
			var messageJSON = JsonSerializer.Serialize(model);
			var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
			var httpResponse = await _httpClient.PostAsync(url, messageContent);
			if (httpResponse.IsSuccessStatusCode)
			{
				var response = await UnserializeAnswer<TResponse>(httpResponse, _jsonDefaultOptions);
				return new HttpResponseWrapper<TResponse>(response, false, httpResponse);
			}
			return new HttpResponseWrapper<TResponse>(default, !httpResponse.IsSuccessStatusCode, httpResponse);
		}
		#endregion

	}
}
