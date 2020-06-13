using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    List<GameObject> planes = new List<GameObject>();

    public InputField inputFieldPlane;
    public InputField inputFieldAlt;
    public InputField inputFieldSec;
    public Button buttonDescend;
    public InputField inputFieldPrepPlane;
    public InputField inputFieldPlaneInfo;
    public InputField inputFieldPlaneInfoText;
    public Button buttonLand;

    int totalPlanes = 0;
    int landedPlanes = 0;
    int crashedPlanes = 0;
    int outFuelPlanes = 0;
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
        totalPlanes++;
        planes.Add(plane);
        return planes.IndexOf(plane);
    }

    public void planeLanded(GameObject plane){
        landedPlanes++;
        planes.Remove(plane);
        isLanding = false;
    }

    public int getTotalPlanes() {
        return totalPlanes;
    }

    public int getFlyingPlanes() {
        return totalPlanes - landedPlanes - crashedPlanes - outFuelPlanes;
    }

    public int getLandedPlanes() {
        return landedPlanes;
    }

    public int getCrashedPlanes() {
        return crashedPlanes;
    }

    public int getOutFuelPlanes() {
        return outFuelPlanes;
    }

    public void incCrashedPlanes() {
        crashedPlanes++;
    }

    public void incOutFuelPlanes() {
        outFuelPlanes++;
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

    public void onPressPlaneInfo() {
        if(!string.IsNullOrEmpty(inputFieldPlaneInfo.text)) {
            int num = int.Parse(inputFieldPlaneInfo.text);
            if(num >= 0 && num < planes.Count) {
                int id = planes[num].GetComponent<PlaneMov>().getId();
                int alt = planes[num].GetComponent<PlaneMov>().getAltitude();
                int fuel = planes[num].GetComponent<PlaneMov>().getFuel();

                inputFieldPlaneInfoText.text = "Plane number: " + id.ToString() + " \n";
                inputFieldPlaneInfoText.text = inputFieldPlaneInfoText.text + "Plane altitude: " + alt.ToString() + " \n";
                inputFieldPlaneInfoText.text = inputFieldPlaneInfoText.text + "Plane fuel: " + fuel.ToString() + " \n";
            }
        }
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

                    planes[num].GetComponent<PlaneMov>().descendPlane(new Vector3(x,y,z), t);
                }
            }
        }
    }

    public void onPressPrepToLand() {
        if(!string.IsNullOrEmpty(inputFieldPrepPlane.text) && !isLanding) {
            int num = int.Parse(inputFieldPrepPlane.text);

            if(num >= 0 && num < planes.Count && planes[num].GetComponent<PlaneMov>().getAltitude() <= 300) {
                isLanding = true;
                planes[num].GetComponent<PlaneMov>().landPlane();
            }
        }
    }

    public void resetSimulator(){
        SceneManager.LoadScene(1);
    }
}
