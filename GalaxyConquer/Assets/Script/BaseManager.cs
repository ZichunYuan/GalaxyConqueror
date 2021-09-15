using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public GameObject[] bases; // 0 for player, 1 for enemy, 2 for neutral
    // public GameObject[] player;
    // public GameObject[] neutural;
    // public GameObject[] enemy;
    public Queue<Base> player;
    public Queue<Base> enemy;
    public Queue<Base> neutural;

    [SerializeField] private int totalBase;

    public static BaseManager instance; //singleton
    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //allBase[0] = new Base();

        //allBase[0].baseGroup = Base.Group.HUMAN;
        //allBase[1].baseGroup = Base.Group.HUMAN;
        //allBase[2].baseGroup = Base.Group.HUMAN;
        //allBase[0].type = Type.PLAYER;
        // allBase[1].type = Type.NEUTRAL;
        //allBase[2].type = Type.ENEMY;
        // gameObject2.GetComponent<Base>().baseGroup = Base.Group.SPIRIT;
        // gameObject2.GetComponent<Base>().type = Type.NEUTRAL;

        // player = new GameObject[totalBase];
        // enemy = new GameObject[totalBase];
        // neutural = new GameObject[totalBase];

        player = new Queue<Base>();
        enemy = new Queue<Base>();
        neutural = new Queue<Base>();

        player.Enqueue(Instantiate(bases[0]).GetComponent<Base>());
        enemy.Enqueue(Instantiate(bases[1], new Vector3(-2.5f,0.5f,-2),Quaternion.identity)
        .GetComponent<Base>());

        //TODO: 2 can be put into a variable for further level development.
        for(int i = 0; i < totalBase - 2; i++){
            neutural.Enqueue(Instantiate(bases[2], new Vector3(0,0.5f,-2+3*i),Quaternion.identity)
            .GetComponent<Base>());
        }

    }

    public void PlayerDecrease()
    {
        this.player.Dequeue();
    }
    public void PlayerIncrease(Base player)
    {
        this.player.Enqueue(player);
    }
    public void EnemyDecrease()
    {
        this.enemy.Dequeue();
    }
    public void EnemyIncrease(Base enemy)
    {
        this.enemy.Enqueue(enemy);
    }
    public void NeuturalDecrease()
    {
        this.neutural.Dequeue();
    }
}
