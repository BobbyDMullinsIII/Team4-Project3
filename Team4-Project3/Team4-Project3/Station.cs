///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Team4_Project3/Team4_Project3
//	File Name:         Station.cs
//	Description:       Station class for dynamic pipeline reservation stations
//	Course:            CSCI-4717-201 - Comp Architecture
//	Authors:           Zachary Lykins, lykinsz@etsu.edu            
//	                   Bobby Mullins, mullinsbd@etsu.edu
//	                   Christopher Poteet, poteetc1@etsu.edu
//	                   William Simmons, simmonswa@etsu.edu
//                     Isaiah Jayne, jaynei@etsu.edu
//	Created:           Monday, April 5, 2022
//	Copyright:         Team 4
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team4_Project3
{
    /// <summary>
    /// Station class for dynamic pipeline reservations stations
    /// </summary>
    class Station
    {
        public string Name { get; set; }    //Name of reservation station
        public bool Busy { get; set; }      //Whether or not reservation station and its functional unit is full
        public string Op { get; set; }      //
        public string Qj { get; set; }      //
        public string Qk { get; set; }      //
        public float Vj { get; set; }       //
        public float Vk { get; set; }       //
        public string A { get; set; }       //
        public Instruction instruction { get; set; }
        public int dest { get; set; }
        public string addressingMode { get; set; }

        /// <summary>
        /// Parameterized Constructor for Station
        /// </summary>
        /// <param name="Name">Name of reservation station</param>
        public Station(string Name)
        {
            this.Name = Name;
            this.Busy = false;
            this.Op = null;
            this.Qj = null;
            this.Qk = null;
            this.Vj = 0f;
            this.Vk = 0f;
            this.A = null;
            this.instruction = null;

        }//end Station()

        /// <summary>
        /// Parameterized Constructor for Station
        /// </summary>
        /// <param name="Name">Name of reservation station</param>
        /// <param name="Busy">Whether or not reservation station and its functional unit is full</param>
        /// <param name="Op"></param>
        /// <param name="Qj"></param>
        /// <param name="Qk"></param>
        /// <param name="Vj"></param>
        /// <param name="Vk"></param>
        /// <param name="A"></param>
        public Station(string Name, bool Busy, string Op, string Qj, string Qk, float Vj, float Vk, string A, Instruction instruction, int dest, string addressingMode)
        {
            this.Name = Name;
            this.Busy = Busy;
            this.Op = Op;
            this.Qj = Qj;
            this.Qk = Qk;
            this.Vj = Vj;
            this.Vk = Vk;
            this.A = A;
            this.instruction = instruction;
            this.addressingMode = addressingMode;

        }//end Station()

    }//Station class

}//end Team4_Project3 namespace
