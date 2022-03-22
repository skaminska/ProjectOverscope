using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public Type weaponType;
    [SerializeField] public string weaponName;
    [SerializeField] public int damage;
}
public enum Type { HANDS, MELEE, PISTOL, RIFFLE, SNIPER }