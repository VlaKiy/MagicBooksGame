using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeQuest : MonoBehaviour
{
    [TextArea(1, 20)] public string text;
    public float timer;

    void Start()
    {
        var lq = gameObject.GetComponentInChildren<LoadQuestToQuestMenu>();
        var startHelper = gameObject.GetComponent<StartHelper>();
        var talkEditor = gameObject.GetComponentInChildren<TalkEditor>();
        var spellsInventory = GameObject.Find("SpellsInventory");
        var joystick = GameObject.Find("Fixed Joystick");
        var questMenuButton = GameObject.Find("QuestMenuButton");

        lq.LoadQuestFromAuthor();
        startHelper.text = text;
        startHelper.timer = timer;
        startHelper.goHelper();

        if (!Camera.main.GetComponent<CheatInput>().isSkipTalks)
        {
            spellsInventory.SetActive(false);
            joystick.SetActive(false);
            questMenuButton.SetActive(false);

            talkEditor.StartTalk();
        }
    }
}
