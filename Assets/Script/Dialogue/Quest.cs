using System.Collections.Generic;
/*using RPGM.Core;
using RPGM.UI;*/
using UnityEngine;

namespace RPGM.Gameplay
{
    /// <summary>
    /// This class implements quests.
    /// </summary>
    public class Quest : MonoBehaviour
    {
        public string title;
        public string desc;
        public ConversationScript questInProgressConversation, questCompletedConversation;

        List<GameObject> cleanup = new List<GameObject>();

        public bool isStarted = false;
        public bool isFinished = false;


        void Awake()
        {

        }

        public void OnStartQuest()
        {
            isFinished = false;

        }

        public bool IsQuestComplete()
        {
            return true;
        }



        public void OnFinishQuest()
        {
            isFinished = true;
        }

    }
}