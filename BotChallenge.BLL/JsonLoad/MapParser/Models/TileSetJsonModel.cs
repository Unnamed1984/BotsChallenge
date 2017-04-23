using Newtonsoft.Json;

namespace BotChallenge.BLL.JsonLoad.MapParser.Models
{
    public class TileSetJsonModel
    {
        [JsonProperty("columns")]
        public int Columns { get; set; }

        [JsonProperty("firstgid")]
        public int FirstGID { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("imagewidth")]
        public int ImageWidth { get; set; }

        [JsonProperty("imageheight")]
        public int ImageHeight { get; set; }

        [JsonProperty("margin")]
        public int Margin { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("spacing")]
        public int Spacing { get; set; }

        [JsonProperty("tilecount")]
        public int TileCount { get; set; }

        [JsonProperty("tilewidth")]
        public int TileWidth { get; set; }

        [JsonProperty("tileheight")]
        public int TileHeight { get; set; }
    }
}
