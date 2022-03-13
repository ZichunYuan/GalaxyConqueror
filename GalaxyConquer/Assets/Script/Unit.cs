using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Type type;
    Planet destination;
    float speed = 5; //TODO: COULD BE CHAGNED

    public void setUnit(Type _type, Planet destBase, Material material){
        this.type = _type;
        destination = destBase;
        //SET THE MATERIAL HERE
        GetComponent<MeshRenderer>().material = material;
    }
    void Update(){
        transform.position = Vector3.MoveTowards(
            transform.position, destination.transform.position,speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider c){
        //If they collide with each other.
        Planet collided = c.GetComponent<Planet>();
        if(collided == destination){
            collided.GotAttacked(type);
        }
        
    }
}
