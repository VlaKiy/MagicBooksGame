using UnityEngine;
using UnityEngine.UI;

public class LoadQuestToQuestMenu : MonoBehaviour
{
    public GameObject questPrefab;
    public GameObject questView;
    public GameObject questViewCont;
    public GameObject quest;

    private float posY = -105f;
    private float weightCont = 210f;

    [HideInInspector] public GameObject creature;

    public void LoadQuestFromCreature()
    {
        creature.GetComponent<CreatureHaveQuestAssignment>().isHaveQuest = false;
        creature.GetComponent<CreatureHaveQuestAssignment>().isHaveAsk = true;
        LoadQuest(creature);
    }

    public void LoadQuestFromAuthor()
    {
        var ccq = gameObject.AddComponent<CheckCompleteQuest>();
        ccq.questView = questViewCont;

        var title = gameObject.GetComponent<QuestEditor>().title;
        var condition = gameObject.GetComponent<QuestEditor>().condition;

        var q = Instantiate(questPrefab);
        var rt = q.GetComponent<RectTransform>();
        q.transform.SetParent(questViewCont.transform);
        q.transform.localPosition = new Vector3(questPrefab.transform.localPosition.x, 0f, questPrefab.transform.localPosition.z);
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, posY);
        q.transform.localScale = new Vector3(1f, 1f, 1f);

        q.transform.Find("QuestTitle").GetComponent<Text>().text = title;
        q.transform.Find("QuestCondition").Find("Text").GetComponent<Text>().text = condition;
        rt.offsetMin = new Vector2(0f, q.GetComponent<RectTransform>().offsetMin.y);
        rt.offsetMax = new Vector2(0f, q.GetComponent<RectTransform>().offsetMax.y);

        questViewCont.GetComponent<RectTransform>().sizeDelta = new Vector2(questViewCont.GetComponent<RectTransform>().sizeDelta.x, weightCont);

        posY -= 210f;
        weightCont += 210f;
    }

    private void LoadQuest(GameObject obj)
    {
        var ccq = obj.AddComponent<CheckCompleteQuest>();
        ccq.questView = questViewCont;

        var title = quest.transform.Find("Title").GetComponent<Text>().text;
        var cond = quest.transform.Find("Descr").Find("Condition").GetComponent<Text>().text;

        var q = Instantiate(questPrefab);
        var rt = q.GetComponent<RectTransform>();
        q.transform.SetParent(questViewCont.transform);
        q.transform.localPosition = new Vector3(questPrefab.transform.localPosition.x, 0f, questPrefab.transform.localPosition.z);
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, posY);
        q.transform.localScale = new Vector3(1f, 1f, 1f);

        q.transform.Find("QuestTitle").GetComponent<Text>().text = title;
        q.transform.Find("QuestCondition").Find("Text").GetComponent<Text>().text = cond;
        rt.offsetMin = new Vector2(0f, q.GetComponent<RectTransform>().offsetMin.y);
        rt.offsetMax = new Vector2(0f, q.GetComponent<RectTransform>().offsetMax.y);

        questViewCont.GetComponent<RectTransform>().sizeDelta = new Vector2(questViewCont.GetComponent<RectTransform>().sizeDelta.x, weightCont);

        posY -= 210f;
        weightCont += 210f;
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
