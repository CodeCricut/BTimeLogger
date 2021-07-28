﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger
{
	public interface IIntervalRepository
	{
		Task<IEnumerable<Interval>> GetIntervals();
		Task<IEnumerable<Interval>> GetIntervals(Activity[] activities, DateTime from, DateTime to);
		Task AddInterval(Interval interval);
	}

	class IntervalRepository : IIntervalRepository
	{
		private readonly List<Interval> _intervals = new();

		public Task AddInterval(Interval interval)
		{
			return Task.Factory.StartNew(() => _intervals.Add(interval));
		}

		public Task<IEnumerable<Interval>> GetIntervals()
		{
			return Task.FromResult(_intervals.ToArray().AsEnumerable());
		}

		public Task<IEnumerable<Interval>> GetIntervals(Activity[] activities, DateTime from, DateTime to)
		{
			// TODO
			return GetIntervals();
		}
	}
}