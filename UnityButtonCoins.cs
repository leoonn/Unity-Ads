using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class UnityButtonCoins : MonoBehaviour, IUnityAdsListener
{
   //this scriptis called when clicked a button UI
    string gameId = "3840355";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;
   
    SoundsMenu soundmenuScript;
    CoinsManager coinsScript;

    public GameObject textWarn; //message error or success

    // Initialize the Ads listener and service:
    void Start()
    {
        Advertisement.Initialize(gameId, testMode); //init ads unity
        coinsScript = GameObject.Find("CoinsManager").GetComponent<CoinsManager>(); //get script coin being exemplo
        coinsScript.GetCoins(); //get coins of player when init game
        soundmenuScript = gameObject.GetComponent<SoundsMenu>(); //script sound for feedback to player
    }
   
    public void ShowRewardedVideo()
    {
        soundmenuScript.AudioSelect();
        // Check if UnityAds ready calling Show method:
        if (Advertisement.IsReady(myPlacementId))
        {
            Advertisement.AddListener(this);
            Advertisement.Show(myPlacementId); //start video ads
          
        }
        else
        {
            textWarn.SetActive(true);
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
            Invoke("DesactiveWarn", 2f);
        }
    }
    public void OnUnityAdsDidFinish(string placement, ShowResult showResult)
    {
        // conditional logic for each ad completion status: reward of player
        if (showResult == ShowResult.Finished) 
        {
            //exemple of reward player
            soundmenuScript.AudioCoins(); //feedback
            coinsScript.coins += 25; //add player coins on finished video
            coinsScript.SetCoins(); //logic script set coins with player prefs
            coinsScript.GetCoins(); //logic script get coins with player prefs
            Debug.Log("Video fished" + showResult);
            Advertisement.RemoveListener(this);
        }
        else if (showResult == ShowResult.Skipped) //if player skip video before finish 
        {
            soundmenuScript.AudioError();

        }
        else if (showResult == ShowResult.Failed) //if will have error 
        {
            soundmenuScript.AudioError();
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }


    public void OnUnityAdsReady(string placementId)
    {
        //do something when unityads was ready
    }

    public void OnUnityAdsDidError(string message)
    {
       //do something after error
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //do something after start video
    }
    void DesactiveWarn()
    {
        textWarn.SetActive(false);

    }
}
