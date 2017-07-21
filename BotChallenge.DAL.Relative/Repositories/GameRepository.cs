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
    /// Class responsible for dealing with 'Game' table in database.
    /// </summary>
    class GameRepository : IRepository<Game>
    {
        private string _connectionString;

        /// <summary>
        /// Constructs object and retrieves connection string from config.
        /// </summary>
        public GameRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RelationalDB"].ConnectionString;
        }

        /// <summary>
        /// Parses SQLDataReader and returns enumerable of 'Game's.
        /// </summary>
        /// <param name="reader"> SQLDataReader - parse source. </param>
        /// <returns> Game enumerable. </returns>
        private IEnumerable<Game> readDataFromDataReader(SqlDataReader reader)
        {
            List<Game> data = new List<Game>();

            while (reader.Read())
            {
                data.Add(new Game()
                {
                    GameId = (string)reader["GameId"],
                    MapId = (string)reader["MapId"],
                    MapName = (string)reader["MapName"],
                    BotNumber = (int)reader["BotNumber"],
                    Name = (string)reader["Name"]
                });
            }

            return data;
        }

        /// <summary>
        /// Creates database record for 'Game' object.
        /// </summary>
        /// <param name="entity"> Game entity. </param>
        /// <returns> Created entity id. </returns>
        public string Add(Game entity)
        {
            string gameId = Guid.NewGuid().ToString();
            entity.GameId = gameId;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.GAME_INSERT);

                    command.Parameters.Add(new SqlParameter("@BotNumber", entity.BotNumber));
                    command.Parameters.Add(new SqlParameter("@MapId", entity.MapId));
                    command.Parameters.Add(new SqlParameter("@GameId", entity.GameId));
                    command.Parameters.Add(new SqlParameter("@MapName", entity.MapName));
                    command.Parameters.Add(new SqlParameter("@Name", entity.Name));

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new DatabaseException("No rows were affected by command.");
                    }
                }
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Error inserting entity. See inner exception for details.", e);
            }

            return gameId; 
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
                    SqlCommand command = new SqlCommand(SQLExpression.GAME_DELETE);

                    command.Parameters.Add(new SqlParameter("@GameId", id));

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new DatabaseException("No rows were affected by command.");
                    }
                }
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Error deleting entity. See inner exception for details.", e);
            }
        }

        /// <summary>
        /// Returns 'Game' entity by specified id.
        /// </summary>
        /// <param name="id"> Unique identifier. </param>
        /// <returns> Retrieved entity. </returns>
        public Game Get(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.GAME_GET_BY_ID);

                    command.Parameters.Add(new SqlParameter("@GameId", id));

                    SqlDataReader reader = command.ExecuteReader();

                    IEnumerable<Game> data = readDataFromDataReader(reader);

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
                throw new DatabaseException("Error retrieving entity. See inner exception for details.", e);
            }
        }

        /// <summary>
        /// Returns all 'Game' entities from database table
        /// </summary>
        /// <returns> Participant enumerable. </returns>
        public IEnumerable<Game> GetAll()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.GAME_GET_ALL);

                    SqlDataReader reader = command.ExecuteReader();

                    return readDataFromDataReader(reader);
                }
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Error retrieving entities. See inner exception for details.", e);
            }
        }

        /// <summary>
        /// Updates database entity with current values.
        /// </summary>
        /// <param name="entity"> Current object state. </param>
        public void Update(Game entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.GAME_UPDATE);

                    command.Parameters.Add(new SqlParameter("@BotNumber", entity.BotNumber));
                    command.Parameters.Add(new SqlParameter("@MapId", entity.MapId));
                    command.Parameters.Add(new SqlParameter("@GameId", entity.GameId));
                    command.Parameters.Add(new SqlParameter("@MapName", entity.MapName));
                    command.Parameters.Add(new SqlParameter("@Name", entity.Name));

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new DatabaseException("No rows were affected by command.");
                    }
                }
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Error updating entity. See inner exception for details.", e);
            }
        }
    }
}
