using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public GameObject player;
    public GameObject deathExplosion;
    public bool PlayerDeath { get; private set; }
    public bool deleteBlocksOnDeath = false;

    private Vector3 checkpoint;
    private Quaternion zeroRotation;

    // Switch to true to test without dying
    private bool GodMode = false;


    void Awake()
    {
        instance = this;
        PlayerDeath = false;
        zeroRotation = player.transform.rotation;
        AudioManager.LoadFabric();

        deathExplosion.CreatePool();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O) == true)
        {
            if (GodMode == false)
                GodMode = true;
            else
                GodMode = false;
        }

        if (Input.GetKeyDown(KeyCode.G) == true)
            playerKilled();
    }


	void Start() {
		AudioManager.PlaySound("Sounds/spawn");
	}

    public void setCheckpoint(Vector3 point) 
    {
        this.checkpoint = point;
    }

    public Vector3 getCheckpoint()
    {
        return this.checkpoint;
    }

    public void playerKilled()
    {
        if (!GodMode)
        {
            PlayerDeath = true;
            player.SetActive(false);

            AudioManager.PlaySound("Sounds/death");

            StartCoroutine("DeathCo");
        }          
    }

    public IEnumerator DeathCo()
    {
        deathExplosion.Spawn(player.transform.position, player.transform.rotation);
        deathExplosion.Spawn(player.transform.position, player.transform.rotation);
        yield return new WaitForSeconds(3);
        deathExplosion.DestroyAll();

        if(deleteBlocksOnDeath)
            destroyBadBlocks();

        // Not sure if spawn should player on respawn
        AudioManager.PlaySound("Sounds/spawn");

        player.transform.position = checkpoint;
        player.transform.rotation = zeroRotation;
        player.SetActive(true);
        PlayerDeath = false;
    }

    //Destroys bad stuff blocks created by emitters
    public void destroyBadBlocks()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("BadStuffTrigger");
        var gameObj = GameObject.FindGameObjectsWithTag("");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].Recycle();
        }
    }

}