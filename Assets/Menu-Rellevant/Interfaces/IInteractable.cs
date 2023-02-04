using System;

namespace Cutegame.Interfaces
{
    public interface IInteractable
    {
        bool IsInteractable { get; set; }

        Action OnStartInteraction { get; set; }
        Action OnEndInteraction { get; set; }

        void StartInteraction();
        void EndInteraction();
    }
}