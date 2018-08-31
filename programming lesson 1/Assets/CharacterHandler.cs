using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//Character Handler
[AddComponentMenu("FirstPerson/Player Health")]

public class CharacterHandler : MonoBehaviour 
{
    #region Variables 
    [Header("Character")]
    #region Character 
    //bool to tell if the player is alive
    public bool alive;
    //connection to players character controller
    public CharacterController controller;
    #endregion
    [Header("Health")]
    #region Health
    //max and current health
    public float maxHealth;    
    public float curHealth;
    public GUIStyle healthBar;
    #endregion
    [Header("Levels and Exp")]
    #region Level and Exp
    //players current level
    public int level;
    //max and current experience 
    public int maxExp, curExp;
    #endregion
    [Header("Camera Connection")]
    #region MiniMap
    //render texture for the mini map that we need to connect to a camera
    public RenderTexture miniMap;
    #endregion
    #endregion
    #region Start
    private void Start()
    {
        //set max health to 100
        maxHealth = 100f;
        //set current health to max
        curHealth = maxHealth;
        //make sure player is alive
        alive = true;
        //max exp starts at 60
        maxExp = 60;
        //connect the Character Controller to the controller variable
        controller = this.GetComponent<CharacterController>();
    }
    #endregion
    #region Update
    private void Update()
    {
        //if our current experience is greater or equal to the maximum experience
        if(curExp >= maxExp)
        {
            //then the current experience is equal to our experience minus the maximum amount of experience
            curExp -= maxExp;
            //our level goes up by one
            level++;
            //the maximum amount of experience is increased by 50
            maxExp += 50;
        }
       // curHealth = (int)curHealth;

    }
    #endregion
    #region LateUpdate
    private void LateUpdate()
    {
        //if our current health is greater than our maximum amount of health
        if (curHealth > maxHealth)
        {
            //then our current health is equal to the max health
            curHealth = maxHealth;
        }
        //if our current health is less than 0 or we are not alive
        if (curHealth < 0)
        {
            //current health equals 0
            curHealth = 0;
            Debug.Log("if less than 0 = 0");
        }

        //if the player is alive and our health is equal to 0

        if (alive && curHealth == 0)
        {
            //alive is false
            alive = false;
            //controller is turned off
            controller.enabled = false;
            Debug.Log("Disable on death");
        }
    }
    #endregion
    #region OnGUI
    private void OnGUI()
    {
        //set up our aspect ratio for the GUI elements
        //scrW - 16
        float scrW = Screen.width / 16;
        //scrH - 9
        float scrH = Screen.height / 9;

        /*for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                GUI.Box(new Rect(scrW * x,scrH*y,scrW,scrH),"");
            }
        }
        */
        //GUI Box on screen for the healthbar background
        GUI.Box(new Rect(6*scrW,0.25f*scrH,4*scrW,0.5f*scrH),"");
        //GUI Box for current health that moves but is in the same place as the background bar//current Health divided by the posistion on screen and timesed by the total max health
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth*(4 * scrW)/maxHealth, 0.5f * scrH), "", healthBar);


        //GUI Box on screen for the experience background
        GUI.Box(new Rect(6 * scrW, 0.75f * scrH, 4 * scrW, 0.25f * scrH), "");
        //GUI Box for current experience that moves in same place as the background bar
        //current experience divided by the posistion on screen and timesed by the total max experience
        GUI.Box(new Rect(6 * scrW, 0.75f * scrH, curExp*(4 * scrW)/maxExp, 0.25f * scrH), "");

        //GUI Draw Texture on the screen that has the mini map render texture attached
        GUI.DrawTexture(new Rect(13.75f*scrW, 0.25f*scrH, 2*scrW, 2*scrH), miniMap);
    }
    #endregion
}









