using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject _dot;
    [SerializeField]
    private Transform _dotContainer;
    [SerializeField]
    private GameObject _voter;
    public Transform _voterContainer;
    [HideInInspector]
    public List<GameObject> voters = new List<GameObject>();
    private LevelData level;


    public void DestroyLevel()
    {
        if (_dotContainer.childCount > 0 && _voterContainer.childCount > 0)
        {
            foreach (Transform child in _dotContainer)
                Destroy(child.gameObject);
            foreach (Transform child in _voterContainer)
                Destroy(child.gameObject);
        }
    }

    //Get the level info from game manager and begin constructing the level
    public void NewLevel(LevelData newLevel)
    {
        level = newLevel;
        ConstructDotGrid();
    }

    private void ConstructDotGrid()
    {
        List <Vector3> columnOne = new List<Vector3>();
        List<Vector3> gridPos = new List<Vector3>();

        int dotColumns = level.columns + 1;
        int dotRows = level.rows + 1;

        //Set the dot in the upper left corner first
        float columnOffset = 0;
        float rowOffset = 0;

        if (dotColumns % 2 == 0)
            columnOffset = Mathf.Abs(level.spacing.x) / 2;
        if (dotRows % 2 == 0)
            rowOffset = Mathf.Abs(level.spacing.y) / 2;

        columnOne.Add(new Vector3(level.origin.x - columnOffset - (((dotColumns-1) / 2)*level.spacing.x), level.origin.y + rowOffset + (((dotRows - 1) / 2) * level.spacing.y), 0));

        //Set up the first column positions
        for (int i = columnOne.Count; i < dotColumns; i++)
        {
            columnOne.Add(columnOne[0] + new Vector3(i * level.spacing.x, 0, 0));
        }

        //Set up the rest of the positions based on the first column
        foreach (Vector3 j in columnOne)
        {
            gridPos.Add(j);

            for (int k = 1; k <dotRows; k++)
            {
                gridPos.Add(j + new Vector3(0, k * -level.spacing.y, 0));
            }
        }

        //Create the grid
        foreach (Vector3 pos in gridPos)
        {
            GameObject gridPoint = Instantiate(_dot, pos, Quaternion.identity);
            gridPoint.transform.SetParent(_dotContainer);
        }

        PopulateGrid();
    }


    // Place the voters inside the grid
    void PopulateGrid()
    {
        List<Vector3> rowOne = new List<Vector3>();
        List<Vector3> gridPos = new List<Vector3>();

        int dotColumns = level.columns;
        int dotRows = level.rows;

        //Set the voter in the upper left corner first
        float columnOffset = 0;
        float rowOffset = 0;

        if (dotColumns % 2 == 0)
            columnOffset = Mathf.Abs(level.spacing.x) / 2;
        if (dotRows % 2 == 0)
            rowOffset = Mathf.Abs(level.spacing.y) / 2;

        rowOne.Add(new Vector3(level.origin.x - columnOffset - (((dotColumns - 1) / 2) * level.spacing.x), level.origin.y + rowOffset + (((dotRows - 1) / 2) * level.spacing.y), 0));

        //Set up the first column positions
        for (int i = rowOne.Count; i < dotColumns; i++)
        {
            rowOne.Add(rowOne[0] + new Vector3(i * level.spacing.x, 0, 0));
        }

        int l = 0;

        //Set up the rest of the positions based on the first column
        foreach (Vector3 j in rowOne)
        {
            gridPos.Add(j);
            GenerateGridPoint(rowOne[l], l, 0);

            for (int k = 1; k < dotRows; k++)
            {
                Vector3 newPos = j + new Vector3(0, k * -level.spacing.y, 0);
                gridPos.Add(newPos);
                GenerateGridPoint(newPos, l, k);
            }

            l++;
        }
    }

    void GenerateGridPoint(Vector3 pos, int xIndex, int yIndex)
    {
        GameObject newVoter = Instantiate(_voter, pos, Quaternion.identity);
        newVoter.transform.SetParent(_voterContainer);
        newVoter.transform.localScale = new Vector3(level.spacing.x, level.spacing.y, 1);
        voters.Add(newVoter);
        Voter voter = newVoter.GetComponent<Voter>();
        voter.voterID = new Vector2(xIndex, yIndex);
        voter.SetColor((Voter.VoterType)Random.Range(0, 2));
    }

}
