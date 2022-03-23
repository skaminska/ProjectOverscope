using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] Slider expSlider; 
    
    public void SetExpPointValue(int value)
    {
        expSlider.value = value;
    }

    public void SetNewLevel(int currentExp, int nextLevelRequirements, int newLevel)
    {
        expSlider.maxValue = nextLevelRequirements;
        expSlider.value = currentExp;
        level.text = newLevel.ToString();
    }

    internal void InitialSettings(int currentLevel, int nextLevelRequirements)
    {
        level.text = currentLevel.ToString();
        expSlider.maxValue = nextLevelRequirements;
    }
}
