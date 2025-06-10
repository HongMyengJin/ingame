using UnityEngine;

public class AchievementDTO
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public readonly int CurrentValue;
    public readonly int GoalValue;
    public readonly ECurrencyType RewardCurrencyType;
    public readonly int RewardAmount;

    public readonly bool RewardClaimed;

    public AchievementDTO(string id,
                          string name,
                          string description,
                          EAchievementCondition condition,
                          int currentValue,
                          int goalValue,
                          ECurrencyType rewardCurrencyType,
                          int rewardAmount,
                          bool rewardClaimed)
    {
        ID = id;
        Name = name;
        Description = description;
        Condition = condition;
        CurrentValue = currentValue;
        GoalValue = goalValue;
        RewardCurrencyType = rewardCurrencyType;
        RewardAmount = rewardAmount;
        RewardClaimed = rewardClaimed;
    }
    public AchievementDTO(Achievement achievement)
    {
        ID = achievement.ID;
        Name = achievement.Name;
        Description = achievement.Description;
        Condition = achievement.Condition;
        CurrentValue = achievement.CurrentValue;
        GoalValue = achievement.GoalValue;
        RewardCurrencyType = achievement.RewardCurrencyType;
        RewardAmount = achievement.RewardAmount;
        RewardClaimed = achievement.RewardClaimed;
    }

    /*
     * public string ID;
    public int CurrentValue;
    public bool RewardClaimed;
     */
    public AchievementDTO(string id,
                          int currentValue,
                          bool rewardClaimed)
    {
        ID = id;
        CurrentValue = currentValue;
        RewardClaimed = rewardClaimed;
    }
    public bool CanClaimReward()
    {
        return RewardClaimed == false && CurrentValue >= GoalValue;
    }

}
