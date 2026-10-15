namespace RevUp.Models;

/// <summary>Base for everything stored in the database.</summary>
public abstract class Entity
{
	public Guid Id { get; init; } = Guid.NewGuid();
}