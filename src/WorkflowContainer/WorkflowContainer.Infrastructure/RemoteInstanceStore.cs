using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using WorkflowContainer.Infrastructure.InstanceStoreBase;

namespace WorkflowContainer.Infrastructure
{
    /// <summary>
    /// Does all remote operations synchronously.
    /// Uses a static instance owner, sets as default owner.
    /// </summary>
    public class RemoteInstanceStore : CustomInstanceStoreBase
    {
        const string _jsonMediaType = "application/json";
        const string _remoteStoreSaveUrl = "http://instancestore/api/save-workflow";
        const string _remoteStoreLoadUrl = "http://instancestore/api/get-workflow/{0}";

        public RemoteInstanceStore() 
            : base(new Guid("24c35e4c-1a2c-4096-912c-23836d2e93ee")) //static instance owner
        { }

        public override void Save(Guid instanceId, XmlDocument doc)
        {
            try
            {
                var contextXml = doc.OuterXml;
                SaveToRemoteStore(instanceId.ToString(), contextXml);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Could not save workflow instance. Exception: {ex.ToString()}");
                throw;
            }            
        }

        public override XmlDocument Load(Guid instanceId)
        {
            try
            {
                var contextXmlString = FetchFromRemoteStore(instanceId.ToString());

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(contextXmlString);

                return doc;
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Could not load workflow instance. Exception: {ex.ToString()}");
                throw;
            }            
        }

        void SaveToRemoteStore(string instanceId, string context)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));
                        
            var payload = new
            {
                InstanceId = instanceId,
                Context = context
            };
            var payloadStringContet = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                _jsonMediaType);
            var response = httpClient
                .PostAsync(_remoteStoreSaveUrl, payloadStringContet)
                .Result;
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = $"Could not store context to remote store. HTTP: {response.StatusCode}, Reason: {response.ReasonPhrase}";
                Trace.TraceError(errorMessage);
                throw new HttpRequestException(errorMessage);
            }
        }

        string FetchFromRemoteStore(string instanceId)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));

            var getInstanceUrl = string.Format(_remoteStoreLoadUrl, instanceId);
            var response = httpClient
                .GetAsync(getInstanceUrl)
                .Result;
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = $"Could not fetch context from remote store. HTTP: {response.StatusCode}, Reason: {response.ReasonPhrase}";
                Trace.TraceError(errorMessage);
                throw new HttpRequestException(errorMessage);
            }
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            var responseData = JsonConvert.DeserializeObject<StoreResponse>(jsonResponse);
            return responseData.Context;
        }
    }

    class StoreResponse
    {
        public string InstanceId { get; set; }
        public string Bookmark { get; set; }
        public string Context { get; set; }
    }
}
