using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public enum Type{
        NEUTRAL,
        PLAYER,
        ENEMY
    }
public class Base : MonoBehaviour
{
    int max;
    int current;
    int growSpeed;

    public GameObject unitPrefab; 

    public TMP_Text population;

    public enum Group{
        HUMAN, 
        ANIMAL,
        SPIRIT
    }

    // public enum Type{
    //     NEUTRAL,
    //     PLAYER,
    //     ENEMY
    // }
    public Group baseGroup;
    public Type type;

    public Material[] materials; //Change this later!!! 0 NPC / 1 Player / 2 Enemy

    // Start is called before the first frame update
    void Start()
    {
        switch (baseGroup){
            case Group.HUMAN:
            {
                current = 1;
                max = 50;
                growSpeed = 1;
            }
            break;
            case Group.ANIMAL:
            {
                current = 10;
                max = 50;
                growSpeed = 0;
            }
            break;
            case Group.SPIRIT:
            {
                current = 10;
                max = 50;
                growSpeed = 2;
            }
            break;
        }
        // switch(type){
        //     case Type.ENEMY:{
        //     growSpeed = 2;
        //     break;  
        //     }
        //     case Type.NEUTRAL:{
        //     growSpeed = 0;
        //     break;  
        //     }
        //     case Type.PLAYER:{
        //     growSpeed = 1;
        //     break;
        //     }
        // }
        updatePopulation();
        StartCoroutine(GrowBySecond());
    }

    void Grow(){
        //Logic
        if(current <= max) current += growSpeed;

        //UI
        updatePopulation();
    }

    IEnumerator GrowBySecond(){
        while (true)
        {
            yield return new WaitForSeconds(1);
            Grow();
        }
    }

    void updatePopulation(){
        population.text = current + "|" + max;
    }

    

    IEnumerator SendAllUnits(Base destBase){
        int unitAmt = current;
        for(int i = 0; i < unitAmt; i++){
            GameObject deployedUnit = Instantiate(unitPrefab,transform.position, Quaternion.identity);
            
            deployedUnit.GetComponent<Unit>().setUnit(type, destBase,
            GetComponent<MeshRenderer>().material);
            yield return new WaitForSeconds(0.3f);
            current--;
            updatePopulation();
        }
    }
    public void SendUnits(Base destBase){
        // int unitAmt = current;
        // for(int i = 0; i < unitAmt; i++){
        //     GameObject deployedUnit = Instantiate(unitPrefab);
        //     deployedUnit.GetComponent<Unit>().setUnit((Unit.Type)type,destBase,
        //     GetComponent<MeshRenderer>().material);
        // }
        // current -= unitAmt;
        StartCoroutine(SendAllUnits(destBase));
    }


        // IEnumerator ReceiveAllUnits(Type t){
        //     if(t == type){
        //    Grow(); //Need to change!!!!!!!!!!!!!
        //    return; 
        // }else{
        //     //current --; //CHAGNE!!!!!!!!!!
        //     DestroyUnit(t);
        // }
        // }


    public void ReceiveUnits(Type t){
        if(t == type){
           Grow(); //Need to change!!!!!!!!!!!!!
           return; 
        }else{
            //current --; //CHAGNE!!!!!!!!!!
            DestroyUnit(t);
        }
    }

    void DestroyUnit(Type t){
        current--;
        if(current <=0) TypeHandler(t);
    }

    void TypeHandler (Type t){
        type = t;
        switch (type){
            case Type.PLAYER:
                GetComponent<MeshRenderer>().material = materials[1];
                growSpeed = 1;
                break;
            case Type.ENEMY:
                GetComponent<MeshRenderer>().material = materials[2];
                growSpeed = 2;
                break;
        }
    }
}
