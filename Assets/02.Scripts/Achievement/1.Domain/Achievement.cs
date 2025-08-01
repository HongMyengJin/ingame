﻿using System;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public enum EAchievementCondition
{ 
    GoldCollect,
    DronKillCount,
    BossKillCount,
    PlayTime,
    Trigger
}
public class Achievement
{
    // 최종 목적: 자기서술형

    // 데이터
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    // 상태
    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _rewardClaimed;
    public bool RewardClaimed => _rewardClaimed;

    // 생성자

    
    public Achievement(AchievementDTO achievementDTO, AchievementSaveData saveData)
    {
        if (string.IsNullOrEmpty(achievementDTO.ID))
        {
            throw new ArgumentException("업적 ID은 비어있을 수 없습니다.");
        }

        if (string.IsNullOrEmpty(achievementDTO.Name))
        {
            throw new ArgumentException("업적 이름은 비어있을 수 없습니다.");
        }
        
        if (string.IsNullOrEmpty(achievementDTO.Description))
        {
            throw new ArgumentException("업적 설명은 비어있을 수 없습니다.");
        }
        
        if (achievementDTO.GoalValue < 0)
        {
            throw new ArgumentException("업적 목표 값은 0보다 커야합니다.");
        }

        if (achievementDTO.RewardAmount < 0)
        {
            throw new ArgumentException("업적 보상 값은 0보다 커야합니다.");
        }

        if (saveData.CurrentValue < 0)
        {
            throw new ArgumentException("업적 진행 값은 0보다 커야합니다.");
        }

        ID = achievementDTO.ID;
        Name = achievementDTO.Name;
        Description = achievementDTO.Description;
        GoalValue = achievementDTO.GoalValue;
        Condition = achievementDTO.Condition;
        RewardCurrencyType = achievementDTO.RewardCurrencyType;
        RewardAmount = achievementDTO.RewardAmount;

        _currentValue = saveData.CurrentValue;
        _rewardClaimed = saveData.RewardClaimed;
    }

    public void Increase(int value)
    {
        if (value <= 0)
        {
            throw new Exception("증가 값은 0보다 커야합니다.");
        }

        _currentValue += value;
    }

    public bool CanClaimReward()
    {
        return RewardClaimed == false && CurrentValue >= GoalValue;
    }

    public bool TryClaimReward()
    {
        if(!CanClaimReward())
        {
            return false;
        }

        _rewardClaimed = true;

        return true;
    }
}
