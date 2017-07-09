using BotChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace BotChallenge.Util
{
    /// <summary>
    /// Class dedicated to mapping bot names.
    /// </summary>
    public class BotNameMapper
    {
        private string _dirPath;
        /// <summary>
        /// Constructs object
        /// </summary>
        public BotNameMapper(string dirPath)
        {
            _dirPath = dirPath;
        }
        /// <summary>
        /// Creates bots name map for code classes. It creates dictionary where key is bots field name 
        /// and value bots class name. The created dictionary is stored inside object.
        /// </summary>
        /// <param name="model"> RunBotsModel </param>
        /// <param name="playerName"> Player name </param>
        /// <returns> Mapped dictionary </returns>
        public Dictionary<string, string> CreateMappingForPlayer(RunBotsModel model, string playerName, string gameId)
        {
            Dictionary<string, string> botsNameMap = new Dictionary<string, string>();

            for (int botIndex = 0; botIndex < model.Code.Length; botIndex++)
            {
                CompilationBotsModel bot = model.Code[botIndex];
                string[] words = bot.Code.Split(' ');
                int classIndex = words.ToList().IndexOf("class");

                botsNameMap.Add(bot.Name, words[classIndex + 1]);
            }

            string filePath = getFilePath(gameId, playerName);
            if (File.Exists(filePath)) 
            {
                File.Create(filePath);
            }

            string content = JsonConvert.SerializeObject(botsNameMap);
            File.WriteAllText(filePath, content);

            return botsNameMap;
        }

        /// <summary>
        /// Gets mapping dictionary for concrete player.
        /// </summary>
        /// <param name="playerName"> Player name </param>
        /// <returns> Mapping dictionary </returns>
        public Dictionary<string, string> GetMappingForPlayer(string playerName, string gameId)
        {
            string filePath = getFilePath(gameId, playerName);
            string content = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
        }

        private string getFilePath(string gameId, string playerName)
        {
            return Path.Combine(_dirPath, gameId + "." + playerName + "." + "botMapping.json");
        }
    }
}
