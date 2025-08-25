using UnityEngine;

public class AudioDisplaysBinder : MonoBehaviour
{
    [SerializeField] private AudioDisplay _sfxDisplay;
    
    private void Start()
    {
        _sfxDisplay.Initialize(SFXController.Instance);
    }
}
