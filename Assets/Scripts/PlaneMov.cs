using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneMov : MonoBehaviour
{
    public GameObject altitudeText;
    float fuel;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //show altitude
        int t = (int)(transform.position.z * 10f);
        altitudeText.GetComponent<Text>().text = t.ToString();
    }

    public void setFuel(float f) {
        fuel = f;
    }
}
