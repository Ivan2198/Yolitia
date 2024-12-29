using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; //Referencia al audioMixer
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    [SerializeField] private AudioClip _startClip, _hoverClip, _pressButtonClip, _returnClip;
    
    

    Resolution[] resolutions; //Arreglo que almacena el valor de las resoluciones disponibles de la pantalla utilizada.
    //public Dropdown resolutionDropDrown;
    public TMPro.TMP_Dropdown resolutionDropdown; //Referencia al elemento de UI("Dropdown") utilizado
   

    [SerializeField] private GameObject portada;
    [SerializeField] private GameObject menu;
    [SerializeField] private string creditsSceneName;

    private void Start()
    {
       resolutions = Screen.resolutions; // Al iniciar almacena las resoluciones en la variable.

        resolutionDropdown.ClearOptions(); //Limpiamos todas las opciones en DropDown de las resoluciones

        List<string> options = new List<string>(); // Creamos una lista de cadenas que almacena la informacion de la resolucion.
        //Nota: la diferencia entre un array y un list es que el list no tiene un tamaño predefinido.

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++) //Ciclo que pasa por todas las resoluciones almacenadas en el arreglo
        {
            //string option = "width" + "x" + "heigth";
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option); //Agrega los valores a la lista

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }
        resolutionDropdown.AddOptions(options); //Cambia los valores del elemnto de UI("Dropdown") por los agregados a la lista.
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
            //ReturnPlay();
        }
    }
    public void SetResolution(int resolutionIndex) //Metodo que establece resolucion
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume) //Metodo que establece volumen
    {
        Debug.Log(volume);
        musicMixer.SetFloat("volume", volume); //Sets the value of the exposed parameter specified. When a parameter is exposed, it is not controlled by mixer snapshots and can therefore only be changed via this function. 
        sfxMixer.SetFloat("volume", volume);
    }
    public void SetMusic(float volume) //Metodo que establece volumen
    {
        Debug.Log(volume);
        musicMixer.SetFloat("volume", volume); //Sets the value of the exposed parameter specified. When a parameter is exposed, it is not controlled by mixer snapshots and can therefore only be changed via this function. 
    }
    public void SetSFX(float volume) //Metodo que establece volumen
    {
        Debug.Log(volume);
        sfxMixer.SetFloat("volume", volume); //Sets the value of the exposed parameter specified. When a parameter is exposed, it is not controlled by mixer snapshots and can therefore only be changed via this function. 
    }

    public void SetQuality(int qualityIndex) //Metodo que establece calidad
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    public void SetFullscreen (bool isFullscreen) //Metodo que establece el tamaño de pantalla
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void StartGameSound()
    {
        SoundManager.Instance.PlaySound(_startClip, 0.8f);
    }

   
    public void HoverButtonPlaySound()
    {
        SoundManager.Instance.PlaySound(_hoverClip, 0.8f);
    }
    public void PressButtonPlaySound()
    {
        SoundManager.Instance.PlaySound(_pressButtonClip, 0.8f);
    }
   
    public void ReturnPlay()
    {
        SoundManager.Instance.PlaySound(_returnClip, 0.8f);
    }
    // Function to set the distortion level
    public void SetDistortionLevel(float level)
    {
        musicMixer.SetFloat("DistortionLevel", level);
        musicMixer.SetFloat("volume", 0.5f);
    }

    public void GoToCreditsScene()
    {
        SceneManager.LoadScene(creditsSceneName);
    }
}
