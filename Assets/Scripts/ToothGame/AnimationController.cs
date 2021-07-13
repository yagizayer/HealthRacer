// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt
// BeyazDisler

/*
 * Button Controller
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    #region Animators
    [SerializeField] private Animator pauseMenu;
    [SerializeField] private Animator endMenu;

    [SerializeField] private Animator boyAnim;
    [SerializeField] private Animator startAnim;
    [SerializeField] private Animator cameraAnim;
    [SerializeField] private Animator brushInOut;
    #endregion

    #region Sprites

    #endregion

    public void PauseMenuOnOff(bool pause)
    {
        pauseMenu.SetBool("pause", pause);
    }

    public void EndMenuOnOff(bool end)
    {
        endMenu.SetBool("end", end);
    }

    public void BoyIn()
    {
        boyAnim.SetBool("BoyIn", true);
    }

    public void MounthOpen(bool current)
    {
        boyAnim.SetBool("MounthOpen", current);
    }
    
    public void StartMenuOff()
    {
        startAnim.SetBool("StartOFF", true);
    }

    public void BrushInOutControl(bool cond)
    {
        brushInOut.SetBool("brushIn", cond);
        
    }
    
    public void CameraMove(int rcl)
    {
        boyAnim.SetInteger("camAnim",rcl);
    }

    

}
