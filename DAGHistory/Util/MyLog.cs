using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace DAGHistory
{
    public class MyLog
    {
        private static string logPath = ReturnLogPath() + Path.DirectorySeparatorChar + "DAGHistory.log";
        private static string lastRoomPath = ReturnLogPath() + Path.DirectorySeparatorChar + "LastRoom.json";

        private static string ReturnLogPath()
        {
            RuntimePlatform platform = Application.platform;
            switch (platform)
            {
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return "~/Library/Logs/Unity/";
                case RuntimePlatform.LinuxPlayer:
                case RuntimePlatform.LinuxEditor:
                    return Path.Combine("~/.config/unity3d", Application.companyName, "Draw_Guess");
                case RuntimePlatform.WindowsPlayer:
                    return Path.Combine(Environment.GetEnvironmentVariable("AppData"), "..", "LocalLow",
                        Application.companyName, "Draw_Guess");
                default:
                    return Path.GetTempPath();
            }
        }

        public static RoomData readLastRoomData()
        {
            try
            {
                string data = File.ReadAllText(lastRoomPath);
                return JsonConvert.DeserializeObject<RoomData>(data);
            }
            catch (Exception e)
            {
                LogError(e);
                return null;
            }
        }

        public static void LogHistory(object msg)
        {
            //写入最近房间记录
            File.WriteAllText(lastRoomPath, msg.ToString());
            //写入历史房间记录
            string[] logText = new string[] { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": \n" + msg };
            File.AppendAllLines(logPath, logText);
        }

        public static void LogObject(object msg)
        {
            string[] logText = new string[] { System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + msg };
            File.AppendAllLines(logPath, logText);
        }

        public static void Log(string message)
        {
            LogObject("XLog: " + message);
        }

        public static void LogError(Exception e)
        {
            LogObject("XLog: Exception: " + e.ToString() + " " + e.Message + " " + e.StackTrace);
        }

        public static void LogError(string tag, Exception e)
        {
            LogObject("XLog: " + tag + " Exception: " + e.ToString() + " " + e.Message + " " + e.StackTrace);
        }
    }
}
