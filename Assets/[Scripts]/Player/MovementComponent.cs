using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5.0f;
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] float jumpForce = 5.0f;

    [SerializeField] Transform turretSpawn;
    [SerializeField] Transform turretParent;
    [SerializeField] GameObject turretPrefab;

    // Components
    private PlayerController playerController;
    Rigidbody rigidbody;
    Animator playerAnimator;
    public GameObject followTarget;

    public bool TempPriorityHeld;

    InventoryManager inventoryManager;
    ConsoleController consoleController;

    // References
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector2 lookInput = Vector3.zero;

    public float aimSensitivity = 0.5f;

    public bool InConsoleRange;
    private bool usingConsole = false;

    private bool inPickupRange;
    private GameObject highlightedPickup;

    // Animator Hashes
    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");
    public readonly int isPlacingHash = Animator.StringToHash("IsPlacing");

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        consoleController = GameObject.Find("Console").GetComponent<ConsoleController>();
        rigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {

        //looking
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.x * aimSensitivity, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.y * aimSensitivity, Vector3.left);

        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTarget.transform.localEulerAngles = angles;

        //rotate player based on look transform
        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        //movement
        if (playerController.isJumping || playerController.isPlacing) return;
        if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDirection;


    }

    public void OnMovement(InputValue value)
    {
<<<<<<< HEAD
        if (playerController.isPickingUp || usingConsole)
=======
        if (playerController.isPlacing)
>>>>>>> parent of 7d33681 (Initial commit)
            return;
        inputVector = value.Get<Vector2>();
        playerAnimator.SetFloat(movementXHash, inputVector.x);
        playerAnimator.SetFloat(movementYHash, inputVector.y);
    }

    public void OnRun(InputValue value)
    {
<<<<<<< HEAD
        if (playerController.isPickingUp || usingConsole)
=======
        if (playerController.isPlacing)
>>>>>>> parent of 7d33681 (Initial commit)
            return;
        playerController.isRunning = value.isPressed;
        playerAnimator.SetBool(isRunningHash, playerController.isRunning);
    }

    public void OnJump(InputValue value)
    {
<<<<<<< HEAD
        if (playerController.isJumping || playerController.isPickingUp || usingConsole)
=======
        if (playerController.isJumping || playerController.isPlacing)
>>>>>>> parent of 7d33681 (Initial commit)
            return;

        playerController.isJumping = value.isPressed;
        rigidbody.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);
    }

    public void OnLook(InputValue value)
    {
<<<<<<< HEAD
        if (playerController.isPickingUp || usingConsole)
=======
        if (playerController.isPlacing)
>>>>>>> parent of 7d33681 (Initial commit)
            return;
        lookInput = value.Get<Vector2>();
    }

    public void OnPlace(InputValue value)
    {
<<<<<<< HEAD
        if (playerController.isPickingUp || !inPickupRange || inventoryManager.TempPlayerInventory.isFull || usingConsole)
            return;

        playerController.isPickingUp = value.isPressed;
        playerAnimator.SetBool(isPickingUpHash, playerController.isPickingUp);
        highlightedPickup.GetComponent<ItemPickup>().RemovePickupFromWorld();
        inPickupRange = false;

        inventoryManager.TempPlayerInventory.isFull = true;

    }

    public void OnTempInventoryPrioritize(InputValue value)
    {
        if (usingConsole)
            TempPriorityHeld = value.isPressed;
    }

    public void OnTriggerConsole(InputValue value)
    {
        if (!InConsoleRange) return;

        consoleController.ToggleConsole();
        usingConsole = !usingConsole;
=======
        if (playerController.isPlacing)
            return;
        playerController.isPlacing = value.isPressed;
        playerAnimator.SetBool(isPlacingHash, playerController.isPlacing);

        Instantiate(turretPrefab, turretSpawn.position, turretSpawn.rotation, turretParent);
>>>>>>> parent of 7d33681 (Initial commit)
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
        playerAnimator.SetBool(isJumpingHash, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            highlightedPickup = other.gameObject;
            inPickupRange = true;
        }
        if (other.gameObject.CompareTag("Console"))
        {
            InConsoleRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            highlightedPickup = null;
            inPickupRange = false;
        }
        if (other.gameObject.CompareTag("Console"))
        {
            InConsoleRange = false;
        }
    }
}
