using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : MonoBehaviour
{
    [SerializeField] private string finalSceneName;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(finalSceneName);
    }
}
