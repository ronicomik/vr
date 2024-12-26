using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Text uiText;

    void Update()
    {
        System.DateTime currentTime = System.DateTime.Now;

        uiText.text = currentTime.ToString("HH:mm:ss");

        if (currentTime.Hour >= 22 || currentTime.Hour < 6)
        {
            uiText.text = "Пора делать дз по ВР"; 
        }
    }
}
