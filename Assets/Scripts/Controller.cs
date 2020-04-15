using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    List<GameObject> planes = new List<GameObject>();

    int generatedPlanes = 0;
    int storedPlanes = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         Debug.Log("Length3: " + planes.Count);
    }

    public void addPlane(GameObject plane) {
        planes.Add(plane);
    }

    public void planeLanded(GameObject plane){
        storedPlanes++;
        planes.Remove(plane);
    }

    public int getNumPlanes() {
        return planes.Count;
    }

    public int getGeneratedPlanes() {
        return generatedPlanes;
    }

    public int getStoredPlanes() {
        return storedPlanes;
    }

    public bool checkIfHits(Vector3 newPosition) {

        foreach(GameObject plane in planes) {
            if(plane.transform.position.x + 0.2 > newPosition.x && plane.transform.position.x - 0.2 < newPosition.x &&
                plane.transform.position.y + 0.2 > newPosition.y && plane.transform.position.y - 0.2 < newPosition.y &&
                plane.transform.position.z + 50 > newPosition.z && plane.transform.position.z - 50 < newPosition.z)
                return true; 
        }

        return false;

    }
}
