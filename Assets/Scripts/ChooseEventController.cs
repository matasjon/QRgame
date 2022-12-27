using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ChooseEventController : MonoBehaviour
{
    //cia padaryt kad pagetintu is bazes naujausia duomenis sito userio kur != 1 (nes vvienetas yra muuziejus)
    //ir veliau gautus duomenis tiesiog prilygint playerprefs
    
    //arba tiesiog patikrinti ar nelygus duomenis 0
    public GameObject continueButton;
    
    public void Start(){
        
        PlayerPrefs.SetInt("isMuseum", 0);
        
        // if(PlayerPrefs.GetInt("EventQRcode") != 0)
        StartCoroutine(checkEventData());
        
    }
    
    IEnumerator checkEventData(){
        
        WWWForm form = new WWWForm();
        
        form.AddField("username", DBManager.username);
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/getEventNameID.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.downloadHandler.text[0] == '0'){
                 
                string eventname = webRequest.downloadHandler.text.Split('\t')[1];
                PlayerPrefs.SetString("eventName", eventname);
                
                continueButton.SetActive(true);
                  
            }else{
                continueButton.SetActive(false);
            }
        }
        
    }
    
    public void ContinueGame(){
        
        PlayerPrefs.SetString("PreviousScene", "ChooseEventScene");
        SceneManager.LoadScene("EventInfoScene");
        
    }
    
    public void GoToScanner(){
        
        PlayerPrefs.SetInt("scanEvent", 1);
        PlayerPrefs.SetInt("isAnswered", 0);
        PlayerPrefs.SetString("PreviousScene", "ChooseEventScene");
        SceneManager.LoadScene("QRScannerScene");   
        
    }

    public void GoBack(){
        
        SceneManager.LoadScene("ChooseGameScene");   
    }
}
