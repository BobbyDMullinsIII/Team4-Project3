///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Team4_Project2/Team4_Project2
//	File Name:         Driver.cs
//	Description:       Main Driver for Project 2 program
//	Course:            CSCI-4717-201 - Comp Architecture
//	Authors:           Zachary Lykins, lykinsz@etsu.edu            
//	                   Bobby Mullins, mullinsbd@etsu.edu
//	                   Christopher Poteet, poteetc1@etsu.edu
//	                   William Simmons, simmonswa@etsu.edu
//                     Isaiah Jayne, jaynei@etsu.edu
//	Created:           Monday, February  14, 2022
//	Copyright:         Team 4
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

namespace Team4_Project2
{
    /// <summary>
    ///  Driver class for starting program via a windows form gui
    /// </summary>
    internal static class Driver
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ProgramController.startProgram();
        }

    }//end Driver class

}//end Team4_Project2 namespace