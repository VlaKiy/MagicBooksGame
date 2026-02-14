using UnityEngine;

public class SpellBtnInfo : MonoBehaviour
{
    public GameObject btnPrefab;
    [HideInInspector] public GameObject bullet;
    [HideInInspector] public string rusNameSpell;
    [HideInInspector] public int id;
    [HideInInspector] public SpellInfo.KillAtTime killAtTime;
}
