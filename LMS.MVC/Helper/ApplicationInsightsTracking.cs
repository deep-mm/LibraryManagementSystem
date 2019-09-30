using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.MVC.Helper
{
    public class ApplicationInsightsTracking
    {
        public TelemetryClient telemetryClient;
        public ApplicationInsightsTracking()
        {
            telemetryClient = new TelemetryClient();
        }

        public void TrackEvent(string eventName)
        {
            telemetryClient.TrackEvent(eventName);
        }

        public void TrackException(Exception exception)
        {
            var telemetry = new ExceptionTelemetry(exception);
            telemetry.Properties.Add("Message", exception.Message);
            telemetryClient.TrackException(exception);
        }
    }
}
