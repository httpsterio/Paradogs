using UnityEngine;
using System.Collections;

public class RealLifeController : MonoBehaviour {

    public float speed = 5f; //player speed
    public float FallDeathThreshold = -30;

    private float move = 0f; //movement (direction) on x-axis
    private bool inAir = false;

    private enum animations { standing, move};
    private animations animationplayer;

    private enum directions { left, right};
    private directions direction;


    void Start()
    {
        animationplayer = animations.standing;
        direction = directions.right;
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal");

        if (move == 0)
            animationplayer = animations.standing;
        else
        {
            animationplayer = animations.move;
            direction = move > 0 ? directions.right : directions.left;
        }

        // Is this still needed?
        inAir = false;
    }

    void FixedUpdate()
    {
        // Move player
        rigidbody2D.velocity = new Vector2(move * speed, rigidbody2D.velocity.y);

        // Change player animation if needed
        AnimationController.Instance.ChangeAnimation(getAnimationText()); 

        // Kill player if falling to bottom (is this needed on real life player?)
        if (transform.position.y < FallDeathThreshold)
            GameManager.instance.playerKilled();
    }

    //Check if player is grounded
    private bool checkGrounded()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        if (Physics2D.Linecast(pos, new Vector2(pos.x, pos.y - 0.84f), 1 << LayerMask.NameToLayer("Ground")))
            return true;

        return false;
    }

    private string getAnimationText()
    {
        string animationText = "";

        // Get movement
        if (inAir)
            animationText += "JUMP";
        else
        {
            if (animationplayer == animations.standing)
                animationText += "STAND";
            else if (animationplayer == animations.move)
                animationText += "WALK";
        }

        // Get direction
        if (direction == directions.right)
            animationText += "RIGHT";
        else if (direction == directions.left)
            animationText += "LEFT";

        return animationText;
    }
}
