using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarAssignment : MonoBehaviour
{
    public GameObject healthBarPrefab;
    private GameObject[] friendlyCreatures;
    private GameObject[] enemies;
    private GameObject[] player;
    private GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");

        player = GameObject.FindGameObjectsWithTag("Player");
        friendlyCreatures = GameObject.FindGameObjectsWithTag("FriendlyCreature");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        Assignment(player);
        Assignment(friendlyCreatures);
        Assignment(enemies);
    }

    private void Assignment (GameObject[] creatures)
    {
        foreach (var creature in creatures)
        {
            if (creature != null)
            {
                if (!CheckHaveHealthBar(creature))
                {
                    var healthBar = Instantiate(healthBarPrefab);
                    healthBar.name = "HealthBar";
                    healthBar.transform.SetParent(canvas.transform);
                    healthBar.transform.SetSiblingIndex(0);
                    healthBar.transform.localScale = new Vector3(0.6f, 0.07f, 1f);
                    healthBar.GetComponent<FollowGameobject>().lookAt = creature.transform;
                    if (creature.GetComponent<SpriteRenderer>())
                    {
                        var rt = creature.GetComponent<SpriteRenderer>();
                        var y = rt.size.y + 1.1f;
                        healthBar.GetComponent<FollowGameobject>().offset = new Vector3(0f, y, 0f);
                    }
                    
                }
            }
        }
    }

    private bool CheckHaveHealthBar(GameObject creature)
    {
        var bars = GameObject.FindGameObjectsWithTag("HealthBar");

        if (bars.Length != 0)
        {
            foreach (var bar in bars)
            {
                if (bar.GetComponent<FollowGameobject>().lookAt == creature.transform)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
