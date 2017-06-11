using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject _dot;
    [SerializeField]
    private GameObject _voter;
    [SerializeField]
    private Vector3 _origin;
    [SerializeField]
    private Vector2 _spacing;
    [SerializeField]
    private int _rows;
    [SerializeField]
    private int _columns;
    private List<GameObject> voters = new List<GameObject>();

    void Start ()
    {
        ConstructDotGrid();
	}

    private void ConstructDotGrid()
    {
        List<Vector3> columnOne = new List<Vector3>();
        List<Vector3> gridPos = new List<Vector3>();

        int dotColumns = _columns + 1;
        int dotRows = _rows + 1;

        //Set the dot in the upper left corner first
        float columnOffset = 0;
        float rowOffset = 0;

        if (dotColumns % 2 == 0)
            columnOffset = Mathf.Abs(_spacing.x) / 2;
        if (dotRows % 2 == 0)
            rowOffset = Mathf.Abs(_spacing.y) / 2;

        columnOne.Add(new Vector3(_origin.x - columnOffset - (((dotColumns-1) / 2)*_spacing.x), _origin.y + rowOffset + (((dotRows - 1) / 2) * _spacing.y), 0));

        //Set up the first column positions
        for (int i = columnOne.Count; i < dotColumns; i++)
        {
            columnOne.Add(columnOne[0] + new Vector3(i * _spacing.x, 0, 0));
        }

        //Set up the rest of the positions based on the first column
        foreach (Vector3 j in columnOne)
        {
            gridPos.Add(j);

            for (int k = 1; k <dotRows; k++)
            {
                gridPos.Add(j + new Vector3(0, k * -_spacing.y, 0));
            }
        }

        //Create the grid
        foreach (Vector3 pos in gridPos)
        {
            Instantiate(_dot, pos, Quaternion.identity);
        }

        PopulateGrid();
    }


    // Place the voters inside the grid
    void PopulateGrid()
    {
        List<Vector3> rowOne = new List<Vector3>();
        List<Vector3> gridPos = new List<Vector3>();

        int dotColumns = _columns;
        int dotRows = _rows;

        //Set the voter in the upper left corner first
        float columnOffset = 0;
        float rowOffset = 0;

        if (dotColumns % 2 == 0)
            columnOffset = Mathf.Abs(_spacing.x) / 2;
        if (dotRows % 2 == 0)
            rowOffset = Mathf.Abs(_spacing.y) / 2;

        rowOne.Add(new Vector3(_origin.x - columnOffset - (((dotColumns - 1) / 2) * _spacing.x), _origin.y + rowOffset + (((dotRows - 1) / 2) * _spacing.y), 0));

        //Set up the first column positions
        for (int i = rowOne.Count; i < dotColumns; i++)
        {
            rowOne.Add(rowOne[0] + new Vector3(i * _spacing.x, 0, 0));
        }

        int l = 0;

        //Set up the rest of the positions based on the first column
        foreach (Vector3 j in rowOne)
        {
            gridPos.Add(j);
            GenerateGridPoint(rowOne[l], l, 0);

            for (int k = 1; k < dotRows; k++)
            {
                Vector3 newPos = j + new Vector3(0, k * -_spacing.y, 0);
                gridPos.Add(newPos);
                GenerateGridPoint(newPos, l, k);
            }

            l++;
        }
    }

    void GenerateGridPoint(Vector3 pos, int xIndex, int yIndex)
    {
        GameObject newVoter = Instantiate(_voter, pos, Quaternion.identity);
        voters.Add(newVoter);
        Voter voter = newVoter.GetComponent<Voter>();
        voter.voterID = new Vector2(xIndex, yIndex);
        voter.SetColor((Voter.VoterType)Random.Range(0, 2));
    }

}
