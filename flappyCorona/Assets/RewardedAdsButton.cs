using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener
{
    string gameId = "3623206";
    bool testMode = false;
    const int MAX_HP = 100;

    Button myButton;
    public string myPlacementId = "rewardedVideo";
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LOL");
        myButton = GetComponent<Button>();
        Advertisement.Load(myPlacementId, this);

        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        Advertisement.Initialize(gameId, testMode, this);
    }

    void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId, this);
        myButton.interactable = false;
        Advertisement.Load(myPlacementId, this);
    }

    // Implement IUnityAdsListener interface methods:
    // public void OnUnityAdsReady(string placementId)
    // {
    //     // If the ready Placement is rewarded, activate the button: 
    //     if (placementId == myPlacementId)
    //     {
    //         myButton.interactable = true;
    //     }
    // }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogWarning("The ad did not finish due to an error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Optional actions to take when the end-users triggers an ad.");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure.");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart.");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick.");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("showed ad.");
        if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Complete ad show ad.");
            GameControl.instance.resumeGame();
            GameControl.instance.HPBar.GetComponent<HealthBar>().Heal(MAX_HP);
        }
        else if (showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED))
        {
            GameControl.instance.BirdDied();
        }
        else if (showCompletionState.Equals(UnityAdsShowCompletionState.UNKNOWN))
        {
            Debug.LogWarning("The ad did not finish due to an error.");
            GameControl.instance.BirdDied();
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        myButton.interactable = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        myButton.interactable = false;
        Debug.Log("Failed to load an ad.");
    }

    public void OnInitializationComplete()
    {
        Debug.Log("OnInitializationComplete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("OnInitializationFailed.");
    }
}
