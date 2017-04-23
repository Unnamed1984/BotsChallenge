using Newtonsoft.Json;

namespace BotChallenge.BLL.JsonLoad.MapParser.Models
{
    public class LayerTileSetModel
    {
        [JsonProperty("data")]
        public int[] Data { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("opacity")]
        public int Opacity { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("visible")]
        public bool Visible { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
    }
}
