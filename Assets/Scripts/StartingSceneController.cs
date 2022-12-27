using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingSceneController : MonoBehaviour
{
    
    
    public void Start(){
        
        if(DBManager.LoggedIn || PlayerPrefs.GetInt("LoggedIn") == 1){
            
            DBManager.name = PlayerPrefs.GetString("name");
            DBManager.username = PlayerPrefs.GetString("username");
            DBManager.email = PlayerPrefs.GetString("email");
            
            PlayerPrefs.SetString("PreviousScene", "MainPage");
            SceneManager.LoadScene("MainPage");  
        }
    }
    
    public void GoToLogin(){
        
        PlayerPrefs.SetString("PreviousScene", "StartingScreenScene");
        SceneManager.LoadScene("LoginScene");   
    }
    
    public void GoToRegister(){
         
        PlayerPrefs.SetString("PreviousScene", "StartingScreenScene");
        SceneManager.LoadScene("RegistrationScene");    
    }
    
}
