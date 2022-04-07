using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] TextMeshProUGUI questName, questDescription, questReward;
    [SerializeField] Button getQuest;
    [SerializeField] QuestLog questLog;

    StarterAssetsInputs starterAssetsInputs;
    ThirdPersonController thirdPersonController;
    void Start()
    {
        starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
        window.SetActive(false);
    }

    public void ShowQuestInfo(Quest quest, QuestGiver questGiver)
    {
        questName.text = quest.GetQuestName();
        questDescription.text = quest.GetQuestDescription();
        questReward.text = quest.GetQuestReward();
        window.SetActive(true);

        starterAssetsInputs.SetCursorState(false);
        thirdPersonController.LockCameraPosition = true;

        getQuest.onClick.RemoveAllListeners();
        getQuest.onClick.AddListener(() => GetQuest(quest, questGiver));
    }

    public void GetQuest(Quest quest, QuestGiver questQiver)
    {
        Debug.Log(quest.GetQuestName());
        PlayerStats.Instance.CollectQuest(quest);
        questLog.AddToQuestLog(quest);
        HideWindow();
        quest.SetQuestStatus(QuestStatus.COLLECTED);
        questQiver.CheckIfQuestAvailable();
    }

    public void HideWindow()
    {
        window.SetActive(false);
        starterAssetsInputs.SetCursorState(true);
        thirdPersonController.LockCameraPosition = false;
    }
}
