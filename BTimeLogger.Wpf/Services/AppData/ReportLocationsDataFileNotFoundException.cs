using System;
using System.Runtime.Serialization;

namespace BTimeLogger.Wpf.Services.AppData
{
	class ReportLocationsDataFileNotFoundException : Exception
	{
		public ReportLocationsDataFileNotFoundException()
		{
		}

		public ReportLocationsDataFileNotFoundException(string message) : base(message)
		{
		}

		public ReportLocationsDataFileNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ReportLocationsDataFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
