﻿using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Reflection;
using Skybrud.Siteimprove.Beta2.Endpoints.Raw;
using Skybrud.Social;
using Skybrud.Social.Http;

namespace Skybrud.Siteimprove.Beta2 {
    
    public class SiteimproveClient {

        public const string ApiUrl = "http://api.siteimprove.com/beta2/";

        #region Properties

        public NetworkCredential Crendentials { get; private set; }

        public SiteimproveSitesRawEndpoint Sites { get; private set; }
        public SiteimprovePagesRawEndpoint Pages { get; private set; }

        #endregion

        #region Constructor

        private SiteimproveClient() {
            Sites = new SiteimproveSitesRawEndpoint(this);
            Pages = new SiteimprovePagesRawEndpoint(this);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Initialize a new instance of <code>SiteimproveRawClient</code> based on values from the app settings.
        /// </summary>
        public static SiteimproveClient CreateFromConfig() {
            string username = ConfigurationManager.AppSettings["SiteimproveUsername"];
            string password = ConfigurationManager.AppSettings["SiteimprovePassword"];
            return new SiteimproveClient {
                Crendentials = new NetworkCredential(username, password)
            };
        }

        /// <summary>
        /// Initialize a new instance of <code>SiteimproveRawClient</code> from the specified username and password.
        /// </summary>
        /// <param name="username">The username of the Siteimprove account.</param>
        /// <param name="password">The password of the Siteimprove account.</param>
        public static SiteimproveClient CreateFromCredentials(string username, string password) {
            return new SiteimproveClient {
                Crendentials = new NetworkCredential(username, password)
            };
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Make a HTTP GET request to the specified URL.
        /// </summary>
        /// <param name="url">The URL of the request.</param>
        public SocialHttpResponse DoHttpGetRequest(string url) {
            return new SocialHttpRequest {
                Url = url,
                Credentials = Crendentials
            }.GetResponse();
        }

        /// <summary>
        /// Make a HTTP GET request to the specified URL.
        /// </summary>
        /// <param name="url">The URL of the request.</param>
        /// <param name="query">The query string of the request.</param>
        public SocialHttpResponse DoHttpGetRequest(string url, NameValueCollection query) {

            // Convert the query to a string
            string queryString = SocialUtils.NameValueCollectionToQueryString(query);

            // Append the query string
            if (queryString.Length > 0) {
                url += url.Contains("?") ? "&" + queryString : "?" + queryString;
            }

            // Make the request to the API
            return new SocialHttpRequest {
                Url = url,
                Credentials = Crendentials
            }.GetResponse();

        }

        public SocialHttpResponse DoHttpGetRequest(string url, object query) {

            NameValueCollection nvc = new NameValueCollection();

            // Get all public properties from the specified data object
            PropertyInfo[] properties = query.GetType().GetProperties();

            foreach (PropertyInfo property in properties) {
                object value = property.GetValue(query, new object[0]);
                nvc[property.Name] = value == null ? null : value + "";
            }

            return DoHttpGetRequest(url, nvc);

        }

        public NameValueCollection ObjectToNameValueCollection(object query) {

            NameValueCollection nvc = new NameValueCollection();

            foreach (PropertyInfo property in query.GetType().GetProperties()) {
                object value = property.GetValue(query, new object[0]);
                nvc[property.Name] = value == null ? null : value + "";
            }

            return nvc;

        }

        #endregion

    }

}
