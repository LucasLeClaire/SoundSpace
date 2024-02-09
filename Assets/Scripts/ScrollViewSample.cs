using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSample : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _prefabListItem;
    
    [Header("Scroll view Events")]
    [SerializeField] private ItemButtonEvent _eventItemClicked;
    [SerializeField] private ItemButtonEvent _eventItemOnSelect;
    [SerializeField] private ItemButtonEvent _eventItemOnSubmit;
    
    [Header("Default Selected Index")]
    [SerializeField] private int _defaultSelectedIndex = 0;
    
    [Header("For test")]
    [SerializeField] private int _testButtonCount = 1;
    
    [SerializeField] public Color Color1;
    [SerializeField] public Color Color2;
    private int _iteration = 0;
    
    private const string _folderPath = "Assets/Resources/Maps";
    

    // private void Start()
    // {
    //     
    //     Debug.Log("a");
    //     if (_testButtonCount > 0)
    //     {
    //         Debug.Log("b");
    //         InitiateItems(_testButtonCount);
    //     }
    //     else
    //     {
    //         //TODO load un beau menu pas de maps
    //     }
    // }

    private void Start()
    {
        string[] assetPaths = AssetDatabase.FindAssets("t:ScriptableObject", new[] { _folderPath });
        Debug.LogWarning("INIT");
        foreach (string assetPath in assetPaths)
        {
            Debug.Log("+1 SO");
            MapData currentMap = AssetDatabase.LoadAssetAtPath<MapData>(AssetDatabase.GUIDToAssetPath(assetPath));
            CreateItem(currentMap);
        }
        Debug.Log("ENDED");
        
        //--TEST
        // for (int i = 0; i < count; i++)
        // {
        //     Debug.Log(i);
        //     CreateItem("Map: " +i);
        // }
    }
    

    private void UpdateAllButtonNavigationalReferences()
    {
        ItemButton[] children = _content.transform.GetComponentsInChildren<ItemButton>();
        // ReSharper disable once RedundantJumpStatement
        if (children.Length < 2) return;
        //TODO Followup de la camera sur la selection (si je fini jouable aux touches fléchées un jour)
    }

    private Color defineDifficultyColor(int difficulty)
    {
        if (difficulty == 1)
        {
            return Color.green;
        } else if (difficulty == 2)
        {
            return Color.yellow;
        } else if (difficulty == 3)
        {
            return Color.red;
        } else if (difficulty == 4)
        {
            return Color.magenta;
        } else
        {
            return Color.cyan; 
        }
    }

    private int getNotesNumber(MapData map)
    {
        return 0; //TODO get from main game
    }

    private ItemButton CreateItem(MapData map)
    {
        _iteration++;
        Debug.Log("+1 create");
        RectTransform go; //GameObject
        ItemButton item;
        go = Instantiate(_prefabListItem, Vector3.zero, Quaternion.identity);
        Transform transform1;
        (transform1 = go.transform).SetParent(_content.transform);
        transform1.localScale = new Vector3(1f, 1f, 1f);
        transform1.localPosition = new Vector3();
        transform1.transform.localRotation = Quaternion.Euler(new Vector3());
        transform1.name = map.name;
        transform1.GetComponent<Image>().color = _iteration % 2 == 0 ? Color1 : Color2;
        transform1.Find("TextName").GetComponent<TextMeshProUGUI>().text = map.name;
        transform1.Find("Difficulty").GetComponent<Image>().color = defineDifficultyColor(map.difficulty);
            
        //TODO Image
        item = transform1.GetComponent<ItemButton>();
        item.name = map.name;
        
        item.OnSelectEvent.AddListener((ItemButton) => HandleEventItemOnSelect(item));
        item.OnClickEvent.AddListener((ItemButton) => HandleEventItemOnClick(item));
        item.OnSubmitEvent.AddListener((ItemButton) => HandleEventItemOnSubmit(item));

        return item;
    }

    private void HandleEventItemOnSubmit(ItemButton item)
    {
        _eventItemOnSubmit.Invoke(item);
    }

    private void HandleEventItemOnClick(ItemButton item)
    {
        _eventItemClicked.Invoke(item);
    }

    private void HandleEventItemOnSelect(ItemButton item)
    {
        _eventItemOnSelect.Invoke(item);
    }
}
