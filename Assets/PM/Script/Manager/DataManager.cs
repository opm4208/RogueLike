using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DataManager : MonoBehaviour
{
    public int StageCount1=1;
    private int StageCount2;
    private int StageCount3;
    private int[] stageCount;

    /*public float PlayTime { 
        get { return Time; }
    }*/
    public int StageCounts1
    {
        get { return StageCount1; }
        set { StageCount1 -= value; }
    }


    private int curScore;

    /*public int CurScore
    {
        get { return curScore; }
        set
        {
            OnCurScoreChanged?.Invoke(value);
            curScore = value;

            if(curScore>BestScore)
                bestScore= curScore;
        }
    }*/
}
