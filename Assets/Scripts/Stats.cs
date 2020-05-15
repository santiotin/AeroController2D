using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public GameObject controller;
    public Text totalStats;
    public Text flyingStats;
    public Text crashedStats;
    public Text outFuelStats;
    public Text landedStats;

    public Text time;

    int total, landed, flying, crashed, outFuel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        total = controller.GetComponent<Controller>().getTotalPlanes();
        landed = controller.GetComponent<Controller>().getLandedPlanes();
        crashed = controller.GetComponent<Controller>().getCrashedPlanes();
        outFuel = controller.GetComponent<Controller>().getOutFuelPlanes();
        flying = controller.GetComponent<Controller>().getFlyingPlanes();

        totalStats.text = "Total: " + total.ToString();
        landedStats.text = "Landed: " + landed.ToString();
        crashedStats.text = "Crashed: " + crashed.ToString();
        outFuelStats.text = "Out Fuel: " + outFuel.ToString();
        flyingStats.text = "Flying: " + flying.ToString();

        time.text = "Time: " + (Time.timeSinceLevelLoad).ToString();
    }
}
