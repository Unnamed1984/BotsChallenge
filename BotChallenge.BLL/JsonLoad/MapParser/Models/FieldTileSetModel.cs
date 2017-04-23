using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BotChallenge.BLL.JsonLoad.MapParser.Models
{
    public class FieldTileSetModel
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("nextobjectid")]
        public int NextObjectId { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("renderorder")]
        public string RenderOrder { get; set; }

        [JsonProperty("tileheight")]
        public int TileHeight { get; set; }

        [JsonProperty("tilewidth")]
        public int TileWidth { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("layers")]
        public IEnumerable<LayerTileSetModel> Layers { get; set; }

        [JsonProperty("tilesets")]
        public IEnumerable<TileSetJsonModel> TileSets { get; set; }

    }
}
