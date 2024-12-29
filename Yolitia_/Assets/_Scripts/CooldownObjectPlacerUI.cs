using UnityEngine;
using UnityEngine.UI;

public class CooldownObjectPlacerUI : MonoBehaviour
{
    [SerializeField] private Image cooldownTimerUI;
    [SerializeField] private PlatformPlacer platformPlacer;

    private void Update()
    {
        cooldownTimerUI.fillAmount = platformPlacer.GetCooldownTimer();
    }
}
