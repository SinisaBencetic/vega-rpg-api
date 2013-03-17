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

namespace SrcChess2
{
    /// <summary>
    /// Uses VEGA RPG AI web service to find best move
    /// </summary>
    public sealed class SearchEngineVegaRPGAI : SearchEngine
    {        
        private const string VEGA_RPG_WEB_GETMOVE = "http://localhost:1600/api/srcchess2";
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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

            var response = GetMove("");
            
            moveBest = moveList[0];

            log.Info(String.Format("Movelist.count = {0}",moveList.Count));

            return true;
        }

        private string GetMove(string dummy)
        {
            
            string responseContent = string.Empty;
            log.Info("Before request");            
            WebRequest request = WebRequest.Create(VEGA_RPG_WEB_GETMOVE);
            request.Method = "POST";
            request.ContentType = "application/xml";
            ChessBoardPoco chessboard = new ChessBoardPoco{ dummy="dummy"};

            XmlSerializer serializer = new XmlSerializer(typeof(ChessBoardPoco));            
            serializer.Serialize(new FileStream(@"c:\s.txt",FileMode.Create),chessboard);
            using (var memStream = new MemoryStream())
            {
                serializer.Serialize(memStream, chessboard);
                StreamReader reader = new StreamReader(memStream);
                string content = reader.ReadToEnd();
                request.ContentLength = memStream.Length;
                using (var requestStream = request.GetRequestStream())
                {
                    byte[] buffer = new byte[memStream.Length];
                    memStream.Read(buffer, 0, (int)memStream.Length);
                    requestStream.Write(buffer, 0, (int)buffer.Length);                    
                }
                memStream.Close();                
            }                     
            //request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();
            log.Info("waiting response...");
            using (var responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream);
                responseContent = reader.ReadToEnd();
                log.Info(string.Format("response: {0}",responseContent));
                reader.Close();
            }
            return responseContent;
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
