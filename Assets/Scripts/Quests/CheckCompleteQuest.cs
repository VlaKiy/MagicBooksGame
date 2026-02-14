using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCompleteQuest : MonoBehaviour
{
    private QuestEditor qe;
    private QuestEditor.TypeOfQuest typeOfQuest;
    [HideInInspector] public bool win = false;

    public GameObject questView;

    private void Awake()
    {
        qe = gameObject.GetComponent<QuestEditor>();
        typeOfQuest = qe.typeOfQuest;
    }

    private void Update()
    {
        if (!win)
        {
            if (typeOfQuest == QuestEditor.TypeOfQuest.Kill)
            {
                CheckCompleteKillQuest();
            }
            if (typeOfQuest == QuestEditor.TypeOfQuest.Find)
            {
                CheckCompleteFindQuest();
            }
        }
    }

    private void CheckCompleteKillQuest()
    {
        var goals = qe.goals;
        var countKill = 0;

        for (int i = 0; i < goals.Length; i++)
        {
            if (goals[i] == null)
            {
                countKill++;
            }
        }

        if (countKill == goals.Length)
        {
            QuestCompleted();
        }
    }

    private void CheckCompleteFindQuest()
    {
        var things = qe.things;
        var thingsFind = 0;

        for (int i = 0; i < things.Length; i++)
        {
            if (things[i] == null)
            {
                thingsFind++;
            }
        }

        if (thingsFind == things.Length)
        {
            QuestCompleted();
        }
    }

    private void QuestCompleted() 
    {
        Debug.Log("Миссия выполнена!");

        win = true;

        // Удаление квеста из Меню Квестов
        var title = qe.title;
        var cond = qe.condition;

        for (int i = 0; i < questView.transform.childCount; i++)
        {
            var quest = questView.transform.GetChild(i);

            var titleQuest = quest.transform.Find("QuestTitle").GetComponent<Text>().text;
            var condQuest = quest.transform.Find("QuestCondition").Find("Text").GetComponent<Text>().text;

            if (titleQuest == title && condQuest == cond)
            {
                Destroy(quest.gameObject);
                Debug.Log("Quest was Destroy");
            }
        }
    }

    public void GiveReward()
    {
        var reward = Instantiate(qe.reward);
        var player = GameObject.Find("Player");
        var distance = 0.1f;

        reward.transform.localPosition = new Vector3(player.transform.position.x - distance, player.transform.position.y, 0f);
    }
}
