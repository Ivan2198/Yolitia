using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectsSource;
    //[SerializeField] private AudioClip _clip;

    private void Awake()
    {
        if(Instance == null) //If there isnt an instance lets make ->
        {
            Instance = this; //-> this new instance that has just been created ->
            //DontDestroyOnLoad(gameObject); //-> and will tell unity to never destroy it ->
        } //So even when we change scenes the intance will always remain active
        else // if theres a instance we  will destroy it
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float intensidad)
    {
        _effectsSource.PlayOneShot(clip, intensidad); //Reproduce el sonido de la instancia
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }
    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
}
