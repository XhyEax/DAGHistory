using DAGHistory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAGHistory
{
    class ToolBar : MonoBehaviour
    {
        public bool isShow = true;
        public string msg;
        float lastMsgShow;

        GUIStyle msgStyle;
        public void Start()
        {
            msgStyle = new GUIStyle();
            msgStyle.fontSize = 24;
            msgStyle.normal.textColor = Color.gray;
        }

        private void Update()
        {
            //按后引号键（esc下方）隐藏
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                isShow = !isShow;
            }
        }

        public void OnGUI()
        {

            if (!isShow)
                return;

            GUILayout.BeginArea(new Rect(10, 10, 200, 60));
            GUILayout.BeginHorizontal();//横向布局

            if (GUILayout.Button("保存"))
            {
                if (RoomUtil.saveRoomData())
                {
                    UpdateLabelMessage("已保存当前房间数据");
                }
                else
                {
                    UpdateLabelMessage("请先加入一个房间");
                }

            }

            if (GUILayout.Button("复制"))
            {
                string code = RoomUtil.getLastRoomCode();
                if (code != null)
                {
                    GUIUtility.systemCopyBuffer = code;
                    UpdateLabelMessage("代码已复制到剪切板: " + code);
                }
                else
                {
                    UpdateLabelMessage("历史房间代码为空，请先保存");
                }
            }

            if (GUILayout.Button("加入"))
            {
                string code = RoomUtil.getLastRoomCode();
                if (code != null)
                {
                    RoomUtil.joinRoom(code);
                    //仅适用于掉线重连，建议手动复制代码
                    UpdateLabelMessage("正在加入，请耐心等待...");
                }
                else
                {
                    UpdateLabelMessage("历史房间代码为空，请先保存");
                }
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();

            if (Time.realtimeSinceStartup - lastMsgShow < 1.5)
                GUI.Label(new Rect(10, 60, 300, 30), msg, msgStyle);

        }
        public void UpdateLabelMessage(string msgstr)
        {
            msg = msgstr;
            lastMsgShow = Time.realtimeSinceStartup;
        }

        public void Destory()
        {
            isShow = false;
        }

    }
}
