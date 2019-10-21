namespace News.Abstractions.Entities
{
	/// <summary>
	/// Represents an entity of a news portal.
	/// </summary>
	/// <typeparam name="TID">The type of the identifier of the entity.</typeparam>
	/// <typeparam name="TModel">The type of the model of the entity.</typeparam>
	public interface IEntity<out TID, out TModel>
	{
		/// <summary>
		/// Gets the identifier of the <see cref="IEntity{TID, TData}"/>.
		/// </summary>
		TID Id { get; }
		/// <summary>
		/// Gets the model of the <see cref="IEntity{TID, TData}"/>.
		/// </summary>
		TModel Model { get; }
	}
}