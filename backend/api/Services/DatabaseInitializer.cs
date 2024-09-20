using MySqlConnector;

namespace api.Services;

public class DatabaseInitializer
{
	private readonly string _connectionString;

	public DatabaseInitializer(IConfiguration configuration)
	{
		_connectionString = configuration["ConnectionString"];
	}

	public void InitializeDatabase(string sqlFilePath)
	{
		var sqlScript = File.ReadAllText(sqlFilePath);

		using var connection = new MySqlConnection(_connectionString);
		connection.Open();

		using var command = new MySqlCommand(sqlScript, connection);
		command.ExecuteNonQuery();
	}
}
