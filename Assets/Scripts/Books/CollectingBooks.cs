using UnityEngine;
using UnityEngine.UI;

public class CollectingBooks : MonoBehaviour
{   
    public GameObject contSpellAsk;

    private GameObject spellAsk;
    private GameObject learnBookError;
    private string rusName;

    [SerializeField] public float seeDistance = 5f;

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("SpellBook"))
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            var isCanCollecting = false;

            if (enemies.Length != 0)
            {
                foreach (var enemy in enemies)
                {
                    if (enemy.GetComponent<EnemyAI>().isRunsForPlayer == false)
                    {
                        isCanCollecting = true;
                    }
                    else
                    {
                        isCanCollecting = false;
                        learnBookError.SetActive(true);
                        Invoke("LearnBookErrorNotActive", 1);
                        break;
                    }
                }
            }
            else if (enemies.Length == 0)
            {
                isCanCollecting = true;
            }

            if (isCanCollecting)
            {
                BookCollecting();
            }
        }
    }

    private void BookCollecting()
    {
        var csa = Instantiate(contSpellAsk);
        csa.transform.SetParent(GameObject.Find("Canvas").transform);
        csa.transform.localScale = contSpellAsk.transform.localScale;
        csa.GetComponent<RectTransform>().offsetMin = new Vector2(contSpellAsk.GetComponent<RectTransform>().offsetMin.x, contSpellAsk.GetComponent<RectTransform>().offsetMin.y);
        csa.GetComponent<RectTransform>().offsetMax = new Vector2(contSpellAsk.GetComponent<RectTransform>().offsetMax.x, contSpellAsk.GetComponent<RectTransform>().offsetMax.y);
        csa.GetComponent<LearnButton>().bookInfo = gameObject;
        csa.SetActive(false);

        rusName = gameObject.GetComponent<SpellInfo>().rusName;
        var title = csa.transform.Find("SpellAsk").Find("Title").GetComponent<Text>();
        title.text = $"Вы нашли книгу с заклинанием '{rusName}'";

        csa.SetActive(true);
    }

    private void LearnBookErrorNotActive()
    {
        learnBookError.SetActive(false);
    }
}
