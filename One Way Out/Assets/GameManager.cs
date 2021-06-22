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

    // Start is called before the first frame update
    void Start()
    {
        adaptableObjects.AddRange(GameObject.FindGameObjectsWithTag("AdaptableWall"));
    }

    // Update is called once per frame
    void Update()
    {
        
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
