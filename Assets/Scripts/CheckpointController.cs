using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("Player") && GameManager.instance.getCheckpoint() != transform.position)
        {
            GameManager.instance.setCheckpoint(transform.position);
            var interactionController = GetComponent<InteractionController>();
            interactionController.OnPlayerInteraction();
        }
    }
}
