using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Rigidbody2D rb;
    Animator anim;

    [Header("Movement:")]
    [SerializeField] float _deadZone = 5;
    [SerializeField] float _movementSpeed = 5;
    [SerializeField] float _jumpForce = 10;


    [Header("GroundCheck:")]
    [SerializeField] Transform groundCheckPos;
    [SerializeField] float _groundCheckRadius;
    [SerializeField] LayerMask groundMask;
    bool isGrounded = false;
    int jumpCount = 2;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, _groundCheckRadius, groundMask);

        if (isGrounded && jumpCount == 0) jumpCount = 2;

        if (PlayerHealth.isKnocked) return;
        if(isGrounded && Input.touchCount <= 0) rb.velocity = new Vector2(0, rb.velocity.y);
    }
    private void OnEnable()
    {
        InputManager.OnTouchMoved += OnTouchMoved;
        InputManager.OnTouchEnded += OnTouchEnded;
        InputManager.OnJumpPressed += OnJumpPressed;
        InputManager.OnAttackPressed += OnAttackPressed;
    }
    private void OnDisable()
    {
        InputManager.OnTouchMoved -= OnTouchMoved;
        InputManager.OnTouchEnded -= OnTouchEnded;
        InputManager.OnJumpPressed -= OnJumpPressed;
        InputManager.OnAttackPressed -= OnAttackPressed;
    }
    void OnTouchMoved(Vector3 _direction)
    {
        if (PlayerHealth.isKnocked) return;

        if (_direction.x < -_deadZone || _direction.x > _deadZone)
        {
            Vector3 movement = new Vector2(_direction.x, 0).normalized * _movementSpeed;
            rb.velocity = new Vector2(movement.x, rb.velocity.y);

            //rotates player
            switch (Mathf.Sign(_direction.x))
            {
                case -1:
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case 1:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
        }
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }
    void OnTouchEnded(Vector3 _direction)
    {
        if (PlayerHealth.isKnocked) return;

        rb.velocity = new Vector2(_direction.x, rb.velocity.y);
    }
    #region Jump
    void OnJumpPressed()
    {
        if (isGrounded)
        {
            jumpCount = 2;
            Jump();
            jumpCount--;
            return;
        }
        if (jumpCount > 0)
        {
            jumpCount = 0;
            Jump();
        }
    }
    void Jump()
    {
        //resets the y velocity to stabilize the jump height even when falling
        rb.velocity = new Vector2(rb.velocity.x, 0);
        //jumps
        rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    } 
    #endregion
    void OnAttackPressed()
    {
        if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Attack")
            anim.SetTrigger("Attack");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPos.position, _groundCheckRadius);
    }
}
