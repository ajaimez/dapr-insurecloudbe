using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace InsureCloudBE.Controllers
{	
	[ApiController]
	[Route("[controller]")]
	public class ClientesController : ControllerBase
	{		
		private readonly ILogger<ClientesController> _logger;
		private readonly DaprClient _daprClient;

		public ClientesController( DaprClient daprClient, ILogger<ClientesController> logger)
		{

			_logger = logger;
			_daprClient = daprClient;

			//Dapr = new DaprClientBuilder()
			//	   //.UseJsonSerializationOptions(config)
			//	   .Build();
		}	

		[HttpGet(Name = "GetCliente")]
		public async Task<IEnumerable<Cliente>> Get(string TipoDoc,string NroDoc,string Aseguradora)
		{

			var response = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get, "integrationa", "WeatherForecast");

			return Enumerable.Range(1, 1).Select(index => new Cliente
			{
				TipoDoc = TipoDoc,
				NroDoc = Random.Shared.Next(-20, 55).ToString(),
				Aseguradora = response.First().Summary
			})
			.ToArray();
		}
	}

	public class Cliente
	{
		public string? TipoDoc { get; set; }

		public string? NroDoc { get; set; }

		public string? Aseguradora { get; set; }
	}
}
