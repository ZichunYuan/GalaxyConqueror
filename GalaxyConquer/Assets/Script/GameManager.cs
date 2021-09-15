using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //singleton
    void Awake(){
        instance = this;
    }
    private Base sourceBase;
    private Base destBase;

    public void Deploy(Base selectedBase){
        //SELECT THE SOURCE BASE, ONLY PlAYER COULD BE SELECTED
        if(sourceBase ==null && selectedBase!=null && selectedBase.type == Type.PLAYER) {
            sourceBase = selectedBase;
            Debug.Log("1");
            return;
        }
        //DESELECTION
        if(sourceBase==selectedBase||selectedBase==null){
            sourceBase = null;
            Debug.Log("0");
            return;
        }

        //SELECT THE DESTINATOIN BASE
        if(selectedBase!=null && sourceBase!=null){
            destBase = selectedBase;
            //Deploy unit!!!!!
            sourceBase.SendUnits(destBase);
            //Clear 
            sourceBase = null;
            destBase = null;
            Debug.Log("2");
        }
        
    }
}
