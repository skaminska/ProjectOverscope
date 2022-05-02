using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillToGameObject : MonoBehaviour
{
    [SerializeField] Skill skill;

    public void SetSkill(Button button, TextMeshProUGUI text)
    {
        button.onClick.AddListener(() => PlayerStats.Instance.ChangeStat(skill.GetAffectStat(), skill.GetAffectValue(), skill.GetValueToAdd()));
        text.text = skill.GetSkillDescription();
    }
}
