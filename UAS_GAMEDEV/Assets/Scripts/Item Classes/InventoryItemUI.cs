using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image iconImage;
    public Text countText;

    public void SetItem(InventoryItem inventoryItem)
    {
        // Update the icon image based on the inventory item
        iconImage.sprite = inventoryItem._icon;
        // Additional code to handle other properties of the inventory item
    }

    public void SetCount(int count)
    {
        // Update the count text or other relevant information
        countText.text = count.ToString();
    }

    // Other methods and event handlers for the inventory slot UI
}
