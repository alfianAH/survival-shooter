using UnityEngine;
using UnityEngine.UI;

public class WarningManager : MonoBehaviour
{
    public Text warningText;
    private Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ShowWarning(float enemyDistance)
    {
        warningText.gameObject.SetActive(true);
        warningText.text = $"! {Mathf.RoundToInt(enemyDistance)} m";
        anim.SetTrigger("Warning");
    }
}