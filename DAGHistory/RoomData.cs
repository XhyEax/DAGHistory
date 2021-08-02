using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAGHistory
{
    public class Player
    {
        ulong steamID;
        string name;

        public Player(string name, ulong steamID)
        {
            this.name = name;
            this.steamID = steamID;
        }
        public string Name { get => name; set => name = value; }
        public ulong SteamID { get => steamID; set => steamID = value; }
    }

    public class RoomData
    {
        string roomCode;
        List<Player> playerList;

        public string RoomCode { get => roomCode; set => roomCode = value; }
        public List<Player> PlayerList { get => playerList; set => playerList = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
