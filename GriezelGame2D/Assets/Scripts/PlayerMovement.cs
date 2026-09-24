using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private int voidDepth = -30;
    private bool canJump = false;

    [SerializeField] private InputActionAsset action;
    private InputActionMap player;
    private InputAction move;
    private InputAction jump;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        player = action.FindActionMap("Player");
        move = player.FindAction("Move");
        jump = player.FindAction("Jump");

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        player.Enable();
    }
    private void OnDisable()
    {
        player.Disable();
    }
    private void Update()
    {
        HorizontalMovement();
        VerticalMovement();
        RespawnChecker();
    }

    private void HorizontalMovement()
    {
        float horizontalInput = move.ReadValue<Vector2>().x;
        transform.position += new Vector3(horizontalInput * speed * Time.deltaTime, 0f, 0f);
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX= false;
        }
    }
    private void VerticalMovement()
    {
        float verticalMovement = move.ReadValue<Vector2>().y;
        if ((verticalMovement > 0f || jump.WasPressedThisFrame()) && canJump)
        {
            rb?.AddForce(Vector3.up * jumpForce);
            canJump = false;
        }
    }

    private void RespawnChecker()
    {
        if (transform.position.y <= voidDepth)
        {
            transform.position = new Vector2(-6.94f, 0.36f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
