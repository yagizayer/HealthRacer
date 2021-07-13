//Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

 /* When clicked to hint button, script displays the 
  * red frame on the correct place where should object go
  * with an animation
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HintSystem : MonoBehaviour
{
    // Objects code imported from another script
    [SerializeField] private RandomOrgan randOrg;
    private GameObject hintFrame;

    // Add CanvasGroup as a component to object
    private CanvasGroup trans;

    //Animation of the slot
    private Animator slotAnim;

    private int tempCurrentOrg = 0;

    private void Update()
    {
        if (tempCurrentOrg != randOrg.getOrgNumber() && tempCurrentOrg>0)
        {
            // Make unvisible the frame and set default settings.
            //trans.alpha = 0.0f;

            //Closes the animation after the object replaced
            slotAnim.SetBool("playhint", false);
        }
        
    }

    // Pops out 
    public void redFrame()
    {
        tempCurrentOrg = randOrg.getOrgNumber();
        hintFrame=randOrg.getSlotBox();

        // Frames canvas group's alpha assigned.
        //trans = hintFrame.GetComponent<CanvasGroup>();
        // Changes transparent per centage of the slot.
        //trans.alpha = 1.0f;

        //Animation controller
        slotAnim = hintFrame.GetComponent<Animator>();
        slotAnim.SetBool("playhint", true);

    }
}
