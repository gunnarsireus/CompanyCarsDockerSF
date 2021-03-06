﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarClient
{
	public static class Utils
	{
        private static readonly string fqdn = Environment.GetEnvironmentVariable("Fabric_NodeIPOrFQDN");
        private static readonly Uri Endpoint = new Uri(Environment.GetEnvironmentVariable("CarApiUrlReverseProxy").Replace("localhost",fqdn));
		public static async Task<T> Get<T>(string url)
		{
            HttpClientHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using (var client = new HttpClient(handler) { BaseAddress = Endpoint })
            {
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				var response = await client.GetAsync(url);
				var content = await response.Content.ReadAsStringAsync();
				return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
			}
		}

		// INSERT
		public static async Task<T> Post<T>(string url, object data)
		{
            HttpClientHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using (var client = new HttpClient(handler) { BaseAddress = Endpoint })
            {
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				var httpContent = new StringContent(JsonConvert.SerializeObject(data));
				httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

				var response = await client.PostAsync(url, httpContent);
				var content = await response.Content.ReadAsStringAsync();
				return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
			}
		}

		// UPDATE
		public static async Task<T> Put<T>(string url, object data)
		{
            HttpClientHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using (var client = new HttpClient(handler) { BaseAddress = Endpoint })
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				var httpContent = new StringContent(JsonConvert.SerializeObject(data));
				httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

				var response = await client.PutAsync(url, httpContent);
				var content = await response.Content.ReadAsStringAsync();
				return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
			}
		}

		// DELETE

		public static async Task<T> Delete<T>(string url)
		{
            HttpClientHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using (var client = new HttpClient(handler) { BaseAddress = Endpoint })
            {
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				var response = await client.DeleteAsync(url);
				var content = await response.Content.ReadAsStringAsync();
				return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
			}
		}
	}
}