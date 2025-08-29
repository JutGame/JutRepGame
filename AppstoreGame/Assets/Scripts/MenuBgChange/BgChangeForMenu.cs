using UnityEngine;
using UnityEngine.UI;


public class BgChangeForMenu : MonoBehaviour
{
    [Header("Button bg")]
    [SerializeField] private Button bg1Btn;
    [SerializeField] private Button bg2Btn;
    [SerializeField] private Button bg3Btn;

    [Header("Bg img")]
    [SerializeField] private Sprite[] spBg;

    [Header("Main img")]
    [SerializeField] private Image mainBgImg;

    [Header("Back btn")]
    [SerializeField] private Button back;

    [Header("StartBg")]
    [SerializeField] private Button StartBg;

    [Header("Canvas bg change")]
    [SerializeField] private Canvas BgChangeCanvas;



    private int bgID = 0;

    void Start()
    {

        StartBg.onClick.AddListener(() => BgChangeCanvas.gameObject.SetActive(true));
        back.onClick.AddListener(() => BgChangeCanvas.gameObject.SetActive(false));

        bgID = PlayerPrefs.GetInt("bgID", 0);

        mainBgImg.sprite = spBg[bgID];

    }

    public void ClickForBtnBg(int id)
    {

        PlayerPrefs.SetInt("bgID", id);
        PlayerPrefs.Save();
        bgID = id;

        mainBgImg.sprite = spBg[bgID];


    }


}
 