using System.Collections;
using UnityEngine;
using TMPro;
using Cinemachine; // Ensure you include the Cinemachine namespace

public class CountdownTimer : MonoBehaviour
{
    private float timeRemaining = 0f;
    [SerializeField] float totalTime = 30f; // Set a default total time (e.g., 30 seconds)
    public TextMeshProUGUI countdownText;         // Reference to a UI Text object (if using UI)
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera
    private CinemachineBasicMultiChannelPerlin noise; // Reference to the noise component

    private bool timerIsRunning = false;

    void Start()
    {
        // Start the timer when the game starts
        timerIsRunning = true;
        timeRemaining = totalTime;

        // Initialize canvasGroup to fully transparent
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f; // Start with opacity 0
        }

        // Get the noise component from the virtual camera
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Reduce the time
                timeRemaining -= Time.deltaTime;

                // Update the UI or log the remaining time
                DisplayTime(timeRemaining);

                // Update the opacity of the CanvasGroup based on remaining time
                UpdateOpacity(timeRemaining);

                // Update AmplitudeGain based on remaining time
                UpdateAmplitudeGain(timeRemaining);
            }
            else
            {
                // Time has run out
                Debug.Log("Time's up!");
                timeRemaining = 0;
                timerIsRunning = false;

                // Optionally set opacity to 1 when time is up
                UpdateOpacity(timeRemaining); // Ensure opacity is updated to 1
                UpdateAmplitudeGain(timeRemaining); // Ensure AmplitudeGain is set to 1
            }
        }
    }

    // This function formats the time and displays it
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(0, timeToDisplay); // Ensure we don't go below 0
        timeToDisplay += 1; // Offset so that it rounds properly

        // Calculate minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Format time as mm:ss
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Display in the console (or use a UI Text element)
        Debug.Log(timeText);

        if (countdownText != null)  // If you're using a UI text element
        {
            countdownText.text = timeText;
        }
    }

    // Update the opacity of the CanvasGroup based on remaining time
    void UpdateOpacity(float timeRemaining)
    {
        // Calculate opacity as a proportion of time remaining (0 when full, 1 when time is up)
        float opacity = Mathf.Clamp01(1 - (timeRemaining / totalTime));
        canvasGroup.alpha = opacity;
    }

    // Update AmplitudeGain of the Cinemachine noise based on remaining time
    void UpdateAmplitudeGain(float timeRemaining)
    {
        if (noise != null)
        {
            // Calculate AmplitudeGain as a proportion of time remaining (0 when full, 1 when time is up)
            float amplitudeGain = Mathf.Clamp01(1 - (timeRemaining / totalTime));
            noise.m_AmplitudeGain = amplitudeGain;
        }
    }
}