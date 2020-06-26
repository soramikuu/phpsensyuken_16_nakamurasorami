using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public string userinfo;
    void Start()
    {
        StartCoroutine(GetUsers());
        StartCoroutine(Login("KUU", "12345678"));
        StartCoroutine(Register("NAKAMURA","12345678"));
    }

    IEnumerator GetUsers()
    {

        
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/unity_backend/Getusers.php"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            //string[] pages = uri.Split('/');
            //int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                //userinfo = webRequest.downloadHandler.text;

                byte[] results = webRequest.downloadHandler.data;
            }
        }

        //WWWForm form = new WWWForm();
        //form.AddField("loginUser", username);
        //form.AddField("loginPass", password);
        //using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity_backend/login.php", form))
        //{
        //    yield return www.SendWebRequest();

        //    if (www.isNetworkError || www.isHttpError)
        //    {
        //        Debug.Log(www.error);
        //    }
        //    else
        //    {
        //        Debug.Log(www.downloadHandler.text);
        //    }
        //}
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity_backend/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //Main.Instance.UserInfo.SetCredentials(username, password);
                //Main.Instance.UserInfo.SetID(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator Register(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/unity_backend/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}