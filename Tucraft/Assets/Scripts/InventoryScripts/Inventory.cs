using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    
	#region Singleton
	
	public static Inventory instance;
	
    void Awake() {
		if (instance != null) {
			Debug.LogWarning("More than one instance of Inventory found!");
			return;
		}
		
		instance = this;
	}

    #endregion


    const int SPACE = 10;

    public Item[] items;
    public Transform slotsParent;


	int count;
    int selectedSlot;
    InventorySlot[] slots;


    void Start() {
        count = selectedSlot = 0;
        items = new Item[SPACE];
        slots = new InventorySlot[SPACE];
        for (int i=0; i< SPACE; i++) {
            items[i] = null;
		}
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
        SelectSlot();
    }

    private void Update()
    {
        int previousSelectedSlot = selectedSlot;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            selectedSlot = selectedSlot < SPACE-1 ? selectedSlot+1 : 0;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedSlot = selectedSlot > 0 ? selectedSlot-1 : SPACE-1;
        }

        if (Input.GetButtonDown("Slot1"))
        {
            selectedSlot = 0;
        }
        if (Input.GetButtonDown("Slot2"))
        {
            selectedSlot = 1;
        }
        if (Input.GetButtonDown("Slot3"))
        {
            selectedSlot = 2;
        }
        if (Input.GetButtonDown("Slot4"))
        {
            selectedSlot = 3;
        }
        if (Input.GetButtonDown("Slot5"))
        {
            selectedSlot = 4;
        }

        if (previousSelectedSlot != selectedSlot)
        {
            SelectSlot();
        }
    }

    public bool Add(Item item)
    {
        if (count >= SPACE) {
			return false;
        }

        bool stop = false;
		int i = 0;
		while (i < SPACE && !stop) {
			if (items[i] == null)
            {
                items[i] = item;
                count++;
				stop = true;
			}
			else {
				i++;
			}
        }
        slots[i].AddItem(items[i]);

        return true;
	}

    public void UseSelected() {

        if (items[selectedSlot] == null) { return; }

        items[selectedSlot] = null;
		count--;

        slots[selectedSlot].UseItem();
        slots[selectedSlot].ClearSlot();
    }


    void SelectSlot()
    {
        for (int i=0; i<SPACE; i++) { 
            if (i == selectedSlot)
            {
                slots[i].SetSelected(true);
            }
            else
            {
                slots[i].SetSelected(false);
            }
        }
    }
}
