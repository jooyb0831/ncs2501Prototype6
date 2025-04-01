using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MainManager : MonoBehaviour
{

    public static MainManager Instance {get; private set;}

    public Color TeamColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);
        try
        {
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        //File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        //Application.persistentDataPath : 어플을 재설치하거나 업데이트하는 동안에 유지되는 데이터를 
        //저장할 수 있는 폴더를 제공하고 파일 이름 savefile.json을 추가함.
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        try
        {
            //File.Exists : .json파일이 존재하는지 확인. 있다면 FileReadAllText통해 콘텐츠 읽음
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                //결과 텍스트가 JsonUtility.From.Json에 전달되어 다시 SaveData인스턴스로 변환

                TeamColor = data.TeamColor;
            }
            else
            {
                Debug.Log($"Failed to load Json : {path}");
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }


    }


}
