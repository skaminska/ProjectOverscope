using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats : Singleton<PlayerStats>
{
    PlayerStatsUIController playerStatsUIController;
    QuestLog questLogController;

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

    List<Quest> collectedQuests;
    Quest followedQuest;


    private void Start()
    {
        maxHealth = 10;
        currentHealth = maxHealth;
        currentLevel = 1;
        experiencePoints = 0;
        baseDamage = 1;
        money = 0;
        currentDamageMin = baseDamage;
        currentDamageMax = baseDamage;
        nextLevelRequirements = 5;
        playerStatsUIController = GetComponent<PlayerStatsUIController>();
        playerStatsUIController.InitialSettings(currentLevel, nextLevelRequirements);
        collectedQuests = new List<Quest>();
        questLogController = FindObjectOfType<QuestLog>();
    }

    internal void AddMoney(int money)
    {
        this.money = money;
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

    public void SetFollowedQuest(Quest quest)
    {
        followedQuest = quest;
    }

    public Quest GetFollowedQuest()
    {
        return followedQuest;
    }

    public void UpdateFollowedQuest()
    {
        questLogController.UpdateFollowInfo();
    }

    public void RemoveFollowedQuest()
    {
        questLogController.HideFollowInfo();
        followedQuest = null;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void CollectQuest(Quest quest)
    {
        collectedQuests.Add(quest);
    }
    public List<Quest> GetColletedQuests()
    {
        return collectedQuests;
    }
}
