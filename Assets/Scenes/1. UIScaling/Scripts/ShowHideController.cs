using UnityEngine;

public class ShowHideController : MonoBehaviour
{
    public Canvas TargetUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetUI.enabled = false;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShowUIClicked()
    {
        TargetUI.enabled = true;
    }

    public void OnHideUIClicked()
    {
        TargetUI.enabled = false;
    }
}
