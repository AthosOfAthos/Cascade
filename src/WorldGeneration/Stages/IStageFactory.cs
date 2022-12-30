using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.WorldGeneration.Stages
{
    internal interface IStageFactory
    {
        void Initialize(Game game);

        IStage GenerateStage();

    }


}
