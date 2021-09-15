using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    private float time = 0.0f;
    private float interpolationPeriod = 10.0f; //TODO: variable can be put into a class?

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= interpolationPeriod)
        {
            Attack();
            time = 0.0f;
        }
    }
    private void Attack(){
        Debug.Log("Attack!");
        if (BaseManager.instance.enemy.Count > 0 && BaseManager.instance.neutural.Count > 0)
        {
            Base enemy = BaseManager.instance.enemy.Peek();
            Base neutural = BaseManager.instance.neutural.Peek();

            //TODO: Enemy logic can be changed here!
            if (enemy != null && neutural != null)
            {
                //Selected enemy source base.
                //Selected enemy desintaiton base.
                enemy.SendUnits(neutural);
            }
        }
    }
}
