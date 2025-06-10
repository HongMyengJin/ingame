using UnityEngine;
using System.Collections.Generic;

public class AchievementLoader : MonoBehaviour
{
    [SerializeField] private TextAsset csvAsset;

    public List<AchievementDTO> _achievements;

    private void Awake()
    {
        ParseAchievement();
    }

    public void ParseAchievement()
    {
        _achievements = CsvParser.Parse<AchievementDTO>(csvAsset);

        foreach (var achievement in _achievements)
        {
            Debug.Log($"[{achievement.ID}] {achievement.Name} - {achievement.Description} / Goal: {achievement.GoalValue}, Reward: {achievement.RewardCurrencyType} {achievement.RewardAmount}");
        }
    }

    public List<AchievementDTO> GetAchievement()
    {
        if (_achievements == null)
            ParseAchievement();
        return _achievements;
    }
}