using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopContainer;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject buttonBuyTrue;
    [SerializeField] private GameObject buttonBuyFalse;

    [SerializeField] private Sprite ownedSkin;
    [SerializeField] private Sprite selectedSkin;
    [SerializeField] private Sprite notOwnedSkin;
    [SerializeField] private Sprite pressedSkin;

    private int pressedSkinIndex = 0;
    void Start()
    {
        pressedSkinIndex = SaveAndLoad.getSelectedSkin();
        StartCoroutine(loadPremiumPrices());
        setShopItems();
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
        for(int i = 0; i < panel.transform.childCount; i++)
        {
            if (SaveAndLoad.skinIsOwned(i))
            {
                panel.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = ownedSkin;
                panel.transform.GetChild(i).transform.GetChild(2).gameObject.SetActive(false);
                panel.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                panel.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = notOwnedSkin;
                panel.transform.GetChild(i).transform.GetChild(2).gameObject.SetActive(true);
                panel.transform.GetChild(i).transform.GetChild(3).gameObject.SetActive(false);
            }
            
        }
        buttonBuyTrue.SetActive(false);
        buttonBuyFalse.SetActive(false);
        if (pressedSkinIndex == SaveAndLoad.getSelectedSkin())
        {
            panel.transform.GetChild(SaveAndLoad.getSelectedSkin()).transform.GetChild(0).GetComponent<Image>().sprite = selectedSkin;
        }
        else
        {
            if (SaveAndLoad.skinIsOwned(pressedSkinIndex))
            {
                SaveAndLoad.setSelectedSkin(pressedSkinIndex);
                SpawnBallInMenu.instance.spawnBall();
                panel.transform.GetChild(pressedSkinIndex).transform.GetChild(0).GetComponent<Image>().sprite = selectedSkin;
            }
            else
            {
                if(pressedSkinIndex != 13 && pressedSkinIndex != 14)
                {
                    panel.transform.GetChild(SaveAndLoad.getSelectedSkin()).transform.GetChild(0).GetComponent<Image>().sprite = selectedSkin;
                    panel.transform.GetChild(pressedSkinIndex).transform.GetChild(0).GetComponent<Image>().sprite = pressedSkin;

                    if (SaveAndLoad.getSkinCost(pressedSkinIndex) <= SaveAndLoad.getPlayerStars())
                    {
                        buttonBuyTrue.SetActive(true);
                        buttonBuyFalse.SetActive(false);
                    }
                    else
                    {
                        buttonBuyTrue.SetActive(false);
                        buttonBuyFalse.SetActive(true);
                    }
                }
                
            }
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

    public void buyItem()
    {     
        if(pressedSkinIndex == 15)
        {
            IAPManager.Instance.BuyPremiumball1();
        }
        else if (pressedSkinIndex == 16)
        {
            IAPManager.Instance.BuyPremiumball2();
        }
        else if (pressedSkinIndex == 17)
        {
            IAPManager.Instance.BuyPremiumball3();
        }
        else if (SaveAndLoad.getSkinCost(pressedSkinIndex) <= SaveAndLoad.getPlayerStars())
        {
            SaveAndLoad.setSkinIsOwned(pressedSkinIndex);
            SaveAndLoad.decreasePlayerStars(SaveAndLoad.getSkinCost(pressedSkinIndex));
            tryUnlockSpecialBall();
            
        }
        setShopItems();
    }

    private void tryUnlockSpecialBall()
    {
        bool unlock = true;
        for(int i = 0; i < 13; i++)
        {
            if (!SaveAndLoad.skinIsOwned(i))
            {
                unlock = false;
            }
        }

        if (unlock)
        {
            SaveAndLoad.setSkinIsOwned(13);
            SaveAndLoad.setSkinIsOwned(14);
        }
    }

    public void closeShop()
    {
        GetComponent<Animator>().Play("closeShop");
    }

    public void offShop()
    {
        shopContainer.SetActive(false);
    }

    public void changePressedSkinIndex(int index)
    {
        pressedSkinIndex = index;
        setShopItems();
    }

    private IEnumerator loadPremiumPrices()
    {
        while (!IAPManager.Instance.IsInitialized())
        {
            yield return null;
        }
        panel.transform.GetChild(15).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.PREMIUM_BALL_1);
        panel.transform.GetChild(16).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.PREMIUM_BALL_2);
        panel.transform.GetChild(17).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.PREMIUM_BALL_3);
    }
}
