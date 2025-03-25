using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ICanColorChange {
    [Header("Movement")]
    public float speed = 15f;
    public float horizontalMovement;
    private bool isFacingRight = false;

    [Header("Jump")]
    public float jumpForce = 12f; 

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new(0.5f, 0.05f);
    public LayerMask wallLayer;
    
    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 10f;
    public float fallSpeedMultiplier = 2f;
    
    [Header("Wall Movement")]
    private bool shouldWallJump;
    public float wallSlideSpeed = 2f;
    public bool isWallSliding = false;
    public bool isWallJumping;
    public float wallJumpDirection;
    public float wallJumpTime = 0.5f;
    public float wallJumpTimer;
    public Vector2 wallJumpPower = new(5f, 10f);

    [Header("Grabbing")]
    private bool _isGrabbing = false;
    private Block _grabbedBlock;

    [Header("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private Material material;
    private Animator animator;
    
    [Header("Movement Constraints")]
    public bool grounded;
    public bool walled;
    public Vector2 CurrentVelocity { get; set; }
    public static bool shouldMove = true;
    public static bool shouldInput = true;
    private float harmonySpeedBonus = 0f;
    
    [Header("Color")]
    public ColorName currentColorName;
    public ColorAttr colorAttr { get; set; }

    [Header("Audio Clips")]
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip die;

    [Header("Other")]
    private Color _color;

    // Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        material = GetComponent<Renderer>().material;
        animator = GetComponent<Animator>();
        ChangeColor(ChrColor.FindColorAttr(currentColorName));

        shouldMove = true;
    }

    private void Update() {
        Gravity();
        WallSlide();
        WallJump();
        
        if (!isWallJumping) {
            rb.linearVelocity = new(horizontalMovement * speed, rb.linearVelocity.y);
            // Flip
        }
        
        animator.SetBool("IsWalking", CurrentVelocity.x != 0f);
    }

    private void FixedUpdate() {
        if (_isGrabbing && _grabbedBlock != null) {
            _grabbedBlock.rb.linearVelocity = new(rb.linearVelocity.x, _grabbedBlock.rb.linearVelocity.y);
        }

        CurrentVelocity = rb.linearVelocity;
    }

    public void Move(InputAction.CallbackContext context) {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context) {
        if (IsGrounded()) {
            if (context.performed) {
                rb.linearVelocity = new(rb.linearVelocity.x, jumpForce);
            } else if (context.canceled) {
                rb.linearVelocity = new(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }
        }

        Debug.Log(shouldWallJump);
        if (context.performed && wallJumpTimer > 0f && shouldWallJump) {
            isWallJumping = true;
            rb.linearVelocity = new(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;

            if (transform.localScale.x != wallJumpDirection) {
                // Flip
            }

            Invoke(nameof(CancelWallJump), wallJumpTime * 0.1f);
        }
    }

    public void ToggleChromaticCircle(InputAction.CallbackContext context) {
        if (context.performed) {
            if (GameManager.chromaticCircleUses != 0) {
                ColorInterface ci = GameObject.Find("UI").transform.GetChild(1).GetComponent<ColorInterface>();
                ci.ToggleVisibility();
            }
        }
    }
    public void SoftBlockJump(float force) {
        rb.linearVelocity = new(rb.linearVelocity.x, force);
    }

    private bool IsGrounded() {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer)) {
            return true;
        }
        
        return false;
    }

    private bool IsWalled() {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    private void Gravity() {
        if (rb.linearVelocity.y < 0) {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        } else {
            rb.gravityScale = baseGravity;
        }
    }

    public void WallSlide() {
        if (!IsGrounded() && IsWalled() && horizontalMovement != 0) {
            isWallSliding = true;
            rb.linearVelocity = new(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));
        } else {
            isWallSliding = false;
        }
    }

    private void WallJump() {
        if (isWallSliding) {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;
            
            CancelInvoke(nameof(CancelWallJump));
        }
         else if (wallJumpTimer> 0f) {
            wallJumpTimer -= Time.deltaTime;
         }
    }

    private void CancelWallJump() {
        isWallJumping = false;
    }

    public void ChangeColor(ColorAttr newColorAttr) {
        colorAttr = newColorAttr;
        currentColorName = colorAttr.chrColorName;
        _color = colorAttr.rgbValue;
        material.SetFloat("_HueShift", newColorAttr.hueShift);
    }

    public void HandleGrabbing(InputAction.CallbackContext context) {
        if (context.performed) {
            if (_grabbedBlock == null) {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
                foreach (Collider2D collider in colliders) {
                    if (collider.CompareTag("Block")) {
                        _grabbedBlock = collider.GetComponent<Block>();
                        _grabbedBlock.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                        _isGrabbing = true;
                        break;
                    }
                }
            }
        } 
        else if (context.canceled) {
            if (_grabbedBlock != null) {
                _grabbedBlock.rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                _grabbedBlock = null;
                _isGrabbing = false;
            }
        }
    }

    public void Pause() {
        GameObject.FindFirstObjectByType<UIManager>().TogglePause();
    }

    public void ResetMovement() {
         rb.linearVelocity = Vector2.zero;
         CurrentVelocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }

    protected void OnTriggerEnter2D(UnityEngine.Collider2D collision) {
        if (collision.gameObject.CompareTag("MixRay") || collision.gameObject.CompareTag("ChangeRay")) {
            collision.gameObject.GetComponent<IColorChanger>().CauseColorChange(gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.CompareTag("Wall")) {
            Debug.Log("Wall Collision");
            Surface s = collider.gameObject.GetComponent<Surface>();

            Harmony harmony = ChrColor.DetermineHarmony(colorAttr, ChrColor.FindColorAttr(s.colorName));

            shouldWallJump = harmony is Harmony.Analogue || harmony is Harmony.Equal;
        }
    }
}