using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class FloorRemovedEventArgs 
    {
        public readonly int FloorNumber;

        public FloorRemovedEventArgs(int floorNumber)
        {
            FloorNumber = floorNumber;
        }
    }
}
