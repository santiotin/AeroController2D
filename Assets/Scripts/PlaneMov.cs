using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneMov : MonoBehaviour
{
    public GameObject altitudeText;
    public GameObject idText;
    float fuel;
    float speed = 20;
    int id;
    private Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);
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
        idText.GetComponent<Text>().text = id.ToString();

        //move around
        transform.RotateAround(origin, Vector3.forward, speed * Time.deltaTime);
        gameObject.transform.GetChild(1).transform.RotateAround(gameObject.transform.GetChild(0).transform.position,Vector3.forward, -speed * Time.deltaTime);

    }

    public void setFuel(float f) {
        fuel = f;
    }

    public void setId(int i) {
        id = i;
    }
}
