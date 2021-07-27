using System;
using System.Collections.Generic;

namespace BTimeLogger.Csv
{
	public static class DictionaryExtensions
	{
		public static TValue GetOrCreateValue<TKey, TValue>(
			this IDictionary<TKey, TValue> dictionary,
			TKey key,
			TValue value)
		{
			return dictionary.GetOrCreateValue(key, () => value);
		}

		public static TValue GetOrCreateValue<TKey, TValue>(
			this IDictionary<TKey, TValue> dictionary,
			TKey key,
			Func<TValue> valueProvider)
		{
			TValue ret;
			if (!dictionary.TryGetValue(key, out ret))
			{
				ret = valueProvider();
				dictionary[key] = ret;
			}
			return ret;
		}
	}
}
