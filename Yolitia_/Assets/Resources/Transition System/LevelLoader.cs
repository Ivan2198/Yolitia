using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition; // Animator a utilizar
    public static LevelLoader Instance;

    public float transitionTime = 1f; //Variable del tiempo de la transicion

    private void Awake()
    {
        //if (Instance == null) //If there isnt an instance lets make ->
        //{
        //    Instance = this; //-> this new instance that has just been created ->
        //    DontDestroyOnLoad(gameObject); //-> and will tell unity to never destroy it ->
        //} //So even when we change scenes the intance will always remain active
        //else // if theres a instance we  will destroy it
        //{
        //    Destroy(gameObject);
        //}
    }
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //Trigger
        {
            LoadNextLevel(); 
        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));  //Argumento que se le pasa a la funcion
    }

    IEnumerator LoadLevel(int levelIndex) 
    {
        //Play Animation
        transition.SetTrigger("Start");
        //Wait
        yield return new WaitForSeconds(transitionTime);
        //Load scene
        SceneManager.LoadScene(levelIndex);
    }
    public void SetTransition()
    {
        transition.SetTrigger("Start");
    }
}
