using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public enum AttackType
    {
        Infighting, // Ближний бой
        Magic, // Магия
        Ranged // Дальний бой
    }

    [Header("General")]
    public AttackType typeOfAttack = AttackType.Infighting;
    public float damage = 5f;

    [Header("For Magic and Ranged attack")]
    public GameObject bullet;
    public float bulletSpeed = 3f;
    

    [Header("For Infigthing attack")]
    public float waitTimeForAttack = 3f;

    
}
