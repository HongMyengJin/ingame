﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField]
    private List<UI_AchievementSlot> _slots;

    private void Start()
    {
        Refresh();
        AchievementManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for (int i = 0; i < achievements.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }

    public void addSlot(UI_AchievementSlot achievementSlot)
    {
        _slots.Add(achievementSlot);
        Refresh();
    }
}
