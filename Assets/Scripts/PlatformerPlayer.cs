using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerPlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.5f;

    [SerializeField]
    private float jumpForce = 12.0f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody2D body;

    private Vector2 direction;
    private Animator anim;

    private BoxCollider2D box;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();

        moveAction = playerInput.actions.FindAction("Movement", true);
        jumpAction = playerInput.actions.FindAction("Jump", true);
    }

    private bool jumpRequested;

    private void Update()
    {
        direction = moveAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame())
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        body.linearVelocity = new Vector2(
            direction.x * speed,
            body.linearVelocity.y
        );

        anim.SetFloat("speed", Mathf.Abs(direction.x));

        if (!Mathf.Approximately(direction.x, 0))
        {
            transform.localScale = new Vector3(
                Mathf.Sign(direction.x),
                1,
                1
            );
        }

        Vector3 max = box.bounds.max;
        Vector3 min  = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .1f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        bool grounded = false;

        if (hit != null)
        {
            grounded = true;
        }
        body.gravityScale = (grounded && Mathf.Approximately(direction.x,0f)) ? 0 : 1;
        if (grounded&&jumpRequested)
        {
            body.AddForce(
                Vector2.up * jumpForce,
                ForceMode2D.Impulse
            );

            jumpRequested = false;
        }

        MovingPlatform platform = null;
        if (hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>();
        }
        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }
        anim.SetFloat("speed", Mathf.Abs(direction.x));

        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }
        if (!Mathf.Approximately(direction.x,0))
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x) / pScale.x, 1 / pScale.y, 1);
        }


    }
}