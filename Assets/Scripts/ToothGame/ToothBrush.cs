// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt
// BeyazDisler

/*
 * Game's main script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBrush : MonoBehaviour
{
    #region Game Objects
    private GameObject stainObject;
    #endregion

    #region  Rendering Componenets
    [SerializeField] private Renderer upTeethMaterial;
    [SerializeField] private Renderer downTeethMaterial;
    private SpriteRenderer stainRenderer;

    [SerializeField] private Material white;
    [SerializeField] private Material yellow1;
    [SerializeField] private Material yellow2;
    [SerializeField] private Material yellow3;

    #endregion

    #region Scripts
    [SerializeField] private ScoreSystemTooth scoreSys;
    [SerializeField] private AudioController audioControl;
    [SerializeField] private ButtonController buttonControl;
    #endregion

    #region Particle System
    [SerializeField] ParticleSystem whiteGlow=null;
    [SerializeField] GameObject whiteGlowGameObj = null;
    #endregion

    #region Variables
    private float eraseAlphaVal= -0.25f;
    private float currentAlpha = 1f;

    #endregion


    private void Start()
    {
        
        upTeethMaterial.material = yellow3;
        downTeethMaterial.material = yellow3;

    }
    // Accessing the stains transparency and changing it.
    private void OnTriggerEnter(Collider stain)
    {        
        if (stain.CompareTag("Stain"))
        {
            // Play the brushing audio
            audioControl.Brushing();

            // Collider type object has changed with gameobject type
            stainObject=stain.gameObject;

            //Particle's location teleported to stain's location
            whiteGlowGameObj.transform.position=stainObject.transform.position;

            // Stain's transparency will be changed in the following function
            ChangeAlpha(stainObject);

            // Teeth color will change if milestone passed.
            TeethColor();

            whiteGlow.Play();

        }  
        
    }

    // Removes the stain by changing their alpha value. 
    private void ChangeAlpha(GameObject stainObj)
    {
        // Score!
        scoreSys.setTotalScore(1);

        // Accessing to alpha value of the collisioned stain
        stainRenderer = stainObj.GetComponent<SpriteRenderer>();
        currentAlpha = stainRenderer.color.a;

        if (currentAlpha != 0f)
        {
            currentAlpha = currentAlpha + eraseAlphaVal;
            // If the stain has been passed over 4 times, remove the stain.
            if (currentAlpha == 0f)
            {
                stainObj.SetActive(false);
            }
            // Else decrease the alpha value of the stain.
            else
            {
                stainRenderer.color = new Color(0.956f, 0.55f, 1, currentAlpha);
            }            
        }       
    }

    // Changes the teeth color depends on the milestone.
    private void TeethColor()
    {
        if (scoreSys.getTotalScore() >=30 && scoreSys.getTotalScore() < 60)   
        {
            upTeethMaterial.material = yellow2;
            downTeethMaterial.material = yellow2;
        }
        else if (scoreSys.getTotalScore() >= 60 && scoreSys.getTotalScore() < 90)  
        {
            upTeethMaterial.material = yellow1;
            downTeethMaterial.material = yellow1;
        }
        else if(scoreSys.getTotalScore() >= 90 && scoreSys.getTotalScore() < 100)   
        {
            upTeethMaterial.material = white;
            downTeethMaterial.material = white;
        }
        else if(scoreSys.getTotalScore() == 100)
        {
            buttonControl.EndTheGameButton();
        }
        
    }

}
