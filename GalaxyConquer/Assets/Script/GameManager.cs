
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance; //singleton
    void Awake(){
        instance = this;
    }
    private Planet sourceBase;
    private Planet destBase;

    public void Deploy(Planet selectedBase){
        //TODO: Add Interaction: Highlight?
        //SELECT THE SOURCE BASE, ONLY PlAYER COULD BE SELECTED
        if(sourceBase ==null && selectedBase!=null && selectedBase.type == Type.PLAYER) {
            sourceBase = selectedBase;
            return;
        }
        //DESELECTION
        if(sourceBase==selectedBase||selectedBase==null){
            sourceBase = null;
            return;
        }

        //SELECT THE DESTINATOIN BASE
        if(selectedBase!=null && sourceBase!=null){
            destBase = selectedBase;
            //Deploy unit!!!!!
            sourceBase.Attack(destBase);
            //Clear 
            sourceBase = null;
            destBase = null;
        }
    }
    private void Update()
    {
        if (PlanetManager.instance.player.Count == 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
        if (PlanetManager.instance.enemy.Count == 0)
        {
            //TODO: Determine the final win condition.
            //&& PlanetManager.instance.neutural.Count==0
            SceneManager.LoadScene("WinScene");
        }
    }
}
