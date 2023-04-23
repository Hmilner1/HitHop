using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Extensions;
using UnityEngine.Assertions;

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

        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            //PlayerSkin info = SaveManager.LoadPlayerSkin();
            string skinInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/SkinData";
            var firestore = FirebaseFirestore.DefaultInstance;

            firestore.Document(skinInfoPath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                Assert.IsNull(task.Exception);

                var SkinInfo = task.Result.ConvertTo<PlayerSkinCloud>();
                OwnedSkins = SkinInfo.AllOwnedSkins;
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

            });
        }
        else
        {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            //PlayerSkin info = SaveManager.LoadPlayerSkin();
            string skinInfoPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/SkinData";
            var firestore = FirebaseFirestore.DefaultInstance;

            firestore.Document(skinInfoPath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                Assert.IsNull(task.Exception);

                var SkinInfo = task.Result.ConvertTo<PlayerSkinCloud>();
                for (int i = 0; i < m_Images.Length; i++)
                {
                    if (i == SkinInfo.CurrentSkin)
                    {
                        m_Images[i].color = new Color(m_Images[i].color.r, m_Images[i].color.g, m_Images[i].color.b, 1f);
                    }
                    else
                    {
                        m_Images[i].color = new Color(m_Images[i].color.r, m_Images[i].color.g, m_Images[i].color.b, 0.5f);
                    }
                }

            });
        }
        else
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
}
