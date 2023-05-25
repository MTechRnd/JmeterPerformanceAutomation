using CsvHelper;
using FastMember;
using JmeterCLIDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Globalization;
namespace JmeterCLIDemo.Migrations
{
    /// <inheritdoc />
    public partial class seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string path = "./files/data.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CityDTO>();
            IConfiguration config = new ConfigurationBuilder()
               .AddUserSecrets<Program>()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
            using var connection = new SqlConnection(config["DBConfiguration:LocalConnectionString"]);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction);
            bulkCopy.DestinationTableName = "GujaratDistricts";
            bulkCopy.ColumnMappings.Add("STCode", "STCode");
            bulkCopy.ColumnMappings.Add("StateName", "StateName");
            bulkCopy.ColumnMappings.Add("DTCode", "DTCode");
            bulkCopy.ColumnMappings.Add("DistrictName", "DistrictName");
            bulkCopy.ColumnMappings.Add("SDTCode", "SDTCode");
            bulkCopy.ColumnMappings.Add("SubDistrictName", "SubDistrictName");
            bulkCopy.ColumnMappings.Add("TownCode", "TownCode");
            bulkCopy.ColumnMappings.Add("AreaName", "AreaName");
            using var recordReader = ObjectReader.Create(records, "STCode", "StateName", "DTCode", "DistrictName", "SDTCode", "SubDistrictName", "TownCode", "AreaName");
            bulkCopy.WriteToServer(recordReader);
            transaction.Commit();
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}