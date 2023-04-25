using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject chooseWordMenu;
    private BannerView bannerView;

    void Start() 
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    private void OnDisable() 
    {
        if (this.bannerView != null)
        {
            this.bannerView.Hide();
            this.bannerView.Destroy();
        }
    }
    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Clean up banner ad before creating a new one.
        if (this.bannerView != null)
        {
            this.bannerView.Hide();
            this.bannerView.Destroy();
        }

        AdSize adaptiveSize =
                AdSize.GetPortraitAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Top);

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpening;
        this.bannerView.OnAdClosed += this.HandleAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void Retry() 
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void ChangeLength() 
    {
        chooseWordMenu.SetActive(true);
    }

    public void Quit() 
    {
        this.bannerView.Hide();
        this.bannerView.Destroy();
        SceneManager.LoadScene("StartMenu");
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        MonoBehaviour.print(String.Format("Ad Height: {0}, width: {1}",
            this.bannerView.GetHeightInPixels(),
            this.bannerView.GetWidthInPixels()));
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
                "HandleFailedToReceiveAd event received with message: " + args.ToString());
    }

    public void HandleAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    #endregion
}
