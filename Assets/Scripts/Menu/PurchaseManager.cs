using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseManager : MonoBehaviour
{
    [SerializeField] private GameObject shopContainer;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject buttonBuyTrue;
    [SerializeField] private GameObject buttonBuyFalse;

    [SerializeField] private Sprite ownedItem;
    [SerializeField] private Sprite selectedItem;
    [SerializeField] private Sprite notOwnedItem;

    [SerializeField] private TextMeshProUGUI stars200Price;
    [SerializeField] private TextMeshProUGUI stars500Price;
    [SerializeField] private TextMeshProUGUI noAdsPrice;

    private int selectedIndex = 0;

    private void Start()
    {
        setShopItems();
        StartCoroutine(loadPriceRoutine());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && shopContainer.activeSelf)
        {
            closeShop();
        }
    }
    private void setShopItems()
    {
        StartCoroutine(loadShop());
    }

    public void buyItem()
    {
        if(selectedIndex == 0)
        {
            IAPManager.Instance.Buy200Stars();
        }
        else if (selectedIndex == 1)
        {
            IAPManager.Instance.Buy500Stars();
        }
        else if (selectedIndex == 2)
        {
            IAPManager.Instance.BuyNoAds();
        }
    }

    public void openShop()
    {
        shopContainer.SetActive(true);
        GetComponent<Animator>().Play("openShop");
        panel.transform.position = new Vector3(panel.transform.position.x,
                                               0,
                                               panel.transform.position.z);
    }

    public void closeShop()
    {
        GetComponent<Animator>().Play("closeShop");
    }

    public void offShop()
    {
        shopContainer.SetActive(false);
    }

    public void changePressedItemIndex(int index)
    {
        selectedIndex = index;
        setShopItems();
    }

    private IEnumerator loadPriceRoutine()
    {
        while (!IAPManager.Instance.IsInitialized())
        {
            yield return new WaitForSeconds(0.5f);
        }
        stars200Price.text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.STAR_200);
        stars500Price.text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.STAR_500);
        noAdsPrice.text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.NO_ADS);
    }

    private IEnumerator loadShop()
    {
        while (!IAPManager.Instance.IsInitialized())
        {
            yield return null;
        }
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            panel.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = notOwnedItem;
        }
        buttonBuyTrue.SetActive(true);
        buttonBuyFalse.SetActive(false);

        if (!SaveAndLoad.playerHaveAds())
        {
            panel.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = ownedItem;
            panel.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(true);
            panel.transform.GetChild(2).transform.GetChild(4).gameObject.SetActive(false);
        }
        if (selectedIndex == 2)
        {
            if (!SaveAndLoad.playerHaveAds())
            {
                buttonBuyTrue.SetActive(false);
                buttonBuyFalse.SetActive(true);
            }
        }
        panel.transform.GetChild(selectedIndex).transform.GetChild(0).GetComponent<Image>().sprite = selectedItem;
    }

}
