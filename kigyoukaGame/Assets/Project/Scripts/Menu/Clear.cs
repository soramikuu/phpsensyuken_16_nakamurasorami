using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Clear : MonoBehaviour
{
    public Button Button;
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(() =>
        {
            
            SceneManager.LoadScene("Menu");
        });

    }

}
