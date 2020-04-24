using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopCamera : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    [SerializeField] private float m_Distance = 2f;
    [SerializeField] private float m_Height = 2f;
    


    // Update is called once per frame
    void Update()
    {
        float z = Mathf.Lerp(transform.position.z, m_Target.position.z - m_Distance, Time.deltaTime*5f);
        Vector3 pos=new Vector3( 0f, m_Height, z);
        transform.position = pos;
        
    }
}
