using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject planePrefab;
    public GameObject controller;

    float instantiationTimer;
    public float generationTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
         instantiationTimer = generationTime;
    }

    // Update is called once per frame
    void Update()
    {
        instantiationTimer -= Time.deltaTime;

        //controller.GetComponent<Controller>().getNumPlanesOfLvl(3) < 12
        if(instantiationTimer <= 0 ) {
            createPlane();
            instantiationTimer = generationTime;
        }
    }

    void createPlane() {
        GameObject newPlane;
        int pos = Random.Range(1, 5);

        Vector3 position = getPosition(pos);
        
        if(!checkIfHits(position)) {
            Vector3 rotation = getRotation(pos);
            float f = Random.Range(4900.0f, 5000.0f);

            newPlane = Instantiate(planePrefab, position, Quaternion.identity);
            newPlane.transform.GetChild(0).gameObject.transform.Rotate(rotation, Space.Self);
            newPlane.GetComponent<PlaneMov>().setFuel(f);

            controller.GetComponent<Controller>().addPlane(newPlane);
        }
    }

    bool checkIfHits(Vector3 position) {
        if(controller.GetComponent<Controller>().checkIfHits(position)) return true;
        else return false;
    }

    Vector3 getPosition(int pos) {
        float z = Random.Range(250f,300f);
        Vector3 position;
        switch (pos) {
            case 1:
                position = new Vector3(0.0f, 4.2f, z);
                break;
            case 2:
                position = new Vector3(4.2f, 0.0f, z);
                break;
            case 3:
                position = new Vector3(0.0f, -4.2f, z);
                break;
            case 4:
                position = new Vector3(-4.2f, 0.0f, z);
                break;
            default:
                position = new Vector3(0.0f, 4.2f, z);
                break;
        }
        return position;
    }

    Vector3 getRotation(int pos) {
        Vector3 rotation;
        switch (pos) {
            case 1:
                rotation = new Vector3(0.0f, 0.0f, 180.0f);
                break;
            case 2:
                rotation = new Vector3(0.0f, 0.0f, 90.0f);
                break;
            case 3:
                rotation = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case 4:
                rotation = new Vector3(0.0f, 0.0f, 270.0f);
                break;
            default:
                rotation = new Vector3(0.0f, 0.0f, 180.0f);
                break;
        }
        return rotation;
    }

}


