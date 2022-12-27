using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class QuestionsController : MonoBehaviour
{
    
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI questionNr;
    public TextMeshProUGUI questionText;
    
    public GameObject backButton;
    public GameObject scanButton;
    
    
    public void Start()
    {       
        StartCoroutine(loadQuestions());
    }
    
    IEnumerator loadQuestions(){
        
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        
        string eventName = "";
        
        if(PlayerPrefs.GetInt("isMuseum") == 1){
            eventName = "Muziejus";
        }else{
            eventName = PlayerPrefs.GetString("eventName");
        }
        
        form.AddField("event", eventName);
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/loadQuestions.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.downloadHandler.text == "end"){
                //cia padaryt kad pereitu i kita scena, kur paraso kad perejote levely ir iduoda achievementa ir su date padaryt
                SceneManager.LoadScene("EndGameScene");   
                
            }
            else if(webRequest.downloadHandler.text[0] == '0'){
                
                string levelNr = webRequest.downloadHandler.text.Split('\t')[1];
                levelText.text = "Lygis\n" + levelNr;
                
                string qNr = webRequest.downloadHandler.text.Split('\t')[2];
                string maxQuestionsNR = webRequest.downloadHandler.text.Split('\t')[3];
                questionNr.text = "Klausimas\n" + qNr + "/" + maxQuestionsNR;
                
                questionText.text = webRequest.downloadHandler.text.Split('\t')[4];
                
                int qrCode = int.Parse(webRequest.downloadHandler.text.Split('\t')[5]);
                int isAnswered = int.Parse(webRequest.downloadHandler.text.Split('\t')[6]);
             
                PlayerPrefs.SetInt("currentLevel", int.Parse(levelNr));
                PlayerPrefs.SetInt("currentquestion", int.Parse(qNr));
                PlayerPrefs.SetInt("maxQuestions", int.Parse(maxQuestionsNR));
                PlayerPrefs.SetInt("QrCodeAnswer", qrCode);
                PlayerPrefs.SetInt("isAnswered", isAnswered);
                
                backButton.SetActive(true);
                scanButton.SetActive(true);
                
            }else{
                
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }
    
    public void GoBack(){
        if(PlayerPrefs.GetInt("isMuseum") == 1){
        
            SceneManager.LoadScene("ChooseGameScene");   
        }else{
            SceneManager.LoadScene("ChooseEventScene");
        }
    }
    
    public void GoToScanner(){
        
        PlayerPrefs.SetString("PreviousScene", "QuestionScene");
        
        SceneManager.LoadScene("QRScannerScene");
    }
}
