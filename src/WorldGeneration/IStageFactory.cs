using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.WorldGeneration
{
    internal interface IStageFactory
    {
        void Initialize();

        IStage GenerateStage();

    }


}
