using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Manager : MonoBehaviour {

    internal static bool isHoldingPassenger = false;
    internal static bool isGameStarted = false;
    internal static bool alerted = false;

    public Animator animator;

    public float minDest;
    private float maxDest;
    public GameObject startInterface;
    public GameObject dropPassengerUI;
    public GameObject informationPanel;
    public GameObject destinationPoint;
    public GameObject comfortObject;
    public GameObject alertPanel;
    public GameObject timerPanel;
    public GameObject optionsPanel;
    public Slider currentPlaceSlider;
    public Slider comfortSlider;
    public Slider durabilitySlider;
    public Slider speedSlider;
    public Button repairDurability;
    public Button dropPassengerButton;

    public Text finishText;
    public Text speedText;
    public Text reputaionPointsTextOnInGame;
    public Text reputaionPointsTextOnPlayMenu;
    public Text cashAmountInMenu;
    public Text cashAmountInGame;
    public Text durabilityCostText;
    public Text dropPassengerCostText;
    public Text timerText;
    public Text gainedText;
    private GameObject cars_0;
    public static float durability = 100;
    public static float comfort = 100;
    public static float reputation;
    public static float cash;
    public static float durabilityCost;
    public static float reputaionCost;
    public float distance;
    internal float gainedCash;
    internal float gainedReputation;
    internal float timeHolder;
    internal float timer = 60;
    public string cashSave = "cash";
    public string reputationSave = "reputation";
    public string durabilitySave = "durability";


    TaxiController taxiController;


    private void Awake()
    {
        cash = PlayerPrefs.GetInt(cashSave, 0);
        reputation = PlayerPrefs.GetInt(reputationSave, 0);
        durability = PlayerPrefs.GetInt(durabilitySave, 100);
    }
    void Start()
    {
        //cash = 500;
        //reputaion = 0;
        cars_0 = GameObject.Find("cars_0");
        taxiController = cars_0.GetComponent<TaxiController>();
        currentPlaceSlider.gameObject.SetActive(false);
        startInterface.SetActive(true);
    }

    /// <summary>
    /// Called from editor by DropPassanger button. Call Succed() or Failed() before this function
    /// no mattter where you call FinishLoop()
    /// </summary>
    /// <param name="message"> message that citizen will say.</param>
    public void FinishLoop()
    {
        // call via succes of failed
        print("Loop Completed!");
        cars_0.GetComponent<ManagePassenger>().DropPassenger();
        isHoldingPassenger = false;
        currentPlaceSlider.gameObject.SetActive(false);
        comfortObject.gameObject.SetActive(false);
        reputaionPointsTextOnInGame.gameObject.SetActive(true);
        cashAmountInGame.gameObject.SetActive(true);
        repairDurability.interactable = true;
        dropPassengerButton.interactable = false;
        timerPanel.SetActive(false);

        gainedText.text = 
            Mathf.RoundToInt(gainedCash) + " $\n" +
            Mathf.RoundToInt(gainedReputation) + " Reputation"; // print cash and rep points to result
        animator.Play("Gained");// display result with animation

        comfort = 100; // restore comfort
    }

    public void Failed()
    {
        ManagePassenger.citMessage = ManagePassenger.badTaxi;
        gainedCash = 0;
        gainedReputation = -200;

        reputation -= 200;
        // prevent to drop passenger when no time remained
        dropPassengerUI.SetActive(false);
    }

    public void Succed()
    {
        ManagePassenger.citMessage = ManagePassenger.thanks;
        gainedReputation = (comfort * 2) + durability;
        reputation += gainedReputation;

        gainedCash = comfort + distance / 100;
        cash += gainedCash ;
    }

    public void MissionDeclined()
    {
        TaxiCaller.rejected = true;
    }

    // called via button
    public void MissionAccepted()
    {
        TaxiCaller.accepted = true;
        isHoldingPassenger = true;
        SetUpDestination();
        cars_0.GetComponent<ManagePassenger>().SetUpCitizen();
        taxiController.startPoint = cars_0.transform.position;
        reputaionPointsTextOnInGame.gameObject.SetActive(false);
        cashAmountInGame.gameObject.SetActive(false);
        repairDurability.interactable = false;
        dropPassengerButton.interactable = true;
        timerPanel.SetActive(true);
    }
    
    public void RepairDurability()
    {
        if (durabilityCost <= cash)
        {
            cash -= durabilityCost;
            durability = 100f;
            alerted = false;
        }
    }

    public void DropPassengerImmediately()
    {
        if (Time.timeScale != 1)
            MenuClosed();
        Failed();
        FinishLoop();
    }
    
    void SetUpDestination()
    {
        // 80 sec for each 5k meter ?
        distance = Random.Range(minDest, minDest + Random.Range(0, 10000));

        Debug.LogWarning("Dest: " + distance);
        timeHolder = Time.time + distance / 80;


        destinationPoint.transform.position = new Vector3(27, 0, cars_0.transform.position.z + distance);
        destinationPoint.SetActive(true);
    }

    public static void MenuOpened()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    public static void MenuClosed()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }
    void UpdateUIValues()
    {
        currentPlaceSlider.value = Mathf.RoundToInt(
            100*(cars_0.transform.position.z - taxiController.startPoint.z)
            / (destinationPoint.transform.position.z - taxiController.startPoint.z )
            );
        comfortSlider.value = comfort;
        durabilitySlider.value = durability;
        speedSlider.value = taxiController.speed;
        speedText.text = Mathf.RoundToInt(taxiController.speed).ToString();

        timer = timeHolder - Time.time;
        timerText.text = Mathf.RoundToInt(timer) + " sec";
        print(timer);

        cashAmountInMenu.text = Mathf.RoundToInt(cash).ToString() + " $";                // print cash amount in menu
        cashAmountInGame.text = Mathf.RoundToInt(cash).ToString() + " $";                // print cash amount in game
        reputaionPointsTextOnInGame.text = Mathf.RoundToInt(reputation).ToString() + " RP"; // print rep points
        reputaionPointsTextOnPlayMenu.text = "You have " + Mathf.RoundToInt(reputation).ToString() + "   (RP)";

        durabilityCost = (int) Mathf.Abs((100 - durability) * 2);                     // calculate drabilityCost
        durabilityCostText.text = Mathf.RoundToInt(durabilityCost).ToString() + " $"; // print durability cost 

        dropPassengerCostText.text ="200 RP \nwill reduce"; // print reputationCost
    }

    // Check durability and comfort levels
    void ControlJourney()
    {
        if (comfort <= 0 || timer <=0)
        {
            DropPassengerImmediately();
            isHoldingPassenger = false;
        }
    }

    void ControlDurability()
    {
        if(isHoldingPassenger)
            ControlJourney();

        if (durability <= 0 && !alerted)
        {
            if (cash < durabilityCost)
            {
                ReturnHomePage();
                return;
            }

            alerted = true;
            alertPanel.SetActive(true); // alert uset for 0 durabiliry
        }
    }

    public void RestoreValues()
    {
        cash = 0;
        reputation = 0;
        durability = 100;
        comfort = 100;
        alerted = false;
        taxiController.speed = 0;
        taxiController.maxSpeed = 0;
        taxiController.targetSteer = 0;
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("fcars"))
        {
            Destroy(g);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("bcars"))
        {
            Destroy(g);
        }
        SpawnOtherCars.frontCars = 0;
        SpawnOtherCars.backCars = 0;
        SaveProcess();

    }
    public void ReturnHomePage()
    {
        isGameStarted = false;
        finishText.text = "Durability ran out :(";
        startInterface.SetActive(true);
        RestoreValues();
    }

    // Called by play button
    public void PressedPlayButton()
    {
        isGameStarted = true;
    }

    public void SaveProcess()
    {
        PlayerPrefs.SetInt(cashSave, (int)cash);
        PlayerPrefs.SetInt(reputationSave, (int)reputation);
        PlayerPrefs.SetInt(durabilitySave, (int)durability);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update () {

        UpdateUIValues();
        ControlDurability();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsPanel.SetActive(!optionsPanel.activeInHierarchy);
            if (optionsPanel.activeInHierarchy)
                MenuOpened();
            else
                MenuClosed();
        }
    }
        
}