using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class UnityAds : MonoBehaviour, IUnityAdsListener
{
    //This scripts is called when player die three times
    string gameId = "3840355"; //ID unity add test
    string myPlacementId = "rewardedVideo"; //video Type of unity adss
    bool testMode = true;


    public GameObject textWarn; //gameobject to show message error or success

    // Initialize the Ads listener and service:
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowRewardedVideo()
    {
        if (PlayerPrefs.HasKey("AdsUnity")) //key unity ads
        {
            if (PlayerPrefs.GetInt("AdsUnity") == 3) //Exemple: after player die three times
            {
                // Check if UnityAds ready before calling Show method:
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

                PlayerPrefs.SetInt("AdsUnity", 1); //Set key on playerprefs
            }
            else
            {
                PlayerPrefs.SetInt("AdsUnity", PlayerPrefs.GetInt("AdsUnity") + 1); //add +1 ao playerprefs count

            }
        }
        else
        {
            PlayerPrefs.SetInt("AdsUnity", 1);
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

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Advertisement.RemoveListener(this); //do something after finish video
    }

    void DesactiveWarn()
    {
        textWarn.SetActive(false);
    }
}
