﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nez;

namespace CloneRoids.Components
{
    public class VirtualInputUpdater : Component, IUpdatable
    {
        public void update()
        {
            VirtualInput.Update();
        }
    }
}
