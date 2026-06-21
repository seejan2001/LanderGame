using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleMeshText;    
    [SerializeField] private TextMeshProUGUI statsMeshText; 
    [SerializeField] private TextMeshProUGUI nextButtonMeshText;    
    [SerializeField] private Button nextButton; 
    private Action nextButtonClickAction;


    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
           nextButtonClickAction();
        });    
    }

    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;
        nextButton.Select();
        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if(e.landingType == Lander.LandingType.Success)
        {
            titleMeshText.text = "SUCCESSFUL LANDED!";
            nextButtonMeshText.text = "CONTINUE";  
            nextButtonClickAction = GameManager.Instance.GoToNextLevel;
        }
        else
        {
            titleMeshText.text = "<color=#ff0000>CRASHED! </color>";
            nextButtonMeshText.text = "RETRY";  
            nextButtonClickAction = GameManager.Instance.RetryLevel;
        }

        statsMeshText.text =
            Mathf.Round(e.landingSpeed * 2f) + "\n" +
            Mathf.Round(e.dotVector * 100f) + "\n" + 
            "x" + e.scoreMultiplier + "\n" + 
            e.score;
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
