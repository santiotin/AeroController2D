using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneMov : MonoBehaviour
{
    public Sprite planeNormal;
    public Sprite planeDesc;
    public Sprite planeColision;
    public GameObject altitudeText;
    public GameObject idText;
    float fuel = 0;
    float speed = 20;
    float module = 0;
    int id;
    private Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);
    bool descending = false;
    bool collision = false;
    // Start is called before the first frame update
    void Start()
    {
        module = calcModule(transform.position);
        fuel = Random.Range(4900.0f, 5000.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //show altitude
        int t = (int)(transform.position.z * 10f);
        altitudeText.GetComponent<Text>().text = t.ToString();
        idText.GetComponent<Text>().text = id.ToString();

        if(!descending) {
            //move around
            transform.RotateAround(origin, Vector3.forward, speed * Time.deltaTime);
            gameObject.transform.GetChild(1).transform.RotateAround(gameObject.transform.GetChild(0).transform.position,Vector3.forward, -speed * Time.deltaTime);
        }
        
    }

    public void setId(int i) {
        id = i;
    }

    public void descendPlane(Transform transform, Vector3 position, float timeToMove) {
        StartCoroutine(MoveToPosition(transform,position,timeToMove));
    }

    IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove) {
        descending = true;
        gameObject.transform.GetChild(0).transform.Rotate(Vector3.forward, 90);
        if(!collision)gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planeDesc;
        var currentPos = transform.position;
        var t = 0f;
        while(t < 1) {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        if(!collision)gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planeNormal;
        gameObject.transform.GetChild(0).transform.Rotate(Vector3.forward, -90);
        descending = false;
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
        collision = true;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = planeColision;
    }


}
