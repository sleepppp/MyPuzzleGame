using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core.UI;
using Core.Data;

namespace Core
{
    public class GameModeTutorial : GameMode
    {
        //============================================================================================
        //unity func~
        private void Awake()
        {
            Core.Util.Timer.StartTimer(3f, OnStartGame);
        }

        //============================================================================================
        //call back func~
        void OnStartGame()
        {
            PanelConsole panelConsole = GameObject.Find("PanelConsole").GetComponent<PanelConsole>();
            PanelBoard panelBoard = GameObject.Find("PanelBoard").GetComponent<PanelBoard>();

            StageData data = DataManager.instance.gameData.StageData.GetValue(1);
            int length = data.StartDailogueList.Length;
            for (int i = 0; i < length; ++i)
            {
                Core.Util.Timer.StartTimer(2 * i , i, (int index) => 
                {
                    DialogueData dialogueData = DataManager.instance.gameData.DialogueData.GetValue(data.StartDailogueList[index]);
                    panelConsole.PushText(dialogueData.Dialogue);
                });
            }

            Core.Util.Timer.StartTimer(2 * length, () =>{panelConsole.PushText("3");});
            Core.Util.Timer.StartTimer(2 * length + 1, () => { panelConsole.PushText("2"); });
            Core.Util.Timer.StartTimer(2 * length + 2, () => { panelConsole.PushText("1"); });
            Core.Util.Timer.StartTimer(2 * length + 3, () => 
            {
                Play();
                panelBoard.OnStartGame();
                panelConsole.ClearText();
            });
        }
    }
}
