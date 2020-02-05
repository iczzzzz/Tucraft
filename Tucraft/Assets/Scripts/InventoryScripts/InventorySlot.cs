using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;
    public Item item;

    Image slotImage;
    Color defaultColor, selectedColor;

    private void Start()
    {
        slotImage = GetComponent<Image>();
        defaultColor = slotImage.color;
        selectedColor = new Color(defaultColor.r-0.2f, defaultColor.g-0.2f, defaultColor.b-0.2f);
    }


    public void AddItem(Item newItem) {
        item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
	}

    public void ClearSlot() {
        item = null;
		icon.sprite = null;
		icon.enabled = false;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            GetComponent<Image>().color = selectedColor;
        }
        else
        {
            GetComponent<Image>().color = defaultColor;
        }
    }
}
