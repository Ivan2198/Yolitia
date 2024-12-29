using UnityEngine.SceneManagement;
using UnityEngine;

public class Logo : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    
    private void Update()
    {

        

        if (!music.isPlaying)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
