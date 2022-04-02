/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Team4_Project3/Team4_Project3
//	File Name:         GUIForm.cs
//	Description:       GUIForm class for program GUI to show visual dynamic pipeline simulation
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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace Team4_Project3
{
    public partial class GUIForm : Form
    {
        //Conducting Static or Dynamic Simulation Bool
        bool isDynamic = false;


        //==Counters==//
        //============================================================//
        //Cycle Counter
        int cycleCounter = 0;

        //Delay Counters (Dynamic)
        int bufferD = 0;
        int stationD = 0;
        int conflictD = 0;
        int dependenceD = 0;

        //Hazard Counters (Static)
        int structHCount = 0;
        int dataHCount = 0;
        int controlHCounter = 0;

        //Dependency Counters (Static)
        int rawCount = 0;
        int warCount = 0;
        int wawCount = 0;

        //Stall Counters (Static)
        int fStall = 0;
        int dStall = 0;
        int eStall = 0;
        int sStall = 0;


        //==Flags==//
        //============================================================//
        //Dependency Flags
        bool rawFlag = true;
        bool warFlag = true;
        bool wawFlag = true;

        //Stall Flags
        bool fFlagCount = true;
        bool dFlagCount = true;
        bool eFlagCount = true;
        bool sFlagCount = true;


        //==Registers==//
        //============================================================//
        //Regular Registers
        int R0 = 0; //Program Counter
        int R1 = 0; //Z (Zero) Flag
        int R2 = 0; //C (Carry) Flag
        int R3 = 0; //S (Sign) Flag
        int R4 = 0;
        int R5 = 0;
        int R6 = 0;
        int R7 = 0;
        int R8 = 0;
        int R9 = 0;
        int R10 = 0;
        int R11 = 0;

        //Floating-Point Registers
        float F12 = 0f;
        float F13 = 0f;
        float F14 = 0f;
        float F15 = 0f;


        //==1MB Memory Array==//
        //============================================================//
        String[,] Memory = new String[65536, 17];


        //List of all assembly instructions
        List<string> instructions = new List<string>();

        //Current statically fetched instructions
        List<Instruction> pipeFetch = new List<Instruction>();
        List<Instruction> pipeDecode = new List<Instruction>();
        List<Instruction> pipeExecute = new List<Instruction>();
        List<Instruction> pipeStore = new List<Instruction>();

        int programIndex = 0;
        bool start = true;

        bool fWall, dWall, eWall, sWall = true;
        bool fGo, dGo, eGo, sGo = false;

        int stopF = 0;
        bool ifStop = false;

        string param1, param2, store = string.Empty;

        bool rF1 = true;
        bool rF2 = true;


        //GUIForm Constructor
        #region GUIForm Constructor
        /// <summary>
        /// GUIForm Constructor
        /// </summary>
        public GUIForm()
        {
            InitializeComponent();
        }
        #endregion

        //GUIForm Button Methods
        #region Dropdown Menu Buttons
        /// <summary>
        /// Opens instruction set information
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramController.openInformation();
        }

        /// <summary>
        /// Resets everything for new simulation
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Reset all variables within GUIForm
            resetAllVariables();

            //Reset Dynamic Phase textboxes
            issueTextBox.Text = string.Empty;
            dynamicExecuteTextBox.Text = string.Empty;
            memoryReadTextBox.Text = string.Empty;
            writeTextBox.Text = string.Empty;
            commitTextBox.Text = string.Empty;

            //Reset Static Phase textboxes
            fetchTextBox.Text = string.Empty;
            decodeTextBox.Text= string.Empty;
            executeTextBox.Text = string.Empty;
            storeTextBox.Text = string.Empty;

            //Reset Register textboxes
            r0TextBox.Text = "0";
            r1TextBox.Text = "0";
            r2TextBox.Text = "0";
            r3TextBox.Text = "0";
            r4TextBox.Text = "0";
            r5TextBox.Text = "0";
            r6TextBox.Text = "0";
            r7TextBox.Text = "0";
            r8TextBox.Text = "0";
            r9TextBox.Text = "0";
            r10TextBox.Text = "0";
            r11TextBox.Text = "0";
            f12TextBox.Text = "0.0";
            f13TextBox.Text = "0.0";
            f14TextBox.Text = "0.0";
            f15TextBox.Text = "0.0";

            //Reset Cycle Counter textbox
            counterTextBox.Text = "0";

            //Reset Hazards textboxes
            structHTextBox.Text = "0";
            dataHTextBox.Text = "0";
            controlHTextBox.Text = "0";

            //Reset Dependencies textboxes
            rawTextBox.Text = "0";
            warTextBox.Text = "0";
            wawTextBox.Text = "0";

            //Reset Stalls textboxes
            fetchStallTextbox.Text = "0";
            decodeStallTextbox.Text = "0";
            executeStallTextbox.Text = "0";
            storeStallTextbox.Text = "0";

            //Reset Pipeline Output textboxe
            pipeLineOutText.Text = string.Empty;

            //Re-enables Assembly textbox and Start Simulation buttons
            assemblyTextBox.Enabled = true;
            startDynamicButton.Enabled = true;
            startStaticButton.Enabled = true;
        }

        /// <summary>
        /// Exits program
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramController.exitProgram();
        }
        #endregion

        #region Assembly TextBox Buttons
        /// <summary>
        /// Clears assembly language text box
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void clearAssemblyButton_Click(object sender, EventArgs e)
        {
            assemblyTextBox.Text = "";
        }

        /// <summary>
        /// Loads content from file to assembly language text box
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void loadAssemblyButton_Click(object sender, EventArgs e)
        {
            assemblyTextBox.Text = ProgramController.openFile();
        }

        /// <summary>
        /// Saves content inside of assembly language text box into a file
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void saveAssemblyButton_Click(object sender, EventArgs e)
        {
            ProgramController.saveFile(assemblyTextBox.Text);
        }
        #endregion

        #region Pipeline TextBox Buttons
        /// <summary>
        /// Clears pipeline output text box
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void clearPipelineOutputButton_Click(object sender, EventArgs e)
        {
            pipelineOutput.Text = "";
        }

        /// <summary>
        /// Loads content from file to pipeline output text box
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void loadPipelineOutputButton_Click(object sender, EventArgs e)
        {
            pipelineOutput.Text = ProgramController.openFile();
        }

        /// <summary>
        /// Saves content inside of pipeline output text box into a file
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void savePipelineOutputButton_Click(object sender, EventArgs e)
        {
            ProgramController.saveFile(pipelineOutput.Text);
        }
        #endregion

        #region Pipeline Simulation GUI Buttons
        /// <summary>
        /// Starts Dynamic Pipeline Simulation
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void startDynamicButton_Click(object sender, EventArgs e)
        {
            //Tells program that current simulation is dynamic
            isDynamic = true;

            //Instantiate and display initial memory
            instantiateMemory();
            storeMemoryInString();

            //Start dynamic pipeline simulation
            startSimulation();
        }

        /// <summary>
        /// Starts Static Pipeline Simulation
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void startStaticButton_Click(object sender, EventArgs e)
        {
            //Tells program that current simulation is static
            isDynamic = false;

            //Instantiate and display initial memory
            instantiateMemory();
            storeMemoryInString();

            //Start static pipeline simulation
            startSimulation();
        }

        /// <summary>
        /// Goes to next cycle within simulation
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void nextCycleButton_Click(object sender, EventArgs e)
        {
            if (isDynamic == true)
            {
                nextDynamicCycle();
            }
            else
            {
                nextStaticCycle();  
            }
        }
        #endregion

        //GUIForm Regular Methods
        #region resetAllVariables() Method
        /// <summary>
        /// Method for resetting all variables in GUIFOrm to start another simulation without closing program
        /// </summary>
        public void resetAllVariables()
        {
            //Conducting Static or Dynamic Simulation Bool
            isDynamic = false;

            //==Counters==//
            //============================================================//
            //Cycle Counter
            cycleCounter = 0;

            //Delay Counters
            bufferD = 0;
            stationD = 0;
            conflictD = 0;
            dependenceD = 0;

            //Hazard Counters
            structHCount = 0;
            dataHCount = 0;
            controlHCounter = 0;

            //Dependency Counters
            rawCount = 0;
            warCount = 0;
            wawCount = 0;

            //Stall Counters
            fStall = 0;
            dStall = 0;
            eStall = 0;
            sStall = 0;


            //==Flags==//
            //============================================================//
            //Dependency Flags
            rawFlag = true;
            warFlag = true;
            wawFlag = true;

            //Stall Flags
            fFlagCount = true;
            dFlagCount = true;
            eFlagCount = true;
            sFlagCount = true;


            //==Registers==//
            //============================================================//
            //Regular Registers
            R0 = 0; //Program Counter
            R1 = 0; //Flag Z (Zero)
            R2 = 0; //Flag C (Carry)
            R3 = 0; //Flag S (Sign)
            R4 = 0;
            R5 = 0;
            R6 = 0;
            R7 = 0;
            R8 = 0;
            R9 = 0;
            R10 = 0;
            R11 = 0;

            //Floating-Point Registers
            F12 = 0f;
            F13 = 0f;
            F14 = 0f;
            F15 = 0f;


            //==1MB Memory Array==//
            //============================================================//
            Memory = new String[65536, 17];


            //List of all assembly instructions
            instructions = new List<string>();

            //Currently fetched instructions
            pipeFetch = new List<Instruction>();
            pipeDecode = new List<Instruction>();
            pipeExecute = new List<Instruction>();
            pipeStore = new List<Instruction>();

            programIndex = 0;
            start = true;

            fWall = true;
            dWall = true;
            eWall = true; 
            sWall = true;
            fGo = false;
            dGo = false;
            eGo = false;
            sGo = false;

            stopF = 0;
            ifStop = false;

            param1 = string.Empty;
            param2 = string.Empty;
            store = string.Empty;

            rF1 = true;
            rF2 = true;

        }//end resetAllVariables()
        #endregion    

        #region incrementCycleCounter() Method
        /// <summary>
        /// Method for incrementing cycle counter and updating gui to reflect it
        /// </summary>
        public void incrementCycleCounter()
        {
            cycleCounter++;
            counterTextBox.Text = cycleCounter.ToString();

        }//end incrementCycleCounter()
        #endregion    

        #region instantiateMemory() Method
        /// <summary>
        /// Method for instantiating memory array
        /// </summary>
        public void instantiateMemory()
        {
            //Instantiate Memory (1MB)
            int memHelp = 0;
            for (int i = 0; i < 65536; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (j == 0)
                    {
                        Memory[i, j] = $"{string.Format("{0}", memHelp.ToString("X5"))} \t";
                        memHelp += 16;
                    }
                    else
                    {
                        Memory[i, j] = "0  ";
                    }
                }
            }

        }//end instantiateMemory()
        #endregion   

        #region storeMemoryInString() Method
        /// <summary>
        /// Method for storing memory into single string for a textbox
        /// </summary>
        public void storeMemoryInString()
        {
            //Store all memory into single string
            StringBuilder memString = new StringBuilder();
            for (int i = 0; i < 65536; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (j == 16)
                    {
                        memString.Append(Memory[i, j]);
                        memString.Append("\r\n");
                    }
                    else
                    {
                        memString.Append(Memory[i, j]);
                    }
                }
            }

            //Output memString to Textbox
            memOutputText.Text = Convert.ToString(memString);

        }//end storeMemoryInString()
        #endregion  

        #region startSimulation() Method
        /// <summary>
        /// Method for starting either static or dynamic pipeline simulation
        /// </summary>
        public void startSimulation()
        {
            //If assemblyTextBox has no code in it, show error message
            if (string.IsNullOrWhiteSpace(assemblyTextBox.Text) == true)
            {
                MessageBox.Show("There is no assembly code to start the simulation.",
                                "Error - No Code To Process",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            //Else, start initial pipeline simulation setup
            else
            {
                initialPipelineSetup();
            }

        }//end startSimulation()
        #endregion  

        #region initialPipelineSetup() Method
        /// <summary>
        /// Method for setting up initial static or dynamicpipeline simulation instructions and buttons
        /// </summary>
        public void initialPipelineSetup()
        {
            //Store each instruction into instructions list for current pipeline simulation
            foreach (string var in assemblyTextBox.Text.Split())
            {
                instructions.Add(var);
            }

            //Make textbox for assembly language readonly during simulation to not mess anything up
            assemblyTextBox.ReadOnly = true;

            //Enables nextPhaseButton to be pressed when simulation has began and disables both start buttons
            nextCycleButton.Enabled = true;
            startStaticButton.Enabled = false;
            startDynamicButton.Enabled = false;

        }//end initialPipelineSetup()
        #endregion  

        #region nextDynamicCycle() Method
        /// <summary>
        /// Method for going to next cycle in dynamic pipeline simulation
        /// </summary>
        public void nextDynamicCycle()
        {
            //=========================================================================//
            //INSERT CODE WITHIN THIS METHOD FOR DYNAMIC PIPELINE SIMULATION NEXT CYCLE//
            //=========================================================================//

            //Increase cycle counter by one
            incrementCycleCounter();

            //Output Dynamic Pipeline Simulation Final Statistics
            ProgramController.outputDynamicPipelineStats(bufferD, stationD, conflictD, dependenceD);


        }//end nextDynamicCycle()
        #endregion 

        #region nextStaticCycle() Method
        /// <summary>
        /// Method for going to next cycle in static pipeline simulation
        /// </summary>
        public void nextStaticCycle()
        {
            if (start == true)
            {
                dWall = true;
                sWall = true;
                eWall = true;
            }

            if (pipeStore.Count > 0)
            {
                if (pipeStore.Count > 0)
                    pipeStore[0].Store--;
                if (pipeStore[0].Store == 0)
                {
                    pipeStore.RemoveAt(0);
                    sGo = false;
                    sWall = true;

                }
                if (ifStop == true && pipeStore.Count == 0)
                {
                    nextCycleButton.Enabled = false;
                    pipeLineOutText.Text = ProgramController.outputStaticPipelineStats(structHCount, dataHCount, 0, rawCount, warCount, 0, fStall, dStall, eStall, sStall, cycleCounter);
                }
            }

            if (pipeExecute.Count > 0)
            {

                if (sGo != true)
                {
                    pipeExecute[0].Execute--;
                }
                ifStop = ProgramController.execute(pipeExecute[0].InstLit);
                if (pipeExecute[0].Execute <= 0 && ifStop == false)
                {
                    sGo = true;

                }
                if (sGo == true && sWall == true)
                {

                    pipeStore.Add(pipeExecute[0]);
                    sWall = false;
                    pipeExecute.RemoveAt(0);
                    eWall = true;

                    eGo = false;
                    sGo = false;

                    eFlagCount = true;
                }
                if (sGo == true && sWall == false && pipeExecute.Count > 0)
                {
                    eStall++;
                    executeStallTextbox.Text = eStall.ToString();

                    if (eFlagCount == true)
                    {
                        structHCount++;
                        structHTextBox.Text = structHCount.ToString();

                        eFlagCount = false;
                    }

                }

            }

            if (pipeDecode.Count > 0)
            {


                if (eGo != true)
                {
                    pipeDecode[0].Decode--;
                }
                if (pipeDecode[0].Decode <= 0)
                {
                    eGo = true;
                }
                if (eGo == true && eWall == true && rawFlag == true)
                {
                    pipeExecute.Add(pipeDecode[0]);
                    eWall = false;
                    pipeDecode.RemoveAt(0);
                    dWall = true;
                    dGo = false;
                    eGo = false;

                    dFlagCount = true;
                }
                if (eGo == true && eWall == false && pipeDecode.Count > 0)
                {
                    dStall++;
                    decodeStallTextbox.Text = dStall.ToString();

                    if (dFlagCount == true)
                    {
                        structHCount++;
                        structHTextBox.Text = structHCount.ToString();

                        dFlagCount = false;
                    }

                }

            }

            if (pipeFetch.Count > 0)
            {

                if (dGo != true)
                {
                    pipeFetch[0].Fetch--;
                }
                if (pipeFetch[0].Fetch <= 0)
                {
                    dGo = true;
                }
                if (dGo == true && dWall == true)
                {
                    (store, param1, param2) = ProgramController.decode(pipeFetch[0]);
                    pipeDecode.Add(pipeFetch[0]);
                    dWall = false;
                    pipeFetch.RemoveAt(0);
                    fWall = true;
                    fGo = false;
                    dGo = false;

                    fFlagCount = true;
                }
                if (dGo == true && dWall == false && pipeFetch.Count > 0)
                {
                    fStall++;
                    fetchStallTextbox.Text = fStall.ToString();

                    if (fFlagCount == true)
                    {
                        structHCount++;
                        structHTextBox.Text = structHCount.ToString();

                        fFlagCount = false;
                    }

                }

                if (pipeExecute.Count > 0 && rawFlag == true)
                {
                    if (pipeExecute[0].SRegister == pipeDecode[0].P1Register || pipeExecute[0].SRegister == pipeDecode[0].P2Register)
                    {
                        rawFlag = false;

                        rawCount++;
                        rawTextBox.Text = rawCount.ToString();

                        dataHCount++;
                        dataHTextBox.Text = dataHCount.ToString();

                        rF1 = false;
                    }
                }
                if (pipeStore.Count > 0 && rawFlag == true)
                {
                    if (pipeStore[0].SRegister == pipeDecode[0].P1Register || pipeStore[0].SRegister == pipeDecode[0].P2Register)
                    {
                        rawFlag = false;

                        rawCount++;
                        rawTextBox.Text = rawCount.ToString();

                        dataHCount++;
                        dataHTextBox.Text = dataHCount.ToString();

                        rF2 = false;
                    }
                }

                if (pipeExecute.Count > 0 && warFlag == true)
                {
                    if (pipeDecode[0].SRegister == pipeExecute[0].P1Register || pipeDecode[0].SRegister == pipeExecute[0].P2Register)
                    {
                        warFlag = false;

                        warCount++;
                        warTextBox.Text = warCount.ToString();

                        dataHCount++;
                        dataHTextBox.Text = dataHCount.ToString();

                        rF1 = false;
                    }
                }

                if (pipeStore.Count > 0 && warFlag == true)
                {
                    if (pipeDecode[0].SRegister == pipeStore[0].P1Register || pipeDecode[0].SRegister == pipeStore[0].P2Register)
                    {
                        warFlag = false;

                        warCount++;
                        warTextBox.Text = warCount.ToString();

                        dataHCount++;
                        dataHTextBox.Text = dataHCount.ToString();

                        rF2 = false;
                    }
                }
                if (rawFlag == false)
                {
                    if (pipeExecute.Count == 0 && pipeStore.Count == 0 && rF1 == false)
                    {
                        rawFlag = true;
                        rF1 = true;
                    }
                    if (pipeExecute.Count == 0 && rF2 == false)
                    {
                        rawFlag = true;
                        rF2 = true;
                    }
                }

                if (warFlag == false)
                {

                    if (pipeExecute.Count == 0 && pipeStore.Count == 0 || rF1 == false)
                    {
                        warFlag = true;
                        rF1 = true;
                    }

                    if (pipeExecute.Count == 0 || rF2 == false)
                    {
                        warFlag = true;
                        rF2 = true;
                    }
                }
            }


            if (start == true)
            {
                (pipeFetch, R0, programIndex, stopF) = ProgramController.fetch(instructions, pipeFetch, R0, programIndex);
                r0TextBox.Text = R0.ToString();
                start = false;

            }
            if (pipeFetch.Count == 0 && stopF == 0)
            {
                (pipeFetch, R0, programIndex, stopF) = ProgramController.fetch(instructions, pipeFetch, R0, programIndex);
                r0TextBox.Text = R0.ToString();
            }


            if (pipeFetch.Count >= 1)
            {
                fetchTextBox.Text = pipeFetch[0].InstLit;
            }
            else
            {
                fetchTextBox.Text = "";
            }
            if (pipeDecode.Count >= 1)
            {
                decodeTextBox.Text = pipeDecode[0].InstLit;
            }
            else
            {
                decodeTextBox.Text = "";
            }
            if (pipeExecute.Count >= 1)
            {
                executeTextBox.Text = pipeExecute[0].InstLit;
            }
            else
            {
                executeTextBox.Text = "";
            }
            if (pipeStore.Count >= 1)
            {
                storeTextBox.Text = pipeStore[0].InstLit;
            }
            else
            {
                storeTextBox.Text = "";
            }
            if (nextCycleButton.Enabled == true)
            {
                //Increase cycle counter by one
                incrementCycleCounter();
            }

        }//end nextStaticCycle()
        #endregion
    }
}
