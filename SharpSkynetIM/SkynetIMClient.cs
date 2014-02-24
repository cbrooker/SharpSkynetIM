using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;

namespace SharpSkynetIM
{
    public class SkynetIMClient
    {
        private const String BaseUrl = "http://192.168.1.198:3000";

        private T Execute<T>(IRestRequest request) where T : new()
        {
            if (request == null) throw new ArgumentNullException("request");
            var client = new RestClient
            {
                BaseUrl = BaseUrl
            };
            
            // Override default RestSharp JSON deserializer
            client.AddHandler("application/json", new DynamicJsonDeserializer());

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var nexalogyException = new ApplicationException(message, response.ErrorException);
                throw nexalogyException;
            }

            return response.Data;
        }
        private T ExecutePost<T>(IRestRequest request) where T : new()
        {
            var client = new RestClient { BaseUrl = BaseUrl };
            request.Method = Method.POST;

            // Override default RestSharp JSON deserializer
            client.AddHandler("application/json", new DynamicJsonDeserializer());

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var nexalogyException = new ApplicationException(message, response.ErrorException);
                throw nexalogyException;
            }
            return response.Data;
        }


        #region Skynet.im Status
        
        public dynamic SkynetStatus()
        {
            var request = new RestRequest { Resource = "status" };
            return Execute<dynamic>(request);
        }

        #endregion

        #region Skynet.im Devices

        public dynamic Devices()
        {
            var request = new RestRequest { Resource = "devices" };
            return Execute<dynamic>(request);
        }
        public dynamic Devices(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var request = new RestRequest { Resource = "devices" };
            AddRequestParameters(parameters, request);
            return Execute<dynamic>(request);
        }
        public dynamic Devices(string uuid)
        {
            var request = new RestRequest { Resource = string.Format("devices/{0}", uuid)  };
            return Execute<dynamic>(request);
        }

        public dynamic AddDevice(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var request = new RestRequest { Resource = "devices" };
            AddRequestParameters(parameters, request);
            return ExecutePost<dynamic>(request);
        }
        
        #endregion

        #region Skynet.im Ip Address

        public dynamic IpAddress()
        {
            var request = new RestRequest { Resource = "ipaddress" };
            return Execute<dynamic>(request);
        }

        #endregion

        #region Static Methods
        private static void AddRequestParameters(IEnumerable<KeyValuePair<string, string>> parameters, IRestRequest request)
        {
            foreach (var p in parameters)
            {
                request.AddParameter(p.Key, p.Value);
            }
        }
        #endregion

    }
}
