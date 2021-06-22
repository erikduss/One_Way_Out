using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoogieTIme : MonoBehaviour
{
    private GameManager gameManager;
    private SphereCollider trigger;

    private bool canActivate = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        trigger = this.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //The trigger is disabled when the boogie is activated. Once it's over the trigger gets enabled again.
        if (!gameManager.canActivateBoogie) return;
        else if(!trigger.enabled)
        {
            trigger.enabled = true;
        }

        if (canActivate && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.ActivateBoogie(); //activate the boogie in the GameManager
            trigger.enabled = false;
            gameManager.SetInteractText(false);
            canActivate = false;
        }
    }

    //When the player enters the trigger, the boogie can be activated
    private void OnTriggerEnter(Collider other)
    {
        canActivate = true;
        gameManager.SetInteractText(true);
    }

    //When the player leaved the trigger, the boogie cant be activated
    private void OnTriggerExit(Collider other)
    {
        canActivate = false;
        gameManager.SetInteractText(false);
    }
}
