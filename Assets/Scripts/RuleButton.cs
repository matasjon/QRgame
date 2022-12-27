using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class RuleButton : MonoBehaviour, IPointerClickHandler
{
        public void OnPointerClick(PointerEventData eventData)
        {
            SceneManager.LoadScene("Rules");
        }
}
