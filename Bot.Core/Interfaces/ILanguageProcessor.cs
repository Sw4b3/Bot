﻿using Bot.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Interfaces
{
    public interface IBotLogicController
    {
        void ProccessQuery(Query query);
    }
}
