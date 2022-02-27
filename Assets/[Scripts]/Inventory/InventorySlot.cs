using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item itemInSlot = null;
    InventoryManager inventoryManager;
    PickupCategory slotPickupCategory;
    InventoryType parentInventoryType;
    Inventory currentInventory;

    Inventory ConsoleDefaultInventoryReference;
    Inventory ConsolePlayerInventoryReference;
    Inventory ConsoleWorldInventoryReference;
    Inventory TempPlayerInventoryReference;
    
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        currentInventory = transform.parent.GetComponent<Inventory>();
        parentInventoryType = currentInventory.inventoryType;

        ConsoleDefaultInventoryReference = inventoryManager.ConsoleDefaultInventory;
        ConsolePlayerInventoryReference = inventoryManager.ConsolePlayerInventory;
        ConsoleWorldInventoryReference = inventoryManager.ConsoleWorldInventory;
        TempPlayerInventoryReference = inventoryManager.TempPlayerInventory;
    }

    void MoveSlotItem()
    {
        if (itemInSlot == null) return;


        if(parentInventoryType != InventoryType.TEMP_PLAYER && Input.GetKey(KeyCode.LeftShift))
        {
            MoveToTempPlayer();
        }


        switch (parentInventoryType)
        {
            case InventoryType.CONSOLE_DEFAULT:
                if (slotPickupCategory == PickupCategory.PLAYER)
                    MoveToConsolePlayer();
                if (slotPickupCategory == PickupCategory.WORLD)
                    MoveToConsoleWorld();
                break;
            case InventoryType.CONSOLE_PLAYER:
                MoveToConsoleDefault();
                break;
            case InventoryType.CONSOLE_WORLD:
                MoveToConsoleDefault();
                break;
            case InventoryType.TEMP_PLAYER:
                MoveToConsoleDefault();
                break;
        }

        
    }

    void MoveToConsoleDefault()
    {
        if (ConsoleDefaultInventoryReference.isFull)
        {
            //do the win state thing/unlock door thing :)
            return;
        }
        InventorySlot openSlot = ConsoleDefaultInventoryReference.GetNextOpenSlot();
        openSlot.itemInSlot = itemInSlot;
        itemInSlot = null;
    }

    void MoveToConsolePlayer()
    {
        if (ConsolePlayerInventoryReference.isFull) return;

        InventorySlot openSlot = ConsolePlayerInventoryReference.GetNextOpenSlot();
        openSlot.itemInSlot = itemInSlot;
        itemInSlot = null;
    }
    
    void MoveToConsoleWorld()
    {
        if (ConsoleWorldInventoryReference.isFull) return;

        InventorySlot openSlot = ConsoleWorldInventoryReference.GetNextOpenSlot();
        openSlot.itemInSlot = itemInSlot;
        itemInSlot = null;
    }
    
    void MoveToTempPlayer()
    {
        if (TempPlayerInventoryReference.isFull) return;

        InventorySlot openSlot = TempPlayerInventoryReference.GetNextOpenSlot();
        openSlot.itemInSlot = itemInSlot;
        itemInSlot = null;
    }

}
