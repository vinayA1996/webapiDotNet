using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace MahaNoticePortalAPI.Business
{
    public class StudentBusiness
    {
        private static IConfiguration configuration;

        public static void Initialize(IConfiguration config)
        {
            configuration = config;
        }

        private static string ConnectionString
        {
            get { return configuration.GetConnectionString("DefaultConnection"); }
        }


        public static async Task<DataTable> SaveStudent(Student student)
        {
            SqlConnection connection = null;
            DataTable dt = new DataTable();
            try
            {
                connection = new SqlConnection(ConnectionString);
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                command.CommandText = "[dbo].[S_Entry]";
                command.Parameters.AddWithValue("@Name",student.name);
                command.Parameters.AddWithValue("@Age", student.age);
                command.Parameters.AddWithValue("@Id", student.id);
                connection.Open();
                SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);
                var sb = new StringBuilder();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public static async Task<DataTable> GetStudent()
        {
            SqlConnection connection = null;
            DataTable dt = new DataTable();
            try
            {
                connection = new SqlConnection(ConnectionString);
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                command.CommandText = "[dbo].[GetStudents]";
                connection.Open();
                SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);
                var sb = new StringBuilder();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    

    }
}
