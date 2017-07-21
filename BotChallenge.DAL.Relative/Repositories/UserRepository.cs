using System;
using System.Collections.Generic;
using BotChallenge.DAL.Shared;
using BotChallenge.DAL.Shared.Models;
using System.Data.SqlClient;
using System.Configuration;
using BotChallenge.DAL.Relative.SQLExpressions;
using BotChallenge.DAL.Shared.Exceptions;
using System.Linq;

namespace BotChallenge.DAL.Relative.Repositories
{
    /// <summary>
    /// Class responsible for dealing with user table in database.
    /// </summary>
    class UserRepository : IRepository<User>
    {
        private string _connectionString;

        /// <summary>
        /// Constructs object and retrieves connection string from config.
        /// </summary>
        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RelationalDB"].ConnectionString;
        }

        /// <summary>
        /// Parses SQLDataReader and returns enumerable of users.
        /// </summary>
        /// <param name="reader"> SQLDataReader - parse source. </param>
        /// <returns> User enumerable. </returns>
        private IEnumerable<User> readDataFromDataReader(SqlDataReader reader)
        {
            List<User> data = new List<User>();

            while (reader.Read())
            {
                data.Add(new User()
                {
                    UserId = (string)reader["UserId"],
                    Login = (string)reader["Login"],
                    Password = (string)reader["Password"],
                    Email = (string)reader["Email"],
                    AccessToken = (string)reader["AccessToken"]
                });
            }

            return data;
        }

        /// <summary>
        /// Creates database record for 'user' object.
        /// </summary>
        /// <param name="entity"> User entity. </param>
        /// <returns> Created user id. </returns>
        public string Add(User entity)
        {
            string userId = Guid.NewGuid().ToString();
            entity.UserId = userId;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.USER_INSERT);

                    command.Parameters.Add(new SqlParameter("@UserId", entity.UserId));
                    command.Parameters.Add(new SqlParameter("@Login", entity.Login));
                    command.Parameters.Add(new SqlParameter("@Password", entity.Password));
                    command.Parameters.Add(new SqlParameter("@Email", entity.Email));
                    command.Parameters.Add(new SqlParameter("@AccessToken", entity.AccessToken));

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new DatabaseException("No rows were affected by command.");
                    }
                }
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Error inserting user. See inner exception for details.", e);
            }

            return userId; 
        }

        /// <summary>
        /// Deletes database record by specified id.
        /// </summary>
        /// <param name="id"> Unique identifier of deleting object. </param>
        public void Delete(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.USER_DELETE);

                    command.Parameters.Add(new SqlParameter("@UserId", id));

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new DatabaseException("No rows were affected by command.");
                    }
                }
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Error deleting user. See inner exception for details.", e);
            }
        }

        /// <summary>
        /// Returns user by specified id.
        /// </summary>
        /// <param name="id"> Unique identifier. </param>
        /// <returns> Retrieved user. </returns>
        public User Get(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.USER_INSERT);

                    command.Parameters.Add(new SqlParameter("@UserId", id));

                    SqlDataReader reader = command.ExecuteReader();

                    IEnumerable<User> data = readDataFromDataReader(reader);

                    if (data.Count() == 1)
                    {
                        return data.First();
                    }
                    else
                    {
                        throw new DatabaseException("No one entity with specified id was found");
                    }
                }
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Error retrieving user. See inner exception for details.", e);
            }
        }

        /// <summary>
        /// Returns all user from database table
        /// </summary>
        /// <returns> User enumerable. </returns>
        public IEnumerable<User> GetAll()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.USER_INSERT);

                    SqlDataReader reader = command.ExecuteReader();

                    return readDataFromDataReader(reader);
                }
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Error retrieving users. See inner exception for details.", e);
            }
        }

        /// <summary>
        /// Updates database entity with current values.
        /// </summary>
        /// <param name="entity"> Current user state. </param>
        public void Update(User entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.USER_UPDATE);

                    command.Parameters.Add(new SqlParameter("@UserId", entity.UserId));
                    command.Parameters.Add(new SqlParameter("@Login", entity.Login));
                    command.Parameters.Add(new SqlParameter("@Password", entity.Password));
                    command.Parameters.Add(new SqlParameter("@Email", entity.Email));
                    command.Parameters.Add(new SqlParameter("@AccessToken", entity.AccessToken));

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new DatabaseException("No rows were affected by command.");
                    }
                }
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Error updating user. See inner exception for details.", e);
            }
        }
    }
}
