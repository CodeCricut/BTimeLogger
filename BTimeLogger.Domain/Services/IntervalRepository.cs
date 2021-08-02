using BTimeLogger.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BTimeLogger.Activity;

namespace BTimeLogger.Domain.Services
{
	public interface IIntervalRepository
	{
		Task<IEnumerable<Interval>> GetIntervals();
		Task<IEnumerable<Interval>> GetIntervals(IEnumerable<ActivityCode> activityCodes, DateTime? from, DateTime? to);
		Task<IEnumerable<Interval>> GetIntervals(ActivityCode activityCode, DateTime? from, DateTime? to);

		Task<Interval> GetInterval(Guid intervalGuid);
		Task<Guid> AddInterval(Interval interval);
		Task UpdateInterval(Guid intervalGuid, Interval interval);
		Task DeleteInterval(Guid intervalGuid);

		Task ClearIntervals();
	}

	class IntervalRepository : IIntervalRepository
	{
		private readonly Dictionary<Guid, Interval> _intervals = new();

		public Task<Guid> AddInterval(Interval interval)
		{
			Guid intervalGuid = Guid.NewGuid();
			interval.Guid = intervalGuid;
			_intervals.Add(intervalGuid, interval);

			return Task.FromResult(intervalGuid);
		}

		public Task<IEnumerable<Interval>> GetIntervals()
		{
			return Task.FromResult(_intervals.Values.AsEnumerable());
		}

		public async Task<IEnumerable<Interval>> GetIntervals(IEnumerable<ActivityCode> activityTypes, DateTime? from, DateTime? to)
		{
			IEnumerable<Interval> intervals = await GetIntervals();

			return intervals
				.BetweenDates(from, to, useOnlyDate: true)
				.OfActivityTypesOrAll(activityTypes);
		}

		public Task ClearIntervals()
		{
			_intervals.Clear();
			return Task.CompletedTask;
		}

		public Task<IEnumerable<Interval>> GetIntervals(ActivityCode activityCode, DateTime? from, DateTime? to)
		{
			return GetIntervals(new ActivityCode[] { activityCode }, from, to);
		}

		public Task<Interval> GetInterval(Guid intervalGuid)
		{
			return Task.FromResult(_intervals.GetValueOrDefault(intervalGuid));
		}

		public Task UpdateInterval(Guid intervalGuid, Interval interval)
		{
			if (!_intervals.ContainsKey(intervalGuid)) throw new KeyNotFoundException();

			_intervals[intervalGuid] = interval;

			return Task.CompletedTask;
		}

		public Task DeleteInterval(Guid intervalGuid)
		{
			if (!_intervals.ContainsKey(intervalGuid)) throw new KeyNotFoundException();
			_intervals.Remove(intervalGuid);
			return Task.CompletedTask;
		}
	}
}
