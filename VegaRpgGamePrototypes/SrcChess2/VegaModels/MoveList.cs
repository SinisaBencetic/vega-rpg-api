using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrcChess2.VegaModels
{
    /// <summary>
    /// Encapsulate collection of moves
    /// </summary>
    public class MoveList
    {
        public List<ChessBoard.MovePosS> moves { get; set; }
    }
}
