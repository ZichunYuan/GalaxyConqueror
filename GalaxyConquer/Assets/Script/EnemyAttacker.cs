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
            BasicAttack();
            time = 0.0f;
        }
    }
    private void BasicAttack(){
        if (PlanetManager.instance.enemy.Count > 0 && PlanetManager.instance.neutural.Count > 0)
        {
            Planet enemy = PlanetManager.instance.enemy[0];
            Planet neutural = PlanetManager.instance.neutural[0];

            //TODO: Enemy logic can be changed here!
            if (enemy != null && neutural != null)
            {
                //Selected enemy source base.
                //Selected enemy desintaiton base.
                enemy.Attack(neutural);
            }
        }
    }
    private void AdvancedAttack()
    {
        Planet enemy = PlanetManager.instance.enemy[0];
        Planet neutural = PlanetManager.instance.player[0];
            //TODO: Enemy logic can be changed here!
            if (enemy != null && neutural != null)
            {
                //Selected enemy source base.
                //Selected enemy desintaiton base.
                enemy.Attack(neutural);
            }
    }

}
