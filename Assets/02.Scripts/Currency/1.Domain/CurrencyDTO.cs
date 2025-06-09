using UnityEngine;

// 계층 간 데이터 전송을 위해 도메인 모델 대신 사용되는 
public class CurrencyDTO
{
    public readonly ECurrencyType Type;
    public readonly int Value;

    // Currency 도메인은 CurrencyManager에서만 관리
    // 중간에 전송하는 
    public CurrencyDTO(Currency currency)
    {
        Type = currency.Type;
        Value = currency.Value;
    }

    public CurrencyDTO(ECurrencyType type, int value)
    {
        Type = type;
        Value = value;
    }

    public bool HaveEnough(int value)
    {
        return Value >= value;
    }
}
