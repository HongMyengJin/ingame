using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    public float Duration = 2.0f;

    private void OnEnable()
    {
        StartCoroutine(ShowPopup());
    }
    IEnumerator ShowPopup()
    {
        yield return new WaitForSeconds(Duration);
        gameObject.SetActive(false);
    }

}
