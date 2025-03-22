using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; //player speed
	public float jumpForce = 300;
    public float FallDeathThreshold = -30;
    public float runSpeedModifier = 2;
    public float maxAngularVelocity = 70f;

	private float maxJumpFrequency = 0.2f; //a small timer to prevent "double" jumps
    private float move = 0f; //movement (direction) on x-axis
    private bool jumpPressed = false;
    private bool sprintPressed = false;
    private float jumpTime = 0f;

    public GameObject deathExplosion;

    void Start()
    {
        jumpTime = maxJumpFrequency;
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetButton("Jump");
        sprintPressed = Input.GetButton("Sprint");
    }

    void FixedUpdate()
    {
        if (sprintPressed)
            rigidbody2D.velocity = new Vector2(move * (speed * runSpeedModifier), rigidbody2D.velocity.y);
        else
            rigidbody2D.velocity = new Vector2(move * speed, rigidbody2D.velocity.y);

        if (jumpTime <= 0 && checkGrounded() && jumpPressed)
        {
                rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x, jumpForce));
                jumpTime = maxJumpFrequency;
        }

        jumpTime -= Time.deltaTime;

        //limit player's angularVeloctiy
        rigidbody2D.angularVelocity = Mathf.Clamp(rigidbody2D.angularVelocity, -maxAngularVelocity, maxAngularVelocity);

        if (transform.position.y < FallDeathThreshold)
            GameManager.instance.playerKilled();
    }


    //Check if player is grounded
    bool checkGrounded()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        bool lineCastLeft = Physics2D.Linecast(new Vector2(pos.x-0.15f, pos.y), new Vector2(pos.x - 0.16f, pos.y - 0.34f), 1 << LayerMask.NameToLayer("Ground"));
        bool lineCastMiddle = Physics2D.Linecast(pos, new Vector2(pos.x, pos.y - 0.34f), 1 << LayerMask.NameToLayer("Ground"));
        bool lineCastRight = Physics2D.Linecast(new Vector2(pos.x+0.15f, pos.y), new Vector2(pos.x + 0.16f, pos.y - 0.34f), 1 << LayerMask.NameToLayer("Ground"));

        if (lineCastLeft || lineCastMiddle || lineCastRight)
			return true;

        return false;
    }

}
