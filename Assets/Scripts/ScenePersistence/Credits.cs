using UnityEngine;
using UnityEngine.UIElements;

public class Credits : MonoBehaviour
{
    public GameObject panel;

    public void OnClickCredits()
    {
        panel.SetActive(true);
    }
    public void OnClickExitCredits()
    {
        panel.SetActive(false);
    }
}
