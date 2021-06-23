using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DoorState
{
    OPENED,
    CLOSED,
    OPENING
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private Animator DoorObjectAnim;
    [SerializeField] private GameObject DoorTrigger;
    public bool doorIsBeingOperated = false;
    private DoorState doorState = DoorState.CLOSED;

    [SerializeField] private Text InteractText;

    private List<GameObject> adaptableObjects = new List<GameObject>();
    [SerializeField] private Material changeMaterial1;
    [SerializeField] private Material changeMaterial2;
    public int currentActiveMaterial = 0;

    //Boogie variables
    public bool canActivateBoogie = true;
    [SerializeField] private GameObject boogieObjectsParent; //The top most (empty) Game Object, this way you only have to activate 1 object.
    [SerializeField] private AudioSource JukeBoxAudio; //The Audio Source the music needs to play from
    [SerializeField] private AudioClip songToPlay;
    

    // Start is called before the first frame update
    void Start()
    {
        adaptableObjects.AddRange(GameObject.FindGameObjectsWithTag("AdaptableWall"));

        boogieObjectsParent.SetActive(false); //The boogie objects get disabled on startup (in case its left enabled on accident in the scene)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: Lotte -> Implement the boogie functionality
    public void ActivateBoogie()
    {
        canActivateBoogie = false; //prevent the player from activating the boogie while it's already playing.

        //Activate (enable) the boogie related objects here
        boogieObjectsParent.SetActive(true);
        //If you want to disable the particle during boogie time, do it here (don't forget to re-enable it)

        //Start the animations of the objects appearing?

        //Load the Jukebox audio clip and play it.
        //Make sure you assign the audio source and the audio clip to the GameManager script.
        JukeBoxAudio.clip = songToPlay;
        JukeBoxAudio.Play();

        //The duration starts, see function below for options during the boogie time.
        StartCoroutine(BoogieDuration());
    }

    //The Coroutine for the boogie time
    private IEnumerator BoogieDuration()
    {
        //Anything that needs to happen during the boogie time, add it here.

        //the line below is basically a Delay(seconds) function. If adding more delays make sure the total seconds are 40-45 seconds.
        yield return new WaitForSeconds(42);
        //Animate the objects going away again? (if you're doing this, add a delay between the SetActive below).

        //The boogie time is over and the objects get disabled again.
        boogieObjectsParent.SetActive(false);
        canActivateBoogie = true; //Boogie time can start again :)
    }

    public void ChangeTextures(int option)
    {
        if(option == 1)
        {
            foreach(GameObject item in adaptableObjects)
            {
                item.GetComponent<Renderer>().material = changeMaterial1;
            }
            currentActiveMaterial = 1;
        }
        else
        {
            foreach (GameObject item in adaptableObjects)
            {
                item.GetComponent<Renderer>().material = changeMaterial2;
            }
            currentActiveMaterial = 2;
        }
    }

    public void SetInteractText(bool state)
    {
        InteractText.gameObject.SetActive(state);
    }

    public void OperateDoor()
    {
        SetInteractText(false);
        if (doorState == DoorState.CLOSED)
        {
            DoorObjectAnim.Play("DoorOpening");
            StartCoroutine(DoorCooldown(DoorState.OPENED));
        }
        else if(doorState == DoorState.OPENED)
        {
            DoorObjectAnim.Play("DoorClosing");
            StartCoroutine(DoorCooldown(DoorState.CLOSED));
        }
        else if(doorState == DoorState.OPENING)
        {
            Debug.Log("For some reason the door opened twice before getting a state update");
        }
    }

    private IEnumerator DoorCooldown(DoorState targetDoorState)
    {
        SetInteractText(false);
        doorState = DoorState.OPENING;
        doorIsBeingOperated = true;
        DoorTrigger.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        doorState = targetDoorState;
        yield return new WaitForSeconds(1);
        doorIsBeingOperated = false;
        DoorTrigger.gameObject.SetActive(true);
    }
}
