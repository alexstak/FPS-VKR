using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public SC_DamageReceiver player;

    private DataStructure loadedData;

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/PlayerSaveData.dat");
        SaveData data = new SaveData();
        data.playerPoints = player.getPlayerPoints();
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
          + "/PlayerSaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/PlayerSaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            player.setPlayerPoints(data.playerPoints);
            Debug.Log("Game data loaded!");
            ApplyData();
        }
        else
            Debug.LogError("There is no save data!");
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/PlayerSaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/PlayerSaveData.dat");
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }

    public void saveData()
    {
        PlayerPrefs.SetInt("PlayerPoints", player.getPlayerPoints());
        Debug.Log("Save");
        Debug.Log(player.getPlayerPoints());
        PlayerPrefs.Save();

        Debug.Log("TestLoad");
        Debug.Log(PlayerPrefs.GetInt("PlayerPoints"));
    }

    public DataStructure loadData()
    {
        Debug.Log("StartLoad");
        if (PlayerPrefs.HasKey("PlayerPoints"))
        {
            loadedData.points = PlayerPrefs.GetInt("PlayerPoints");
            Debug.Log("Load");
            Debug.Log(loadedData.points);
            ApplyData();
        }
        return loadedData;
    }

    private void ApplyData()
    {
        player.setPlayerPoints(loadedData.points);
    }
}

[Serializable]
class SaveData
{
    public int playerPoints;
}
