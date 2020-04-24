using System;
using UnityEngine;
using UnityEngine.Serialization;


public class HopPlatform : MonoBehaviour
    { 
        //Тип платформы
        public enum PlatformType
        {
            Basic,
            Jump,
            Boost
        }
        
        [SerializeField] private GameObject m_BasePlatform;
        [SerializeField] private GameObject m_JumpPlatform;
        [SerializeField] private GameObject m_BoostPlatform;
        [SerializeField] private GameObject m_ActivatedPlatform;
        public PlatformType Type;
        

            //настраиваем платформу в зависимости от типа
        private void Start()
        {
            switch (Type)
            {
                case PlatformType.Basic:
                    m_BasePlatform.SetActive((true));
                    m_JumpPlatform.SetActive((false));
                    m_BoostPlatform.SetActive(false);
                    break;
                case PlatformType.Jump:
                    m_BasePlatform.SetActive((false));
                    m_JumpPlatform.SetActive((true));
                    m_BoostPlatform.SetActive(false);
                    break;
                case PlatformType.Boost:
                    m_BasePlatform.SetActive((false));
                    m_JumpPlatform.SetActive((false));
                    m_BoostPlatform.SetActive(true);
                    break;
                default:
                    m_BasePlatform.SetActive((true));
                    m_JumpPlatform.SetActive((false));
                    m_BoostPlatform.SetActive(false);
                    break;
            }
            m_ActivatedPlatform.SetActive(false);

        }

        public void SetupDone()
        {
            switch (Type)
            {
                case PlatformType.Basic:
                    m_BasePlatform.SetActive((false));
                    m_ActivatedPlatform.SetActive(true);
                    break;
                case PlatformType.Jump:
                    m_JumpPlatform.SetActive((false));
                    m_ActivatedPlatform.SetActive(true);                    
                    break;
                case PlatformType.Boost:
                    m_BoostPlatform.SetActive(false);
                    m_ActivatedPlatform.SetActive(true);
                    break;
                default:
                    m_BasePlatform.SetActive((false));
                    m_ActivatedPlatform.SetActive(true);
                    break;
            }
        }
    }
