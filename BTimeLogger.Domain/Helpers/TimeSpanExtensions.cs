using System;

namespace BTimeLogger.Domain.Helpers
{
	public static class TimeSpanExtensions
	{
		public static decimal PercentOf(this TimeSpan dividend, TimeSpan divisor)
		{
			return (decimal)dividend.Duration().Ticks / (decimal)divisor.Duration().Ticks;
		}
	}
}
