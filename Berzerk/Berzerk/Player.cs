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
        private static int m_playerXP;  // Current XP
        private static int m_playerLVL; // Current LVL
        private static int m_highscore; // The furthest distance reached
        private static int m_modifier;  // Increases each level & adds to swing power

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

        public static int Modifier
        {
            get { return m_modifier; }
            set { m_modifier = value; }
        }
        #endregion

        #region Methods
        public static void Initialize()
        {
            string[] stats = ReadFile();
            m_playerXP  = Convert.ToInt32( stats[0] );
            m_playerLVL = Convert.ToInt32( stats[1] );
            m_highscore = Convert.ToInt32( stats[2] );
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
                                      Convert.ToString( Highscore ) 
                                    };
                System.IO.File.WriteAllLines("PlayerStats.txt", newStats);
            }
            else
            {
                string[] newStats = { Convert.ToString( XP ),
                                      Convert.ToString( LVL ),
                                    };
                System.IO.File.WriteAllLines("PlayerStats.txt", newStats);
            }        
        }
        #endregion
    }
}
