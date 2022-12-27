using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
   //Player data tvarkymas
   public static string name;
   public static string username;
   public static string email;
   
   public static bool LoggedIn{get {return username != null;}}
   
   public static void LogOut(){
       
        username = null;
   }
    
   public static int currentLevel;
   public static int currentQuestion;
    
    //klasimu visas data
    public static List<AchievementData> AchievementList;

    public class AchievementData{
        public int ID {get;set;}
        public string Name {get;set;}
        public string ImageLink {get;set;}
        public string InfoText {get;set;}
        public string Requirement {get;set;}

        public AchievementData(int iD, string name, string imageLink, string infoText, string requirement)
        {
            this.ID = iD;
            this.Name = name;
            this.ImageLink = imageLink;
            this.InfoText = infoText;
            this.Requirement = requirement;
        }
    }

    public static void AddList(List<AchievementData> list)
    {
        AchievementList = list;
    }
}
