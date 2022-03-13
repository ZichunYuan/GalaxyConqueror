using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public enum Type{
        NEUTRAL,
        PLAYER,
        ENEMY
    }
public class Planet : MonoBehaviour
{
    private int maxUnitCount;
    private int currentUnitCount;
    private int growRate;

    public GameObject unitPrefab; 
    public TMP_Text unitCount;

    public enum Group{
        HUMAN, 
        ANIMAL,
        SPIRIT
    }

    public Group baseGroup; //TODO: set the data for each group.
    public Type type;
    public Material[] materials; // 0 NPC / 1 Player / 2 Enemy

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
                currentUnitCount = 10;
                maxUnitCount = 50;
                growRate = 1;
            break;  
            }
            case Type.NEUTRAL:{
                currentUnitCount = 10;
                maxUnitCount = 50;
                growRate = 0;
            break;  
            }
            case Type.PLAYER:{
                currentUnitCount = 10;
                maxUnitCount = 50;
                growRate = 1;
            break;
            }
        }
        updatePopulation();
        StartCoroutine(GrowBySecond());
    }

    private void Update() 
    {
        updatePopulation();
    }

    private void Grow(){
        //Logic
        if(currentUnitCount < maxUnitCount) currentUnitCount += growRate;

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
        unitCount.text = currentUnitCount + "|" + maxUnitCount;
    }
    
    IEnumerator SendAllUnits(Planet destBase){
        int unitAmt = currentUnitCount;
        //TODO: Change the deploying method
        currentUnitCount = 0;
        Type originalType = type;
        Material originalMaterial = GetComponent<MeshRenderer>().material;

        for(int i = 0; i < unitAmt; i++){
            GameObject deployedUnit = Instantiate(unitPrefab,transform.position, Quaternion.identity);
            
            deployedUnit.GetComponent<Unit>().setUnit(originalType, destBase, originalMaterial);
            yield return new WaitForSeconds(0.3f);
            //current--;
            updatePopulation();
        }
    }
    public void Attack(Planet destBase){
        StartCoroutine(SendAllUnits(destBase));
    }

    public void GotAttacked(Type t){
        if(t == type){
           Grow(); //TODO: UI Could Look better
           return; 
        }else{
            GotConquered(t); //TODO: UI Could Look better
        }
    }

    private void GotConquered(Type t){
        if(currentUnitCount > 0) currentUnitCount--;
        else TakeOver(t);
    }

    private void TakeOver(Type t){
        DecreaseCount(); //Decrease the count in the PlanetManager.
        type = t;
        switch (type){
            case Type.PLAYER:
                GetComponent<MeshRenderer>().material = materials[1];
                growRate = 1;//TODO: change it based on Group.
                break;
            case Type.ENEMY:
                GetComponent<MeshRenderer>().material = materials[2];
                growRate = 1; //TODO: change it based on Group.
                break;
        }
        PlanetManager.instance.Increase(type, this);
    }
    private void DecreaseCount(){
        PlanetManager.instance.Decrease(type,this);
    }
}
