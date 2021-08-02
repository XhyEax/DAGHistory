using DAGHistory.ProxyClass;
using System.Collections.Generic;
using System.Text;

namespace DAGHistory
{
    public class Loader
    {
        static UnityEngine.GameObject gameObject;
        static ToolBar toolbar;
        static bool isLoad = false;


        public static void Load()
        {
            isLoad = true;
            LobbyChatProxy.Install();
            LobbyPlayerInfoProxy.Install();
            // 增加GameObject
            gameObject = new UnityEngine.GameObject();
            toolbar = gameObject.AddComponent<ToolBar>();
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }

        public static void UnLoad()
        {
            if (isLoad)
            {
                isLoad = false;
                LobbyChatProxy.Install();
                LobbyPlayerInfoProxy.Uninstall();
                toolbar.Destory();
                UnityEngine.Object.Destroy(gameObject);
            }
        }
    }
}
