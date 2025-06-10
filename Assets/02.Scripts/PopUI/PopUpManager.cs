using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;

    [SerializeField] private GameObject achievementPopup;

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
    }
    public void ShowAchievementPopup()
    {
        achievementPopup.SetActive(true);
    }

    public void CloseAchievementPopup()
    {
        achievementPopup.SetActive(false);
    }

}

