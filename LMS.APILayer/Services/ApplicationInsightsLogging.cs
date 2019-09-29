using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.APILayer.Services
{
    public class ApplicationInsightsLogging
    {
        public static TelemetryClient telemetryClient;
        public ApplicationInsightsLogging()
        {
            telemetryClient = new TelemetryClient();
        }
    }
}
