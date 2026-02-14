using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OvercrowdingSpell : MonoBehaviour
{
    public GameObject spell;
    public GameObject player;
    public GameObject enemy;
    public GameObject overcrowdingHelper;
    public GameObject contSpellOvercrowding;
    public GameObject learnBookError;
    private GameObject bookInfo;
    private GameObject spellsInventory;
    private string rusName;
    private float seeDistance = 5f;
    private ButtonClickedEvent onCl;

    private void Start()
    {
        spellsInventory = GameObject.Find("SpellsInventory");
    }

    public void SelectSlot()
    {
        var btn = EventSystem.current.currentSelectedGameObject;
        if (btn.name == "SpellHave")
        {
            if (overcrowdingHelper.activeSelf)
            {
                bookInfo = player.GetComponent<CollectingBooks>().gameObject;
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");

                if (enemies.Length != 0)
                {
                    foreach (var enemy in enemies)
                    {
                        if (bookInfo.GetComponent<SpellInfo>() && Vector2.Distance(player.transform.position, enemy.transform.position) > seeDistance)
                        {
                            AddSpell(btn);
                        }
                        else
                        {
                            learnBookError.SetActive(true);
                            contSpellOvercrowding.SetActive(false);
                            overcrowdingHelper.SetActive(false);
                            Invoke("LearnBookErrorNotActive", 1);
                        }
                    }
                }
                else
                {
                    AddSpell(btn);
                }
            }
        }
    }
    
    private void AddSpell(GameObject btn)
    {
        btn.name = "deleted";
        Destroy(btn);

        var overcrowdingSpell = contSpellOvercrowding.GetComponent<OvercrowdingSpell>();
        var playerUseSpell = player.GetComponent<PlayerUseSpell>();
        var spellInfo = bookInfo.GetComponent<SpellInfo>();
        var IDManager = spellsInventory.GetComponent<IDManager>();

        // Добавление способности в иерархию
        spell = Instantiate(spell);
        spell.transform.SetParent(btn.transform.parent, true);
        spell.transform.localScale = new Vector3(1, 1, 1);
        spell.transform.localPosition = new Vector3(0, 0, 0);
        spell.name = "SpellHave";

        var spellBtnInfo = spell.GetComponent<SpellBtnInfo>();

        onCl = spell.GetComponent<Button>().onClick;
        onCl.AddListener(overcrowdingSpell.SelectSlot);
        onCl.AddListener(playerUseSpell.UseSpell);
        overcrowdingHelper.SetActive(false);
        contSpellOvercrowding.SetActive(false);

        // Находим текст у кнопки и присваеваем ему русское название способности
        rusName = spellInfo.rusName;

        spell.GetComponentInChildren<Text>().text = rusName;
        spellBtnInfo.rusNameSpell = rusName;

        var id = IDManager.Check();
        spellBtnInfo.id = id;

        // Присваеваем префаб пули кнопке
        var bullet = spellInfo.bullet;
        spellBtnInfo.bullet = bullet;

        Destroy(spellInfo.spellBook);
    }

    private void LearnBookErrorNotActive()
    {
        learnBookError.SetActive(false);
    }

}
