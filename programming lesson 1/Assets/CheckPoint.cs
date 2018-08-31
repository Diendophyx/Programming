using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//CheckPoint
public class CheckPoint : MonoBehaviour 
{
    #region Variables
    [Header("Check Point Elements")]
    // GameObject for our currentCheck
    public GameObject curCheckpoint;
    [Header("Character Handler")]
    // character handler script that holds the players health
    public CharacterHandler charH;
    #endregion
    #region Start
    private void Start()
    {
        charH = this.GetComponent<CharacterHandler>();
        //the character handler is the component attached to our player
        #region Check if we have Key
        //if we have a save key called SpawnPoint
        if(PlayerPrefs.HasKey("SpawnPoint"))
        {
            curCheckpoint = GameObject.Find(PlayerPrefs.GetString("SpawnPoint"));
            transform.position = curCheckpoint.transform.position;
        }
        #endregion

    }
    #endregion
    #region Update
    private void Update()
    {

        //if our characters health is less than or equal to 0
        if (charH.curHealth == 0)
        {
            //our transform.position is equal to that of the checkpoint
            transform.position = curCheckpoint.transform.position;
            //our characters health is equal to full health
            charH.curHealth = charH.maxHealth;
            //character is alive
            charH.alive = true;
            //characters controller is active
            charH.controller.enabled = true;
        }

        #endregion
    }
    #region OnTriggerEnter (collider other)
    //Collider other
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint"))
        {
            curCheckpoint = other.gameObject;
            PlayerPrefs.SetString("SpawnPoint", curCheckpoint.name);
        }
    }
      
    #endregion
}





