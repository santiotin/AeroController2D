using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;
    private float zoomLerpSpeed = 10;

    float inputX;
    float inputY;
    float speed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        if(inputX != 0) moveHorizontal();
        if(inputY != 0) moveVertical();


        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 4.0f, 15f);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

    }

    void moveHorizontal() {
        transform.position += transform.right * inputX * speed * Time.deltaTime;
    }

    void moveVertical() {
        transform.position += transform.up * inputY * speed * Time.deltaTime;
    }
}
