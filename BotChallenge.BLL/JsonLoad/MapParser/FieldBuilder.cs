using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotChallenge.BLL.JsonLoad.MapParser.Models;
using Newtonsoft.Json;
using System.IO;
using BotChallenge.Runner.CodeRunners.Models;

namespace BotChallenge.BLL.JsonLoad.MapParser
{
    public class FieldBuilder
    {
        private FieldTileSetModel field;

        private readonly string obstaclesConst = "Obstacles";

        public FieldBuilder(string path)
        {
            field = JsonConvert.DeserializeObject<FieldTileSetModel>(File.ReadAllText(path));
        }

        public Field GetFieldForRunner()
        {
            Field field = new Field();

            field.Width = this.field.Width;
            field.Height = this.field.Height;
            field.Points = new Point[field.Height][];

            LayerTileSetModel obstacleLayer = this.field.Layers.First(l => l.Name == obstaclesConst);

            int dataIndex = 0;
            for (int i = 0; i < field.Height; i++)
            {
                field.Points[i] = new Point[field.Width];

                for (int j = 0; j < field.Width; j++)
                {
                    field.Points[i][j] = getPointByObstacleType(obstacleLayer.Data[dataIndex++]);
                }
            }

            return field;
        }

        public Field PlaceBots(Field f, IEnumerable<BotChallenge.BLL.Models.Bot> bots, int playerNum)
        {
            foreach (BotChallenge.BLL.Models.Bot b in bots)
            {
                f.Points[b.Y][b.X] = (Point)playerNum;
            }

            return f;
        }

        private Point getPointByObstacleType(int obstacleType)
        {
            if (obstacleType > 0)
            {
                return Point.Obstacle;
            }
            else
            {
                return Point.Empty;
            }
        } 
    }
}
