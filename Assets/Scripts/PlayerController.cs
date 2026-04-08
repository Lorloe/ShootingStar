using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float leftBoundPadding = 1f;
    [SerializeField] private float rightBoundPadding = 1f;
    [SerializeField] private float topBoundPadding = 2f;
    [SerializeField] private float bottomBoundPadding = 2.5f;

    private Shooter playerShooter;
    private InputAction fireAction;
    private InputAction moveAction;

    private Vector2 moveVector;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    void Start()
    {
        playerShooter = GetComponent<Shooter>();
        if (playerShooter == null)
            Debug.LogError("Shooter component not found on " + gameObject.name);

        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Attack");

        InitBounds();
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        MovePlayer();
        FireShooter();
    }

    private void MovePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        Vector3 moveVectorDirection = new Vector3(moveVector.x, moveVector.y, 0);

        Vector3 newPosition = transform.position + moveVectorDirection * moveSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + leftBoundPadding, maxBounds.x - rightBoundPadding);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + bottomBoundPadding, maxBounds.y - topBoundPadding);

        transform.position = newPosition;
    }

    private void FireShooter()
    {
        playerShooter.isFiring = fireAction.IsPressed();
    }
}
