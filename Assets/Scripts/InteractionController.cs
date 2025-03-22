using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour {

    public enum types { lever, door, sceneChanger, badStuffEmitter, checkPoint };
    public types type = types.lever;
    public bool activateOnCollision = false;
	public bool playSound = true;
    public InteractionController[] targets;
    public string collisionText = "";
    public Text hudTextBox;
    public string sceneToLoad = "";

	public GraphicalEffect[] objectInteractionEffects;
	public GraphicalEffect[] playerInteractionEffects;

    private bool playerCollision = false;
	
	// Update is called once per frame
	void Update () {
        if (playerCollision)
        {
            if (activateOnCollision)
            {
                OnPlayerInteraction();
            }
            else if (Input.GetButtonDown("Action"))
            {
                OnPlayerInteraction();
            }
        }
	}

    // Can be used to chain interactable objects together. e.g. lever -> door
    void OnObjectInteraction()
    {
        switch (type)
        {
            case types.lever:
            {
                break;
            }

            case types.door:
            {
                gameObject.SetActive(!gameObject.activeSelf);
                break;
            }
                
            case types.badStuffEmitter:
            {
                var contr = gameObject.GetComponent<BadStuffEmitterController>();
                contr.emitterActivated = !contr.emitterActivated;
                break;
            }

            case types.checkPoint:
            {
                break;
            }   
        }
		ActivateObjectInteractionEffects ();
    }
    
    // Called when player is in collision with object or has pressed action button
    public void OnPlayerInteraction()
    {
        switch (type)
        {
            case types.lever:
            {
                foreach (var target in targets)
                {
                    target.OnObjectInteraction();
                }
                break;
            }

            case types.door:
            {
                break;
            }

            case types.sceneChanger:
            {
                Application.LoadLevel(sceneToLoad);
                break;
            }

            case types.checkPoint:
            {
                foreach (var target in targets)
                {
                    target.OnObjectInteraction();
                }
                break;
            }            
        }
		ActivatePlayerInteractionEffects ();

		if (playSound) 
			PlaySound();

        playerCollision = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            playerCollision = true;

            if (hudTextBox)
            {
                hudTextBox.gameObject.SetActive(true);
                hudTextBox.text = collisionText.Length > 0 ? collisionText : (activateOnCollision ? "Activated": "Press E to activate");            
            }

        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            playerCollision = false;

            if (hudTextBox)
            {
                hudTextBox.gameObject.SetActive(false);
                hudTextBox.text = "";
            }

        }
    }

	private void PlaySound() {
		switch (type)
		{
		    case types.lever:
		    {
			    AudioManager.PlaySound("Sounds/lever");
			    break;
		    }
			
		    case types.door:
		    {
			    break;
		    }
			
		    case types.badStuffEmitter:
		    {
			    break;
		    }		
		    case types.checkPoint:
		    {
			    AudioManager.PlaySound("Sounds/checkpoint");
			    break;
		    }   
		}
	}

	private void ActivateObjectInteractionEffects() {
		foreach (GraphicalEffect effect in objectInteractionEffects) {
			effect.EnqueueToScheduler();
		}
	}

	private void ActivatePlayerInteractionEffects() {
		foreach (GraphicalEffect effect in playerInteractionEffects) {
			effect.EnqueueToScheduler();
		}
	}
}
