using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : Singleton<IAPManager>, IStoreListener
{
    private static IStoreController m_StoreController;          
    private static IExtensionProvider m_StoreExtensionProvider; 

    public string STAR_200 = "star_200";
    public string STAR_500 = "star_500";
    public string NO_ADS = "no_ads";

    public string PREMIUM_BALL_1 = "premium_ball_1";
    public string PREMIUM_BALL_2 = "premium_ball_2";
    public string PREMIUM_BALL_3 = "premium_ball_3";

    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(STAR_200, ProductType.Consumable);
        builder.AddProduct(STAR_500, ProductType.Consumable);
        builder.AddProduct(NO_ADS, ProductType.NonConsumable);
        builder.AddProduct(PREMIUM_BALL_1, ProductType.NonConsumable);
        builder.AddProduct(PREMIUM_BALL_2, ProductType.NonConsumable);
        builder.AddProduct(PREMIUM_BALL_3, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }


    public bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void Buy200Stars()
    {
        BuyProductID(STAR_200);
    }
    public void Buy500Stars()
    {
        BuyProductID(STAR_500);
    }
    public void BuyNoAds()
    {
        BuyProductID(NO_ADS);
    }
    public void BuyPremiumball1()
    {
        BuyProductID(PREMIUM_BALL_1);
    }
    public void BuyPremiumball2()
    {
        BuyProductID(PREMIUM_BALL_2);
    }
    public void BuyPremiumball3()
    {
        BuyProductID(PREMIUM_BALL_3);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }

            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }

        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public string GetProducePriceFromStore(string id)
    {
        if(m_StoreController != null && m_StoreController.products != null)
        {
            return m_StoreController.products.WithID(id).metadata.localizedPriceString;
        }
        return "";
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, STAR_200, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            SaveAndLoad.increasePlayerStars(200);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, STAR_500, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            SaveAndLoad.increasePlayerStars(500);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, NO_ADS, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            SaveAndLoad.removeAds();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PREMIUM_BALL_1, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            SaveAndLoad.setSkinIsOwned(15);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PREMIUM_BALL_2, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            SaveAndLoad.setSkinIsOwned(16);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PREMIUM_BALL_3, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            SaveAndLoad.setSkinIsOwned(17);
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
