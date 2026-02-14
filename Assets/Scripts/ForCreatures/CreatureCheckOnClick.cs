using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureCheckOnClick : MonoBehaviour
{
    private bool isHaveQuest;

    public GameObject questCont;
    public GameObject quest;
    public GameObject acceptQuestBtn;

    private void OnMouseDown()
    {
        var qa = gameObject.GetComponent<CreatureHaveQuestAssignment>();
        isHaveQuest = qa.isHaveQuest;
        
        acceptQuestBtn.GetComponent<LoadQuestToQuestMenu>().creature = gameObject;
        var ccq = gameObject.GetComponent<CheckCompleteQuest>();

        if (isHaveQuest)
        {
            questCont.SetActive(true);

            AddInfoToQuest();
        }

        if (ccq != null && ccq.win)
        {
            ccq.GiveReward();
            Destroy(ccq);
            qa.isHaveAsk = false;
            qa.DestroySign();
        }
    }

    private void AddInfoToQuest()
    {
        var titleCanvas = quest.transform.Find("Title").GetComponent<Text>();
        var conditionCanvas = quest.transform.Find("Descr").transform.Find("Condition").GetComponent<Text>();
        var historyCanvas = quest.transform.Find("Descr").transform.Find("HistoryCont").transform.Find("History").GetComponent<Text>();

        var title = gameObject.GetComponent<QuestEditor>().title;
        var condition = gameObject.GetComponent<QuestEditor>().condition;
        var history = gameObject.GetComponent<QuestEditor>().history;

        titleCanvas.text = title;
        conditionCanvas.text = condition;
        historyCanvas.text = history;
    }
}
