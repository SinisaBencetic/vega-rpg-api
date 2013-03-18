using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using System.Net;
using System.IO;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using System.Xml.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace SrcChess2
{
    /// <summary>
    /// Uses VEGA RPG AI web service to find best move
    /// </summary>
    public sealed class SearchEngineVegaRPGAI : SearchEngine
    {
        private const string VEGA_RPG_WEB_GETMOVE = "http://localhost:1600/api/srcchess2";
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static bool finished = false;

        /// <summary>
        /// Find the best move using Vega RGP AI web service
        /// </summary>
        /// <param name="chessBoard">Chess board</param>
        /// <param name="searchMode">Search mode</param>
        /// <param name="ePlayerColor">Color doing the move</param>
        /// <param name="moveList">Move list</param>
        /// <param name="arrIndex">Order of evaluation of the moves</param>
        /// <param name="posInfo">Position information</param>
        /// <param name="moveBest">Best move found</param>
        /// <param name="iPermCount">Total permutation evaluated</param>
        /// <param name="iCacheHit">Number of moves found in the translation table cache</param>
        /// <param name="iMaxDepth">Maximum depth to use</param>
        /// <returns>
        /// true if a move has been found
        /// </returns>
        protected override bool FindBestMove(ChessBoard chessBoard, SearchEngine.SearchMode searchMode, ChessBoard.PlayerColorE ePlayerColor, List<ChessBoard.MovePosS> moveList, int[] arrIndex, ChessBoard.PosInfoS posInfo, ref ChessBoard.MovePosS moveBest, out int iPermCount, out int iCacheHit, out int iMaxDepth)
        {
            iPermCount = -1;
            iCacheHit = -1;
            iMaxDepth = -1;

            if (moveList.Count < 1) return false;

            string response = string.Empty;
            finished = false;
            response = GetMove();            
            moveBest = moveList[0];

            log.Info(String.Format("Movelist.count = {0}", moveList.Count));

            return true;
        }

        private string GetMove()
        {
            using (HttpClient client = new HttpClient())
            {
                return client.GetStringAsync(VEGA_RPG_WEB_GETMOVE).Result;
            }
        }
        
            
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchEngineVegaRPGAI"/> class.
        /// </summary>
        /// <param name="trace">Trace object or null</param>
        /// <param name="rnd">Random object</param>
        /// <param name="rndRep">Repetitive random object</param>
        public SearchEngineVegaRPGAI(ITrace trace, Random rnd, Random rndRep)
            : base(trace, rnd, rndRep)
        {
        }
        [Serializable]
        public class ChessBoardPoco
        {
            public string dummy { get; set; }
        }
    }
}
