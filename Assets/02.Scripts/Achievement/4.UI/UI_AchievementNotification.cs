using System.Collections;
using TMPro;
using UnityEngine;

public class UI_AchievementNotification : PopupUI
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;

    private AchievementDTO _achievementDTO;

    private void Awake()
    {
        AchievementManager.Instance.OnNewAchievementRewarded += Refresh;
    }

    public void Refresh(AchievementDTO achievementDTO)
    {
        _achievementDTO = achievementDTO;
        NameTextUI.text = achievementDTO.Name.ToString();
        DescriptionTextUI.text = achievementDTO.Description;
    }

}
