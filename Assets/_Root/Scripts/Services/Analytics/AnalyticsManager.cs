using Services.Analytics.UnityAnalytics;
using System.Collections.Generic;

namespace Services.Analytics
{
    internal class AnalyticsManager
    {
        private static AnalyticsManager _instance;

        public static AnalyticsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AnalyticsManager();
                }
                return _instance;
            }
        }

        private static IAnalyticsService[] _services;

        private AnalyticsManager()
        {
            InitializeServices();
        }

        private void InitializeServices()
        {
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }


        public void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }

        public void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName, eventData);
        }

        public void SentTransaction(string productId, decimal amount, string currency)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendTransaction(productId, amount, currency);
        }
    }
}
