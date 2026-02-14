using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class LearnButton : MonoBehaviour
{
    public Button btn;
    public GameObject spell;
    private GameObject player;
    public GameObject spellOvercrowdingCont;
    public GameObject spellOvercrowding;
    public GameObject learnBookError;
    private GameObject parent;
    private GameObject spellAskCont;
    [HideInInspector] public GameObject bookInfo;
    private GameObject spellsInventory;
    private string rusName;
    private int countSlots = 5;
    private int countIteration;
    private float seeDistance = 5f;
    private ButtonClickedEvent onCl;
    private bool isPerformAdd = false;

    private void Awake()
    {
        spellAskCont = gameObject;
        spellsInventory = GameObject.Find("SpellsInventory");
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        btn = btn.GetComponent<Button>();
        btn.onClick.AddListener(BtnCheck);
    }

    private void BtnCheck()
    {
        CheckOvercrowding();
    }

    private void CheckOvercrowding()
    {
        if (countIteration >= 4)
        {
            spellAskCont.SetActive(false);
            spellOvercrowdingCont.SetActive(true);
            spellOvercrowding.SetActive(true);
        }
        else
        {
            CheckDistance();
        }
    }

    private void CheckDistance()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length != 0)
        {
            foreach (var enemy in enemies)
            {
                if (bookInfo.GetComponent<SpellInfo>() && Vector2.Distance(player.transform.position, enemy.transform.position) > seeDistance)
                {
                    isPerformAdd = true;
                }
                else
                {
                    isPerformAdd = false;
                    learnBookError.SetActive(true);
                    spellAskCont.SetActive(false);
                    Invoke("LearnBookErrorNotActive", 1);
                }
            }
        }

        if (isPerformAdd)
        {
            AddSpell();
        }
    }

    private void AddSpell()
    {
        var overcrowdingSpell = spellOvercrowdingCont.GetComponent<OvercrowdingSpell>();
        var playerUseSpell = player.GetComponent<PlayerUseSpell>();
        var spellInfo = bookInfo.GetComponent<SpellInfo>();
        var IDManager = spellsInventory.GetComponent<IDManager>();

        // Добавление способности в иерархию
        spell = Instantiate(spell);
        spell.transform.SetParent(CheckParent(), true);
        spell.transform.localScale = new Vector3(1, 1, 1);
        spell.transform.localPosition = new Vector3(0, 0, 0);
        spell.name = "SpellHave";

        var spellBtnInfo = spell.GetComponent<SpellBtnInfo>();

        // Ставим события клика на кнопки
        onCl = spell.GetComponent<Button>().onClick;
        onCl.AddListener(overcrowdingSpell.SelectSlot);
        onCl.AddListener(playerUseSpell.UseSpell);
        spellAskCont.SetActive(false);

        rusName = spellInfo.rusName;

        // Делаем кнопку интерактивной и возвращаем все значения по умолчанию
        spell.GetComponent<Button>().interactable = true;
        spell.GetComponentInChildren<Text>().text = rusName;
        spell.GetComponentInChildren<Text>().fontSize = 14;
        spell.GetComponentInChildren<Text>().color = Color.black;

        // Находим текст у кнопки и присваеваем ему русское название способности
        spell.GetComponentInChildren<Text>().text = rusName;
        spellBtnInfo.rusNameSpell = rusName;

        /*// Проверяем какой id у кнопки и присваеваем его
        var id = IDManager.Check();
        spellBtnInfo.id = id;*/

        player.GetComponent<PlayerUseSpell>().contSpellAsk = gameObject;

        // Присваеваем префаб пули кнопке
        var bullet = spellInfo.bullet;
        bullet.GetComponent<BulletInfo>().damage = spellInfo.damage;
        spellBtnInfo.bullet = bullet;

        spellBtnInfo.killAtTime = spellInfo.killAtTime;
        Destroy(spellInfo.spellBook);
    }

    private void LearnBookErrorNotActive()
    {
        learnBookError.SetActive(false);
    }

    // Проверка слота на то, есть ли в нём уже какая-либо способность?
    private Transform CheckParent()
    {
        for (int i = 0; i < countSlots; i++)
        {
            countIteration = i;
            parent = GameObject.Find("SpellSlot" + i);
            if (parent.transform.childCount == 0)
            {
                return parent.transform;
                
            }
        }
        return null;
    }
}
