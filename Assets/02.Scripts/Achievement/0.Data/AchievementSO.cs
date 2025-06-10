using UnityEngine;

// 런타임시 변하지 않는값을 SO로 관리하면:
// - 기획자가 에디터에서 직접 수정이 가능하다.
// - 유지보수와 확장성이 증가한다.
// - 도메인 객체(Achievement)는 상태(CurrentValue, isClaimed)만 관리하면 된다.

[CreateAssetMenu(fileName = "AchievementSO", menuName = "Scriptable Objects/AchievementSO")]
public class AchievementSO : ScriptableObject
{
    [Header("기본 정보")]
    [SerializeField, Tooltip("업적 고유 ID")]
    private string _id;
    public string ID => _id;

    [SerializeField, Tooltip("업적 이름")]
    private string _name;
    public string Name => _name;

    [SerializeField, TextArea, Tooltip("업적 설명")]
    private string _description;
    public string Description => _description;

    [Space]
    [Header("조건")]
    [SerializeField, Tooltip("업적 달성 조건 타입")]
    private EAchievementCondition _condition;
    public EAchievementCondition Condition => _condition;

    [SerializeField, Tooltip("조건 달성에 필요한 수치")]
    private int _goalValue;
    public int GoalValue => _goalValue;

    [Space]
    [Header("보상")]
    [SerializeField, Tooltip("보상 화폐 타입")]
    private ECurrencyType _rewardCurrencyType;
    public ECurrencyType RewardCurrencyType => _rewardCurrencyType;

    [SerializeField, Tooltip("보상 수치")]
    private int _rewardAmount;
    public int RewardAmount => _rewardAmount;
}
