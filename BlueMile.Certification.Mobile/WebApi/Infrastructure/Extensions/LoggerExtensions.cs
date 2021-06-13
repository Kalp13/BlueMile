using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlueMile.Certification.WebApi.Infrastructure.Extensions
{
	public static class LoggerHelpers
	{
		public static void TraceRequest(this ILogger logger, object model, [CallerMemberName] string operationName = null)
		{
			logger.LogInformation($"{operationName} - Request: {System.Text.Json.JsonSerializer.Serialize(model)}");
		}

		public static void TraceResponse(this ILogger logger, object model, [CallerMemberName] string operationName = null)
		{
			logger.LogInformation($"{operationName} - Response: {System.Text.Json.JsonSerializer.Serialize(model)}");
		}

		public static void TraceErrorResponse(this ILogger logger, Exception exception, object model, [CallerMemberName] string operationName = null)
		{
			logger.LogError($"{operationName} - Response: {System.Text.Json.JsonSerializer.Serialize(model)}, Exception: {exception.Message}");
		}
	}
}
