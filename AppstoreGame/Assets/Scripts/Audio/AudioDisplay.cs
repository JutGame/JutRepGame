using UnityEngine;

public abstract class AudioDisplay : MonoBehaviour
{
    protected AudioController AudioController;

    public virtual void Initialize(AudioController audioController)
    {
        AudioController = audioController;
    }
}
