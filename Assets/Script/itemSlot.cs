using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class itemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data
    public string itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;

    // Item Slot
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int MaxStackSize = 99;

    //slot management
    [SerializeField] public GameObject SelectedPanel;
    public bool isItemSelected;
    public inventoryManager InventoryManager;
    public ItemDetails itemDetails;

    public void Awake()
    {
        InventoryManager = FindAnyObjectByType<inventoryManager>();
    }


    private void Start()
    {
        //  if (!isFull)
        if (itemQuantity == 0)
        {
            quantityText.enabled = false;
            itemImage.enabled = false;
        }
    }

    public void AddItem(ItemDetails item)
    {
        itemName = item.ItemName;
        // itemQuantity = item.Quantity;
        itemIcon = item.ItemIcon;
        isFull = true;
        AddQuantity(item.Quantity);
        itemDetails = item;

        // quantityText.text = itemQuantity.ToString();
        // quantityText.enabled = true;
        // itemImage.sprite = itemIcon;
        itemImage.enabled = true;
        itemImage.sprite = itemIcon;
        // itemImage.color = Color.yellow;
        itemImage.color = Color.white;
    }

    public bool IsEmpty => itemQuantity <= 0;

    public bool CanStackWith(string otherItemName)
    {
        // return !isFull && itemName == otherItemName;
        return itemQuantity > 0 && !isFull && itemName == otherItemName;
    }
    public void AddQuantity(int amount)
    {
        itemQuantity += amount;
        isFull = itemQuantity >= MaxStackSize;

        quantityText.text = itemQuantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemIcon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }
    }

    public void LeftClick()
    {
        if (InventoryManager == null)
        {
            Debug.LogWarning("InventoryManager tidak ditemukan untuk slot ini.", this);
            return;
        }

        if (IsEmpty)
        {
            return;
        }

        InventoryManager.SelectSlot(this);

        
    }

    public ItemDetails GetItemDetails()
    {
        return itemDetails;
    }

    public void SetSelected(bool selected)
    {
        isItemSelected = selected;

        if (SelectedPanel != null)
        {
            SelectedPanel.SetActive(selected);
        }
    }

    public void RightClick()
    {
        Debug.Log("kanan di klik");
    }
}
