using UnityEngine;
using UnityEngine.UI;

public class WarningManager : MonoBehaviour
{
    #region Singleton

    private static WarningManager instance;
    private const string Log = nameof(WarningManager);

    public static WarningManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WarningManager>();

                if (instance == null)
                {
                    Debug.LogError($"{Log} not found");
                }
            }

            return instance;
        }
    }

    #endregion
    
    public Text warningText;
    private Animator anim;
    private static readonly int Warning = Animator.StringToHash("Warning");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ShowWarning(float enemyDistance)
    {
        warningText.gameObject.SetActive(true);
        warningText.text = $"! {Mathf.RoundToInt(enemyDistance)} m";
        anim.SetTrigger(Warning);
    }
}