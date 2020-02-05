using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void Update()
    {
        if (selected == false)
        {
            currentSelection = null;
            capsule.gameObject.SetActive(false);
        }

        if (currentSelection != null)
        {
            selected = false;
        }
    }

    void PickUp()
    {
        //Debug.Log("Picking up " + item.name);
        if (Inventory.instance.Add(item))
        {
            Destroy(gameObject);
        }
    }

    public override void SetHighlighted(Interactable selection)
    {
        selected = true;
        currentSelection = selection;
        capsule.gameObject.SetActive(true);
    }
}
