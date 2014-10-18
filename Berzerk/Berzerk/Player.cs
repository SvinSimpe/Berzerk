using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameLib;

namespace Berzerk
{
    public static class Player
    {
        #region Fields & Properties
        private static int      m_playerXP;         // Current XP
        private static int      m_playerLVL;        // Current LVL
        private static int      m_highscore;        // The furthest distance reached
        private static float    m_modifier;         // Increases each level & adds to swing power
        private static int      m_currentDistance;  // Distance from each round
        private static int      m_xpLimit;          // XP-limit of current lvl
        private static int      m_nrOfPowerUps;     // Number of power ups, modifies XP and reset each round
        private static int      m_playerGold;       // Amout of gold

        public static int XP
        {
            get { return m_playerXP; }
            set { m_playerXP = value; }
        }

        public static int LVL
        {
            get { return m_playerLVL; }
            set { m_playerLVL = value; }
        }

        public static int Highscore
        {
            get { return m_highscore; }
            set { m_highscore = value; }
        }

        public static float Modifier
        {
            get { return m_modifier; }
            set { m_modifier = value; }
        }

        public static int CurrentDistance
        {
            get { return m_currentDistance; }
            set { m_currentDistance = value; }
        }

        public static int XpLimit
        {
            get { return m_xpLimit; }
            set { m_xpLimit = value; }
        }

        public static int NrOfPowerUps
        {
            get { return m_nrOfPowerUps; }
            set { m_nrOfPowerUps = value; }
        }

        public static int PlayerGold
        {
            get { return m_playerGold; }
            set { m_playerGold = value; }
        }
        #endregion

        #region Methods
        public static void Initialize()
        {
            string[] stats = ReadFile();
            XP          = Convert.ToInt32( stats[0] );
            LVL         = Convert.ToInt32( stats[1] );
            Highscore   = Convert.ToInt32( stats[2] );
            Modifier    = (float)Convert.ToDecimal( stats[3] );
            XpLimit     = Convert.ToInt32( stats[4] );
            PlayerGold  = Convert.ToInt16( stats[5] );

            CurrentDistance = 0;
            m_nrOfPowerUps  = 0;
        }

        public static string[] ReadFile()
        {
            string[] savedStats = System.IO.File.ReadAllLines("PlayerStats.txt");
            return savedStats;
        }

        public static void WriteFile()
        {
            string[] savedStats = ReadFile();

            // If new Highscore
            if (Convert.ToInt32(savedStats[2]) < Highscore)
            {
                string[] newStats = { Convert.ToString( XP ),
                                      Convert.ToString( LVL ),
                                      Convert.ToString( Highscore ),
                                      Convert.ToString( Modifier ),
                                      Convert.ToString( XpLimit ),
                                      Convert.ToString( PlayerGold )
                                    };
                System.IO.File.WriteAllLines("PlayerStats.txt", newStats);
            }
            else
            {
                string[] newStats = { Convert.ToString( XP ),
                                      Convert.ToString( LVL ),
                                      savedStats[2],
                                      Convert.ToString( Modifier ),
                                      Convert.ToString( XpLimit ),
                                      Convert.ToString( PlayerGold )
                                    };
                System.IO.File.WriteAllLines("PlayerStats.txt", newStats);
            }        
        }

        public static void CheckPlayerLvl()
        {
            // Convert distance to XP
            XP += CurrentDistance/100;
            
            // Check if PowerUps is taken
            if( NrOfPowerUps > 0 )
                XP *= (int)(1.0f + ( NrOfPowerUps * 0.5f ) );

            while ( XP >= XpLimit )
            {
                //Reset XP but save remainder
                XP = XP - XpLimit;

                // Increase XP-limit with 50%
                XpLimit = (int)((float)XpLimit * (float)1.5);


                //Increase LVL
                LVL++;

                //Increase Mofifier
                Modifier *= (float)1.1;    
            }
        }
        #endregion
    }
}
