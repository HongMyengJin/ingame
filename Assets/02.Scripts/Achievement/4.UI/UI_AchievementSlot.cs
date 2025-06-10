using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressTextUI;
    public TextMeshProUGUI RewardClaimDate;
    public Button RewardClaimButton;

    private AchievementDTO _achievementDTO;


    public void Refresh(AchievementDTO achievementDTO)
    {
        _achievementDTO = achievementDTO;
        NameTextUI.text = achievementDTO.Name.ToString();
        DescriptionTextUI.text = achievementDTO.Description;
        RewardCountTextUI.text = achievementDTO.RewardAmount.ToString();
        ProgressSlider.value = (float)achievementDTO.CurrentValue / achievementDTO.GoalValue;
        ProgressTextUI.text = $"{achievementDTO.CurrentValue} / {achievementDTO.GoalValue}";

        RewardClaimButton.interactable = achievementDTO.CanClaimReward();
        // achievement.Increase(30);
    }

    public void ClaimReward()
    {
        if(AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {

        }
        else
        {

        }

    }
}
