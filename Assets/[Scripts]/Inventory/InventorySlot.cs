using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Item itemInSlot = null;
    InventoryManager inventoryManager;
    PickupCategory slotPickupCategory;
    InventoryType parentInventoryType;
    Inventory currentInventory;
    MovementComponent movementComponent;

    Inventory ConsoleDefaultInventoryReference;
    Inventory ConsolePlayerInventoryReference;
    Inventory ConsoleWorldInventoryReference;
    Inventory TempPlayerInventoryReference;

    private TextMeshProUGUI slotText;
    
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        movementComponent = GameObject.Find("Jackie").GetComponent<MovementComponent>();
        currentInventory = transform.parent.GetComponent<Inventory>();
        parentInventoryType = currentInventory.inventoryType;

        ConsoleDefaultInventoryReference = inventoryManager.ConsoleDefaultInventory;
        ConsolePlayerInventoryReference = inventoryManager.ConsolePlayerInventory;
        ConsoleWorldInventoryReference = inventoryManager.ConsoleWorldInventory;
        TempPlayerInventoryReference = inventoryManager.TempPlayerInventory;


        slotText = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (slotText)
            UpdateSlotText();
    }

    public void MoveSlotItem()
    {
        if (itemInSlot == null) return;

        slotPickupCategory = itemInSlot.pickupType;
        //if(parentInventoryType != InventoryType.TEMP_PLAYER && movementComponent.TempPriorityHeld)
        //{
        //    MoveToTempPlayer();
        //}


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
                if (movementComponent.TempPriorityHeld)
                {
                    if (slotPickupCategory == PickupCategory.PLAYER)
                        MoveToConsolePlayer();
                    else if (slotPickupCategory == PickupCategory.WORLD)
                        MoveToConsoleWorld();
                }
                else
                    MoveToConsoleDefault();
                break;
        }

        
    }

    void MoveToConsoleDefault()
    {
        if (ConsoleDefaultInventoryReference.isFull)
        {
            //do the win state thing/unlock door thing :)
            Debug.Log("Didn't move " + itemInSlot.itemName + ". Default is full.");
            return;
        }

        Debug.Log("Moved " + itemInSlot.itemName + " to Console Default");
        
        InventorySlot openSlot = ConsoleDefaultInventoryReference.GetNextOpenSlot();
        openSlot.itemInSlot = itemInSlot;
        itemInSlot = null;

        if (currentInventory.isFull)
            currentInventory.isFull = false;

    }

    void MoveToConsolePlayer()
    {
        if (ConsolePlayerInventoryReference.isFull) return;

        Debug.Log("Moved " + itemInSlot.itemName + " to Console Player");

        InventorySlot openSlot = ConsolePlayerInventoryReference.GetNextOpenSlot();
        openSlot.itemInSlot = itemInSlot;
        itemInSlot = null;

        if (currentInventory.isFull)
            currentInventory.isFull = false;
    }
    
    void MoveToConsoleWorld()
    {
        if (ConsoleWorldInventoryReference.isFull) return;

        Debug.Log("Moved " + itemInSlot.itemName + " to Console World");

        InventorySlot openSlot = ConsoleWorldInventoryReference.GetNextOpenSlot();
        openSlot.itemInSlot = itemInSlot;
        itemInSlot = null;

        if (currentInventory.isFull)
            currentInventory.isFull = false;

    }
    
    void MoveToTempPlayer()
    {
        if (TempPlayerInventoryReference.isFull) return;

        Debug.Log("Moved " + itemInSlot.itemName + " to Temp Player");

        InventorySlot openSlot = TempPlayerInventoryReference.GetNextOpenSlot();
        openSlot.itemInSlot = itemInSlot;
        itemInSlot = null;

        if (currentInventory.isFull)
            currentInventory.isFull = false;
    }

    void UpdateSlotText()
    {
        if (itemInSlot == null)
            slotText.text = "EMPTY";
        else
            slotText.text = itemInSlot.itemName;
    }

}
