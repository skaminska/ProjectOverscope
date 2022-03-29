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
    [SerializeField] GameObject inventory;

    StarterAssetsInputs starterAssetsInputs;
    ThirdPersonController thirdPersonController;

    private void Start()
    {
        inventory.SetActive(false);
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (starterAssetsInputs.inventory && !inventory.activeInHierarchy)
        {
            inventory.SetActive(true);
            starterAssetsInputs.inventory = false;
            starterAssetsInputs.cursorLocked = false;
            thirdPersonController.LockCameraPosition = true;
        }
        else if (starterAssetsInputs.inventory && inventory.activeInHierarchy)
        {
            inventory.SetActive(false);
            starterAssetsInputs.inventory = false;
            starterAssetsInputs.cursorLocked = true;
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
