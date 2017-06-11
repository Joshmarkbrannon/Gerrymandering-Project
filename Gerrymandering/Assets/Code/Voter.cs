using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voter : MonoBehaviour
{
    public enum VoterType {Democrat, Republican, Independant};
    [SerializeField]
    private SpriteRenderer voterSprite;
    [SerializeField]
    private SpriteRenderer precinctSprite;
    [HideInInspector]
    public Vector2 voterID = new Vector2(0,0);
    private DistrictManager _manager;
    [HideInInspector]
    public bool InADistrict = false;

    void Start ()
    {
        _manager = Camera.main.GetComponent<DistrictManager>();
	}

    public void SetColor(VoterType party)
    {
        switch (party)
        {
            case VoterType.Democrat:
                voterSprite.color = Color.blue;
                break;
            case VoterType.Republican:
                voterSprite.color = Color.red;
                break;
            case VoterType.Independant:
                voterSprite.color = Color.green;
                break;
            default:
                break;
        }

    }

    public void PrecinctSelected()
    {
        if (Input.GetMouseButton(0) && InADistrict == false)
        {
            _manager.CheckSelectLimit(this.gameObject);
            //Debug.Log("clicked: " + voterID);
        }
    }

    public void DistrictDrawn(Color districtColor)
    {
        precinctSprite.color = districtColor;
    }
}
