using System.Threading.Tasks;

namespace BTimeLogger.Domain.Services
{
	public interface IRepository
	{
		/// <summary>
		/// Remove all entities from the repository.
		/// </summary>
		Task Clear();

		/// <summary>
		/// Save all working changes made to the repository.
		/// </summary>
		Task SaveChanges();

		/// <summary>
		/// Remove all working changes made to the repository.
		/// </summary>
		/// <returns></returns>
		Task RemoveChanges();
	}
}
