using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapController : MonoBehaviour
{

    public static MapController instance;

    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public GameObject[] maps;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject map in maps)
        {
            if(PlayerPrefs.GetInt("Map" + map.name) == 1)
            {
                map.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMap(string mapToActivate)
    {
        foreach(GameObject map in maps)
        {
            if(map.name == mapToActivate)
            {
                map.SetActive(true);
                PlayerPrefs.SetInt("Map", 1);
            }
        }
    }
}
