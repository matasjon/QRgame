using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CorrectAnswerController : MonoBehaviour
{
    public TextMeshProUGUI answerText;
    public AchievementController achievementController;
    
    void Start()
    {
       StartCoroutine(GetQuestion());
       
       if(PlayerPrefs.GetInt("currentLevel") == 1 && PlayerPrefs.GetInt("currentquestion") == 1 ){
            
            string nameOfEvent;
            
            if(PlayerPrefs.GetInt("isMuseum") == 1){
                
                nameOfEvent = "Muziejus";
            }else{
                nameOfEvent = PlayerPrefs.GetString("eventName");
            }
            
            string str = nameOfEvent + " 1 lygis 1 klausimas";
            achievementController.CallAddAchievements(str);
        }
    }
    
    IEnumerator GetQuestion(){
        
        WWWForm form = new WWWForm();
        form.AddField("qrAnswer", PlayerPrefs.GetInt("QrCodeAnswer").ToString());
   
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/getAnswer.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.downloadHandler.text[0] == '0'){
                
                answerText.text = webRequest.downloadHandler.text.Split('\t')[1];
                
            }else{
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
        
    }
    
    public void CallSessionChange(){
        
        StartCoroutine(ChangeSession()); 
        
    }
    
    IEnumerator ChangeSession(){
     
        int level = PlayerPrefs.GetInt("currentLevel");
        int questionNr = PlayerPrefs.GetInt("currentquestion");
        int maxQuestions = PlayerPrefs.GetInt("maxQuestions");
        string placeName = "";   
        
        if(questionNr == maxQuestions){
            
            level = level + 1;
            questionNr = 1;

        }else{
            questionNr += 1;
        }
        
        if(PlayerPrefs.GetInt("isMuseum") == 1){
            placeName = "Muziejus";
        }else{
            placeName = PlayerPrefs.GetString("eventName");
        }
        
        WWWForm form = new WWWForm();
        form.AddField("level", level.ToString());
        form.AddField("question", questionNr.ToString());
        form.AddField("placename", placeName);
        form.AddField("username", DBManager.username);
        
       using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/changeSession.php",form)){
           
           yield return webRequest.SendWebRequest();
           
          if( webRequest.downloadHandler.text == "0" ){
             
            SceneManager.LoadScene("QuestionScene");   
          }else{
              Debug.Log(webRequest.downloadHandler.text);
          }
           
       }
    }
    
}

