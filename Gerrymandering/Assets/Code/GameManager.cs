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
    private UIManager _gameUI;
    [SerializeField]
    private GameObject _completeUI;
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

        if (isProgressing && currentLevel < levelData.Length)
            currentLevel++;

        //Send the information about the current level to the grid constructor
        _grid.NewLevel(levelData[currentLevel]);
        _gameUI.InitializeUI(levelData[currentLevel].maxDistricts);
        _district.NewLevel(levelData[currentLevel].maxDistricts, levelData[currentLevel].maxPrecinctSize);
    }

    public void DeclareResults(int democratAreas, int republicanAreas)
    {
        Transform voterRegistry = _grid._voterContainer;
        foreach (Transform child in voterRegistry)
        {
            if (child.GetComponent<Voter>().inADistrict == false)
            {
                _gameUI.UpdateText("No voter left behind!");
                return;
            }
        }

        if (republicanAreas > democratAreas)
        {
            _completeUI.SetActive(true);
        }
    }
}
