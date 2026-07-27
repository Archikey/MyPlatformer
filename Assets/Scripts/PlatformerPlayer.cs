using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerPlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.5f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private Rigidbody2D body;

    private Vector2 direction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        body = GetComponent<Rigidbody2D>();

        moveAction = playerInput.actions.FindAction("Movement", true);
    }

    private void Update()
    {
        direction = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        body.linearVelocity = new Vector2(
            direction.x * speed,
            body.linearVelocity.y
        );
    }
}