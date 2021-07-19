﻿using System;
using System.Runtime.Serialization;

namespace BTimeLogger.Wpf.Services.ViewManagement
{
	[Serializable]
	internal class AssociatedViewNotWindowException : Exception
	{
		public AssociatedViewNotWindowException()
		{
		}

		public AssociatedViewNotWindowException(string message) : base(message)
		{
		}

		public AssociatedViewNotWindowException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected AssociatedViewNotWindowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}