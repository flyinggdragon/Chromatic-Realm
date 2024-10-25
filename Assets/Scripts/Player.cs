using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Private
    private float _speed = 15f;
    private float _harmonySpeedBonus = 0f;
    private float _jumpForce = 10f; 
    private bool _isFacingRight = true;
    private bool _isWallSliding;
    private float _wallSlidingSpeed = 2f;
    private float _wallJumpingDirection;
    private float _wallJumpingTime = 0.2f;
    private float _wallJumpingCounter;
    private float _wallJumpingDuration = 0.4f;
    private Vector2 _wallJumpingPower = new Vector2(8f, 16f);
    private bool _shouldWallJump = false;
    private Color32 _color;
    

    // Public
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public bool grounded;
    public bool walled;
    public ColorName currentColorName;
    public ColorAttr colorAttr { get; private set; }
    public bool shouldMove = true;
    public bool shouldInput = true;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    // Methods
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ChangeColor(ChrColor.FindColorAttr(currentColorName));
    }

    public void ChangeColor(ColorAttr newColorAttr) {
        colorAttr = newColorAttr;
        _color = colorAttr.rgbValue;
        sr.color = _color;
    }

    private void Update() {
        if (shouldInput) HandleInput();
        
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        walled = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        if (!_isWallSliding) Flip();
        if (_shouldWallJump) WallJump(); WallSlide();
    }

    private void FixedUpdate() {
        if (shouldMove && !_isWallSliding) {
            Move();
        }
    }

    private void Move() {
        float hInput = Input.GetAxis("Horizontal");
        Vector2 movement = new(hInput * (_speed + _harmonySpeedBonus), rb.velocity.y);
        rb.velocity = movement;
    }

    private void HandleInput() {
        // Toggleia a interface de cor
        if (Input.GetKeyDown(KeyCode.Q)) {
            ColorInterface ci = GameObject.Find("UI").transform.GetChild(0).GetComponent<ColorInterface>();
            ci.ToggleVisibility();
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            Jump(_jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Jump(float jumpForce) {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        grounded = false;
    }

    private void WallSlide() {
        if (walled && !grounded && Input.GetAxisRaw("Horizontal") != 0f) {
            _isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -_wallSlidingSpeed, float.MaxValue));
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
            rb.velocity = new Vector2(_wallJumpingDirection * _wallJumpingPower.x, _wallJumpingPower.y);
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
        rb.velocity = Vector2.zero;
    }

    public void EnableMovement() {
        shouldMove = true;
        shouldInput = true;
    }

    public void DisableMovement() {
        ResetMovement();
        shouldMove = false;
        shouldInput = false;
    }

    private void GiveHarmonyEffects(Surface s) {
        switch(s.colorName) {
            case ColorName.White:
                _harmonySpeedBonus = 3f;
                return;
            case ColorName.Black:
                _harmonySpeedBonus = 0f;
                return;
        }

        Harmony h = ChrColor.DetermineHarmony(colorAttr, ChrColor.FindColorAttr(s.colorName));

        switch(h) {
            case Harmony.Complementary:
                _harmonySpeedBonus = -2.5f;
                break;
            case Harmony.Triadic:
                _harmonySpeedBonus = 0.5f;
                break;
            case Harmony.Analogue:
                _harmonySpeedBonus = 3f;
                break;
            case Harmony.None:
                _harmonySpeedBonus = 0f;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //if (other.gameObject.CompareTag("Ground")) {
            //grounded = true;
        //}

        //if (other.gameObject.CompareTag("Block")) {
            //if (other.gameObject.GetComponent<Block>() is not SoftBlock) return;
            //grounded = true;
        //}
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            GiveHarmonyEffects(other.gameObject.GetComponent<Surface>());
        }

        if (other.gameObject.CompareTag("Wall")) {
            ColorAttr wallColor = ChrColor.FindColorAttr(other.gameObject.GetComponent<Surface>().colorName);
            
            Harmony h = ChrColor.DetermineHarmony(colorAttr, wallColor);

            if (h == Harmony.Analogue || h == Harmony.Equal) {
                _shouldWallJump = true;
            } else {
                _shouldWallJump = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        //if (other.gameObject.CompareTag("Ground")) {
            //grounded = false;
        //}

        if (other.gameObject.CompareTag("Block")) {
            if (other.gameObject.GetComponent<Block>() is not SoftBlock) return;

            grounded = false;
        }
    }
}