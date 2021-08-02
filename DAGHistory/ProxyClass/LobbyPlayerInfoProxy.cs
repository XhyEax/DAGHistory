using DAGHistory.Util;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DAGHistory.ProxyClass
{
    class LobbyPlayerInfoProxy
    {
        static Type tTarget, tProxy;
        static MethodHook methodHook;

        public static void Install()
        {
            tTarget = typeof(LobbyPlayerInfo);
            tProxy = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            methodHook = MyHook.Install(tTarget, tProxy, "UserCode_RpcGetCountdown");
            //MyLog.Log(tTarget.Name + " Hook finished.");
        }

        public static void Uninstall()
        {
            if (methodHook != null)
                methodHook.Uninstall();
        }


        static float lastCallTime = 0;

        public static void UserCode_RpcGetCountdownReplace(byte s)
        {
            // 调用原函数
            UserCode_RpcGetCountdownProxy(s);
            // 如果距离上一次调用该函数超过10秒，获取房间数据并保存到文件
            if (Time.realtimeSinceStartup - lastCallTime > 10)
            {
                RoomUtil.saveRoomData();
                LobbyChat.Instance.ReceiveChat("<color=#778899>[DAGHistory]: Saved</color>");
                lastCallTime = Time.realtimeSinceStartup;
            }
        }

        public static void UserCode_RpcGetCountdownProxy(byte s)
        {
            MyLog.Log("UserCode_RpcGetCountdownProxy");
        }

    }
}
