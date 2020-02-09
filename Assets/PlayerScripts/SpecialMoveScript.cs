using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialMoveScript : MonoBehaviour {

    ///ector3 localScale;
    Image SpecialBar;
    float maxSpecialMove = 100f;
    public static float SpecialMove;

    // Use this for initialization
    void Start()
    {
        SpecialBar = GetComponent<Image>();
        SpecialMove = 0;
        //localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //calScale.x = MovePlayer.healthAmount;
        //ransform.localScale = localScale;
        SpecialBar.fillAmount = SpecialMove / maxSpecialMove;
    }
}
