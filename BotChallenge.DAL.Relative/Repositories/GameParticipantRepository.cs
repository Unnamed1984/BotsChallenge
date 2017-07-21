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
    /// Class responsible for dealing with 'GameParticipant' table in database.
    /// </summary>
    class GameParticipantRepository : IRepository<GameParticipant>
    {
        private string _connectionString;

        /// <summary>
        /// Constructs object and retrieves connection string from config.
        /// </summary>
        public GameParticipantRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RelationalDB"].ConnectionString;
        }

        /// <summary>
        /// Parses SQLDataReader and returns enumerable of 'GameParticipant's.
        /// </summary>
        /// <param name="reader"> SQLDataReader - parse source. </param>
        /// <returns> Particiapant enumerable. </returns>
        private IEnumerable<GameParticipant> readDataFromDataReader(SqlDataReader reader)
        {
            List<GameParticipant> data = new List<GameParticipant>();

            while (reader.Read())
            {
                data.Add(new GameParticipant()
                {
                    UserId = (string)reader["UserId"],
                    GameParticipantId = (string)reader["GameParticipantId"],
                    GameId = (string)reader["GameId"],
                    UserCode = (string)reader["UserCode"],
                    IsWinner = (bool?)reader["IsWinner"]
                });
            }

            return data;
        }

        /// <summary>
        /// Creates database record for 'GameParticipant' object.
        /// </summary>
        /// <param name="entity"> Participant entity. </param>
        /// <returns> Created entity id. </returns>
        public string Add(GameParticipant entity)
        {
            string gameParticipantId = Guid.NewGuid().ToString();
            entity.GameParticipantId = gameParticipantId;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.GAME_PARTICIPANT_INSERT);

                    command.Parameters.Add(new SqlParameter("@UserId", entity.UserId));
                    command.Parameters.Add(new SqlParameter("@GameParticipantId", entity.GameParticipantId));
                    command.Parameters.Add(new SqlParameter("@GameId", entity.GameId));
                    command.Parameters.Add(new SqlParameter("@UserCode", entity.UserCode));
                    command.Parameters.Add(new SqlParameter("@IsWinner", entity.IsWinner));

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

            return gameParticipantId; 
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
                    SqlCommand command = new SqlCommand(SQLExpression.GAME_PARTICIPANT_DELETE);

                    command.Parameters.Add(new SqlParameter("@GameParticipantId", id));

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
        /// Returns 'GameParticipant' entity by specified id.
        /// </summary>
        /// <param name="id"> Unique identifier. </param>
        /// <returns> Retrieved entity. </returns>
        public GameParticipant Get(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.GAME_PARTICIPANT_GET_BY_ID);

                    command.Parameters.Add(new SqlParameter("@GameParticipantId", id));

                    SqlDataReader reader = command.ExecuteReader();

                    IEnumerable<GameParticipant> data = readDataFromDataReader(reader);

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
        /// Returns all 'GameParticipant' entities from database table
        /// </summary>
        /// <returns> Participant enumerable. </returns>
        public IEnumerable<GameParticipant> GetAll()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.GAME_PARTICIPANT_GET_ALL);

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
        public void Update(GameParticipant entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(SQLExpression.USER_UPDATE);

                    command.Parameters.Add(new SqlParameter("@UserId", entity.UserId));
                    command.Parameters.Add(new SqlParameter("@GameParticipantId", entity.GameParticipantId));
                    command.Parameters.Add(new SqlParameter("@GameId", entity.GameId));
                    command.Parameters.Add(new SqlParameter("@UserCode", entity.UserCode));
                    command.Parameters.Add(new SqlParameter("@IsWinner", entity.IsWinner));

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
