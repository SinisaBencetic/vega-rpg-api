﻿using System;

namespace SrcChess2GameEngine {
    /// <summary>
    /// Handle the timer for both player
    /// </summary>
    public class GameTimer {
        /// <summary>true if timer is tickling</summary>
        private bool                    m_bEnabled;
        /// <summary>Time of last commit</summary>
        private DateTime                m_timerStart;
        /// <summary>Commited time for the white</summary>
        private TimeSpan                m_timeSpanCommitedWhite;
        /// <summary>Commited time for the black</summary>
        private TimeSpan                m_timeSpanCommitedBlack;
        /// <summary>Player currently playing</summary>
        private ChessBoard.PlayerColorE m_ePlayerColor;

        /// <summary>
        /// Class constructor
        /// </summary>
        public GameTimer() {
            m_bEnabled      = false;
            Reset(ChessBoard.PlayerColorE.White);
        }

        /// <summary>
        /// Commit the uncommited time to the current player
        /// </summary>
        private void Commit() {
            DateTime    now;
            TimeSpan    span;
            
            if (m_bEnabled) {
                now             = DateTime.Now;
                span            = now - m_timerStart;
                m_timerStart    = now;
                if (m_ePlayerColor == ChessBoard.PlayerColorE.White) {
                    m_timeSpanCommitedWhite += span;
                } else {
                    m_timeSpanCommitedBlack += span;
                }
            }
        }

        /// <summary>
        /// Enabled state of the timer
        /// </summary>
        public bool Enabled {
            get {
                return(m_bEnabled);
            }
            set {
                if (value != m_bEnabled) {
                    if (value) {
                        m_timerStart = DateTime.Now;
                    } else {
                        Commit();
                    }
                    m_bEnabled = value;
                }
            }
        }

        /// <summary>
        /// Reset the timer of both player
        /// </summary>
        /// <param name="ePlayerColor"> Playing color</param>
        /// <param name="lWhiteTicks">  White Ticks</param>
        /// <param name="lBlackTicks">  Black Ticks</param>
        public void ResetTo(ChessBoard.PlayerColorE ePlayerColor, long lWhiteTicks, long lBlackTicks) {
            m_ePlayerColor          = ePlayerColor;
            m_timeSpanCommitedWhite = new TimeSpan(lWhiteTicks);
            m_timeSpanCommitedBlack = new TimeSpan(lBlackTicks);
            m_timerStart            = DateTime.Now;
        }

        /// <summary>
        /// Reset the timer of both player
        /// </summary>
        /// <param name="ePlayerColor"> Playing color</param>
        public void Reset(ChessBoard.PlayerColorE ePlayerColor) {
            ResetTo(ePlayerColor, 0, 0);
        }

        /// <summary>
        /// Color of the player playing
        /// </summary>
        public ChessBoard.PlayerColorE PlayerColor {
            get {
                return(m_ePlayerColor);
            }
            set {
                m_ePlayerColor = value;
            }
        }

        /// <summary>
        /// Time spent by the white player
        /// </summary>
        public TimeSpan WhitePlayTime {
            get {
                Commit();
                return(m_timeSpanCommitedWhite);
            }
        }

        /// <summary>
        /// Time spent by the black player
        /// </summary>
        public TimeSpan BlackPlayTime {
            get {
                Commit();
                return(m_timeSpanCommitedBlack);
            }
        }

        /// <summary>
        /// Time span to string
        /// </summary>
        public static string GetHumanElapse(TimeSpan timeSpan) {
            string  strRetVal;
            int     iIndex;
            
            strRetVal = timeSpan.ToString();
            iIndex    = strRetVal.IndexOf(':');
            if (iIndex != -1) {
                iIndex = strRetVal.IndexOf('.', iIndex);
                if (iIndex != -1) {
                    strRetVal = strRetVal.Substring(0, iIndex);
                }
            }
            return(strRetVal);
        }
    } // Class GameTimer
} // Namespace
