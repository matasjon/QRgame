using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZXing;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI emailText;
    
    void Start()
    {
        nameText.text = "Vardas: " + DBManager.name;
        usernameText.text = "Slapyvardis: " + DBManager.username;
        emailText.text = "El. paštas: " + DBManager.email;
    }
    
    public void LogOut(){
        
        PlayerPrefs.SetInt("LoggedIn", 0);
        DBManager.LogOut();
        PlayerPrefs.SetString("PreviousScene", "StartingScreenScene");
        SceneManager.LoadScene("StartingScreenScene");   
    }
    
    public void GoToDataChange(){
        PlayerPrefs.SetString("PreviousScene", "OptionsScene");
        SceneManager.LoadScene("ChangeDataScene");   
    }
     
    public void GoToPasswordChange(){
        PlayerPrefs.SetString("PreviousScene", "OptionsScene");
        SceneManager.LoadScene("ForgotPassword");   
    }
     
    public void GoBack(){
        
        SceneManager.LoadScene("MainPage");   
    }
}
