using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour
{
    //[HideInInspector] //The number of districts that can be drawn
    public int maxDistrictCount;
    private int currentDistrict;
    //[HideInInspector] //The maximum number of precincts that can be selected per district
    public int maxPrecinctsSelected;
    private int currentPrecinct;

    //private List<GameObject> districtOne;
    private List<GameObject>[] district;

    void Start()
    {
        currentPrecinct = 0;
        currentDistrict = 0;
        district = new List<GameObject>[maxDistrictCount];
        for (int i = 0; i < maxDistrictCount; i++)
        {
            district[i] = new List<GameObject>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            currentPrecinct = 0;
            currentDistrict++;
            //Check for game over once max districts reached
            Debug.Log("district number: " + currentDistrict);
        }
    }
    
    public void CheckSelectLimit(GameObject precinct)
    {
        if (currentDistrict <= maxDistrictCount)
        {
            if (precinct.GetComponent<Voter>().InADistrict)
            {
                return;
            }

            if (currentPrecinct < maxPrecinctsSelected)
            {
                currentPrecinct++;
                district[currentDistrict].Add(precinct);
                CheckColor();
                return;
            }
        }
    }

    public void CheckColor()
    {
        //Check for when to change color
        if (currentPrecinct >= maxPrecinctsSelected)
        {
            foreach (GameObject go in district[currentDistrict])
            {
                Voter voter = go.GetComponent<Voter>();
                voter.DistrictDrawn(Color.yellow);
                voter.InADistrict = true;
            }
        }
        else
        {
            district[currentDistrict][currentPrecinct - 1].GetComponent<Voter>().DistrictDrawn(new Color(0, 0, 0, 0.3f));
        }
    }
}
