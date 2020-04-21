using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    List<GameObject> planes = new List<GameObject>();

    public InputField inputFieldPlane;
    public InputField inputFieldAlt;
    public InputField inputFieldSec;
    public Button buttonDescend;
    public InputField inputFieldPrepPlane;
    public Button buttonLand;

    int generatedPlanes = 0;
    int storedPlanes = 0;
    bool isLanding = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Length3: " + planes.Count);
    }

    public int addPlane(GameObject plane) {
        planes.Add(plane);
        return planes.IndexOf(plane);
    }

    public void planeLanded(GameObject plane){
        storedPlanes++;
        planes.Remove(plane);
        isLanding = false;
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

    public int getIndex(GameObject plane) {
        return planes.IndexOf(plane);
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

    public void onPressDescend() {
        //comprobar que nada esté vacío
        if( !string.IsNullOrEmpty(inputFieldAlt.text) && !string.IsNullOrEmpty(inputFieldPlane.text) && !string.IsNullOrEmpty(inputFieldSec.text) ) {
            
            int num = int.Parse(inputFieldPlane.text);
            float t = float.Parse(inputFieldSec.text);
            float z = float.Parse(inputFieldAlt.text) / 10;

            if(num >= 0 && num < planes.Count && t >= 0) {
                if(z >= 30 && z < planes[num].transform.position.z) {

                    float am = planes[num].GetComponent<PlaneMov>().getModule();
                    float nm =  Mathf.Sqrt((z*z) + ((z/20)*(z/20)));
                    
                    float x = planes[num].transform.position.x;
                    float y = planes[num].transform.position.y;

                    x = (x / am) * nm;
                    y = (y / am) * nm;

                    planes[num].GetComponent<PlaneMov>().descendPlane(planes[num].transform, new Vector3(x,y,z), t);
                }
            }
        }
    }

    public void onPressPrepToLand() {
        if(!string.IsNullOrEmpty(inputFieldPrepPlane.text) && !isLanding) {
            int num = int.Parse(inputFieldPrepPlane.text);

            if(num >= 0 && num < planes.Count) {
                isLanding = true;
                planes[num].GetComponent<PlaneMov>().descendPlane(planes[num].transform, new Vector3(0,0,0), 5.0f);
            }
        }
    }
}
