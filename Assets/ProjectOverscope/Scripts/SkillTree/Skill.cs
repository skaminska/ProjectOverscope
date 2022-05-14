using UnityEngine;

[CreateAssetMenu(menuName = "Skills")]
public class Skill : ScriptableObject
{
    [SerializeField] StatType affectStat;
    [SerializeField] int valueToAdd;
    [SerializeField] Skill nextSkill;
    [SerializeField] bool bought;
    [SerializeField] AffectValue affectValue;

    public StatType GetAffectStat()
    {
        return affectStat;
    }

    public AffectValue GetAffectValue()
    {
        return affectValue;
    }

    public int GetValueToAdd()
    {
        return valueToAdd;
    }

    public string GetSkillDescription()
    {
        return affectStat + " | " + affectValue + " + " + valueToAdd;
    }
}

public enum AffectValue { BASE, BOOST }
