using UnityEngine;

public class AchievementDTO
{
    [CsvColumn("ID")]
    public string ID;
    [CsvColumn("Name")]
    public string Name;
    [CsvColumn("Description")]
    public string Description;

    // EAchievementCondition
    [CsvColumn("EAchievementCondition")]
    public EAchievementCondition Condition;

    [CsvColumn("GoalValue")]
    public int GoalValue;
    [CsvColumn("RewardType")]
    public ECurrencyType RewardCurrencyType;
    [CsvColumn("RewardAmount")]
    public int RewardAmount;

    public bool RewardClaimed;
    public int CurrentValue;

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

    public AchievementDTO()
    {

    }
    public bool CanClaimReward()
    {
        return RewardClaimed == false && CurrentValue >= GoalValue;
    }

}
