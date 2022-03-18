/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Team4_Project2/Team4_Project2
//	File Name:         GUIForm.cs
//	Description:       GUIForm class for program GUI to show visual static pipeline simulation
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team4_Project2
{
    public partial class GUIForm : Form
    {
        //Counter for cycles
        int cycleCounter = 0;

        //0 = not started,
        //1 = fetch phase
        //2 = decode phase
        //3 = execute phase
        //4 = store/finish phase
        int phaseCounterOne = 0;
        int phaseCounterTwo;
        int phaseCounterThree;
        int phaseCounterFour;

        int fStall, dStall, eStall, sStall = 0;
        int progCount = 0;

        List<string> instructions = new List<string>();
        int programIndex = 0;
        bool start = true;

        int fetchStall, decodeStall, executeStall = 0;
        int readyFetch, readyDecode, readyExecute = 0;

        bool fWall, dWall, eWall, sWall = true;
        bool fGo, dGo, eGo, sGo = false;

        List<Instruction> pipeFetch = new List<Instruction>();
        List<Instruction> pipeDecode = new List<Instruction>();
        List<Instruction> pipeExecute = new List<Instruction>();
        List<Instruction> pipeStore = new List<Instruction>();

        string instLit = string.Empty;
        int stopF = 0;

        int readyFlag = 0;

        bool ifStop = false;

        string param1, param2, store = string.Empty;

        int dataHCount = 0;

        bool rawFlag = true;
        bool warFlag = true;

        bool rF1 = true;
        bool rF2 = true;

        int rawCount = 0;
        int warCount = 0;
        int structCount = 0;

        bool sFlagCount = true;
        bool eFlagCount = true;
        bool fFlagCount = true;
        bool dFlagCount = true;
        bool rFree, wFree = true;



        public GUIForm()
        {
            InitializeComponent();
        }

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
            pipelineOutputTextBox.Text = "";
        }

        /// <summary>
        /// Loads content from file to pipeline output text box
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void loadPipelineOutputButton_Click(object sender, EventArgs e)
        {
            pipelineOutputTextBox.Text = ProgramController.openFile();
        }

        /// <summary>
        /// Saves content inside of pipeline output text box into a file
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void savePipelineOutputButton_Click(object sender, EventArgs e)
        {
            ProgramController.saveFile(pipelineOutputTextBox.Text);
        }
        #endregion

        #region Pipeline Simulation GUI Buttons
        /// <summary>
        /// Starts Static Pipeline Simulation
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void startButton_Click(object sender, EventArgs e)
        {
            //If assemblyTextBox has no code in it, show error message
            if (String.IsNullOrWhiteSpace(assemblyTextBox.Text) == true)
            {
                MessageBox.Show("There is no assembly code to start the simulation.",
                                "Error - No Code To Process",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            //Else, start static pipeline simulation
            else
            {
                foreach (string var in assemblyTextBox.Text.Split())
                {
                    instructions.Add(var);
                }


                assemblyTextBox.ReadOnly = true;
                //Sets counter to 1, equal to fetch phase
                phaseCounterOne++;

                //Sets fetch phase label to red to signify we are in that phase
                fetchLabel.ForeColor = Color.Red;

                //Enables nextPhaseButton to be pressed when simulation has began and disables startButton
                nextCycleButton.Enabled = true;
                startButton.Enabled = false;
            }
        }

        /// <summary>
        /// Goes to next cycle within simulation
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void nextCycleButton_Click(object sender, EventArgs e)
        {
            //Increase cycle counter by one


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
                    pipelineOutputTextBox.Text = ProgramController.outputPipelineStats(structCount, dataHCount, 0, rawCount, warCount, 0, fStall, dStall, eStall, sStall, cycleCounter);
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
                    if (eFlagCount == true)
                    {
                        structCount++;
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
                    if (dFlagCount == true)
                    {
                        structCount++;
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
                    if (fFlagCount == true)
                    {
                        structCount++;
                        fFlagCount = false;
                    }

                }

                if (pipeExecute.Count > 0 && rawFlag == true)
                {
                    if (pipeExecute[0].SRegister == pipeDecode[0].P1Register || pipeExecute[0].SRegister == pipeDecode[0].P2Register)
                    {
                        rawFlag = false;
                        rawCount++;
                        dataHCount++;
                        rF1 = false;
                    }
                }
                if (pipeStore.Count > 0 && rawFlag == true)
                {
                    if (pipeStore[0].SRegister == pipeDecode[0].P1Register || pipeStore[0].SRegister == pipeDecode[0].P2Register)
                    {
                        rawFlag = false;
                        rawCount++;
                        dataHCount++;
                        rF2 = false;
                    }
                }

                if (pipeExecute.Count > 0 && warFlag == true)
                {
                    if (pipeDecode[0].SRegister == pipeExecute[0].P1Register || pipeDecode[0].SRegister == pipeExecute[0].P2Register)
                    {
                        warFlag = false;
                        warCount++;
                        dataHCount++;
                        rF1 = false;
                    }
                }

                if (pipeStore.Count > 0 && warFlag == true)
                {
                    if (pipeDecode[0].SRegister == pipeStore[0].P1Register || pipeDecode[0].SRegister == pipeStore[0].P2Register)
                    {
                        warFlag = false;
                        warCount++;
                        dataHCount++;
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
                (pipeFetch, progCount, programIndex, stopF) = ProgramController.fetch(instructions, pipeFetch, progCount, programIndex);
                start = false;

            }
            if (pipeFetch.Count == 0 && stopF == 0)
            {
                (pipeFetch, progCount, programIndex, stopF) = ProgramController.fetch(instructions, pipeFetch, progCount, programIndex);
            }




            if (pipeFetch.Count >= 1)
            {
                instructOneText.Text = pipeFetch[0].InstLit;
            }
            else
            {
                instructOneText.Text = "";
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
                incrementCycleCounter();
        }
        #endregion

        private void pipelineOutputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void assemblyTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        #region incrementCycleCounter() Method
        /// <summary>
        /// Method for incrementing cycle counter and updating gui to reflect it
        /// </summary>
        public void incrementCycleCounter()
        {
            cycleCounter++;
            counterTextBox.Text = cycleCounter.ToString();
            counterTextBox.SelectionAlignment = HorizontalAlignment.Center;

        }//end incrementCycleCounter()
        #endregion    

    }
}
