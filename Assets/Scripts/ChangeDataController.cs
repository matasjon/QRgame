using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using ZXing;

public class ChangeDataController : MonoBehaviour
{
    public GameObject changeCanvas;
    public GameObject successCanvas;
    public TextMeshProUGUI errorText;
    public TMP_InputField namefield;
    public TMP_InputField email;
    public TMP_InputField username;
    public TMP_InputField password;
    
    // private static string linkMobile = "http://192.168.43.111:8080/";
    // private static string linkPc = "http://localhost:8080/";
    
    
    public void Start(){
        
        
        namefield.text = DBManager.name;
        username.text = DBManager.username;
        email.text = DBManager.email;
        
        changeCanvas.SetActive(true);
        successCanvas.SetActive(false);
        
    }
    
    public void CallUserDataUpdate(){
        
        bool dataInputCorrect = checkInputData();
        
        if(dataInputCorrect){
            errorText.text = "";
            StartCoroutine(UpdateUserData());
        }
    }

    IEnumerator UpdateUserData(){
        
        WWWForm form = new WWWForm();
        
        form.AddField("oldusername", DBManager.username);
        form.AddField("newusername", username.text);
        form.AddField("email", email.text);
        form.AddField("name", namefield.text);
        form.AddField("password", password.text);
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/changeUserData.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.downloadHandler.text == "0"){
                
                
                changeCanvas.SetActive(false);
                successCanvas.SetActive(true);
                
                DBManager.name = namefield.text;
                DBManager.username = username.text;
                DBManager.email = email.text;
                
                PlayerPrefs.SetString("name",  DBManager.name);
                PlayerPrefs.SetString("username",  DBManager.username);
                PlayerPrefs.SetString("email",  DBManager.email);
                
            }else{
                
                errorText.text = webRequest.downloadHandler.text;
            }
        }
    }
    
    public bool checkInputData(){
        
        if(namefield.text == ""){
        
            errorText.text = "Įveskite vardą";
            
            return false;
        }
        
        if(username.text == ""){
        
            errorText.text = "Įveskite slapyvardį";
            
            return false;
        }
        
        if(email.text == ""){
        
            errorText.text = "Įveskite el. paštą";
            
            return false;
        }
        
        if(namefield.text == DBManager.name && username.text == DBManager.username && email.text == DBManager.email){
            
            errorText.text = "Duomenys nepakeisti";
            
            return false;
        }
        
        return true;
    }
    
    public void GoToNextPage(){
        
            
        SceneManager.LoadScene("OptionsScene");
            
    }
    
    public void GoToPrevious(){
        
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene"));   
    }
    
}
