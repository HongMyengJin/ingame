using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [SerializeField]
    private List<AchievementSO> _metaDatas;

    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll((a) => new AchievementDTO(a));

    public event Action OnDataChanged;
    public event Action<AchievementDTO> OnNewAchievementRewarded;

    private AchievementRepository _repository;

    [SerializeField]
    private AchievementLoader _achievementLoader;

    [SerializeField]
    private GameObject ui_AchievementContent;
    [SerializeField]
    private GameObject ui_AchievementPrefab;
    [SerializeField]
    private UI_Achievement ui_Achievement;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        // 초기화

        _achievements = new List<Achievement>();

        _repository = new AchievementRepository();

        List<AchievementDTO> achievementDTO = _achievementLoader.GetAchievement(); // csv 파일 로드
        List<AchievementSaveData> saveDatas = _repository.Load();/*_repository.Load();*/

        // 도메인끼리 상호작용해야 할 부분은 여기에 넣는다.
        foreach (var metaData in achievementDTO)
        {
            Achievement duplicatedAchievement = FindByID(metaData.ID);
            if (duplicatedAchievement != null)
            {
                throw new Exception($"업적 ID({metaData.ID}가 중복됩니다.)");
            }

            // 데이터 생성
            AchievementSaveData saveData = saveDatas?.Find(a => a.ID == metaData.ID) ?? new AchievementSaveData();
            Achievement achievement = new Achievement(metaData, saveData);
            _achievements.Add(achievement);

            GameObject achievementObject = Instantiate(ui_AchievementPrefab);
            achievementObject.transform.SetParent(ui_AchievementContent.transform, false);

            UI_AchievementSlot ui_AchievementSlot  = achievementObject.GetComponent<UI_AchievementSlot>();
            ui_Achievement.addSlot(ui_AchievementSlot);
        }
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Condition == condition)
            {
                bool prevCanClaimReward = achievement.CanClaimReward();

                achievement.Increase(value);
                bool canCanClaimReward = achievement.CanClaimReward();

                if(prevCanClaimReward == false && canCanClaimReward == true)
                {
                    PopUpManager.Instance.ShowAchievementPopup();
                    OnNewAchievementRewarded?.Invoke(new AchievementDTO(achievement));
                }
            }
        }
        _repository.Save(Achievements);
        OnDataChanged?.Invoke();
    }

    private Achievement FindByID(string id)
    {
        return _achievements.Find(a => a.ID == id);
    }

    public bool TryClaimReward(AchievementDTO achievementDto)
    {
        Achievement achievement = FindByID(achievementDto.ID);
        if (achievement == null)
        {
            return false;
        }

        if (achievement.TryClaimReward())
        {
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);
            OnDataChanged?.Invoke();
            return true;
        }

        return false;
    }
}
