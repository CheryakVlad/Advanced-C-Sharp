using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace HttpHandler.Tests.Handlers
{
	public class OrderReportHandlerTest
	{
		#region Private fields 

		private HttpClient _client;

		#endregion

		#region Test context

		public OrderReportHandlerTest()
		{
			_client = new HttpClient();
		}

		#endregion

		[Fact]
		public async Task Test_should_check_that_handler_returns_Excel_file_as_attachment()
		{
			#region ARRANGE

			string url = "http://localhost/Module10/handler?customer=VINET";

			HttpRequestMessage request = new HttpRequestMessage
			{
				RequestUri = new Uri(url),
				Method = HttpMethod.Get
			};

			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			#endregion

			#region ACT and ASSERT

			using (var response = await _client.SendAsync(request))
			{
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("attachment", response.Content.Headers.ContentDisposition.DispositionType);
				Assert.Contains(".xlsx", response.Content.Headers.ContentDisposition.FileName);
			}

			#endregion
		}

		[Fact]
		public async Task Test_should_check_that_handler_returns_Xml_file_as_attachment()
		{
			#region ARRANGE

			string url = "http://localhost/Module10/handler?customer=VINET";

			HttpRequestMessage request = new HttpRequestMessage
			{
				RequestUri = new Uri(url),
				Method = HttpMethod.Get
			};

			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

			#endregion

			#region ACT and ASSERT

			using (var response = await _client.SendAsync(request))
			{
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("attachment", response.Content.Headers.ContentDisposition.DispositionType);
				Assert.Contains(".xml", response.Content.Headers.ContentDisposition.FileName);
			}

			#endregion
		}

		[Fact]
		public async Task Test_should_check_that_handler_returns_document_without_order_elements()
		{
			#region ARRANGE

			string url = "http://localhost/Module10/handler?customer=12";

			HttpRequestMessage request = new HttpRequestMessage
			{
				RequestUri = new Uri(url),
				Method = HttpMethod.Get
			};

			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

			#endregion

			#region ACT and ASSERT

			using (var response = await _client.SendAsync(request))
			{
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("attachment", response.Content.Headers.ContentDisposition.DispositionType);
				Assert.Contains(".xml", response.Content.Headers.ContentDisposition.FileName);

				var textContent = response.Content.ReadAsStringAsync().Result;

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(textContent);

				XmlElement xRoot = xmlDoc.DocumentElement;

				XmlNodeList childnodes = xRoot.SelectNodes("*");

				Assert.True(childnodes.Count == 0);
			}

			#endregion
		}

		[Fact]
		public async Task Test_should_check_that_handler_returns_number_of_order_if_add_skip()
		{
			#region ARRANGE

			string url = "http://localhost/Module10/handler?customer=VINET&skip=1";

			HttpRequestMessage request = new HttpRequestMessage
			{
				RequestUri = new Uri(url),
				Method = HttpMethod.Get
			};

			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

			#endregion

			#region ACT and ASSERT

			using (var response = await _client.SendAsync(request))
			{
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("attachment", response.Content.Headers.ContentDisposition.DispositionType);
				Assert.Contains(".xml", response.Content.Headers.ContentDisposition.FileName);

				var textContent = response.Content.ReadAsStringAsync().Result;

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(textContent);

				XmlElement xRoot = xmlDoc.DocumentElement;

				XmlNodeList childnodes = xRoot.SelectNodes("*");

				Assert.True(childnodes.Count == 4);
			}

			#endregion
		}

		[Fact]
		public async Task Test_should_check_that_handler_returns_number_of_order_if_add_skip_and_take()
		{
			#region ARRANGE

			string url = "http://localhost/Module10/handler?customer=VINET&skip=1&take=2";

			var request = new HttpRequestMessage
			{
				RequestUri = new Uri(url),
				Method = HttpMethod.Get
			};

			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

			#endregion

			#region ACT and ASSERT

			using (var response = await _client.SendAsync(request))
			{
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("attachment", response.Content.Headers.ContentDisposition.DispositionType);
				Assert.Contains(".xml", response.Content.Headers.ContentDisposition.FileName);

				var textContent = response.Content.ReadAsStringAsync().Result;

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(textContent);

				XmlElement xRoot = xmlDoc.DocumentElement;

				XmlNodeList childnodes = xRoot.SelectNodes("*");

				Assert.True(childnodes.Count == 2);
			}

			#endregion
		}

		[Fact]
		public async Task Test_should_check_that_handler_returns_number_of_order_if_add_dateTo_and_dateFrom()
		{
			#region ARRANGE

			string url = "http://localhost/Module10/handler?dateFrom=1996-12-16&dateTo=1996-12-31";

			var request = new HttpRequestMessage
			{
				RequestUri = new Uri(url),
				Method = HttpMethod.Get
			};

			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

			#endregion

			#region ACT and ASSERT

			using (var response = await _client.SendAsync(request))
			{
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("attachment", response.Content.Headers.ContentDisposition.DispositionType);
				Assert.Contains(".xml", response.Content.Headers.ContentDisposition.FileName);

				var textContent = response.Content.ReadAsStringAsync().Result;

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(textContent);

				XmlElement xRoot = xmlDoc.DocumentElement;

				XmlNodeList childnodes = xRoot.SelectNodes("*");

				Assert.True(childnodes.Count == 15);
			}

			#endregion
		}

		[Fact]
		public async Task Test_should_check_that_handler_returns_number_of_order_if_add_skip_and_dateTo_and_dateFrom()
		{
			#region ARRANGE

			string url = "http://localhost/Module10/handler?dateFrom=1996-12-16&dateTo=1996-12-31&skip=10";

			var request = new HttpRequestMessage
			{
				RequestUri = new Uri(url),
				Method = HttpMethod.Get
			};

			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

			#endregion

			#region ACT and ASSERT

			using (var response = await _client.SendAsync(request))
			{
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("attachment", response.Content.Headers.ContentDisposition.DispositionType);
				Assert.Contains(".xml", response.Content.Headers.ContentDisposition.FileName);

				var textContent = response.Content.ReadAsStringAsync().Result;

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(textContent);

				XmlElement xRoot = xmlDoc.DocumentElement;

				XmlNodeList childnodes = xRoot.SelectNodes("*");

				Assert.True(childnodes.Count == 5);
			}

			#endregion
		}

		[Fact]
		public async Task Test_should_check_that_handler_returns_number_of_order_if_add_skip_and_dateTo_and_dateFrom_and_take()
		{
			#region ARRANGE

			string url = "http://localhost/Module10/handler?dateFrom=1996-12-16&dateTo=1996-12-31&skip=10&take=3";

			var request = new HttpRequestMessage
			{
				RequestUri = new Uri(url),
				Method = HttpMethod.Get
			};

			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

			#endregion

			#region ACT and ASSERT

			using (var response = await _client.SendAsync(request))
			{
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("attachment", response.Content.Headers.ContentDisposition.DispositionType);
				Assert.Contains(".xml", response.Content.Headers.ContentDisposition.FileName);

				var textContent = response.Content.ReadAsStringAsync().Result;

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(textContent);

				XmlElement xRoot = xmlDoc.DocumentElement;

				XmlNodeList childnodes = xRoot.SelectNodes("*");

				Assert.True(childnodes.Count == 3);
			}

			#endregion
		}
	}
}
