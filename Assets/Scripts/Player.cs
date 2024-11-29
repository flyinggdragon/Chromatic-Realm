using UnityEngine;

public class Player : MonoBehaviour {
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
    private Color _color;
    private Material material;
    private Animator animator;

    // Public
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public bool grounded;
    public bool walled;
    public ColorName currentColorName;
    public ColorAttr colorAttr { get; private set; }
    public int chromaticCircleUses;
    public Vector2 CurrentVelocity { get; set; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    // Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        material = GetComponent<Renderer>().material;
        animator = GetComponent<Animator>();
        ChangeColor(ChrColor.FindColorAttr(currentColorName));
    }

    public void ChangeColor(ColorAttr newColorAttr) {
        colorAttr = newColorAttr;
        currentColorName = colorAttr.chrColorName;
        _color = colorAttr.rgbValue;
        material.SetFloat("_HueShift", newColorAttr.hueShift);
    }

    private void Update() {
        chromaticCircleUses = GameManager.chromaticCircleUses;
        
        if (GameManager.shouldInput) HandleInput();
        
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        walled = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        if (!_isWallSliding) Flip();
        if (_shouldWallJump) WallJump(); WallSlide();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameObject.FindFirstObjectByType<UIManager>().TogglePause();
        }

        animator.SetBool("IsWalking", CurrentVelocity.x != 0f);
    }

    private void FixedUpdate() {
        if (GameManager.shouldMove && !_isWallSliding) {
            Move();
        }

        CurrentVelocity = rb.linearVelocity;
    }

    private void Move() {
        float hInput = Input.GetAxis("Horizontal");
        Vector2 movement = new(hInput * (_speed + _harmonySpeedBonus), rb.linearVelocity.y);
        rb.linearVelocity = movement;
    }

    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (chromaticCircleUses != 0) {
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
}