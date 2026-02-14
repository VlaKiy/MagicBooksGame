using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureHaveQuestAssignment : MonoBehaviour
{
    public bool isHaveQuest = false;
    [HideInInspector] public bool isHaveAsk = false;
    public GameObject signPrefab;
    public GameObject signAskPrefab;
    private GameObject canvas;
    private GameObject[] signs;
    private bool isDestroied = false;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    private void Update()
    {
        if (isHaveQuest)
        {
            Assignment(signPrefab);
        }
        else
        {
            if (!isDestroied)
            {
                DestroySign();
            }
            
            if (isHaveAsk)
            {
                Assignment(signAskPrefab);
            }
        }
    }

    private void Assignment(GameObject signPrefab)
    {
        if (!CheckHaveSign())
        {
            var sign = Instantiate(signPrefab);
            sign.name = "QuestSign";
            sign.transform.SetParent(canvas.transform);
            sign.transform.localScale = new Vector3(1f, 1f, 1f);

            var follow = sign.GetComponent<FollowGameobject>();
            follow.lookAt = gameObject.transform;
        }
    }

    public void DestroySign()
    {
        if (CheckHaveSign())
        {
            var sign = GameObject.FindWithTag("QuestSign");
            Destroy(sign);
            isDestroied = true;
        }
    }

    private bool CheckHaveSign()
    {
        signs = GameObject.FindGameObjectsWithTag("QuestSign");

        if (signs.Length != 0)
        {
            foreach (var sign in signs)
            {
                if (sign.GetComponent<FollowGameobject>().lookAt == gameObject.transform)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
