using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private MapData Map;
    private const string _folderPath = "Maps";
    public GameObject NoteTemplate;
    public float NoteTemplateTargetSize;
    public float ApproachSpeed;
    
    private string[] noteEntries;
    private float previousAppearTime = 0;
    

    public void Awake()
    {
        Map = Instantiate(Resources.Load<MapData>(_folderPath + "/" + StaticData.MapNameKeep));
    }

    private string[] TranslateMapData()
    {
        noteEntries = Map.data.Split(',');
        foreach(string a in noteEntries) Debug.LogWarning(a);
        return noteEntries;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void NoteAppear(GameObject note)
    {
        RectTransform rt = note.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(NoteTemplateTargetSize, NoteTemplateTargetSize);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator InitGame()
    {
        foreach (string noteData in noteEntries)
        {
            string[] noteParams = noteData.Split('|');
            
            foreach(string a in noteParams) Debug.LogWarning(a);
            float xPos = float.Parse(noteParams[0]);
            float yPos = float.Parse(noteParams[1]);
            string combinedPosition = $"{float.Parse(noteParams[0])}{float.Parse(noteParams[1])}";
            GameObject positioner = GameObject.Find(combinedPosition);
            Vector3 objPosition = positioner.transform.position;
            Debug.Log(objPosition);
            
            float appearTime = float.Parse(noteParams[2]);

            GameObject note = Instantiate(NoteTemplate, objPosition, Quaternion.identity);
            NoteAppear(note);
            
            float timeDifference = appearTime - previousAppearTime;
            Debug.LogWarning(timeDifference);
            yield return new WaitForSeconds(timeDifference/1000);
            previousAppearTime = appearTime;
        }
    }
    
    public void Start()
    {
        Debug.Log(Map.data);
        
        Debug.Log("game");
        TranslateMapData();
        StartCoroutine(InitGame());
        //TODO Musicplay

    }
}
