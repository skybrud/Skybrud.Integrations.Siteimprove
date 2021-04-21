﻿using System;
using Skybrud.Integrations.Siteimprove.Objects.Analytics.Content;
using Skybrud.Social.Http;

namespace Skybrud.Integrations.Siteimprove.Responses.Analytics.Content {

    public class SiteimproveAnalyticsGetPopularPagesResponse : SiteimproveResponse<SiteimproveAnalyticsPopularPagesList> {

        #region Constructors

        private SiteimproveAnalyticsGetPopularPagesResponse(SocialHttpResponse response) : base(response) {

            // Validate the response
            ValidateResponse(response);

            // Parse the response body
            Body = ParseJsonObject(response.Body, SiteimproveAnalyticsPopularPagesList.Parse);

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <code>response</code> into an instance of <see cref="SiteimproveAnalyticsGetPopularPagesResponse"/>.
        /// </summary>
        /// <param name="response">The response to be parsed.</param>
        /// <returns>Returns an instance of <see cref="SiteimproveAnalyticsGetPopularPagesResponse"/>.</returns>
        public static SiteimproveAnalyticsGetPopularPagesResponse ParseResponse(SocialHttpResponse response) {

            // Some input validation
            if (response == null) throw new ArgumentNullException("response");

            // Initialize the response object
            return new SiteimproveAnalyticsGetPopularPagesResponse(response);

        }

        #endregion

    }
}