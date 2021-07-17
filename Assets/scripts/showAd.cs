using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;

public class showAd : MonoBehaviour
{
  string adUnitId="ca-app-pub-3940256099942544/5224354917";
  private RewardedAd rewardedAd;
  public hangmanRenderer hr;
  public GameObject loading;
  public Text erroText;
  bool canClose=true;

  void Start(){
    loading.SetActive(false);
    MobileAds.Initialize(initStatus => { });
  }

  public IEnumerator error(string msg){
    erroText.text=msg;
    yield return new WaitForSeconds(2.5f);
    erroText.text="Ad is loading \nplease wait";
    loading.SetActive(false);
  }

  public void CreateAndLoadRewardedAd()
  {
    loading.SetActive(true);
    if(Application.internetReachability == NetworkReachability.NotReachable)
    {
           StartCoroutine(error("Error. Check internet connection!"));
    }

      rewardedAd = new RewardedAd(adUnitId);

      rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded;
      rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;
      rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;
      this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
      this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

      // Create an empty ad request.
      AdRequest request = new AdRequest.Builder().Build();
      // Load the rewarded ad with the request.
      rewardedAd.LoadAd(request);
  }

  public void RewardedAd_OnAdClosed(object sender, System.EventArgs e)
  {
    loading.SetActive(false);
  }

  public void RewardedAd_OnUserEarnedReward(object sender, Reward e)
  {
      loading.SetActive(false);
      hr.hint();
  }

  public void RewardedAd_OnAdLoaded(object sender, System.EventArgs e)
  {
      Debug.Log("heh loaded");
      rewardedAd.Show();
  }

  public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args){
    StartCoroutine(error("Load failed!"));
    CreateAndLoadRewardedAd();
  }

  public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
   {
     StartCoroutine(error("Showing failed!"));
     CreateAndLoadRewardedAd();
     MonoBehaviour.print("HandleRewardedAdFailedToShow event received with message: "+ args.Message);
   }

   public void closeArrert(){
     if (!canClose) {
      return;
     }
     loading.SetActive(false);
   }


}
