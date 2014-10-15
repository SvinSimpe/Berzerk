﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Berzerk
{
    public class GUI
    {
        #region Fields & Properties
        /// Always visible
        private SpriteFont      m_guiFont;
        private string          m_distanceString;
        private Vector2         m_distancePosition;
        private string          m_heightString;
        private Vector2         m_heightPosition;
        private string          m_highscoreString;
        private Vector2         m_highscorePosition;

        /// Visible when landed
        private string          m_currXpString;
        private Vector2         m_currXpPosition; 
        private string          m_xpToNextString;
        private Vector2         m_xpToNextPosition;
        private string          m_lvlString;
        private Vector2         m_lvlPosition;
        
        public string DistanceString
        {
            set{ m_distanceString = value; }
        }

        public string HeightString
        {
            set{ m_heightString = value; }
        }

        public string HighscoreString
        {
            set{ m_highscoreString = value; }
        }

        public string CurrXpString
        {
            set{ m_currXpString = value; }
        }

        public string XpToNextString
        {
            set{ m_xpToNextString = value; }
        }

        public string LvlString
        {
            set{ m_lvlString = value; }
        }
        #endregion

        #region Methods
        public GUI( ContentManager content )
        {
            m_guiFont = content.Load<SpriteFont>("GuiFont");

            m_distanceString    = "0";
            m_distancePosition  = new Vector2(15, 10);

            m_heightString      = "0";
            m_heightPosition    = new Vector2(530, 10);

            m_highscoreString   = "HIGHSCORE";
            m_highscorePosition = new Vector2(950, 10);

            m_currXpString      = "CURRXP";
            m_currXpPosition    = new Vector2(250, 300);

            m_xpToNextString    = "XPTONEXT";
            m_xpToNextPosition  = new Vector2(750, 300);

            m_lvlString         = "LVL";
            m_lvlPosition       = new Vector2(550, 400);
        }

        public void Update( GameTime gameTime )
        {

        }

        public void Draw( SpriteBatch spriteBatch, bool landed = false )
        {
            //m_distancePosition.X = m_guiFont.MeasureString( m_distanceString ).X /2;
            spriteBatch.DrawString( m_guiFont, m_distanceString,  m_distancePosition,  Color.White );
            spriteBatch.DrawString( m_guiFont, m_heightString,    m_heightPosition,    Color.White );
            spriteBatch.DrawString( m_guiFont, m_highscoreString, m_highscorePosition, Color.White );

            if ( landed )
            {
                spriteBatch.DrawString(m_guiFont, m_currXpString,   m_currXpPosition,   Color.White);
                spriteBatch.DrawString(m_guiFont, m_xpToNextString, m_xpToNextPosition, Color.White);
                spriteBatch.DrawString(m_guiFont, m_lvlString,      m_lvlPosition,      Color.White);
            }
        }
        #endregion


    }
}
