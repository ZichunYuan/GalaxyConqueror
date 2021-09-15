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
    float speed = 5; //TODO: COULD BE CHAGNED!!!!!!!

   // public float turnSpeed = 1.5f; // rotation speed control ****


    public void setUnit(Type _type, Base destBase, Material material){
        this.type = _type;
        destination = destBase;
        //SET THE MATERIAL HERE!!!!!!!!
        GetComponent<MeshRenderer>().material = material;
    }
    void Update(){


        // update direction each frame:
        //Vector3 dir = destination.transform.position - transform.position;
        // calculate desired rotation:
        //Quaternion rot = Quaternion.LookRotation(dir);
        // Slerp to it over time:
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        // move in the current forward direction at specified speed:
        //transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));



        transform.position = Vector3.MoveTowards(
            transform.position, destination.transform.position,speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider c){
        //If they collide with each other.
        Base collided = c.GetComponent<Base>();
        if(collided == destination){
            collided.ReceiveUnits(type);
        }
        
    }
}
