using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// 아키텍쳐: 설계 그 잡채(설계마다 철학이 있다.)
// 디자인 패턴: 설계를 구현하는 과정에서 쓰이는 패턴

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;

    // 도메인에 변화가 있을 때 호출되는 액션
    public event Action OnDataChanged;

    private CurrencyRepository _repository;

    // 마틴 아저씨: 미리하는 성능 최적화는 없다..?
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
        _repository = new CurrencyRepository();
        List<CurrencyDTO> loadedCurrencies = _repository.Load();

        // 생성
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);
        if (loadedCurrencies == null)
        {
            for (int i = 0; i < (int)ECurrencyType.Count; ++i)
            {
                ECurrencyType type = (ECurrencyType)i;

                // 골드, 다이아몬드 등을 0 값으로 생성
                Currency currency = new Currency(type, 0);
                _currencies.Add(type, currency);
            }
        }
        else
        {
            foreach (CurrencyDTO data in loadedCurrencies)
            {
                Currency currency = new Currency(data.Type, data.Value);
                _currencies.Add(currency.Type, currency);
            }
        }
    }

    private List<CurrencyDTO> ToDtoList()
    {
        return _currencies.ToList().ConvertAll(currency => new CurrencyDTO(currency.Value));
    }

    // 도메인 규칙은 항상 도메인 쪽에 있어야한다.
    // 유효성 검사는 항상 도메인 쪽!
    // 관리 -> 매니저, 규칙 -> 도메인

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }
    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);

        Debug.Log($"{type}: {_currencies[type].Value}");

        _repository.Save(ToDtoList());

        AchievementManager.Instance.Increase(EAchievementCondition.GoldCollect, value);

        OnDataChanged?.Invoke();
    }

    public bool TryBuy(ECurrencyType type, int value)
    {
        if (!_currencies[type].TryBuy(value))
        {
            return false;
        }

        _repository.Save(ToDtoList());

        OnDataChanged?.Invoke();
        return true;
    }
}
