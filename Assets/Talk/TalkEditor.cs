using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkEditor : MonoBehaviour
{
    public GameObject talkCont;
    public float timer;
    [TextArea(0, 20)] public string[] talks;
    public GameObject lookAt;
    public GameObject[] lookAtObj;

    private GameObject canvas;
    private GameObject[] panels;
    private int i = 0;

    private GameObject spellsInventory;
    private GameObject joystick;
    private GameObject questMenuButton;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        spellsInventory = GameObject.Find("SpellsInventory");
        joystick = GameObject.Find("Fixed Joystick");
        questMenuButton = GameObject.Find("QuestMenuButton");
    }

    public void StartTalk()
    {
        if (talks.Length > 0)
        {
            panels = new GameObject[talks.Length];
            foreach (var talk in talks)
            {
                var talkPanel = Instantiate(talkCont);
                var rt = talkPanel.GetComponent<RectTransform>();
                
                talkPanel.name = "TalkPanel";
                talkPanel.SetActive(false);
                talkPanel.transform.SetParent(canvas.transform);
                talkPanel.transform.localScale = talkCont.transform.localScale;

                rt.transform.position = new Vector2(talkCont.GetComponent<RectTransform>().transform.position.x, talkCont.GetComponent<RectTransform>().transform.position.y);
                

                if (lookAtObj.Length == 0 || lookAtObj[0] == null)
                {
                    talkPanel.GetComponent<FollowGameobject>().lookAt = lookAt.transform;
                    talkPanel.GetComponent<FollowGameobject>().offset = new Vector3(3.17f, 1.61f, 0f);
                }
                else
                {

                }

                talkPanel.GetComponentInChildren<Text>().text = talk;

                panels[i] = talkPanel;
                i++;
            }
            StartCoroutine(ShowTalkPanel());
        }
    }

    private IEnumerator ShowTalkPanel()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(true);
            yield return new WaitForSeconds(timer);
            Destroy(panel);
        }
        spellsInventory.SetActive(true);
        joystick.SetActive(true);
        questMenuButton.SetActive(true);
    }
}
