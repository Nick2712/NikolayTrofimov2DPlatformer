using UnityEngine;


namespace NikolayT2DGame
{
    public interface IQuestModel
    {
        bool TryComplete(GameObject activator);
    }
}