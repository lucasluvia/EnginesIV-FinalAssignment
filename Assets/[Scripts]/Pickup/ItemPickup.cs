using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item itemType;

    private Renderer renderer;
    private Collider collider;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 60 * Time.deltaTime, 0), Space.World);
    }

    public void RemovePickupFromWorld()
    {
        renderer.enabled = false;
        collider.enabled = false;
        itemType.CollectItem();
    }

}
