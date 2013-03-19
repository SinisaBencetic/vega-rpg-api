using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using log4net;
using System.Reflection;
using SrcChess2GameEngine;

namespace VegaRpgWebApi.Controllers
{
    public class SrcChess2Controller : ApiController
    {
        //public class DummyWrapper
        //{
        //    public DummyEntity dummy { get; set; }
        //    public class DummyEntity
        //    {
        //        public string value { get; set; }
        //    }
        //}                
        
        public string Get()
        {
            return "success2";
        }
                
        //public DummyWrapper Post(DummyWrapper pocoParam)
        //{            
        //    //var jsonResult = new JsonResult();
        //    //jsonResult.Data = new DummyWrapper() { dummy = new DummyWrapper.DummyEntity() { value=pocoParam.dummy.value.ToUpper() } };            
        //    var response = new DummyWrapper() { dummy = new DummyWrapper.DummyEntity() { value = pocoParam.dummy.value.ToUpper() } };
        //    return response;
        //}

        public ChessBoard.MovePosS Post(SrcChess2GameEngine.VegaModels.MoveList moveList)
        {
            var moveResult = new ChessBoard.MovePosS();
            if (moveList == null || moveList.moves == null || moveList.moves.Count==0) return moveResult;
            return moveList.moves[moveList.moves.Count-1];
        }
    }
}
