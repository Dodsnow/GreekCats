using System;
using System.Collections;
using System.Collections.Generic;
using GameUI;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;
using File = System.IO.File;


public class ResourceManager : MonoBehaviour
{
    public Dictionary<ResourceType, ResourceEntity> ResourceEntityList;
    

    private void Awake()
    {
        ResourceEntityList = new Dictionary<ResourceType, ResourceEntity>();
        foreach (ResourceType resource in Enum.GetValues(typeof(ResourceType)))
        {
            ResourceEntityList.Add(resource, new ResourceEntity(0));
        }
        if (File.Exists(Application.persistentDataPath + "ResourceData.csv"))
        {
            Debug.Log("ResourceData.csv exists");
        }
        else
        { 
            FileStream fileStream = new FileStream(Application.persistentDataPath + "ResourceData.csv", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            Debug.Log(Application.persistentDataPath);
            fileStream.Close();
            File.WriteAllLines(Application.persistentDataPath + "ResourceData.csv", ResourceDataConverter());
            Debug.Log("ResourceData.csv created");
        }
    }

    string[] ResourceDataConverter()
    {
        string[] lines = new string [4];
        int counter = 0;
        foreach (ResourceEntity resource in ResourceEntityList.Values)
        {
            Debug.Log(resource.CurrentValue.ToString());
            lines[counter] = resource.CurrentValue.ToString();
            counter++;
            if (counter >= 4)break;
            
        }

        return lines;


    }
}
