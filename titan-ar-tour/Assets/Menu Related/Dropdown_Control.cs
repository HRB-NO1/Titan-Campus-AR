using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dropdown_Control : MonoBehaviour
{
    public GameObject CPACObj, CPObj, CSObj, DBObj, ENPSObj, ESPSObj, ECObj, EObj, ABObj, GASObj, HObj, MCObj, PLObj, SRCObj, TSUObj;
    public void HandleInputData(int val)
    {
        if (val == 1)
        {
            if (!CPACObj.activeSelf)
            {
                CPACObj.SetActive(true);
            }
        }
        if (val == 2)
        {
            if (!CPObj.activeSelf)
            {
                CPObj.SetActive(true);
            }
        }
        if (val == 3)
        {
            if (!CSObj.activeSelf)
            {
                CSObj.SetActive(true);
            }
        }
        if (val == 4)
        {
            if (!DBObj.activeSelf)
            {
                DBObj.SetActive(true);
            }
        }
        if (val == 5)
        {
            if (!ENPSObj.activeSelf)
            {
                ENPSObj.SetActive(true);
            }
        }
        if (val == 6)
        {
            if (!ESPSObj.activeSelf)
            {
                ESPSObj.SetActive(true);
            }
        }
        if (val == 7)
        {
            if (!ECObj.activeSelf)
            {
                ECObj.SetActive(true);
            }
        }
        if (val == 8)
        {
            if (!EObj.activeSelf)
            {
                EObj.SetActive(true);
            }
        }
        if (val == 9)
        {
            if (!ABObj.activeSelf)
            {
                ABObj.SetActive(true);
            }
        }
        if (val == 10)
        {
            if (!GASObj.activeSelf)
            {
                GASObj.SetActive(true);
            }
        }
        if (val == 11)
        {
            if (!HObj.activeSelf)
            {
                HObj.SetActive(true);
            }
        }
        if (val == 12)
        {
            if (!MCObj.activeSelf)
            {
                MCObj.SetActive(true);
            }
        }
        if (val == 13)
        {
            if (!PLObj.activeSelf)
            {
                PLObj.SetActive(true);
            }
        }
        if (val == 14)
        {
            if (!SRCObj.activeSelf)
            {
                SRCObj.SetActive(true);
            }
        }
        if (val == 15)
        {
            if (!TSUObj.activeSelf)
            {
                TSUObj.SetActive(true);
            }
        }
    }

}
