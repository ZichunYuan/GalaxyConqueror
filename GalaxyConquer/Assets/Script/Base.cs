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
    private int max;
    private int current;
    private int growSpeed;

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
    public Group baseGroup; //TODO: 种族数据
    public Type type;

    public Material[] materials; //Change this later!!! 0 NPC / 1 Player / 2 Enemy


    // Start is called before the first frame update
    void Start()
    {
        // switch (baseGroup){
        //     case Group.HUMAN:
        //     {
        //         current = 20;
        //         max = 50;
        //         growSpeed = 1;
        //     }
        //     break;
        //     case Group.ANIMAL:
        //     {
        //         current = 10;
        //         max = 50;
        //         growSpeed = 0;
        //     }
        //     break;
        //     case Group.SPIRIT:
        //     {
        //         current = 10;
        //         max = 50;
        //         growSpeed = 0;
        //     }
        //     break;
        // }
        switch(type){
            case Type.ENEMY:{
                current = 10;
                max = 50;
                growSpeed = 1;
            break;  
            }
            case Type.NEUTRAL:{
                current = 10;
                max = 50;
                growSpeed = 0;
            break;  
            }
            case Type.PLAYER:{
                current = 10;
                max = 50;
                growSpeed = 1;
            break;
            }
        }
        updatePopulation();
        StartCoroutine(GrowBySecond());
    }

    private void Grow(){
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

    
    //TODO: the send units can't go down pass 0!!!!!!!!!!
    IEnumerator SendAllUnits(Base destBase){
        //NOTE!! BOTH IMPLEMENTATION IS KINDA PROBLEMATIC.
        int unitAmt = current;
        current = 0;

        Type originalType = type;
        Material originalMaterial = GetComponent<MeshRenderer>().material;

        for(int i = 0; i < unitAmt; i++){
            GameObject deployedUnit = Instantiate(unitPrefab,transform.position, Quaternion.identity);
            
            deployedUnit.GetComponent<Unit>().setUnit(originalType, destBase,
            originalMaterial);
            yield return new WaitForSeconds(0.3f);
            //current--;
            updatePopulation();
        }
    }
    public void SendUnits(Base destBase){
        //NOTE!! BOTH IMPLEMENTATION IS KINDA PROBLEMATIC.
        // int unitAmt = current;
        // current = 0;

        // Type originalType = type;
        // Material originalMaterial = GetComponent<MeshRenderer>().material;

        // for(int i = 0; i < unitAmt; i++){
        //     GameObject deployedUnit = Instantiate(unitPrefab,transform.position, Quaternion.identity);
            
        //     deployedUnit.GetComponent<Unit>().setUnit(originalType, destBase,
        //     originalMaterial);
        //    // yield return new WaitForSeconds(0.3f);
        //     //current--;
        //     updatePopulation();
        // }
        StartCoroutine(SendAllUnits(destBase));
    }

    public void ReceiveUnits(Type t){
        if(t == type){
           Grow(); //TODO: UI Could Look better!!!!!!!!
           return; 
        }else{
            DestroyUnit(t); //TODO: UI Could Look better!!!!!!!!
        }
    }

    private void DestroyUnit(Type t){
        current--;
        if(current <=0) TypeHandler(t);
    }

    private void TypeHandler (Type t){
        decrease(); //Decrease the count in the BaseManager.
        type = t;
        switch (type){
            case Type.PLAYER:
                Debug.Log("become player");
                BaseManager.instance.PlayerIncrease(this);
                GetComponent<MeshRenderer>().material = materials[1];
                growSpeed = 1;
                break;
            case Type.ENEMY:
                Debug.Log("become enemy");
                BaseManager.instance.EnemyIncrease(this);
                GetComponent<MeshRenderer>().material = materials[2];
                growSpeed = 1;
                break;
        }
    }
    private void decrease(){
        switch (type)
        {
            case Type.PLAYER:
                BaseManager.instance.PlayerDecrease();
                break;
            case Type.ENEMY:
                BaseManager.instance.EnemyDecrease();
                break;
            case Type.NEUTRAL:
                BaseManager.instance.NeuturalDecrease();
                break;
        }
    }
}
