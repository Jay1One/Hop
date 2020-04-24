using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HopPlayer : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_JumpCurve;
    [SerializeField] private float m_JumpHeight=1f;
    [SerializeField] private float m_JumpDistance = 2f;
    [SerializeField] private float m_Ballspeed = 1f;
    [SerializeField] private HopInput m_Input;
    [SerializeField] private HopTrack m_Track;
    private float iteration; //цикл прыжка
    private float startZ; // точко начала прыжка
    
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        //смещение
        pos.x = Mathf.Lerp(pos.x, m_Input.Strafe, Time.deltaTime * 5f);
        pos.y = m_JumpCurve.Evaluate(iteration) * m_JumpHeight;
        //движение вперед
        pos.z = startZ + iteration * m_JumpDistance;
        transform.position = pos;
        //увеличиваем счетчик прыжка
        iteration += Time.deltaTime * m_Ballspeed;

        if (iteration < 1f)
        {
            return;
        }

        iteration = 0f;
        startZ += m_JumpDistance;
        if (m_Track.IsBallOnPlatform(transform.position))
        {
            //если мяч приземлился на платформу, меняем тип прыжка
            setJumpBehavior();
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    
    //В зависимости от типа платформы настраиваем разные свойства прыжка 
    private void setJumpBehavior()
    {
        HopPlatform.PlatformType type = m_Track.LastTouchedPlatformType;
        switch (type)
        {
         case   HopPlatform.PlatformType.Boost:
             m_JumpDistance = 4f;
             m_Ballspeed = 1.5f;
             m_JumpHeight = 1f;
             break;
         case HopPlatform.PlatformType.Jump:
             m_Ballspeed = 1f;
             m_JumpDistance = 2f;
             m_JumpHeight = 2f;
             break;
         default:
             m_JumpDistance = 2f;
             m_Ballspeed = 1f;
             m_JumpHeight = 1f;
             break;
        }
    }
}
