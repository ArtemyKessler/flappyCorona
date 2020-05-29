using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    string gameId = "3623206";
    bool testMode = false;
    const int MAX_HP = 100;

    Button myButton;
    public string myPlacementId = "rewardedVideo";
    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            GameControl.instance.resumeGame();
            GameControl.instance.HPBar.GetComponent<HealthBar>().Heal(MAX_HP);
        }
        else if (showResult == ShowResult.Skipped)
        {
            GameControl.instance.BirdDied();
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
            GameControl.instance.BirdDied();
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogWarning("The ad did not finish due to an error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Optional actions to take when the end-users triggers an ad.");
    }
}
