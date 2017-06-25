using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour
{
    //The number of districts that can be drawn
    private int maxDistricts;
    private int currentDistrict;
    private int districtCount;

    private int democratDistricts;
    private int republicanDistricts;

    //The maximum number of precincts that can be selected per district
    private int maxPrecinctSize;
    [SerializeField]
    private int minPrecinctSize;
    private int currentPrecinct;

    private int partyID;
    [SerializeField]
    PartyColors partyColors;

    private List<GameObject>[] district;
    private string[] districtIdentity;

    [SerializeField]
    private GameManager _manager;
    [SerializeField]
    private UIManager _UI;

    //Get data from the game manager about the max districts and max precinct size, then initialize
    public void NewLevel(int maxDistrictCount, int maxPrecintSelectionSize)
    {
        maxDistricts = maxDistrictCount;
        maxPrecinctSize = maxPrecintSelectionSize;

        currentPrecinct = 0;
        currentDistrict = 0;
        districtCount = 0;
        democratDistricts = 0;
        republicanDistricts = 0;
        district = new List<GameObject>[maxDistricts];
        districtIdentity = new string[maxDistrictCount];
        for (int i = 0; i < district.Length; i++)
        {
            district[i] = new List<GameObject>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentPrecinct >= minPrecinctSize)
            {
                CheckDistrict();

                if (districtCount < maxDistricts)
                {
                    //make current district the next empty district
                    int i = 0;
                    while (district[i].Count != 0 && i < maxDistricts - 1)
                    {
                        i++;
                    }
                    currentDistrict = i;
                    districtCount++;
                    if (districtCount < maxDistricts)
                    {
                        _UI.SelectNext(currentDistrict);
                    }
                    else
                    {
                        _UI.ClearSelection();
                    }
                    if (districtCount == maxDistricts)
                    {
                        _manager.DeclareResults(democratDistricts, republicanDistricts);
                    }

                        //Check for game over once max districts reached
                        Debug.Log("district number: " + currentDistrict);
                }
            }
            //If there's only one precinct in the district
            else
            {
                if (districtCount < maxDistricts && district[currentDistrict].Count != 0)
                {
                    //Tell each precinct selected that it's no longer in a district
                    foreach (GameObject go in district[currentDistrict])
                    {
                        Voter voter = go.GetComponent<Voter>();
                        //Fade back to clear
                        voter.DistrictDrawn(Color.clear);
                        voter.inADistrict = false;
                    }
                }
            }

            currentPrecinct = 0;
        }
    }
    
    public void CheckSelectLimit(GameObject precinct)
    {
        if (districtCount < maxDistricts)
        {
            if (precinct.GetComponent<Voter>().inADistrict)
            {
                return;
            }

            if (currentPrecinct < maxPrecinctSize)
            {
                currentPrecinct++;
                district[currentDistrict].Add(precinct);
                precinct.GetComponent<Voter>().inADistrict = true;
                if (currentPrecinct >= maxPrecinctSize)
                {
                    CheckDistrict();
                }
                else
                {
                    district[currentDistrict][currentPrecinct - 1].GetComponent<Voter>().DistrictDrawn(new Color(0, 0, 0, 0.3f));
                }
                return;
            }
            else
                Debug.Log("Precinct too big");
        }
    }

    public void CheckDistrict()
    {
        //Check when the district is complete
        int dem = 0;
        int rep = 0;

        foreach (GameObject go in district[currentDistrict])
        {
            Voter voter = go.GetComponent<Voter>();
            voter.inADistrict = true;
            voter.districtIndex = currentDistrict;
            if (voter.voterParty == Voter.VoterType.Democrat)
                dem++;
            if (voter.voterParty == Voter.VoterType.Republican)
                rep++;
        }

        if (dem > rep)
        {
            districtIdentity[currentDistrict] = "Democrat";
            democratDistricts++;
            _UI.ColorDistrictUI(partyColors.democratColor, currentDistrict);
            foreach (GameObject go in district[currentDistrict])
            {
                Voter voter = go.GetComponent<Voter>();
                voter.DistrictDrawn(partyColors.democratDistrictColor);
            }
        }
        else if (dem < rep)
        {
            districtIdentity[currentDistrict] = "Republican";
            republicanDistricts++;
            _UI.ColorDistrictUI(partyColors.republicanColor, currentDistrict);
            foreach (GameObject go in district[currentDistrict])
            {
                Voter voter = go.GetComponent<Voter>();
                voter.DistrictDrawn(partyColors.republicanDistrictColor);
            }
        }
        else if (dem == rep)
        {
            districtIdentity[currentDistrict] = "Contested";
            _UI.ColorDistrictUI(partyColors.stalemateColor, currentDistrict);
            foreach (GameObject go in district[currentDistrict])
            {
                Voter voter = go.GetComponent<Voter>();
                voter.DistrictDrawn(partyColors.stalemateColor);
            }
        }
    }

    public void DestroyDistrict(int districtIndex)
    {
        if (district[districtIndex].Count != 0)
        {
            districtCount--;
            currentPrecinct = 0;

            if (districtIdentity[districtIndex] == "Democrat")
            {
                democratDistricts--;
                districtIdentity[districtIndex] = "";
            }
            else if (districtIdentity[districtIndex] == "Republican")
            {
                republicanDistricts--;
                districtIdentity[districtIndex] = "";
            }

            _UI.ColorDistrictUI(Color.white, districtIndex);
            
            foreach (GameObject go in district[districtIndex])
            {
                Voter voter = go.GetComponent<Voter>();
                //Fade back to clear
                voter.DistrictDrawn(Color.clear);
                voter.inADistrict = false;
            }
            district[districtIndex].Clear();
            currentDistrict = districtIndex;
            _UI.SelectNext(currentDistrict);
        }
    }
}
