using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameLogic gl;
    // public BubbleSpawner bs;

    public void SpawnTarget(GameObject tst)
    {
        if(gl.GetCurrentStage() == GameLogic.GameStage.Numbers)
        {
            AssignTargetNum(tst);
        }
        else if(gl.GetCurrentStage() == GameLogic.GameStage.Alphabet)
        {
            AssignTargetAlph(tst);
        }
    }
    void AssignTargetNum(GameObject obj)
    {
        BubbleData data = obj.GetComponent<BubbleData>();
        data.SetValue(gl.GetTarget());
    }
    void AssignTargetAlph(GameObject obj)
    {
        BubbleData data = obj.GetComponent<BubbleData>();
        data.SetValue(gl.GetTarget());
    }
}
