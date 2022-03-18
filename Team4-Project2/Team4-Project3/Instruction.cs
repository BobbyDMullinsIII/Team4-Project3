///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Team4_Project2/Team4_Project2
//	File Name:         Instruction.cs
//	Description:       Instruction class for keeping information about a specific instruction
//	Course:            CSCI-4717-201 - Comp Architecture
//	Authors:           Zachary Lykins, lykinsz@etsu.edu            
//	                   Bobby Mullins, mullinsbd@etsu.edu
//	                   Christopher Poteet, poteetc1@etsu.edu
//	                   William Simmons, simmonswa@etsu.edu
//                     Isaiah Jayne, jaynei@etsu.edu
//	Created:           Monday, March  8, 2022
//	Copyright:         Team 4
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team4_Project2
{
    /// <summary>
    ///  Instruction class for keeping information about a specific instruction
    /// </summary>
    public class Instruction
    {
        string instLit,p1Register, p2Register = string.Empty;
        string sRegister = string.Empty;
        int fetch, decode, execute, store,  name = 0;

        /// <summary>
        /// Parameterized Constructor for Instruction
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="fetch">Number of fetch cycles</param>
        /// <param name="decode">Number of decode cycles</param>
        /// <param name="execute">Number of execute cycles</param>
        /// <param name="store">Number of register cycles</param>
        /// <param name="register">Register currently being worked on</param>
        public Instruction(int name, int fetch, int decode, int execute, int store,string sRegister,string p1Register, string instLit )
        {
            this.name = name;
            this.fetch = fetch;
            this.decode = decode;
            this.execute = execute;
            this.store = store;
            this.sRegister = sRegister;
            this.p1Register = p1Register;
            this.instLit = instLit;

        }//end Instruction()
        public Instruction(int name, int fetch, int decode, int execute, int store, string sRegister, string p1Register,string p2Register, string instLit)
        {
            this.name = name;
            this.fetch = fetch;
            this.decode = decode;
            this.execute = execute;
            this.store = store;
            this.sRegister = sRegister;
            this.p1Register = p1Register;
            this.p2Register = p2Register;
            this.instLit = instLit;

        }//end Instruction()

        public int Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Fetch
        {
            get { return fetch; }
            set { fetch = value; }
        }

        public int Decode
        {
            get { return decode; } 
            set { decode = value; }
        }

        public int Execute
        {
            get { return execute; }
            set { execute = value; }
        }

        public int Store
        {
            get { return store; }
            set { store = value; }
        }

        public string SRegister
        {
            get { return sRegister; }
            set { sRegister = value; }
        }
        public string P1Register
        {
            get { return p1Register; }
            set { p1Register = value; }
        }
        public string P2Register
        {
            get { return p2Register; }
            set { p2Register = value; }
        }
        public string InstLit
        {
            get { return instLit; }
            set { instLit = value; }
        }
    }//end Instruction class

}//end Team4_Project2 namespace
