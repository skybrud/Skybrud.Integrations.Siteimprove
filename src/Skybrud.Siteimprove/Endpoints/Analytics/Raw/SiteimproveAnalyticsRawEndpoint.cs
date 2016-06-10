﻿namespace Skybrud.Siteimprove.Endpoints.Analytics.Raw {
    
    public class SiteimproveAnalyticsRawEndpoint {

        #region Properties

        public SiteimproveClient Client { get; private set; }

        public SiteimproveAnalyticsContentRawEndpoint Content { get; private set; }

        public SiteimproveAnalyticsVisitorsRawEndpoint Visitors { get; private set; }

        #endregion

        #region Constructor

        internal SiteimproveAnalyticsRawEndpoint(SiteimproveClient client) {
            Client = client;
            Content = new SiteimproveAnalyticsContentRawEndpoint(client, this);
            Visitors = new SiteimproveAnalyticsVisitorsRawEndpoint(client, this);
        }

        #endregion

    }

}