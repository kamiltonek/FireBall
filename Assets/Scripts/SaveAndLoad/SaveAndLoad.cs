using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveAndLoad
{
    private static readonly string PLAYER_FILENAME = Application.persistentDataPath + "/playerINFO.dat";
    
    public static int getPlayerStars()
    {
        if (File.Exists(PLAYER_FILENAME))
        {
            return getPlayerData().StarAmount;
        }
        else
        {
            saveGame();
            return getPlayerStars();
        }
    }
    public static bool allowVibrate()
    {
        if (File.Exists(PLAYER_FILENAME))
        {
            return getPlayerData().AllowVibrate;
        }
        else
        {
            saveGame();
            return allowVibrate();
        }
    }
    public static void changeAllowVibrate()
    {
        if (File.Exists(PLAYER_FILENAME))
        {
            PlayerData data = getPlayerData();
            data.AllowVibrate = !data.AllowVibrate;
            saveGame(data);
        }
        else
        {
            saveGame();
            changeAllowVibrate();
        }
    }
    public static void increasePlayerStars(int value)
    {
        if(value > 0)
        {
            if (File.Exists(PLAYER_FILENAME))
            {
                PlayerData data = getPlayerData();
                data.StarAmount += value;
                saveGame(data);
            }
            else
            {
                saveGame();
                increasePlayerStars(value);
            }
        }
    }
    public static void decreasePlayerStars(int value)
    {
        if (value > 0)
        {
            if (File.Exists(PLAYER_FILENAME))
            {
                PlayerData data = getPlayerData();
                data.StarAmount -= value;
                saveGame(data);
            }
            else
            {
                saveGame();
                decreasePlayerStars(value);
            }
        }
    }
    public static int getBestScore()
    {
        if (File.Exists(PLAYER_FILENAME))
        {
            return getPlayerData().BestScore;
        }
        else
        {
            saveGame();
            return getBestScore();
        }
    }
    public static void setBestScore(int value)
    {
        if (value > 0)
        {
            if (File.Exists(PLAYER_FILENAME))
            {
                PlayerData data = getPlayerData();
                data.BestScore = value;
                saveGame(data);
            }
            else
            {
                saveGame();
                setBestScore(value);
            }
        }
    }
    public static int getSelectedSkin()
    {
        if (File.Exists(PLAYER_FILENAME))
        {
            return getPlayerData().SelectedSkin;
        }
        else
        {
            saveGame();
            return getSelectedSkin();
        }
    }
    public static void setSelectedSkin(int value)
    {
        if (value >= 0)
        {
            if (File.Exists(PLAYER_FILENAME))
            {
                PlayerData data = getPlayerData();
                data.SelectedSkin = value;
                saveGame(data);
            }
            else
            {
                saveGame();
                setSelectedSkin(value);
            }
        }
    }
    public static void setSkinIsOwned(int index)
    {
        if (File.Exists(PLAYER_FILENAME))
        {
            PlayerData data = getPlayerData();
            data.SkinList.Find(x => x.Index == index).Owned = true;
            saveGame(data);
        }
        else
        {
            saveGame();
            setSkinIsOwned(index);
        }
    }
    public static bool skinIsOwned(int index)
    {
        if (File.Exists(PLAYER_FILENAME))
        {
            return getPlayerData().SkinList.Find(x => x.Index == index).Owned;
        }
        else
        {
            saveGame();
            return skinIsOwned(index);
        }
    }
    public static int getSkinCost(int index)
    {
        if (File.Exists(PLAYER_FILENAME))
        {
            return getPlayerData().SkinList.Find(x => x.Index == index).Cost;
        }
        else
        {
            saveGame();
            return getSkinCost(index);
        }
    }
    public static bool playerHaveAds()
    {
        if (File.Exists(PLAYER_FILENAME))
        {
            return getPlayerData().HaveAds;
        }
        else
        {
            saveGame();
            return playerHaveAds();
        }
    }
    public static void removeAds()
    {
        if (File.Exists(PLAYER_FILENAME))
        {
            PlayerData data = getPlayerData();
            data.HaveAds = false;
            saveGame(data);         }
        else
        {
            saveGame();
            removeAds();
        }
    }
    private static PlayerData getPlayerData()
    {
        FileStream saveFile = null;
        PlayerData myData = null;
        try
        {
            BinaryFormatter binaryConverter = new BinaryFormatter();
            saveFile = new FileStream(PLAYER_FILENAME, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            myData = binaryConverter.Deserialize(saveFile) as PlayerData;
            myData = checkPlayerData(myData);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            if (saveFile != null)
            {
                saveFile.Close();
            }
        }

        return myData;
    }
    private static void saveGame()
    {
        FileStream saveFile = null;
        try
        {
            BinaryFormatter binaryConverter = new BinaryFormatter();
            saveFile = new FileStream(PLAYER_FILENAME, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            PlayerData pd = new PlayerData();
            pd = checkPlayerData(pd);
            binaryConverter.Serialize(saveFile, pd);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            if (saveFile != null)
            {
                saveFile.Close();
            }
        }
    }
    private static void saveGame(PlayerData pd)
    {
        FileStream saveFile = null;
        try
        {
            BinaryFormatter binaryConverter = new BinaryFormatter();
            saveFile = new FileStream(PLAYER_FILENAME, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            pd = checkPlayerData(pd);
            binaryConverter.Serialize(saveFile, pd);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            if (saveFile != null)
            {
                saveFile.Close();
            }
        }
    }
    private static PlayerData checkPlayerData(PlayerData pd)
    {
        List<Skin> skinList = new List<Skin>();
        skinList.Add(new Skin(0, true, 0));         //1
        skinList.Add(new Skin(1, true, 0));         //2
        skinList.Add(new Skin(2, true, 0));         //3
        skinList.Add(new Skin(3, false, 50));       //4
        skinList.Add(new Skin(4, false, 50));       //5
        skinList.Add(new Skin(5, false, 100));      //6
        skinList.Add(new Skin(6, false, 100));      //7
        skinList.Add(new Skin(7, false, 100));      //8
        skinList.Add(new Skin(8, false, 50));       //9
        skinList.Add(new Skin(9, false, 100));      //10
        skinList.Add(new Skin(10, false, 100));     //11
        skinList.Add(new Skin(11, false, 100));     //12
        skinList.Add(new Skin(12, false, 100));     //13
        skinList.Add(new Skin(13, false, 0));       //14
        skinList.Add(new Skin(14, false, 0));       //15
        skinList.Add(new Skin(15, false, 0));       //16
        skinList.Add(new Skin(16, false, 0));       //17
        skinList.Add(new Skin(17, false, 0));       //18

        if (pd.SkinList.Count < skinList.Count)
        {
            for(int i = pd.SkinList.Count; i <skinList.Count; i++)
            {
                pd.SkinList.Add(skinList[i]);
            }
        }

        return pd;
    }
}
