using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IDManager : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public int Check()
    {
        var rusName = player.GetComponent<CollectingBooks>().gameObject.GetComponent<SpellInfo>().rusName;
        

        if (rusName == "Огненный дождь")          { return 0; }
        if (rusName == "Ураган")                  { return 1; }
        if (rusName == "Метель")                  { return 2; }
        if (rusName == "Возрождение деревьев")    { return 3; }
        if (rusName == "Удар глыбой льда")        { return 4; }
        if (rusName == "Кислотный плевок")        { return 5; }
        if (rusName == "Восстановление здоровья") { return 6; }

        return -1;
    }
}
