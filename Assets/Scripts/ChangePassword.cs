using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZXing;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ChangePassword : MonoBehaviour
{
    public GameObject dataCanvas;
    public GameObject successCanvas;
    
    public TextMeshProUGUI errorText;
    public TMP_InputField emailInput;
    public TMP_InputField oldPasswordInput;
    public TMP_InputField firsPasswordInput;
    public TMP_InputField secondPasswordInput;
    
    public GameObject emailObject;
    public GameObject oldPassword;
    public TextMeshProUGUI nextButtonText;
    
    private bool fromOptions = false;
    
    public void Start(){
        
        if(PlayerPrefs.GetString("PreviousScene") == "OptionsScene"){
            
            fromOptions = true;
            emailObject.SetActive(false);
            oldPassword.SetActive(true);
        }
        else{
            emailObject.SetActive(true);
            oldPassword.SetActive(false);
        }
        
        dataCanvas.SetActive(true);
        successCanvas.SetActive(false);
        
    }
    
    
    public void CallPasswordUpdate(){
        
        bool dataInputCorrect = checkInputData();
        
        if(dataInputCorrect){
            errorText.text = "";
            StartCoroutine(UpdatePassword());
        }
    }

    IEnumerator UpdatePassword(){
        
        WWWForm form = new WWWForm();
        
        string emailAddress;
        
        if(fromOptions){
            
            emailAddress = DBManager.email;
            
        }else{
            
            emailAddress = emailInput.text;
        }
        
        form.AddField("email", emailAddress);
        form.AddField("password", firsPasswordInput.text);
        
        if(fromOptions){
            form.AddField("oldpassword", oldPasswordInput.text);
        }else{
            form.AddField("oldpassword", "");
        }
        
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://192.168.43.111:8080/qrgamedb/changePassword.php",form)){
            
            yield return webRequest.SendWebRequest();
            
            if(webRequest.downloadHandler.text == "0"){
                
                
                dataCanvas.SetActive(false);
                successCanvas.SetActive(true);
                
                if(fromOptions){
                    nextButtonText.text = "Grįžti";
                }
                else{
                    nextButtonText.text = "Prisijungti";
                }
                
            }else{
                
                errorText.text = webRequest.downloadHandler.text;
            }
        }
    }
    
    public bool checkInputData(){
        
        if(emailInput.text == "" && fromOptions == false){
        
            errorText.text = "Įveskite el. paštą";
            
            return false;
        }
        
        if(oldPasswordInput.text == "" && fromOptions){
        
            errorText.text = "Įveskite esamą slaptažodį";
            
            return false;
        }
        
        if(firsPasswordInput.text == ""){
        
            errorText.text = "Įveskite slaptažodį";
            
            return false;
        }
        
        if(secondPasswordInput.text == ""){
        
            errorText.text = "Pakartokite slaptažodį";
            
            return false;
        }
        
        if(firsPasswordInput.text != secondPasswordInput.text){
            
            errorText.text = "Nesutampa slaptažodžiai";
            
            return false;
            
        }
        
        return true;
    }
    
    public void GoToNextPage(){
        
        if(fromOptions){
            
            SceneManager.LoadScene("OptionsScene");
            
        }else{

            SceneManager.LoadScene("LoginScene");  
        }
    }
    
    public void GoBack(){
        
        SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene"));  
        
    }
}
