using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.Stream
{
    public class RootObject
    {
        public Player[] Player { get; set; }
    }

    public class Player :IComparable<Player>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("second_name")]
        public string SecondName { get; set; }

        [JsonProperty("points_per_game")]
        public double PointsPerGame { get; set; }

        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        public int CompareTo(Player other)
        {
            return (PointsPerGame.CompareTo(other.PointsPerGame) * -1);
        }
    }
}
