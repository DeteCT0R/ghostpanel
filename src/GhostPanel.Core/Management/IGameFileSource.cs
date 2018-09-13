using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Managment
{
    interface IGameFileSource
    {
        void GetGameFiles();
        void UpdateGameFiles();
    }
}
