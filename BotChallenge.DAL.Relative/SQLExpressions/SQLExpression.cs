namespace BotChallenge.DAL.Relative.SQLExpressions
{
    static class SQLExpression
    {
        #region User

        public static string USER_GET_ALL { get; } = "SELECT * FROM User;";
        public static string USER_GET_BY_ID { get; } = "SELECT * FROM User WHERE UserId = @UserId;";
        public static string USER_INSERT { get; } = "INSERT INTO User (UserId, Login, Password, Email, AccessToken) VALUES ( @UserId, @Login, @Password, @Email, @AccessToken );";
        public static string USER_UPDATE { get; } = "UPDATE User SET Login=@Login, Password=@Password, Email=@Email, AccessToken=@AccessToken WHERE UserId=@UserId;";
        public static string USER_DELETE { get; } = "DELETE FROM User WHERE UserId=@UserId;";

        #endregion

        #region Game Participants 

        public static string GAME_PARTICIPANT_GET_ALL { get; } = "SELECT * FROM GameParticipant;";
        public static string GAME_PARTICIPANT_GET_BY_ID { get; } = "SELECT * FROM GameParticipant WHERE GameParticipantId = @GameParticipantId;";
        public static string GAME_PARTICIPANT_INSERT { get; } = "INSERT INTO GameParticipant (GameParticipantId, GameId, UserId, UserCode, IsWinner) VALUES ( @GameParticipantId, @GameId, @UserId, @UserCode, @IsWinner );";
        public static string GAME_PARTICIPANT_UPDATE { get; } = "UPDATE GameParticipant SET GameId=@GameId, UserId=@UserId, UserCode=@UserCode, IsWinner=@IsWinner WHERE GameParticipantId=@GameParticipantId;";
        public static string GAME_PARTICIPANT_DELETE { get; } = "DELETE FROM GameParticipant WHERE GameParticipantId=@GameParticipantId;";

        #endregion

        #region Game

        public static string GAME_GET_ALL { get; } = "SELECT * FROM Game;";
        public static string GAME_GET_BY_ID { get; } = "SELECT * FROM Game WHERE GameId = @GameId;";
        public static string GAME_INSERT { get; } = "INSERT INTO Game (GameId, MapId, MapName, BotCount, Name) VALUES ( @GameId, @MapId, @MapName, @BotCount, @Name );";
        public static string GAME_UPDATE { get; } = "UPDATE Game SET MapId=@MapId, MapName=@MapName, BotNumber=@BotNumber, Name=@Name WHERE GameId=@GameId;";
        public static string GAME_DELETE { get; } = "DELETE FROM Game WHERE GameId=@GameId;";

        #endregion
    }
}
