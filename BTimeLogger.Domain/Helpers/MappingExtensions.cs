using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTimeLogger.Domain.Helpers;

public static class MappingExtensions
{
	/// <summary>
	/// Paginate a queryable source.
	/// </summary>
	/// <typeparam name="TDestination"></typeparam>
	/// <param name="queryable"></param>
	/// <param name="pagingParams"></param>
	/// <returns></returns>
	public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, PagingParams pagingParams)
		=> PaginatedList<TDestination>.CreateAsync(queryable, pagingParams.PageNumber, pagingParams.PageSize);

	/// <summary>
	/// Map each item in the <paramref name="sourceList"/> of type <typeparamref name="TSource"/> to a new item of type <typeparamref name="TDestination"/>,
	/// otherwise maintaining other properties of the list.
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TDestination"></typeparam>
	/// <param name="sourceList"></param>
	/// <param name="mapper"></param>
	/// <returns></returns>
	public static PaginatedList<TDestination> ToMappedPagedList<TSource, TDestination>(this PaginatedList<TSource> sourceList, Func<TSource, TDestination> mappingCallback)
	{
		var destinationList = new List<TDestination>();
		foreach (var item in sourceList.Items)
		{
			TDestination dest = mappingCallback(item);
			destinationList.Add(dest);
		}

		return new PaginatedList<TDestination>(destinationList, sourceList.TotalCount, sourceList.PageIndex, sourceList.PageSize);
	}
}
