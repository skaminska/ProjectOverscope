using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class PlayerStatsUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] Slider expSlider;
    [SerializeField] GameObject equipment;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject questLog;

    StarterAssetsInputs starterAssetsInputs;
    ThirdPersonController thirdPersonController;

    private void Start()
    {
        equipment.SetActive(false);
        inventory.SetActive(false);
        questLog.SetActive(false);
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (starterAssetsInputs.equipment && !equipment.activeInHierarchy)
        {
            equipment.SetActive(true);
            starterAssetsInputs.equipment = false;
            starterAssetsInputs.SetCursorState(false);
            thirdPersonController.LockCameraPosition = true;
        }
        else if (starterAssetsInputs.equipment && equipment.activeInHierarchy)
        {
            equipment.SetActive(false);
            inventory.SetActive(false);
            starterAssetsInputs.equipment = false;
            starterAssetsInputs.SetCursorState(true);
            thirdPersonController.LockCameraPosition = false;
        }

        if (starterAssetsInputs.quests && !questLog.activeInHierarchy)
        {
            questLog.SetActive(true);
            starterAssetsInputs.quests = false;
            starterAssetsInputs.SetCursorState(false);
            thirdPersonController.LockCameraPosition = true;
        }
        else if (starterAssetsInputs.quests && questLog.activeInHierarchy)
        {
            questLog.SetActive(false);
            starterAssetsInputs.quests = false;
            starterAssetsInputs.SetCursorState(true);
            thirdPersonController.LockCameraPosition = false;
        }
    }

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
