using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public GameObject[] bases; // 0 for player, 1 for enemy, 2 for neutral
    public List<Planet> player;
    public List<Planet> enemy;
    public List<Planet> neutural;

    [SerializeField] private int totalBase;
    public static PlanetManager instance; //singleton
    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO: GET RID OF MAGIC NUMBER
        //TODO: 2 can be put into a variable for further level development. Now the spawning logic is pretty naive.

        player = new List<Planet>();
        enemy = new List<Planet>();
        neutural = new List<Planet>();

        player.Add(Instantiate(bases[0], 
            transform.position,
            Quaternion.Euler(0,180,0)).GetComponent<Planet>());

        enemy.Add(Instantiate(bases[1], 
            new Vector3(-2.5f, 0.5f, -2) + transform.position, 
            Quaternion.Euler(0, 180, 0)).GetComponent<Planet>());

        for (int i = 0; i < totalBase - 2; i++){
            neutural.Add(Instantiate(bases[2], 
                new Vector3(Random.Range(1,3)*i,3-2*i, Random.Range(1, 3) * i)+transform.position, 
                Quaternion.Euler(0, 180, 0)).GetComponent<Planet>());
        }
    }


    public void Increase(Type type, Planet p)
    {
        switch (type)
        {
            case Type.PLAYER:
                PlayerIncrease(p);
                break;
            case Type.ENEMY:
                EnemyIncrease(p);
                break;                
        }
    }

    public void Decrease(Type type, Planet p)
    {
        switch (type)
        {
            case Type.PLAYER:
                PlayerDecrease(p);
                break;
            case Type.ENEMY:
                EnemyDecrease(p);
                break;
            case Type.NEUTRAL:
                NeuturalDecrease(p);
                break;
        }
    }

    private void PlayerDecrease(Planet p)
    {
        this.player.Remove(p);
    }
    private void PlayerIncrease(Planet player)
    {
        this.player.Add(player);
    }
    private void EnemyDecrease(Planet p)
    {
        this.enemy.Remove(p);
    }
    private void EnemyIncrease(Planet enemy)
    {
        this.enemy.Add(enemy);
    }
    private void NeuturalDecrease(Planet p)
    {
        this.neutural.Remove(p);
    }
}
