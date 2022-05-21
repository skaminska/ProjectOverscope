using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Stat
{
    PlayerStatsUIController UIController;
    private void Start()
    {
        UIController = FindObjectOfType<PlayerStatsUIController>();
        UIController.UpdateHealthBar(GetMaxValue(), GetCurrentValue());
    }
    public override void ChangeCurrentValue(int change)
    {
        base.ChangeCurrentValue(change);
        UIController.UpdateHealthBar(GetMaxValue(), GetCurrentValue());
    }
}
