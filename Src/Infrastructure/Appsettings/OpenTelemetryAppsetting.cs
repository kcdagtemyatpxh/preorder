﻿using Microsoft.Extensions.Configuration;
using TripleSix.Core.Appsettings;

namespace Sample.Infrastructure.Appsettings
{
    public class OpenTelemetryAppsetting : BaseAppsetting
    {
        public OpenTelemetryAppsetting(IConfiguration configuration)
            : base(configuration, "OpenTelemetry")
        {
            if (ServiceName is null) throw new ArgumentNullException(nameof(ServiceName));
        }

        /// <summary>
        /// Tên service.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Phiên bản của service.
        /// </summary>
        public string ServiceVersion { get; set; } = "1.0.0";

        /// <summary>
        /// Bật/tắt hiển thị trace trên console.
        /// </summary>
        public bool EnableConsoleExporter { get; set; } = false;

        /// <summary>
        /// Bật/tắt gửi trace lên jarger.
        /// </summary>
        public bool EnableJaegerExporter { get; set; } = false;

        /// <summary>
        /// Jaeger host để gửi trace.
        /// </summary>
        public string? JaegerHost { get; set; }

        /// <summary>
        /// Jaeger port để gửi trace.
        /// </summary>
        public int JaegerPort { get; set; } = 6831;
    }
}
