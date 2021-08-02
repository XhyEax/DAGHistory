using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAGHistory.Util
{
    class RoomUtil
    {

        // 获取房间数据
        public static RoomData getRoomData()
        {
            RoomData roomData = new RoomData();
            if (LobbyPlayerList.Instance != null && LobbyPlayerList.Instance.Players != null
                && LobbyPlayerList.Instance.Players.Count != 0)
            {
                List<LobbyPlayerInfo> players = LobbyPlayerList.Instance.Players;
                roomData.PlayerList = new List<Player>();
                for (int i = 0; i < players.Count; i++)
                {
                    LobbyPlayerInfo lobbyPlayerInfo = players[i];
                    string name = lobbyPlayerInfo.Name;
                    ulong steamID = lobbyPlayerInfo.SteamID.m_SteamID;
                    if (i == 0)
                    {
                        roomData.RoomCode = CodeEncoder.codeEncode(steamID);
                    }
                    roomData.PlayerList.Add(new Player(name, steamID));
                }
            }
            return roomData;
        }

        //保存房间数据
        public static bool saveRoomData()
        {
            RoomData roomData = getRoomData();
            if (roomData.RoomCode == null)
                return false;
            MyLog.LogHistory(roomData);
            return true;
        }

        public static string getLastRoomCode()
        {
            RoomData roomData = MyLog.readLastRoomData();
            if (roomData != null)
            {
                return roomData.RoomCode;
            }
            return null;
        }

        public static void joinRoom(string code)
        {
            if (code != null)
            {
                LobbyManager.s_Singleton.Join(CodeEncoder.codeDecode(code).ToString());
            }
        }
    }
}
