using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 8;
    public float JumpSpeed = 20;
    float vx = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal") * Speed;
        float vy = GetComponent<Rigidbody2D>().linearVelocityY;

        if (vx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (vx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            vy = JumpSpeed;
        }

        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(vx, vy);
    }
}
