using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager.Requests;
using Unity.VisualScripting;

public class ItemDetailsPanel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Image ItemImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private TMP_Text itemQtyText;

    public void show(ItemDetails item, int quantity)
    {
        if (item == null)
        {
            Clear();
            return;
        }
        ;

        ItemImage.sprite = item.ItemIcon;
        ItemImage.enabled = true;
        itemNameText.text = item.ItemName;
        itemQtyText.text = "Quantity" + quantity;
        itemDescriptionText.text = item.Description;
    }
    public void Clear()
    {
        ItemImage.sprite = null;
        ItemImage.enabled = false;
        itemNameText.text = "";
        itemQtyText.text = "";
        itemDescriptionText.text = "";
    }
}

