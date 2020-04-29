using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource btnPress;
    public GameObject instrMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("p")) {
            playGame();
        }
        if (Input.GetKey("i")) {
            showInstructions();
        }
        if (Input.GetKey("q")) {
            quitGame();
        }
        if (Input.GetKey("b")) {
            hideInstructions();
        }
    }

    public void playGame() {
        btnPress.Play();
        StartCoroutine(changeScene(1));
    }

    public void showInstructions() {
        btnPress.Play();
        //startMenu.SetActive(false);
        instrMenu.SetActive(true);
    }

    public void hideInstructions() {
        btnPress.Play();
        instrMenu.SetActive(false);
    }

    public void quitGame() {
        btnPress.Play();
        Debug.Log("Quit!");
        Application.Quit();
    }

    IEnumerator changeScene(int sceneNum) {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(sceneNum);
    }
}
