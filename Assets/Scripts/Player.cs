using UnityEngine;

public class Player : MonoBehaviour, ICanColorChange {
    // Private
    private float _speed = 15f;
    private float _harmonySpeedBonus = 0f;
    private float _jumpForce = 12f; 
    private bool _isFacingRight = true;
    private bool _isWallSliding;
    private float _wallSlidingSpeed = 2f;
    private float _wallJumpingDirection;
    private float _wallJumpingTime = 0.2f;
    private float _wallJumpingCounter;
    private float _wallJumpingDuration = 0.4f;
    private Vector2 _wallJumpingPower = new Vector2(8f, 16f);
    private bool _shouldWallJump = false;
    private bool _isGrabbing = false;
    private Block _grabbedBlock;
    private Color _color;
    private Material material;
    private Animator animator;

    // Public
    [Header("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    
    [Header("Movement")]
    public bool grounded;
    public bool walled;
    public Vector2 CurrentVelocity { get; set; }
    public static bool shouldMove = true;
    public static bool shouldInput = true;

    
    [Header("Color")]
    public ColorName currentColorName;
    public ColorAttr colorAttr { get; set; }


    [Header("Collision Checks")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip die;

    // Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        material = GetComponent<Renderer>().material;
        animator = GetComponent<Animator>();
        ChangeColor(ChrColor.FindColorAttr(currentColorName));

        shouldMove = true;
    }

    public void ChangeColor(ColorAttr newColorAttr) {
        colorAttr = newColorAttr;
        currentColorName = colorAttr.chrColorName;
        _color = colorAttr.rgbValue;
        material.SetFloat("_HueShift", newColorAttr.hueShift);
    }

    private void Update() {
        if (shouldMove) HandleInput();
        
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        walled = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        if (!_isWallSliding) Flip();
        if (_shouldWallJump) WallJump(); WallSlide();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameObject.FindFirstObjectByType<UIManager>().TogglePause();
        }

        animator.SetBool("IsWalking", CurrentVelocity.x != 0f);
        HandleGrabbing();

        Debug.Log(_harmonySpeedBonus);
    }

    private void FixedUpdate() {
        if (shouldMove && !_isWallSliding) {
            Move();
        }

        if (_isGrabbing && _grabbedBlock != null) {
            _grabbedBlock.rb.linearVelocity = new Vector2(rb.linearVelocity.x, _grabbedBlock.rb.linearVelocity.y);
        }

        CurrentVelocity = rb.linearVelocity;
    }

    private void HandleGrabbing() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (_grabbedBlock == null) {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
                foreach (Collider2D collider in colliders) {
                    if (collider.CompareTag("Block")) {
                        _grabbedBlock = collider.GetComponent<Block>();
                        _grabbedBlock.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                        _isGrabbing = true;
                        break; // Sai do loop ao encontrar o primeiro bloco
                    }
                }
            } else {
                _grabbedBlock.rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                _grabbedBlock = null;
                _isGrabbing = false;
            }
        }
    }

    private void Move() {
        float hInput = Input.GetAxis("Horizontal");
        Debug.Log("hInput: " + hInput);
        rb.linearVelocity = new Vector2(hInput * (_speed + _harmonySpeedBonus), rb.linearVelocity.y);
    }

    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (GameManager.chromaticCircleUses != 0) {
                ColorInterface ci = GameObject.Find("UI").transform.GetChild(1).GetComponent<ColorInterface>();
                ci.ToggleVisibility();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            Jump(_jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    public void Jump(float jumpForce) {
        animator.SetTrigger("JumpTrigger");
        AudioManager.Instance.PlaySFX(jump);

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        grounded = false;
    }

    private void WallSlide() {
        if (walled && !grounded && Input.GetAxisRaw("Horizontal") != 0f) {
            _isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -_wallSlidingSpeed, float.MaxValue));
        } else {
            _isWallSliding = false;
        }
    }

    private void WallJump() {
        if (_isWallSliding) {
            _wallJumpingDirection = -transform.localScale.x;
            _wallJumpingCounter = _wallJumpingTime;
        } else {
            _wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _wallJumpingCounter > 0f) {
            _isWallSliding = false;
            rb.linearVelocity = new Vector2(_wallJumpingDirection * _wallJumpingPower.x, _wallJumpingPower.y);
            _wallJumpingCounter = 0f;
            Invoke(nameof(StopWallJumping), _wallJumpingDuration);
        }
    }

    private void StopWallJumping() {
        _isWallSliding = false;
    }

    private void Flip() {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (_isFacingRight && horizontal < 0f || !_isFacingRight && horizontal > 0f) {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void ResetMovement() {
         rb.linearVelocity = Vector2.zero;
         CurrentVelocity = Vector2.zero;
    }

    protected void OnTriggerEnter2D(UnityEngine.Collider2D collision) {
        if (collision.gameObject.CompareTag("MixRay") || collision.gameObject.CompareTag("ChangeRay")) {
            collision.gameObject.GetComponent<IColorChanger>().CauseColorChange(gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.CompareTag("Wall")) {
            Surface s = collider.gameObject.GetComponent<Surface>();
            
            if (
                ChrColor.DetermineHarmony(colorAttr, ChrColor.FindColorAttr(s.colorName))
                is Harmony.Analogue
            ) {
                _shouldWallJump = true;
            }
        }
    }
}