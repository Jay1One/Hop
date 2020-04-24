using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HopTrack : MonoBehaviour
{
    [SerializeField] private HopPlatform m_Platform;
    private List<HopPlatform> platforms=new List<HopPlatform>();
    //тип последней платформы на которой был игрок    
    public HopPlatform.PlatformType LastTouchedPlatformType = HopPlatform.PlatformType.Basic;
        //номер последней платформы на которой был игрок
    private int lastPlatformIndex=0;
    
    // Start is called before the first frame update
    void Start()
    {
        //Генерация платформ
        platforms.Add(m_Platform);
        for (int i = 0; i < 25; i++)
        {
            GameObject obj = GenerateRandomPlatform();
            Vector3 pos = Vector3.zero;
            pos.z = 2 * (i + 1);
            pos.x = Random.Range(-1, 2);
            obj.transform.position = pos;

            obj.name = $"Platform{i}";
            platforms.Add(obj.GetComponent<HopPlatform>());
        }
    }

    public bool IsBallOnPlatform(Vector3 position)
    {
        position.y = 0f;

        GameObject nearestPlatform = platforms[lastPlatformIndex].gameObject;

        for (int i = lastPlatformIndex; i < platforms.Count; i++)
        {
            if (platforms[i].transform.position.z + 0.5f < position.z)
            {
                continue;
            }
            nearestPlatform = platforms[i].gameObject;
            lastPlatformIndex = i;
            break;
        }

        if (nearestPlatform.transform.position.z + 0.5f < position.z)
            return false;

        float minX = nearestPlatform.transform.position.x - 0.5f;
        float maxX = nearestPlatform.transform.position.x + 0.5f;

        bool result = position.x > minX && position.x < maxX;
        if (result)
        {
            nearestPlatform.GetComponent<HopPlatform>().SetupDone();
            LastTouchedPlatformType = nearestPlatform.GetComponent<HopPlatform>().Type;
        } 
        return result;
    }

    // Создаем платформу случайного типа
    public GameObject GenerateRandomPlatform()
    {
        GameObject result= Instantiate(m_Platform.gameObject, transform);
        Array platformTypes = Enum.GetValues(typeof(HopPlatform.PlatformType));
        int i = Random.Range(0, platformTypes.Length);
        result.GetComponent<HopPlatform>().Type = (HopPlatform.PlatformType)platformTypes.GetValue(i);
            return result;
    }
    


}
