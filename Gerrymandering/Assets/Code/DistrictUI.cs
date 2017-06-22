using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistrictUI : MonoBehaviour
{
    [SerializeField]
    private Image _selectionImage;
    [SerializeField]
    private Image _districtImage;

    public void SetSelected(bool selected)
    {
        _selectionImage.gameObject.SetActive(selected);
    }

    public void SetColor(Color newColor)
    {
        _districtImage.color = newColor;
    }
}
