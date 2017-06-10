using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voter : MonoBehaviour
{
    public enum VoterType {Democrat, Republican, Independant};
    [SerializeField]
    private SpriteRenderer voterSprite;
    [SerializeField]
    private SpriteRenderer districtSprite;
    [HideInInspector]
    public Vector2 voterID = new Vector2(0,0);

    void Start ()
    {
		
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

    public void DistrictClicked()
    {
        Debug.Log("clicked: " + voterID);
        districtSprite.color = new Color(0, 0, 0, 0.3f);
    }
}
