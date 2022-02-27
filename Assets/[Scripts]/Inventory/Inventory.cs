using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryType
{
    DEFAULT,
    PLAYER,
    WORLD,
    TEMP_STORAGE
}

public class Inventory : MonoBehaviour
{
    public InventoryType inventoryType;
    [SerializeField] int inventorySpaces;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
