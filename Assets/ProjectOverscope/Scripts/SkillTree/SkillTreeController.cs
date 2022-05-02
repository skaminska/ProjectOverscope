using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI skillPointsAmount;
    [SerializeField] List<GameObject> skills;

    void Start()
    {
        foreach(var skill in skills)
        {
            skill.GetComponent<SkillToGameObject>().SetSkill(skill.GetComponent<Button>(), skill.GetComponentInChildren<TextMeshProUGUI>());
        }
        skillPointsAmount.text = PlayerStats.Instance.GetSkillPoints().ToString();
    }

}
