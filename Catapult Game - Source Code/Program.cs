﻿
#region File Description
//-----------------------------------------------------------------------------
// Program.cs
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
#endregion

namespace CatapultGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CatapultGame game = new CatapultGame())
            {
                game.Run();
            }
        }
    }
#endif
}

