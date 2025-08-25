using UnityEngine;

public class SkinsController : MonoBehaviour
{
    private string _key = "SkinsKey";
    public SkinType SelectedSkin => (SkinType)PlayerPrefs.GetInt(_key, 0);


    private void Awake()
    {
        PlayerPrefs.SetInt("ShopItem0", 1);    
    }

    public void SetSelectedSkin(SkinType selectedSkin)
    {
        PlayerPrefs.SetInt(_key, (int)selectedSkin);
    }
}

public enum SkinType
{
    Default,
    Forest,
    Hat,
    Headphones
}