using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        LevelTransitions.instance.LoadScene(sceneName);
    }
}
