using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    PlayerStatsUIController playerStatsUIController;

    int maxHealth;
    int currentHealth;
    int baseDamage;
    int currentDamageMin;
    int currentDamageMax;
    int experiencePoints;
    int skillPoints;
    int currentLevel;
    int nextLevelRequirements;
    int money;

    private void Start()
    {
        maxHealth = 10;
        currentHealth = maxHealth;
        currentLevel = 3;
        experiencePoints = 0;
        baseDamage = 1;
        money = 0;
        currentDamageMin = baseDamage;
        currentDamageMax = baseDamage;
        nextLevelRequirements = 5;
        playerStatsUIController = GetComponent<PlayerStatsUIController>();
        playerStatsUIController.InitialSettings(currentLevel, nextLevelRequirements);
    }
    
    public int GetCurrentDamage()
    {
        return UnityEngine.Random.Range(currentDamageMin, currentDamageMax);
    }

    public void SetCurrentDamage(int weaponDamageMin, int weaponDamageMax)
    {
        currentDamageMin = baseDamage + weaponDamageMin;
        currentDamageMax = baseDamage + weaponDamageMax;
    }

    public void AddExperiencePoint(int value)
    {
        experiencePoints += value;
        playerStatsUIController.SetExpPointValue(experiencePoints);
        if(experiencePoints >= nextLevelRequirements)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        currentLevel++;
        skillPoints++;
        experiencePoints -= nextLevelRequirements;
        nextLevelRequirements = currentLevel * 5;
        playerStatsUIController.SetNewLevel(experiencePoints, nextLevelRequirements, currentLevel);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
