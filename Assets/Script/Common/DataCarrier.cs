using UnityEngine;

public class DataCarrier : MonoBehaviour
{
    public static DataCarrier Instance { get; private set; }

    private ResultData resultData;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetData (ResultData data)
    {
        this.resultData = data;
    }

    public ResultData GetData ()
    {
        return resultData;
    }
}
