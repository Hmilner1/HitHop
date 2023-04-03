using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipableSkins : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_SkinButtons;
    private List<int> OwnedSkins;

    [SerializeField]
    private GameObject[] m_ImageObject;
    [SerializeField]
    private Image[] m_Images;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_ImageObject.Length; i++)
        {
            m_Images[i] = m_ImageObject[i].GetComponent<Image>();
        }
        foreach (Image pic in m_Images)
        { 
            pic.color = new Color(pic.color.r, pic.color.g, pic.color.b, 0.5f);
        }

        PlayerSkin info = SaveManager.LoadPlayerSkin();
        OwnedSkins = info.AllOwnedSkins;
        foreach (GameObject Button in m_SkinButtons)
        {
            Button.SetActive(false);
            for (int i = 0; i < m_SkinButtons.Length; i++)
            {
                if (OwnedSkins.Contains(Int32.Parse(m_SkinButtons[i].name)))
                {
                    m_SkinButtons[i].SetActive(true);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        PlayerSkin info = SaveManager.LoadPlayerSkin();
        for (int i = 0; i < m_Images.Length; i++)
        {
            if (i == info.CurrentSkin)
            {
                m_Images[i].color = new Color(m_Images[i].color.r, m_Images[i].color.g, m_Images[i].color.b, 1f);
            }
            else
            {
                m_Images[i].color = new Color(m_Images[i].color.r, m_Images[i].color.g, m_Images[i].color.b, 0.5f);
            }
        }
    }
}
