using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> dungeonLayouts;
    private GameObject initialDungeon;
    [SerializeField]
    private int numberOfRooms = 1;
    [SerializeField]
    private Transform dungeonLayoutParent;
    // Start is called before the first frame update
    void Start()
    {
        CreateDungeonLayout();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateDungeonLayout()
    {
        for (int i = 0; i <= numberOfRooms; ++i)
        {
            if (i == 0)
            {
                initialDungeon = Instantiate(dungeonLayouts[Random.Range(0, dungeonLayouts.Count)], dungeonLayoutParent);
            }
            else
            {
                GameObject currentDungeon = Instantiate(dungeonLayouts[Random.Range(0, dungeonLayouts.Count)], dungeonLayoutParent);
                // Set entry position to exit position
                currentDungeon.transform.GetChild(0).position = initialDungeon.transform.GetChild(0).GetChild(0).position;
                currentDungeon.transform.GetChild(0).rotation = initialDungeon.transform.GetChild(0).GetChild(0).rotation;
                initialDungeon = currentDungeon;
            }
        }
    }
}
