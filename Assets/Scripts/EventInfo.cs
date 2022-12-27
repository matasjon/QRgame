using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class EventInfo : MonoBehaviour
{
    public TextMeshProUGUI title;
    
    void Start()
    {
        if(PlayerPrefs.GetInt("scanEvent") == 1)
        {
            StartCoroutine(SetUserToEvent());
            
            PlayerPrefs.SetInt("scanEvent", 0);
        }
        
        title.text = PlayerPrefs.GetString("eventName");
        
    }
    
    IEnumerator SetUserToEvent(){
       
        WWWForm form = new WWWForm();
        
        form.AddField("username", DBManager.username);
        form.AddField("eventName", PlayerPrefs.GetString("eventName"));
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/setUserToEvent.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            // if(webRequest.downloadHandler.text != "0"){
                
                // Debug.Log(webRequest.downloadHandler.text);
            // }   
        }
    }
    
    public void GoNext(){
        
        SceneManager.LoadScene("QuestionScene");   
        
    }

}
