using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZXing;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


// Ka reikia gauti:
// Varda
// username
// email
// sesija(jei tokios nera sukurt muziejuj sukurt)


public class LoginController : MonoBehaviour
{
    
    public TextMeshProUGUI errorText;
    public TMP_InputField userName;
    public TMP_InputField password;
    
    public void CallLogin(){
        
        bool dataInputCorrect = checkInputData();
        
        if(dataInputCorrect){
            errorText.text = "";
            StartCoroutine(LoginPlayer());
        }
    }

    IEnumerator LoginPlayer(){
        
        WWWForm form = new WWWForm();
        form.AddField("username", userName.text);
        form.AddField("password", password.text);
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/login.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.downloadHandler.text[0] == '0'){
                
                DBManager.username = userName.text;
                DBManager.name = webRequest.downloadHandler.text.Split('\t')[1];
                DBManager.email = webRequest.downloadHandler.text.Split('\t')[2];
                
                PlayerPrefs.SetString("name", DBManager.name);
                PlayerPrefs.SetString("username", DBManager.username);
                PlayerPrefs.SetString("email",  DBManager.email);
                PlayerPrefs.SetInt("LoggedIn", 1);
                
                SceneManager.LoadScene("MainPage");   
                
            }else{
                
                errorText.text = webRequest.downloadHandler.text;
            }
        }
    }
    
    public bool checkInputData(){
        
        if(userName.text == ""){
        
            errorText.text = "Įveskite slapyvardį";
            
            return false;
        }
        
        if(password.text == ""){
        
            errorText.text = "Įveskite slaptažodį";
            
            return false;
        }
        
        return true;
    }
    
    
    public void GoToForgotPassword(){
        
        PlayerPrefs.SetString("PreviousScene", "LoginScene");
        SceneManager.LoadScene("ForgotPassword");   
    }
    
    public void GoBack(){
        
        SceneManager.LoadScene("StartingScreenScene");   
    }
}
