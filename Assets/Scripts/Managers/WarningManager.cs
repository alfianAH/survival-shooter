using UnityEngine;
using UnityEngine.UI;

public class WarningManager : SingletonBaseClass<WarningManager>
{
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