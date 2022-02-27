using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupCategory
{
    DEFAULT,
    WORLD,
    PLAYER
}

public enum PickupEffect
{
    NONE,
    SPEED,
    HIGH_JUMP,
    STORAGE,
    PICKUP_RADUIS
}

public class ItemPickup : MonoBehaviour
{
    public PickupCategory pickupType;
    public PickupEffect pickupEffect;

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
    }
    
}
