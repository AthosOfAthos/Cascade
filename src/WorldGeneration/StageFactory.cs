using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cascade.src.WorldGeneration
{
    internal class StageFactory : IStageFactory
    {
        public IStage GenerateStage()
        {
            return new SampleStage();
        }

        public void Initialize()
        {
            //TODO
        }
    }
}
