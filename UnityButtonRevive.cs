using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class UnityButtonRevive : MonoBehaviour, IUnityAdsListener
{
    //this scriptis called when clicked a button UI
    string gameId = "3840355";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;

    
  
    PlayerMove player;
    SpawnPlayer spawnPlayerScript;
    GameOver gameoverScript;
    SoundsScript sound;

    public GameObject textWarn;
    public GameObject buttonrevie; //button will be clcked

    // Initialize the Ads listener and service:
    void Start()
    {
        sound = gameObject.GetComponent<SoundsScript>();
        Advertisement.Initialize(gameId, testMode); //init ads unity
        spawnPlayerScript = gameObject.GetComponent<SpawnPlayer>(); //getcomponent Spawn of player when finish video
    }
   
    public void ShowRewardedVideo()
    {
        sound.AudioSelect();
        // Check if UnityAds ready calling Show method:
        if (Advertisement.IsReady(myPlacementId))
        {
            Advertisement.AddListener(this);
            Advertisement.Show(myPlacementId); //show rewarded video
        }
        else
        {
            textWarn.SetActive(true);
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!"); //Debug error
            Invoke("DesactiveWarn", 2f);
        }
    }
    public void OnUnityAdsDidFinish(string placement, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) // conditional logic for each ad completion status: reward of player
        {
            //do something 
            gameoverScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameOver>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>(); 
            sound.AudioWin();
            gameoverScript.GameOverDesactive();
            Destroy(player.gameObject);
            spawnPlayerScript.Spawn();
            Advertisement.RemoveListener(this);
            buttonrevie.SetActive(false);
        }
        else if (showResult == ShowResult.Skipped) //if player skip video before finish 
        {
            sound.AudioError();// Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed) //if will have error 
        {
            sound.AudioError();
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
