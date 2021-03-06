using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] StatType statType;
    [SerializeField] int currentValue;
    [SerializeField] int maxValue;
    [SerializeField] int valueBase; //basic max value without the boost
    [SerializeField] int boostPercent; //boost is the percent value that will be added to the base value and will create a max value


    public int GetCurrentValue()
    {
        return currentValue;
    }
    public int GetMaxValue()
    {
        return maxValue;
    }
    public StatType GetStatType()
    {
        return statType;
    }
    public virtual void ChangeCurrentValue(int change)
    {
        currentValue += change;
    }
    public void ChangeValue(int valueToAdd)//valueToAdd could be >0
    {
        valueBase += valueToAdd;
        RecalculateMaxValue();
    }

    public void ChangeBoost(int valueToAdd)//valueToAdd could be >0
    {
        boostPercent += valueToAdd;
        RecalculateMaxValue();
    }

    public void RecalculateMaxValue()
    {
        maxValue = valueBase + (valueBase * boostPercent) / 100;
        Debug.Log(statType + " | " + valueBase + " + " + boostPercent + "%  | NewMaxValue = " + maxValue);
    }
}

public enum StatType { HEALTH, DAMAGE, }
