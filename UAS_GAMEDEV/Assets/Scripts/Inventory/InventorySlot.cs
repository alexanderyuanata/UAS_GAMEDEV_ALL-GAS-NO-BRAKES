using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private PlayerInventory.ItemInsideInventory _item;

    private Image _uiImage;

    private void Awake()
    {

    }

    private void Start()
    {
        _uiImage = GetComponent<Image>();
    }

    public Image UI_Image
    {
        get { return _uiImage; }
    }

    public PlayerInventory.ItemInsideInventory Item
    {
        get { return _item; }
        set { _item = value; }
    }

    public void updateSprite(Sprite _sprite)
    {
        _uiImage.sprite = _sprite;
    }
}
