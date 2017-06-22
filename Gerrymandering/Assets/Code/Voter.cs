using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voter : MonoBehaviour
{
    public enum VoterType {Democrat, Republican};
    public VoterType voterParty;
    [SerializeField]
    private SpriteRenderer voterSprite;
    [SerializeField]
    private SpriteRenderer precinctSprite;
    [HideInInspector]
    public Vector2 voterID = new Vector2(0,0);
    private DistrictManager _manager;
    [HideInInspector]
    public bool inADistrict = false;
    [HideInInspector]
    public int districtIndex;
    [SerializeField]
    private PartyColors partyColors;


    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<DistrictManager>();
    }

    public void SetColor(VoterType party)
    {
        switch (party)
        {
            case VoterType.Democrat:
                voterParty = VoterType.Democrat;
                voterSprite.color = partyColors.democratColor;
                break;
            case VoterType.Republican:
                voterParty = VoterType.Republican;
                voterSprite.color = partyColors.republicanColor;
                break;
            default:
                break;
        }

    }

    public void PrecinctSelected()
    {
        if (Input.GetMouseButton(0) && inADistrict == false)
        {
            _manager.CheckSelectLimit(this.gameObject);
            //Debug.Log("clicked: " + voterID);
        }
    }

    public void DestroyPrecinct()
    {
        _manager.DestroyDistrict(districtIndex);
    }

    public void DistrictDrawn(Color districtColor)
    {
        precinctSprite.color = districtColor;
    }
}
