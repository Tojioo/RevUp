using LinqToDB.Mapping;
using RevUp.Models;
using System.Text.Json;

namespace RevUp.Data;

/// <summary>Maps the domain models to tables, so the models themselves stay free of persistence attributes.</summary>
public static class RevUpMappingSchema
{
	public static MappingSchema Create()
	{
		var schema = new MappingSchema();
		new FluentMappingBuilder(schema)
			.Entity<Meet>()
			.HasTableName("Meets")
			.Property(meet => meet.Id)
			.IsPrimaryKey()
			.Property(meet => meet.AttendeeNames)
			.HasConversion(
				names => JsonSerializer.Serialize(names, RevUpJsonContext.Default.ListString),
				json => JsonSerializer.Deserialize(json, RevUpJsonContext.Default.ListString) ?? new List<string>())
			.Property(meet => meet.AttendeeCount)
			.IsNotColumn()
			.Property(meet => meet.IsFull)
			.IsNotColumn()
			.Entity<JoinRequest>()
			.HasTableName("JoinRequests")
			.Property(request => request.Id)
			.IsPrimaryKey()
			.Build();
		return schema;
	}
}