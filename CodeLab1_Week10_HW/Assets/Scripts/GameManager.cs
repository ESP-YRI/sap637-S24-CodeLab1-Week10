using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Queue<GameObject> peopleQueue = new Queue<GameObject>();
    public GameObject waiter;
    private GameObject instantiatedWaiter;
    public List<Vector2> locationList = new List<Vector2>();
    public int locationNumber;
    
    public Button queueFiller;
    public Button resetter;
    
    public TextMeshProUGUI screenText;
    
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        screenText.text = "Press the button to fill your line!\nRemember the order...!";
        
        //fills the location list with the predetermined locations I want things to spawn at
        locationList.Add(new Vector2(0, 3));
        locationList.Add(new Vector2(-4, 1));
        locationList.Add(new Vector2(4, 1));
        locationList.Add(new Vector2(2, -3));
        locationList.Add(new Vector2(-2, -3));
        
        //ensures the resetter button is not active at the start of the game
        resetter.gameObject.SetActive(false);
    }

    //when called, this function instantiates a prefab at a random location from the 5 contained in locationList
    //and enqueues them into the peopleQueue queue
    //it then removes the location it chose randomly from the list so it is not repeated
    //this function is called by pressing a button, so when there are no locations left
    //the button is set to inactive to prevent a nullreferenceexception
    //and the text on screen changes to tell the player what to do next in the game
    public void PopulateQueue()
    {
        if (locationList.Count > 0)
        {
            locationNumber = Random.Range(0, locationList.Count);
            instantiatedWaiter = Instantiate(waiter, locationList[locationNumber], Quaternion.identity);
            peopleQueue.Enqueue(instantiatedWaiter);
            locationList.Remove(locationList[locationNumber]);
        }
        else
        {
            screenText.text = "Your line is full!\nClick on the waiters in order to remove them\nfrom the line!";
            queueFiller.gameObject.SetActive(false);
        }
        
        Debug.Log(peopleQueue);
    }

    
    //this function checks if the game object the player clicked on is at the front of the queue
    //if it is, screentext tells the player theyve done a good job, then dequeues and destroys the object
    //if it isn't the first in queue, screentext tells the player they've clicked the wrong object
    //also checks if the queue is empty, and if so, allows the player to restart the game
    public void DepopulateQueue(GameObject clickedOn)
    {
        if (clickedOn == peopleQueue.Peek())
        {
            screenText.text = "Good Job!";
            peopleQueue.Dequeue();
            Destroy(clickedOn);
        }
        else
        {
            screenText.text = "Oops! They're not first in line!";
        }

        //if there are no people in the queue, gives the player a congratulatory message
        //and also activates the game resetting button so the player may play again
        if (peopleQueue.Count == 0)
        {
            screenText.text = "Everyone's out of line! Yay!";
            resetter.gameObject.SetActive(true);
        }
    }

    //repopulates the locationlist and reactivates the queuefiller button so the game can be played again
    //also, deactivates the resetter so that it cannot be clicked again right away
    public void resetGame()
    {
        locationList.Add(new Vector2(0, 3));
        locationList.Add(new Vector2(-4, 1));
        locationList.Add(new Vector2(4, 1));
        locationList.Add(new Vector2(2, -3));
        locationList.Add(new Vector2(-2, -3));
        
        resetter.gameObject.SetActive(false);
        queueFiller.gameObject.SetActive(true);
        screenText.text = "Press the button to fill your line!\nRemember the order...!";
    }
    
}
