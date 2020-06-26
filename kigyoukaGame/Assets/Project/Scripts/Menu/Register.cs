using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button RegisterButton;

    // Start is called before the first frame update
    void Start()
    {
        RegisterButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.Web.Register(UsernameInput.text, PasswordInput.text));
            SceneManager.LoadScene("Menu");
        });
    }

}
