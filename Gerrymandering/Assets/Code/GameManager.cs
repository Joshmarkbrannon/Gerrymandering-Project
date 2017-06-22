using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int currentLevel;
    [SerializeField]
    private DotGrid _grid;
    [SerializeField]
    private DistrictManager _district;
    [SerializeField]
    private UIManager _UI;
    public LevelData[] levelData;


    void Awake()
    {
        currentLevel = 0;
    }

    void Start ()
    {
        LoadNewLevel(false);
	}

    public void LoadNewLevel(bool isProgressing)
    {
        //Clear out existing level, if there is one
        _grid.DestroyLevel();

        if (isProgressing)
            currentLevel++;

        //Send the information about the current level to the grid constructor
        _grid.NewLevel(levelData[currentLevel]);
        _UI.InitializeUI(levelData[currentLevel].maxDistricts);
        _district.NewLevel(levelData[currentLevel].maxDistricts, levelData[currentLevel].maxPrecinctSize);
    }

    public void DeclareResults(int democratAreas, int republicanAreas)
    {
        if (democratAreas > republicanAreas)
        {

        }
    }
}
