using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AchievementController : MonoBehaviour
{
    
    public GameObject addText;
    
    public void CallAddAchievements(string str){
        
        StartCoroutine(AddAchievements(str));
    }
    
    public IEnumerator AddAchievements(string req){
       
        WWWForm form = new WWWForm();
        
        form.AddField("username", DBManager.username);
        form.AddField("req", req);
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/setAchievementData.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.downloadHandler.text == "0"){
                
                addText.SetActive(true);
            }else{
                Debug.Log(webRequest.downloadHandler.text);  

            }
        } 
    }
}
