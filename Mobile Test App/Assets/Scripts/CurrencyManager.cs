using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.Assertions;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text CurrencyText;

    public int CurrencyAmount = 0;

    public delegate void CurrencyCheck(int SkinNum);
    public static event CurrencyCheck OnCurrencyCheck;

    private void OnEnable()
    {
        ShopPurcahseButton.OnItemPurchased += OnPurchase;
        PurchaseCurrency.OnPurchaseDisc += CurrencyAdded;
    }

    private void OnDisable()
    {
        ShopPurcahseButton.OnItemPurchased -= OnPurchase;
        PurchaseCurrency.OnPurchaseDisc -= CurrencyAdded;
    }

    private void Start()
    {
        TokenSave info = SaveManager.LoadToken();
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string tokenPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/TokenData";
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document(tokenPath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                Assert.IsNull(task.Exception);

                var tokenInfo = task.Result.ConvertTo<TokenSaveCloud>();
                CurrencyAmount = tokenInfo.OwnedCurrencyAmount;
                CurrencyText.text = CurrencyAmount.ToString();
            });
        }
        else
        {
            if (info != null)
            {
                CurrencyAmount = info.OwnedCurrencyAmount;
            }
            else
            {
                SaveManager.SaveToken(this);

                if (FirebaseAuth.DefaultInstance.CurrentUser != null)
                {
                    string tokenPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/TokenData";
                    var tokenInfo = new TokenSaveCloud
                    {
                        OwnedCurrencyAmount = info.OwnedCurrencyAmount,
                    };
                    var firestore = FirebaseFirestore.DefaultInstance;
                    firestore.Document(tokenPath).SetAsync(tokenInfo);
                }

            }
        }
        CurrencyText.text = CurrencyAmount.ToString();
    }

    private void Update()
    {
        TokenSave info = SaveManager.LoadToken();
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string tokenPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/TokenData";
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document(tokenPath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                Assert.IsNull(task.Exception);

                var tokenInfo = task.Result.ConvertTo<TokenSaveCloud>();
                CurrencyAmount = tokenInfo.OwnedCurrencyAmount;
                CurrencyText.text = CurrencyAmount.ToString();
            });
        }
        else
        {
            if (info != null)
            {
                CurrencyAmount = info.OwnedCurrencyAmount;
                CurrencyText.text = CurrencyAmount.ToString();
            }
        }
    }

    public void OnPurchase(int CostAmount, int SkinNum)
    {
        if (CurrencyAmount >= CostAmount)
        {
            CurrencyAmount = CurrencyAmount - CostAmount;
            SaveManager.SaveToken(this);

            if (FirebaseAuth.DefaultInstance.CurrentUser != null)
            {
                string tokenPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/TokenData";
                TokenSave info = SaveManager.LoadToken();
                var tokenInfo = new TokenSaveCloud
                {
                    OwnedCurrencyAmount = info.OwnedCurrencyAmount,
                };
                var firestore = FirebaseFirestore.DefaultInstance;
                firestore.Document(tokenPath).SetAsync(tokenInfo);
            }

            CurrencyText.text = CurrencyAmount.ToString();
            OnCurrencyCheck?.Invoke(SkinNum);
        }
    }

    public void CurrencyAdded(int Amount)
    { 
        CurrencyAmount = CurrencyAmount + Amount;
        SaveManager.SaveToken(this);

        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string tokenPath = FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/TokenData";
            TokenSave info = SaveManager.LoadToken();
            var tokenInfo = new TokenSaveCloud
            {
                OwnedCurrencyAmount = info.OwnedCurrencyAmount,
            };
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document(tokenPath).SetAsync(tokenInfo); 
        }
        CurrencyText.text = CurrencyAmount.ToString();
    }


}
