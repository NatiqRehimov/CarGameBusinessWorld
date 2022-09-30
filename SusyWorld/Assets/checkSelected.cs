using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkSelected : MonoBehaviour
{
    [SerializeField] private Button[] buttonList;
    [SerializeField] private GameObject carModel;
    private void Update()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            if (buttonList[i].GetComponentInChildren<MeshRenderer>().material.name[0] ==
                carModel.GetComponent<MeshRenderer>().material.name[0])
            {
                buttonList[i].image.color = new Color(0.5f, 0.5f, 0, 1);
                buttonList[i].interactable = false;
            }
            else
            {
                buttonList[i].image.color = new Color(1, 1, 1, 1);
                buttonList[i].interactable = true;
            }
        }
    }
}
