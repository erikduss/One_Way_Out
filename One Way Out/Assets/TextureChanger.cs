using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private int materialID;
    private SphereCollider trigger;

    private bool canChangeMaterial = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        trigger = this.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentActiveMaterial == materialID) return;
        else if(!trigger.enabled)
        {
            trigger.enabled = true;
        }

        if (canChangeMaterial && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.ChangeTextures(materialID);
            trigger.enabled = false;
            gameManager.SetInteractText(false);
            canChangeMaterial = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canChangeMaterial = true;
        gameManager.SetInteractText(true);
    }

    private void OnTriggerExit(Collider other)
    {
        canChangeMaterial = false;
        gameManager.SetInteractText(false);
    }
}
