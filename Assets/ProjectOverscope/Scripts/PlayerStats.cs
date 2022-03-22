using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    float health;
    int baseDamage;
    int currentDamage;

    private void Start()
    {
        health = 10;
        baseDamage = 1;
        currentDamage = baseDamage;
    }
    
    public int GetCurrentDamage()
    {
        return currentDamage;
    }

    public void SetCurrentDamage(int weaponDamage)
    {
        currentDamage = baseDamage + weaponDamage;
    }
}
