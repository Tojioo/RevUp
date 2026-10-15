using LinqToDB;
using LinqToDB.Data;
using RevUp.Models;

namespace RevUp.Data;

/// <summary>The app's database, one table property per entity.</summary>
public class RevUpDataConnection(DataOptions options) : DataConnection(options)
{
	public ITable<Meet> Meets => this.GetTable<Meet>();

	public ITable<JoinRequest> JoinRequests => this.GetTable<JoinRequest>();
}