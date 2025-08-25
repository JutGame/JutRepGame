using UnityEngine;

public abstract class AudioController : MonoBehaviour
{
    [SerializeField] private string _accessKey;
    protected AudioSource AudioSource;
    public float Volume => AudioSource.volume;

    protected virtual void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.volume = PlayerPrefs.GetFloat(_accessKey, 0f);
    }

    public void ChangeVolume(float volume)
    {
        AudioSource.volume = volume;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat(_accessKey, AudioSource.volume);
        PlayerPrefs.Save();
    }
}