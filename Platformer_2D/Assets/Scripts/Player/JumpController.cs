using UnityEngine;

public class JumpController : MonoBehaviour
{
    public float _jumpForce;
    public bool onGround;
    private Rigidbody2D rb;
    public Transform Grounded;
    public float Radiused;
    public LayerMask Ground;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Jump();
        chekingGround();
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector2.up * _jumpForce);
        }
    }
    void chekingGround()
    {
        onGround = Physics2D.OverlapCircle(Grounded.position, Radiused, Ground);
    }
}
