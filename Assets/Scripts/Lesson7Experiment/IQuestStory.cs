using System;


namespace NikolayT2DGame
{
    public interface IQuestStory : IDisposable
    {
        bool IsDone { get; }
    }
}