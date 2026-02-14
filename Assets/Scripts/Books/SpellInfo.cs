using UnityEngine;

public class SpellInfo : MonoBehaviour
{
    public enum KillAtTime // Сколько можно убить за раз
    {
        One,
        Many
    }

    public string rusName;
    public float damage;
    public GameObject bullet;
    public GameObject btnSpell;
    public GameObject spellBook;

    [Header("Settings")]

    public KillAtTime killAtTime = KillAtTime.One;
}
