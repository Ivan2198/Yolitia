using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    public UnityEvent onPlayerDeath;  // Event for player death
    public Animator playerAnimator;   // Reference to the player's Animator
   
    public GameObject inputHandler;   // Reference to the Input System or Movement Script
    [SerializeField] private AudioClip deathAudioClip;

    private bool isDead = false;

    void Start()
    {
        if (onPlayerDeath == null)
            onPlayerDeath = new UnityEvent();
    }

    // Call this function when the player dies
    public void Die()
    {
        if (isDead) return; // Avoid multiple death triggers
        isDead = true;

        // 1. Deactivate the input system
        if (inputHandler != null)
        inputHandler.SetActive(false);  // Disable input handling or movement scripts

        // 2. Execute death animation
        playerAnimator.CrossFade("Death", 0f);

        // 3. Play death sound
        if (deathAudioClip != null ) 
        SoundManager.Instance.PlaySound(deathAudioClip, 0.5f);

        // Invoke any additional events tied to player death
        onPlayerDeath.Invoke();

        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}