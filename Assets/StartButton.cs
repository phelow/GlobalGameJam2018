using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    public void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
