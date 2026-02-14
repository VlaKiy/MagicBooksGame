using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public GameObject helper;
    public GameObject questSignHelp;

    private GameObject player;
    private GameObject[] friendlyCreatures;

    private bool isPlayerApproached = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        friendlyCreatures = GameObject.FindGameObjectsWithTag("FriendlyCreature");
    }

    private void Update()
    {
        if (IsPlayerApproached() && !isPlayerApproached)
        {
            helper.SetActive(true);
            questSignHelp.SetActive(true);
            isPlayerApproached = true;
            Invoke("NotShowQuestSignHelp", 8f);
        }
    }

    private bool IsPlayerApproached()
    {
        var seeDistance = 3f;

        foreach (var creature in friendlyCreatures)
        {
            if (Vector3.Distance(creature.transform.position, player.transform.position) <= seeDistance)
            {
                return true;
            }
        }

        return false;
    }
    private void NotShowQuestSignHelp()
    {
        helper.SetActive(false);
        questSignHelp.SetActive(false);
    }
}
