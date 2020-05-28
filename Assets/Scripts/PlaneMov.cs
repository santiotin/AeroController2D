using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneMov : MonoBehaviour
{
    RandomFromDistribution.ConfidenceLevel_e conf_level = RandomFromDistribution.ConfidenceLevel_e._95;
    public Sprite planeNormal;
    public Sprite planeDesc;
    public Sprite planeColision;
    public Sprite planeOutFuel;
    public Sprite planeLanding;
    public GameObject altitudeText;
    public GameObject fuelText;
    public GameObject idText;
    GameObject controller;
    InputField console;
    float fuel = 0;
    float speed = 20;
    float module = 0;
    int id;
    private Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);
    bool descending = false;
    bool collision = false;
    bool fuelEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find ("Controller");
        console = GameObject.Find("InputConsole").GetComponent<InputField>();
        module = calcModule(transform.position);
        fuel = Random.Range(4900.0f, 5000.0f);
    }

    // Update is called once per frame
    void Update()
    {
        fuel -= 1f;
        if(fuel < 0 && !fuelEnd) {
            fuelEnd = true;
            console.text = "Plane " + id.ToString() + " don't have fuel";
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planeOutFuel;
            controller.GetComponent<Controller>().incOutFuelPlanes();
        }


        id = controller.GetComponent<Controller>().getIndex(gameObject);
        //show altitude
        int t = (int)(transform.position.z * 10f);
        int f = (int)fuel;

        idText.GetComponent<Text>().text = id.ToString();
        altitudeText.GetComponent<Text>().text = t.ToString();

        if(fuel >= 0 ) fuelText.GetComponent<Text>().text = f.ToString();
        else fuelText.GetComponent<Text>().text = "0";

        if(!descending) {
            //move around
            transform.RotateAround(origin, Vector3.forward, speed * Time.deltaTime);
            gameObject.transform.GetChild(1).transform.RotateAround(gameObject.transform.GetChild(0).transform.position,Vector3.forward, -speed * Time.deltaTime);
        }
        
    }

    public void setId(int i) {
        id = i;
    }

    public int getId() {
        return id;
    }

    public int getAltitude(){
        return (int)(transform.position.z * 10f);
    }

    public int getFuel(){
        return (int)fuel;
    }

    public void descendPlane(Vector3 position, float timeToMove) {
        console.text = "Descending plane " + id.ToString() + " to altitude " + (position.z * 10.0f).ToString() + " in " + timeToMove.ToString() + " seconds" + "\n";
        StartCoroutine(MoveToPosition(position,timeToMove));
    }

    public void landPlane() {
        StartCoroutine(landRoutine());
    }

    IEnumerator MoveToPosition(Vector3 position, float timeToMove) {
        descending = true;
        gameObject.transform.GetChild(0).transform.Rotate(Vector3.forward, 90);
        if(!collision && !fuelEnd && position != new Vector3(0,0,0))gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planeDesc;
        var currentPos = transform.position;
        var t = 0f;
        while(t < 1) {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        if(!collision && !fuelEnd)gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planeNormal;
        gameObject.transform.GetChild(0).transform.Rotate(Vector3.forward, -90);
        descending = false;

        console.text = "DONE" + "\n";

        if(position == new Vector3(0,0,0)) {
            console.text = console.text + "Plane landed" + "\n";
            destroyPlane();
        }
    }

    IEnumerator landRoutine() {
        if(!collision && !fuelEnd)gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planeLanding;
        console.text = "Preparing Landing" + "\n";
        yield return new WaitForSeconds(RandomFromDistribution.RandomRangeNormalDistribution(1, 3, conf_level));
        console.text = console.text + "\n" + "Seatbells - CHECK";
        yield return new WaitForSeconds(RandomFromDistribution.RandomRangeNormalDistribution(1, 3, conf_level));
        console.text = console.text + "\n" + "Security - CHECK";
        yield return new WaitForSeconds(RandomFromDistribution.RandomRangeNormalDistribution(1, 3, conf_level));
        console.text = console.text + "\n" + "Spoilers - CHECK";
        yield return new WaitForSeconds(RandomFromDistribution.RandomRangeNormalDistribution(1, 3, conf_level));
        console.text = console.text + "\n" + "Wheels - CHECK" + "\n";
        console.text = console.text + "\n" + "Start Landing";
        StartCoroutine(MoveToPosition(new Vector3(0,0,0), 5.0f));
    }

    public float getModule(){
        return module;
    }
    float calcModule(Vector3 position) {
        float sx = position.x * position.x;
        float sy = position.y * position.y;
        float sz = position.z * position.z;

        return Mathf.Sqrt(sx + sy + sz);
    }

    private void OnTriggerEnter(Collider collider)
    {
        controller.GetComponent<Controller>().incCrashedPlanes();
        collision = true;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planeColision;
        console.text = "Collision between plane " + id.ToString() + " and plane " + collider.gameObject.GetComponent<PlaneMov>().getId().ToString();
    }

    void destroyPlane() {
        controller.GetComponent<Controller>().planeLanded(gameObject);
        Destroy(gameObject);
    }

}
