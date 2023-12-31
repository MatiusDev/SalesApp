using System.Net;

namespace Sales.WEB.Repositories
{
	public class HttpResponseWrapper<T>
	{
		#region Constructor
		public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
		{
			Response = response;
			Error = error;
			HttpResponseMessage = httpResponseMessage;
		}
		#endregion

		#region Props
		public T? Response { get; set; }
		public bool Error { get; set; }
		public HttpResponseMessage HttpResponseMessage { get; set; }
		#endregion

		#region Methods
		public async Task<string?> GetErrorMessage()
		{
			if (!Error) return null;

			var statusCode = HttpResponseMessage.StatusCode;
			switch (statusCode)
			{
				case HttpStatusCode.NotFound:
					return "Recurso no encontrado";
				case HttpStatusCode.BadRequest:
					return await HttpResponseMessage.Content.ReadAsStringAsync();
				case HttpStatusCode.Unauthorized:
					return "Tienes que loguearte para hacer esta operación";
				case HttpStatusCode.Forbidden:
					return "No tienes permisos para hacer esta operación";
				default:
					return "Ha ocurrido un error inesperado";
			}
		}
		#endregion
	}
}
