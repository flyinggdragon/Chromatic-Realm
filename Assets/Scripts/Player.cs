using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ICanColorChange {
    [Header("Movement")]
    public float baseSpeed = 10f;
    public float speed;
    public float horizontalMovement;
    private bool IsFacingRight => transform.localScale.x > 0;
    private float grabbingSpeedDemodifier = 0.5f;

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
    public bool isGrabbing = false;
    public Grab grab;

    [Header("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private Material material;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private Animator smokeAnimator;
    [SerializeField] private SpriteRenderer smokeAnimation;
    
    [Header("Movement Constraints")]
    public bool grounded;
    public bool walled;
    public Vector2 CurrentVelocity { get; set; }
    public static bool shouldMove = true;
    public static bool shouldInput = true;
    private float harmonySpeedModifier;
    
    [Header("Color")]
    public ColorName currentColorName;
    public ColorAttr colorAttr { get; set; }

    [Header("Audio Clips")]
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip die;

    [Header("Other")]
    private Color _color;
    private Surface s;

    // Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        material = GetComponent<Renderer>().material;
        ChangeColor(ChrColor.FindColorAttr(currentColorName));

        shouldMove = true;
        shouldInput = true;
    }

    private void Update() {
        Gravity();
        WallSlide();
        WallJump();
        
        grabbingSpeedDemodifier = isGrabbing ? 0.5f : 1.0f;
        
        speed = baseSpeed * harmonySpeedModifier * grabbingSpeedDemodifier;

        if (!isWallJumping) {
            rb.linearVelocity = new(horizontalMovement * speed, rb.linearVelocity.y);
            CheckFlipDirection(horizontalMovement);
        }
        
        smokeAnimation.enabled = horizontalMovement != 0f && IsGrounded();
        animator.SetBool("IsWalking", CurrentVelocity.x != 0f);
    }

    private void FixedUpdate() {
        if (isGrabbing && grab.shouldGrab && grab.grabbingBlock != null && grab.rb != null) {
        grab.rb.linearVelocity = rb.linearVelocity;
    }

        CurrentVelocity = rb.linearVelocity;
    }

    public void Move(InputAction.CallbackContext context) {
        if (!shouldMove && !shouldInput) return;

        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context) {
        if (!shouldMove && !shouldInput) return;
        
        if (IsGrounded() && !isGrabbing) {
            AudioManager.Instance.PlaySFX(jump);
            if (context.performed) rb.linearVelocity = new(rb.linearVelocity.x, jumpForce);
            else if (context.canceled) rb.linearVelocity = new(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (context.performed && wallJumpTimer > 0f && shouldWallJump) {
            isWallJumping = true;
            rb.linearVelocity = new(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;

            if (transform.localScale.x != wallJumpDirection) Flip();

            Invoke(nameof(CancelWallJump), wallJumpTime * 0.1f);
        }
    }

    private void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void CheckFlipDirection(float direction) {
        if ((direction > 0 && !IsFacingRight) || (direction < 0 && IsFacingRight)) {
            Flip();
        }
    }

    public void ToggleChromaticCircle(InputAction.CallbackContext context) {
        if (context.performed) {
            if (GameManager.chromaticCircleUses != 0) {
                ChromaticCircle cc = GameObject.Find("UI").transform.GetChild(2).GetComponent<ChromaticCircle>();
                cc.ToggleVisibility();
            }
        }
    }
    public void SoftBlockJump(float force) {
        AudioManager.Instance.PlaySFX(jump);
        rb.linearVelocity = new(rb.linearVelocity.x, force);
    }

    private bool IsGrounded() {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer)) return true;
        
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

        if (s is not null) {
            harmonySpeedModifier = DetermineHarmonySpeedModifier(s.colorName);
            ChangeAnimSpeed();

            Harmony harmony = ChrColor.DetermineHarmony(colorAttr, ChrColor.FindColorAttr(s.colorName));
            shouldWallJump = harmony is Harmony.Analogue || harmony is Harmony.Equal || harmony is Harmony.All;
        }
    }

    private void ChangeAnimSpeed() {
        animator.SetFloat("speedModifier", harmonySpeedModifier);
        smokeAnimator.SetFloat("speedModifier", harmonySpeedModifier);
    }

    
    public void HandleGrabbing(InputAction.CallbackContext context) {
        if (context.performed) {
            isGrabbing = true;
        }

        else if (context.canceled) {
            isGrabbing = false;
        }
    }


   public void Pause(InputAction.CallbackContext context) {
        if (context.performed) {
            GameObject.FindFirstObjectByType<UIManager>().TogglePause();
        }
    }

    public void ResetMovement() {
        horizontalMovement = 0f;
        rb.linearVelocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    private float DetermineHarmonySpeedModifier(ColorName colorName) {
        Harmony harmony = ChrColor.DetermineHarmony(colorAttr, ChrColor.FindColorAttr(colorName));

        switch(harmony) {
            case Harmony.All:
                return 1.5f;
            
            case Harmony.Analogue:
                return 1.5f;
            
            case Harmony.Triadic:
                return 1.25f;

            case Harmony.Complementary:
                return 0.5f;

            case Harmony.Black:
                return 0.8f;
            
            default:
                return 1.0f;
        }
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
            s = collider.gameObject.GetComponent<Surface>();
            Harmony harmony = ChrColor.DetermineHarmony(colorAttr, ChrColor.FindColorAttr(s.colorName));

            shouldWallJump = harmony is Harmony.Analogue || harmony is Harmony.Equal || harmony is Harmony.All;
        }

        if (collider.gameObject.CompareTag("Ground")) {
            s = collider.gameObject.GetComponent<Surface>();

            harmonySpeedModifier = DetermineHarmonySpeedModifier(s.colorName);
            ChangeAnimSpeed();
        }
    }
}