using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


// Used for the Hat selection logic
public class PlayerConfigurator : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HatInstance;

    [SerializeField]
    private Transform m_HatAnchor;

    private AsyncOperationHandle<GameObject> m_HatLoadOpHandle;

    void Start()
    {
        //SetHat(string.Format("Hat{0:00}", GameManager.s_ActiveHat));
        LoadInRandomHat();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Destroy(m_HatInstance);
            Addressables.ReleaseInstance(m_HatLoadOpHandle);

            LoadInRandomHat();
        }
    }

    private void LoadInRandomHat()
    {
        int randomIndex = UnityEngine.Random.Range(0, 6);
        string hatAddress = string.Format("Hat{0:00}", randomIndex);

        m_HatLoadOpHandle = Addressables.LoadAssetAsync<GameObject>(hatAddress);
        m_HatLoadOpHandle.Completed += OnHatLoadedComplete;
    }

    //public void SetHat(string hatKey)
    //{
    //    //m_HatLoadOpHandle = Resources.LoadAsync(hatKey);
    //    //m_HatLoadOpHandle.completed += OnHatLoaded;

    //    //m_HatLoadOpHandle = Addressables.LoadAssetAsync<GameObject>(m_Address);        
    //    //m_HatLoadOpHandle.Completed += OnHatLoadedComplete;

    //    if (!m_HatInstance.RuntimeKeyIsValid())
    //    {
    //        return;
    //    }

    //    m_HatLoadOpHandle = m_HatInstance.LoadAssetAsync<GameObject>();
    //    m_HatLoadOpHandle.Completed += OnHatLoadedComplete;
    //}

    private void OnHatLoadedComplete(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        Debug.Log($"AsynscOperationHandle Status: {asyncOperationHandle.Status}");
        if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            m_HatInstance = Instantiate(asyncOperationHandle.Result, m_HatAnchor);
        }
        
    }

    private void OnDisable()
    {
        m_HatLoadOpHandle.Completed -= OnHatLoadedComplete;
    }
}
