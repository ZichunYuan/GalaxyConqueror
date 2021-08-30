using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // public enum Type{
    //     NEUTRAL,
    //     PLAYER,
    //     ENEMY
    // }
    public Type type;
    Base destination;
    float speed = 5; //CHAGNE THIS!!!!!!!
    public void setUnit(Type _type, Base destBase, Material material){
        this.type = _type;
        destination = destBase;
        //SET THE MATERIAL HERE!!!!!!!!
        GetComponent<MeshRenderer>().material = material;
    }
    void Update(){
        transform.position = Vector3.MoveTowards(
            transform.position, destination.transform.position,speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider c){
        //If they collide with each other.
        Base collided = c.GetComponent<Base>();
        if(collided == destination){
            collided.ReceiveUnits(type);
            Debug.Log("got");
        }
        
    }
}
