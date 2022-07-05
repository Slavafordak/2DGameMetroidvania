using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(GetUsers());
        //StartCoroutine(Login("testuser", "123456"));
    }

    IEnumerator GetDate()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/UnityBackend/GetDate.php");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
    IEnumerator GetUsers()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/UnityBackend/GetUsers.php");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackend/Login.php", form))
        {
            
            yield return www.SendWebRequest();

            

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                Debug.Log(www.downloadHandler.text);
                if(www.downloadHandler.text== "Login Success.")
                {
                    SceneManager.LoadScene(1);
                    Debug.Log("Good");
                }
                if(www.downloadHandler.text == ("Wrong Credentials.") || www.downloadHandler.text == ("Username does not exists"))
                {
                    Main.Instance.Invalid.SetActive(true);
                }
            }
        }
    }
}
