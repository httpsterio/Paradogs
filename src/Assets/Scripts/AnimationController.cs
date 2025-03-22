using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
    public static AnimationController Instance { get; private set; }

    public float updateSpeed = 0.5f;

    private Sprite[] StandRightSprites;
    private Sprite[] StandLeftSprites;
    private Sprite[] WalkRightSprites;
    private Sprite[] WalkLeftSprites;
    private Sprite[] JumpRightSprites;
    private Sprite[] JumpLeftSprites;
    private enum animationTypes { standRight, standLeft, walkRight, walkLeft, jumpRight, jumpLeft }
    private animationTypes animationType;
    private animationTypes oldAnimationType;
    private int animationCounter = 0;
    private float updateTime;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StandRightSprites = Resources.LoadAll<Sprite>("Sprites/StandRight");
        StandLeftSprites = Resources.LoadAll<Sprite>("Sprites/StandLeft");      
        WalkRightSprites = Resources.LoadAll<Sprite>("Sprites/WalkRight");
        WalkLeftSprites = Resources.LoadAll<Sprite>("Sprites/WalkLeft");
        JumpRightSprites = Resources.LoadAll<Sprite>("Sprites/JumpRight");
        JumpLeftSprites = Resources.LoadAll<Sprite>("Sprites/JumpLeft");

        animationType = animationTypes.standRight;
        oldAnimationType = animationTypes.standRight;

        updateTime = Time.time + updateSpeed;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.time > updateTime)
        {
            NextSprite();
            updateTime = Time.time + updateSpeed;
        }
    }

    private void NextSprite()
    {
        switch (animationType)
        {
            case animationTypes.standRight: { ShowSprite(StandRightSprites); break; }
            case animationTypes.standLeft: { ShowSprite(StandLeftSprites); break; }      
            case animationTypes.walkRight: { ShowSprite(WalkRightSprites); break; }
            case animationTypes.walkLeft: { ShowSprite(WalkLeftSprites); break; }
            case animationTypes.jumpRight: { ShowSprite(JumpRightSprites); break; }
            case animationTypes.jumpLeft: { ShowSprite(JumpLeftSprites); break; }
        }
    }

    public void ChangeAnimation(string anim)
    {
        switch (anim)
        {
            case "STANDRIGHT": { animationType = animationTypes.standRight; break; }
            case "STANDLEFT": { animationType = animationTypes.standLeft; break; }
            case "WALKRIGHT": { animationType = animationTypes.walkRight; break; }
            case "WALKLEFT": { animationType = animationTypes.walkLeft; break; }
            case "JUMPRIGHT": { animationType = animationTypes.jumpRight; break; }
            case "JUMPLEFT": { animationType = animationTypes.jumpLeft; break; }
        }

        if (animationType != oldAnimationType)
        {
            oldAnimationType = animationType;
            animationCounter = 0;
        }
    }

    private void ShowSprite(Sprite[] array)
    {
        if (animationCounter >= array.Length)
            animationCounter = 0;

        if (array.Length != 0)
        {
            spriteRenderer.sprite = array[animationCounter];
        }

        animationCounter += 1;
    }
}