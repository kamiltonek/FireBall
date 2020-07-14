using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : Singleton<AdController>, IUnityAdsListener
{
    private string storeID = "3553436";

    private string rewardedVideoAd = "rewardedVideo";
    private string videoAd = "video";
    private string bannerAd = "banner";

    private bool hideBanner;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(storeID, true);   
        Debug.Log("Initialize Ads ...");
    }

    public void showVideo()
    {
        if (SaveAndLoad.playerHaveAds())
        {
            Debug.Log("Show video ...");
            Advertisement.Show(videoAd);
        } 
    }

    public void showBanner()
    {
        if (SaveAndLoad.playerHaveAds())
        {
            hideBanner = false;
            StartCoroutine(ShowBannerWhenReady());
        }    
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(bannerAd))
        {
            Debug.Log(Advertisement.isInitialized);
            Debug.Log("Try to show banner");
            yield return new WaitForSeconds(0.2f);
        }

        if (!hideBanner)
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show(bannerAd);
        } 
    }

    public void destroyBanner()
    {
        hideBanner = true;
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide();
        }
    }

    public bool videoIsReady()
    {
        if (Advertisement.IsReady(videoAd))
        {
            return true;
        }
        return false;
    }

    public void showRewardedVideo()
    {
        Advertisement.Show(rewardedVideoAd);
    }
    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public bool rewardedVideoIsReady()
    {
        if (Advertisement.IsReady(rewardedVideoAd))
        {
            return true;
        }
        return false;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            Debug.LogWarning("User will be rewarded.");
            SaveAndLoad.increasePlayerStars(GameInfo.instance.stars * 2);
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.LogWarning("Do not reward the user for skipping the ad.");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
}
