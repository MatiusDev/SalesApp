
using System.Text;
using System.Text.Json;

namespace Sales.WEB.Repositories
{
	public class Repository : IRepository
	{
		#region Attributes
		private readonly HttpClient _httpClient;

		private JsonSerializerOptions _jsonDefaultOptions;
		#endregion

		#region Constructor
		public Repository(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions)
		{
			_httpClient = httpClient;
			_jsonDefaultOptions = jsonSerializerOptions;
		}
		#endregion

		#region Methods
		private async Task<T> UnserializeAnswer<T>(HttpResponseMessage httpResponse)
		{
			var strResponse = await httpResponse.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<T>(strResponse, _jsonDefaultOptions);
		}
		public async Task<HttpResponseWrapper<T>> Get<T>(string url)
		{
			var response = await _httpClient.GetAsync(url);
			if (response.IsSuccessStatusCode)
			{
				var UnserializedResponse = await UnserializeAnswer<T>(response);
				return new HttpResponseWrapper<T>(UnserializedResponse, false, response);
			}

			return new HttpResponseWrapper<T>(default, true, response);
		}

		public async Task<HttpResponseWrapper<object>> Post<T>(string url, T model)
		{
			var messageJSON = JsonSerializer.Serialize(model);
			var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(url, messageContent);
			return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
		}

		public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T model)
		{
			var messageJSON = JsonSerializer.Serialize(model);
			var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(url, messageContent);
			if (response.IsSuccessStatusCode)
			{
				var UnserializedResponse = await UnserializeAnswer<TResponse>(response);
				return new HttpResponseWrapper<TResponse>(UnserializedResponse, false, response);
			}
			return new HttpResponseWrapper<TResponse>(default, !response.IsSuccessStatusCode, response);
		}
		public async Task<HttpResponseWrapper<object>> Put<T>(string url, T model)
		{
			var messageJSON = JsonSerializer.Serialize(model);
			var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync(url, messageContent);
			return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
		}

		public async Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T model)
		{
			var messageJSON = JsonSerializer.Serialize(model);
			var messageContent = new StringContent(messageJSON, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync(url, messageContent);
			if (response.IsSuccessStatusCode)
			{
				var UnserializedResponse = await UnserializeAnswer<TResponse>(response);
				return new HttpResponseWrapper<TResponse>(UnserializedResponse, false, response);
			}
			return new HttpResponseWrapper<TResponse>(default, !response.IsSuccessStatusCode, response);
		}
		public async Task<HttpResponseWrapper<object>> Delete(string url)
		{
			var response = await _httpClient.DeleteAsync(url);
			return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
		}
		#endregion

	}
}
