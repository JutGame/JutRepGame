using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private SkinType _skinType;
    [SerializeField] private int _price;
    [SerializeField] private TextMeshProUGUI _priceDisplay;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Animator _animator;
    private ShopItemState _currentState = ShopItemState.None;
    public SkinType SkinType => _skinType;
    public int Price => _price;
    public ShopItemState State => _currentState;

    public void ChangeState(ShopItemState state)
    {
        _currentState = state;
        if(_currentState == ShopItemState.Nonbued)
        {
            _priceDisplay.gameObject.SetActive(true);
            _priceDisplay.text = _price.ToString();
            _buttonText.text = "BUY";
            _animator.Play("Idle");
        }
        else if(_currentState == ShopItemState.Buyed)
        {
            _priceDisplay.gameObject.SetActive(false);
            _buttonText.text = "SELECT";
            _animator.Play("Idle");
        }
        else if(_currentState == ShopItemState.Selected)
        {
            _priceDisplay.gameObject.SetActive(false);
            _buttonText.text = "SELECTED";
            _animator.Play("Selected");
        }
    }


}
