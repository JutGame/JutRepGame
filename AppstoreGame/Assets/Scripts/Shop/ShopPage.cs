using UnityEngine;

public class ShopPage : MonoBehaviour
{
    [SerializeField] private SkinsController _skinsController;
    [SerializeField] private ShopItem[] _items;
    [SerializeField] private CoinsController _coinsController;
    private SkinType _selectedType;

    private void OnEnable()
    {
        _selectedType = _skinsController.SelectedSkin;
        for(int i = 0; i < _items.Length; i++)
        {
            if (_items[i].SkinType == _selectedType)
            {
                _items[i].ChangeState(ShopItemState.Selected);
            }
            else
            {
                _items[i].ChangeState((ShopItemState)PlayerPrefs.GetInt("ShopItem" + i, 0));
            }
            
        }
    }

    public void ChangeItemState(int index)
    {
        if (_items[index].State == ShopItemState.Nonbued)
        {
            if (_coinsController.TryRemoveValue(_items[index].Price))
            {
                _items[index].ChangeState(ShopItemState.Buyed);
                PlayerPrefs.SetInt("ShopItem" + index, 1);
            }
        }
        else if (_items[index].State == ShopItemState.Buyed)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].SkinType == _selectedType)
                {
                    _items[i].ChangeState(ShopItemState.Buyed);
                    break;
                }
            }
            _selectedType = _items[index].SkinType;
            _items[index].ChangeState(ShopItemState.Selected);
            _skinsController.SetSelectedSkin(_items[index].SkinType);
        }
            
    }
}

public enum ShopItemState
{
    None = -1,
    Nonbued = 0,
    Buyed = 1,
    Selected = 2,
}
