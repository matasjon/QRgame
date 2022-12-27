using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using ZXing;

public class RegistrationController : MonoBehaviour
{
    public GameObject registration;
    public GameObject login;
    public TextMeshProUGUI errorText;
    public TMP_InputField nameField;
    public TMP_InputField Email;
    public TMP_InputField userName;
    public TMP_InputField password;
    
    // public Button submitButton;
    
    // private static string linkMobile = "http://192.168.43.111:8080/";
    // private static string linkPc = "http://localhost:8080/";
    
    
    public void Start(){
        
        registration.SetActive(true);
        login.SetActive(false);
        
    }
    
    public void CallRegister(){
        
        bool dataInputCorrect = checkInputData();
        
        if(dataInputCorrect){
            errorText.text = "";
            StartCoroutine(Register());
        }
    }
    
    IEnumerator Register(){
        
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("email", Email.text);
        form.AddField("username", userName.text);
        form.AddField("password", password.text);
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/registration.php",form)){
            
            yield return webRequest.SendWebRequest();
            // Debug.Log(webRequest.downloadHandler.text);
            
            if(webRequest.downloadHandler.text == "0"){
                
                registration.SetActive(false);
                login.SetActive(true);
                
            }else{
                
                errorText.text = webRequest.downloadHandler.text;
            }
        }
    }
    
    public bool checkInputData(){
        
        if(nameField.text == ""){
            
            errorText.text = "Įveskite vardą";
            
            return false;
        }
        
        if(Email.text == ""){
        
            errorText.text = "Įveskite el. pašto adresą";
            
            return false;
        }
        
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
    
    
    public void GoToLogin(){
        
        SceneManager.LoadScene("LoginScene");
    }
    
    public void GoToPrevious(){
        
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene"));   
    }
    
}
