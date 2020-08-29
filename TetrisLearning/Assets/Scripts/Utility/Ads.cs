using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class Ads : MonoBehaviour, IUnityAdsListener
{
    Button myButton; //
    public string myPlacementId = "rewardedVideo"; // тип рекламы которую мы будем воиспроизводить
    public FortuneWheel fortuneWheel;

    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.interactable = Advertisement.IsReady(myPlacementId);
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);
        Advertisement.AddListener(this);
        Advertisement.Initialize("3790013", false);
    }

    // Implement a function for showing a rewarded video ad:
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
            fortuneWheel.CanRotate = true;
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }
}