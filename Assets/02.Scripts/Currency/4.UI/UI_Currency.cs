using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using Unity.FPS.UI;
using UnityEngine;
using UnityEngine.UI;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI BuyHealthText;
    private void Start()
    {
        Refresh();
        CurrencyManager.Instance.OnDataChanged += Refresh;
    }

    // 지울 코드..
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            BuyHealth();
        }
    }

    private void Refresh()
    {
        var gold = CurrencyManager.Instance.Get(ECurrencyType.Gold);
        var diamond = CurrencyManager.Instance.Get(ECurrencyType.Diamond);

        GoldCountText.text = $"Gold: {gold.Value}";
        DiamondCountText.text = $"Diamond: {diamond.Value}";


        // --묻지 말고 시켜라~~~--
        // 돈있니?
        // 할인중이니?
        // 쿠폰있니?
        // 살래

        BuyHealthText.color = gold.HaveEnough(300) ? Color.green : Color.red;
    }

    public void BuyHealth()
    {

        if (CurrencyManager.Instance.TryBuy(ECurrencyType.Gold, 300))
        {
            var player = GameObject.FindFirstObjectByType<PlayerCharacterController>();
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(100);
        }
    }
}
