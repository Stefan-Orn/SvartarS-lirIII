using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void byrjaLeik(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void uppHafsSena(){
        SceneManager.LoadScene(0);
    }
    public void haettaLeik(){
        Application.Quit();
    }
}
