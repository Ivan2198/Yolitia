using UnityEngine;
using TMPro;
using System.Collections;

public class OTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private string currentText = "No";
    private int maxOCount = 50;
    private Coroutine addOCoroutine;
    [SerializeField] private AudioClip oSoundClip;

    private void Update()
    {
        // No need to call AddO in Update
    }

    // Method to start or stop adding 'o'
    public void ToggleAdding()
    {
        if (addOCoroutine == null)
        {
            addOCoroutine = StartCoroutine(AddOCoroutine());
        }
        else
        {
            StopCoroutine(addOCoroutine);
            addOCoroutine = null;
        }
    }

    // Coroutine to add 'o' every half second
    private IEnumerator AddOCoroutine()
    {
        while (currentText.Length < maxOCount + 2) // +2 to account for initial "No"
        {
            currentText += "o";
            textMeshProUGUI.text = currentText;
            SoundManager.Instance.PlaySound(oSoundClip, 0.5f);
            yield return new WaitForSeconds(1.0f);
        }

        // Stop coroutine when max count is reached
        addOCoroutine = null;
    }

    // Method to reset the text
    public void ResetText()
    {
        currentText = "No";
        textMeshProUGUI.text = currentText;

        if (addOCoroutine != null)
        {
            StopCoroutine(addOCoroutine);
            addOCoroutine = null;
        }
    }
}