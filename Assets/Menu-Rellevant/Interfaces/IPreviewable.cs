using System;

namespace Cutegame.Interfaces
{
    public interface IPreviewable
    {
        bool IsPreviewable { get; set; }
        
        void StartPreview();
        void EndPreview();
        
        Action OnStartPreview { get; set; }
        Action OnEndPreview { get; set; }
    }
}