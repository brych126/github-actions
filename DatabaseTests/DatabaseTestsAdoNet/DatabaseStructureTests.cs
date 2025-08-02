using Microsoft.Data.SqlClient;

namespace DatabaseTestsAdoNet;

[TestFixture]
public class DatabaseStructureTests
{
    private const string TestDbName = "TestDb";

    private static readonly string MasterConnectionString = new SqlConnectionStringBuilder
    {
        DataSource = "localhost",
        InitialCatalog = "master",
        UserID = "sa",
        Password = "Password1",
        TrustServerCertificate = true
    }.ConnectionString;

    private static readonly string TestDbConnectionString = new SqlConnectionStringBuilder
    {
        DataSource = "localhost",
        InitialCatalog = TestDbName,
        UserID = "sa",
        Password = "Password1",
        TrustServerCertificate = true
    }.ConnectionString;

    [Test]
    public void DatabaseExists()
    {
        using var masterConn = new SqlConnection(MasterConnectionString);
        masterConn.Open();
        using var cmd = new SqlCommand(
            "SELECT COUNT(*) FROM sys.databases WHERE name = @dbName", masterConn);
        cmd.Parameters.AddWithValue("@dbName", TestDbName);
        int dbCount = (int)cmd.ExecuteScalar();
        Assert.That(dbCount, Is.EqualTo(1), $"Database '{TestDbName}' does not exist.");
    }

    [Test]
    public void TableExists()
    {
        using var testDbConn = new SqlConnection(TestDbConnectionString);
        testDbConn.Open();
        using var cmd = new SqlCommand(
            "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TestTable'", testDbConn);
        int tableCount = (int)cmd.ExecuteScalar();
        Assert.That(tableCount, Is.EqualTo(1), $"Table 'TestTable' does not exist.");
    }

    [Test]
    public void TableHasData()
    {
        using var testDbConn = new SqlConnection(TestDbConnectionString);
        testDbConn.Open();
        using var cmd = new SqlCommand("SELECT COUNT(*) FROM TestTable", testDbConn);
        int rowCount = (int)cmd.ExecuteScalar();
        Assert.That(rowCount, Is.GreaterThanOrEqualTo(1), "TestTable contains data.");
    }
}