using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
   

    public static void DeleteData()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.fun");

        try
        {
            File.Delete(path);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    public static void SaveRubies()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "player.fun");

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);

        stream.Close();
    }
    public static bool FileExits()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.fun");

        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            
            return false;
        }
    }
    public static PlayerData LoadRubies()
    {

        string path = Path.Combine(Application.persistentDataPath, "player.fun");

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found" + path);
            return null;
        }

    }
}
