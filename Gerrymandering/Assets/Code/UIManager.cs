﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _districtUI;
    [SerializeField]
    private Transform _districtUIHolder;

    private List<GameObject> districtUIObjects;

    void Start()
    {
        districtUIObjects = new List<GameObject>();
    }

    public void InitializeUI(int districtCount)
    {
        //Clear out old UI
        if (_districtUIHolder.childCount > 0)
        {
            districtUIObjects.Clear();
            foreach (Transform child in _districtUIHolder)
                Destroy(child.gameObject);
        }

        //Make new UI
        for (int i = 0; i < districtCount; i++)
        {
            GameObject district = Instantiate(_districtUI, Vector3.zero, Quaternion.identity);
            district.transform.SetParent(_districtUIHolder);
            district.transform.localScale = Vector3.one;
            districtUIObjects.Add(district);

            //Set the first one to be selected
            districtUIObjects[0].GetComponent<DistrictUI>().SetSelected(true);
        }
    }

    public void ColorDistrictUI(Color color, int currentDistrict)
    {
        districtUIObjects[currentDistrict].GetComponent<DistrictUI>().SetColor(color);
    }

    public void ClearSelection()
    {
        foreach (GameObject go in districtUIObjects)
        go.GetComponent<DistrictUI>().SetSelected(false);
    }

    public void SelectNext(int currentDistrict)
    {
        ClearSelection();

        districtUIObjects[currentDistrict].GetComponent<DistrictUI>().SetSelected(true);
    }
}
