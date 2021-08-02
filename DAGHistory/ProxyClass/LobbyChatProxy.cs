using System;
using System.Collections.Generic;
using System.Text;

namespace DAGHistory.ProxyClass
{
    class LobbyChatProxy
    {
        static Type tTarget, tProxy;
        static MethodHook methodHook;

        public static void Install()
        {
            tTarget = typeof(LobbyChat);
            tProxy = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            methodHook = MyHook.Install(tTarget, tProxy, "Start");
            //MyLog.Log(tTarget.Name + " Hook finished.");
        }

        public static void Uninstall()
        {
            if (methodHook != null)
                methodHook.Uninstall();
        }

        public static void StartReplace()
        {
            // 调用原函数
            StartProxy();
            // 输出信息到聊天框
            LobbyChat.Instance.ReceiveChat("<color=#778899>[DAGHistory]: Loaded</color>");
        }

        public static void StartProxy()
        {
            MyLog.Log("StartProxy");
        }

    }
}
