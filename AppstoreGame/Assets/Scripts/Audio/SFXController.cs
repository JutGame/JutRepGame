using UnityEngine;

public class SFXController : AudioController
{
    public static SFXController Instance;
    public AudioClip Button, Shot, Hit, Draw;
    protected override void Awake()
    {
        base.Awake();

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        SaveVolume();
    }

    public void Play(SoundType soundType)
    {
        AudioClip clip = null;

        switch(soundType)
        {
            case SoundType.Button:
                clip = Button;
                break;
            case SoundType.Shot:
                clip = Shot;
                break;
            case SoundType.Hit:
                clip = Hit;
                break;
            case SoundType.Draw:
                clip = Draw;
                break;

        }
        AudioSource.PlayOneShot(clip);
    }
}

public enum SoundType
{
    Button,
    Hit,
    Shot,
    Draw,
}
