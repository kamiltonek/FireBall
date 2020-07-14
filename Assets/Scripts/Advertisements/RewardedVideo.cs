using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedVideo : MonoBehaviour
{
    public void showVideo()
    {
        AdController.Instance.showRewardedVideo();
        gameObject.SetActive(false);
    }
}
