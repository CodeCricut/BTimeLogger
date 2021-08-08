using BTimeLogger.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Domain.Services
{
	public interface IIntervalRepository : IRepository
	{
		Task<IEnumerable<Interval>> GetIntervals();
		Task<IEnumerable<Interval>> GetIntervals(IEnumerable<ActivityCode> activityCodes, DateTime? from, DateTime? to);
		Task<IEnumerable<Interval>> GetIntervals(ActivityCode activityCode, DateTime? from, DateTime? to);

		Task<Interval> GetInterval(Guid intervalGuid);
		Task<Guid> AddInterval(Interval interval);
		Task UpdateInterval(Guid intervalGuid, Interval interval);
		Task DeleteInterval(Guid intervalGuid);
	}

	class IntervalRepository : IIntervalRepository
	{
		private readonly Dictionary<Guid, Interval> _intervals = new();

		private readonly Dictionary<Guid, Interval> _unsavedIntervals = new();

		public Task<Guid> AddInterval(Interval interval)
		{
			Guid intervalGuid = Guid.NewGuid();
			interval.Guid = intervalGuid;
			_unsavedIntervals.Add(intervalGuid, interval);

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

		public Task Clear()
		{
			_unsavedIntervals.Clear();
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
			if (!_unsavedIntervals.ContainsKey(intervalGuid)) throw new KeyNotFoundException();

			_unsavedIntervals[intervalGuid] = interval;

			return Task.CompletedTask;
		}

		public Task DeleteInterval(Guid intervalGuid)
		{
			if (!_unsavedIntervals.ContainsKey(intervalGuid)) throw new KeyNotFoundException();
			_unsavedIntervals.Remove(intervalGuid);
			return Task.CompletedTask;
		}

		public Task SaveChanges()
		{
			return Task.Factory.StartNew(() =>
			{

				_intervals.Clear();
				foreach (var unsavedInterval in _unsavedIntervals)
				{
					_intervals.Add(unsavedInterval.Key, unsavedInterval.Value);
				}
			});
		}

		public Task RemoveChanges()
		{
			return Task.Factory.StartNew(() =>
			{
				_unsavedIntervals.Clear();
				foreach (KeyValuePair<Guid, Interval> savedInterval in _intervals)
				{
					_unsavedIntervals.Add(savedInterval.Key, savedInterval.Value);
				}
			});
		}
	}
}
