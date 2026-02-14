using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEditor : MonoBehaviour
{
    public enum TypeOfQuest
    {
        Kill,
        KillAndGive,
        Find
    }

    [Header("General")]

    public TypeOfQuest typeOfQuest = TypeOfQuest.Kill;

    public string title;
    [TextArea(1, 20)] public string condition;
    [TextArea(1, 20)] public string history;

    public GameObject reward;

    [Header("For Kill")]

    public GameObject[] goals;

    [Header("For Find")]

    public GameObject[] things;
}
