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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

        //CDB (Common Data Bus)
        string name, Qj, Qk, Vj, Vk, A = string.Empty;


        //==Counters==//
        //============================================================//
        //Cycle Counter
        int cycleCounter = 0;

        //Hazard Counters (Both Static & Dynamic)
        int structHCount = 0;
        int dataHCount = 0;
        int controlHCount = 0;

        //Dependency Counters (Both Static & Dynamic)
        int rawCount = 0;
        int warCount = 0;
        int wawCount = 0;

        //Delay Counters (Dynamic)
        int bufferD = 0;
        int stationD = 0;
        int conflictD = 0;
        int dependenceD = 0;

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
        float[] regArray = new float[16];
        //R0 - Program Counter
        //R1 - Z (Zero) Flag
        //R2 - C (Carry) Flag
        //R3 - S (Sign) Flag


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

        bool fWall = true;
        bool dWall = true;
        bool eWall = true;
        bool sWall = true;
        bool fGo = false;
        bool dGo = false;
        bool eGo = false;
        bool sGo = false;

        int stopF = 0;
        bool ifStop = false;
        int stopFF = 0;

        bool rF1 = true;
        bool rF2 = true;

        int i = 0;

        //==Dynamic Pipeline Variables==//
        //============================================================//
        Queue<Instruction> instructionQueue = new Queue<Instruction>(9);
        List<Station> reorderBuffer = new List<Station>();
        Queue<Station> fExec1 = new Queue<Station>(1);
        Queue<Station> intExec1 = new Queue<Station>(1);
        Queue<Station> loadStoreExec1 = new Queue<Station>(1);
        Queue<Station> memExec1 = new Queue<Station>(1);

        //Reservation Stations
        Queue<Station> resFExec1 = new Queue<Station>(2);
        Queue<Station> resIntExec1 = new Queue<Station>(2);
        Queue<Station> resLoadStoreExec1 = new Queue<Station>(2);
        Queue<Station> resMem = new Queue<Station>(2);
        String[] Qi = new String[16];
        int destinationCounter = 0;

        int commitCounter = 1;

        int loadCounter = 0;
        int memCounter = 0;
        int intCounter = 0;
        int fCounter = 0;

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

            //Instantiate initial memory
            instantiateMemory();

            //Display first 32nd of memory to GUI
            displayMemoryInString32nd(1);

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

            //Instantiate initial memory
            instantiateMemory();

            //Display first 32nd of memory to GUI
            displayMemoryInString32nd(1);

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

        /// <summary>
        ///  Slider for going through each 1/32 of 1MB memory array
        /// </summary>
        /// <param name="sender">object that raised the event (auto-generated, unused here)</param>
        /// <param name="e">arguments for event (auto-generated, unused here)</param>
        private void memSlider_Scroll(object sender, EventArgs e)
        {
            displayMemoryInString32nd(memSlider.Value);
        }
        #endregion


        //Simulation Startup Methods
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


        //Cycle Methods
        #region nextDynamicCycle() Method
        /// <summary>
        /// Method for going to next cycle in dynamic pipeline simulation
        /// </summary>
        public void nextDynamicCycle()
        {
            //Increase cycle counter by one
            incrementCycleCounter();

            //Create new list of currently fetched instructions and fetch instructions of queue count is less than 9
            if (stopFF != 1)
            {
                List<Instruction> fetchedIntructs = new List<Instruction>();
                if (instructionQueue.Count < 9)
                {
                    (fetchedIntructs, regArray[0], i, stopF) = ProgramController.fetch(instructions, fetchedIntructs, (int)regArray[0], i);
                    instructionQueue.Enqueue(fetchedIntructs[fetchedIntructs.Count - 1]);
                    if (stopF == 1)
                    {
                        stopFF = 1;
                    }
                }
            }

            //Commit Phase
            if (reorderBuffer.Count > 0)
            {
                if (reorderBuffer[0].dest == commitCounter)
                {
                    if ($"{reorderBuffer[0].instruction.InstLit[0]}{reorderBuffer[0].instruction.InstLit[1]}{reorderBuffer[0].instruction.InstLit[2]}{reorderBuffer[0].instruction.InstLit[3]}" == "STOP")
                    {
                        nextCycleButton.Enabled = false;
                    }
                    else
                    {
                        Qi[Convert.ToInt32(reorderBuffer[0].instruction.SRegister.Remove(0, 1))] = string.Empty;
                        commitCounter++;
                    }



                    commitTextBox.Text = reorderBuffer[0].instruction.InstLit;
                    reorderBuffer.RemoveAt(0);
                }
                else
                    bufferD++;
                //switch (reorderBuffer[0].addressingMode)
                //{
                //    case string x when (x == "00"):
                //        int sReg = Convert.ToInt32(reorderBuffer[0].instruction.sRegister.Remove(0, 1));
                //        regArray[sReg] = reorderBuffer[0].Vj;
                //        reorderBuffer.RemoveAt(0);
                //        break;
                //}

            }
            //Issue Phase
            //Gets instruction pneumonic from instructionlit in instruction object
            if (ifStop != true&& instructionQueue.Count>0)
            {
                string name = $"{instructionQueue.Peek().InstLit[0]}{instructionQueue.Peek().InstLit[1]}{instructionQueue.Peek().InstLit[2]}{instructionQueue.Peek().InstLit[3]}";
                switch (name)
                {
                    case string n when (n == "LDRE"):
                        Instruction temp = instructionQueue.Peek();
                        if (temp.P1Register[0] == '&')
                        {
                            if (resMem.Count == 2)
                            {
                                //Increase structural hazard count and display update to GUI
                                structHCount++;
                                structHTextBox.Text = structHCount.ToString();
                            }
                            else
                            {
                                destinationCounter++;
                                loadCounter++;
                                Station resStatMem = new Station($"Load{loadCounter}", true, returnOp(instructionQueue), setQj(instructionQueue.Peek()), setQk(instructionQueue.Peek()), setVj(instructionQueue.Peek()), setVk(instructionQueue.Peek()), "", instructionQueue.Dequeue(), destinationCounter, "10");
                                resMem.Enqueue(resStatMem);
                                issueTextBox.Text = resMem.Peek().instruction.InstLit;
                            }
                        }
                        else
                        {
                            if (resLoadStoreExec1.Count == 2)
                            {
                                //Increase structural hazard count and display update to GUI
                                structHCount++;
                                structHTextBox.Text = structHCount.ToString();
                            }
                            else
                            {
                                destinationCounter++;
                                loadCounter++;
                                Station resStatLoadStoreExec1 = new Station($"Load{loadCounter}", true, returnOp(instructionQueue), setQj(instructionQueue.Peek()), setQk(instructionQueue.Peek()), setVj(instructionQueue.Peek()), setVk(instructionQueue.Peek()), "", instructionQueue.Dequeue(), destinationCounter, "00");
                                resLoadStoreExec1.Enqueue(resStatLoadStoreExec1);
                                issueTextBox.Text = resLoadStoreExec1.Peek().instruction.InstLit;
                            }
                        }
                        break;

                    case string n when (n == "STRE"):
                        if (resLoadStoreExec1.Count == 2)
                        {
                            //Increase structural hazard count and display update to GUI
                            structHCount++;
                            structHTextBox.Text = structHCount.ToString();
                        }
                        else
                        {
                            destinationCounter++;
                            loadCounter++;
                            Station resStatLoadStoreExec1 = new Station($"Store{loadCounter}", true, returnOp(instructionQueue), setQj(instructionQueue.Peek()), setQk(instructionQueue.Peek()), setVj(instructionQueue.Peek()), setVk(instructionQueue.Peek()), "", instructionQueue.Dequeue(), destinationCounter, "00");
                            resMem.Enqueue(resStatLoadStoreExec1);
                            issueTextBox.Text = resLoadStoreExec1.Peek().instruction.InstLit;
                        }

                        break;

                    case string n when (n == "FADD"):
                        if (resFExec1.Count == 2)
                        {
                            //Increase structural hazard count and display update to GUI
                            structHCount++;
                            structHTextBox.Text = structHCount.ToString();
                        }
                        else
                        {
                            destinationCounter++;
                            fCounter++;
                            Station resStatFExec1 = new Station($"Float{fCounter}", true, returnOp(instructionQueue), setQj(instructionQueue.Peek()), setQk(instructionQueue.Peek()), setVj(instructionQueue.Peek()), setVk(instructionQueue.Peek()), "", instructionQueue.Dequeue(), destinationCounter, "11");
                            resFExec1.Enqueue(resStatFExec1);
                            issueTextBox.Text = resFExec1.Peek().instruction.InstLit;
                        }
                        break;

                    case string n when (n == "FSUB"):
                        if (resFExec1.Count == 2)
                        {
                            //Increase structural hazard count and display update to GUI
                            structHCount++;
                            structHTextBox.Text = structHCount.ToString();
                        }
                        else
                        {
                            destinationCounter++;
                            fCounter++;
                            Station resStatFExec1 = new Station($"Float{fCounter}", true, returnOp(instructionQueue), setQj(instructionQueue.Peek()), setQk(instructionQueue.Peek()), setVj(instructionQueue.Peek()), setVk(instructionQueue.Peek()), "", instructionQueue.Dequeue(), destinationCounter, "11");
                            resFExec1.Enqueue(resStatFExec1);
                            issueTextBox.Text = resFExec1.Peek().instruction.InstLit;
                        }
                        break;

                    case string n when (n == "FMUL"):
                        if (resFExec1.Count == 2)
                        {
                            //Increase structural hazard count and display update to GUI
                            structHCount++;
                            structHTextBox.Text = structHCount.ToString();
                        }
                        else
                        {
                            destinationCounter++;
                            fCounter++;
                            Station resStatFExec1 = new Station($"Float{fCounter}", true, returnOp(instructionQueue), setQj(instructionQueue.Peek()), setQk(instructionQueue.Peek()), setVj(instructionQueue.Peek()), setVk(instructionQueue.Peek()), "", instructionQueue.Dequeue(), destinationCounter, "11");
                            resFExec1.Enqueue(resStatFExec1);
                            issueTextBox.Text = resFExec1.Peek().instruction.InstLit;
                        }
                        break;

                    case string n when (n == "FDIV"):
                        if (resFExec1.Count == 2)
                        {
                            //Increase structural hazard count and display update to GUI
                            structHCount++;
                            structHTextBox.Text = structHCount.ToString();
                        }
                        else
                        {
                            destinationCounter++;
                            fCounter++;
                            Station resStatFExec1 = new Station($"Float{fCounter++}", true, returnOp(instructionQueue), setQj(instructionQueue.Peek()), setQk(instructionQueue.Peek()), setVj(instructionQueue.Peek()), setVk(instructionQueue.Peek()), "", instructionQueue.Dequeue(), destinationCounter, "11");
                            resFExec1.Enqueue(resStatFExec1);
                            issueTextBox.Text = resFExec1.Peek().instruction.InstLit;
                        }
                        break;

                    default:
                        if (resIntExec1.Count == 2)
                        {
                            //Increase structural hazard count and display update to GUI
                            structHCount++;
                            structHTextBox.Text = structHCount.ToString();
                        }
                        else
                        {
                            destinationCounter++;
                            intCounter++;
                            Station resStatIntExec1 = new Station($"int{intCounter}", true, returnOp(instructionQueue), setQj(instructionQueue.Peek()), setQk(instructionQueue.Peek()), setVj(instructionQueue.Peek()), setVk(instructionQueue.Peek()), "", instructionQueue.Dequeue(), destinationCounter, "00");
                            resIntExec1.Enqueue(resStatIntExec1);
                            if ($"{resIntExec1.Peek().instruction.InstLit[0]} {resIntExec1.Peek().instruction.InstLit[1]}{resIntExec1.Peek().instruction.InstLit[2]}{resIntExec1.Peek().instruction.InstLit[3]}" == "STOP")
                            {
                                ifStop = true;
                            }
                            issueTextBox.Text = resIntExec1.Peek().instruction.InstLit;
                        }
                        break;
                }
            }
            //Execute Phase
            if (resLoadStoreExec1.Count > 0)
            {
                if (resLoadStoreExec1.Peek().instruction.fetch == 0)
                {
                    if (string.IsNullOrEmpty(resLoadStoreExec1.Peek().Qj) == true && string.IsNullOrEmpty(Qi[Convert.ToInt32(resLoadStoreExec1.Peek().instruction.p1Register.Remove(0, 1))]) == true)
                    {
                        Qi[Convert.ToInt32(resLoadStoreExec1.Peek().instruction.sRegister.Remove(0, 1))] = resLoadStoreExec1.Peek().Name;
                        resLoadStoreExec1.Peek().instruction.fetch++;
                        loadStoreExec1.Enqueue(resLoadStoreExec1.Dequeue());
                        dynamicLSExecTBox.Text = loadStoreExec1.Peek().instruction.InstLit;

                    }
                    else if (Qj == resLoadStoreExec1.Peek().Qj)
                    {
                        dependenceD++;
                        trueDependenceTextBox.Text = dependenceD.ToString();
                        resLoadStoreExec1.Peek().Qj = string.Empty;
                        resLoadStoreExec1.Peek().Vj = setVj(resLoadStoreExec1.Peek().instruction);
                    }
                    else 
                    {
                        dependenceD++;
                        trueDependenceTextBox.Text = dependenceD.ToString();
                    }

                }
                else
                {
                    resLoadStoreExec1.Peek().instruction.fetch--;
                }
            }
            if (resMem.Count > 0)
            {
                if (resMem.Peek().instruction.fetch == 0)
                {
                    if (string.IsNullOrEmpty(resMem.Peek().Qj) == true && string.IsNullOrEmpty(Qi[Convert.ToInt32(resMem.Peek().instruction.sRegister.Remove(0, 1))]) == true)
                    {
                        Qi[Convert.ToInt32(resMem.Peek().instruction.sRegister.Remove(0, 1))] = resMem.Peek().Name;
                        resMem.Peek().instruction.fetch++;
                        memExec1.Enqueue(resMem.Dequeue());
                        dynamicMemExecTBox.Text += memExec1.Peek().instruction.InstLit;

                    }
                    else if (Qj == resMem.Peek().Qj)
                    {
                        dependenceD++;
                        trueDependenceTextBox.Text = dependenceD.ToString();
                        resMem.Peek().Qj = string.Empty;
                        resMem.Peek().Vj = setVj(resMem.Peek().instruction);
                    }
                    else
                    {
                        dependenceD++;
                        trueDependenceTextBox.Text = dependenceD.ToString();
                    }
                }
                else
                {
                    resMem.Peek().instruction.fetch--;
                }
            }
            if (resFExec1.Count > 0)
            {
                if (resFExec1.Peek().instruction.fetch == 0)
                {
                    if (string.IsNullOrEmpty(resFExec1.Peek().Qk) == true && string.IsNullOrEmpty(resFExec1.Peek().Qk) == true && string.IsNullOrEmpty(Qi[Convert.ToInt32(resFExec1.Peek().instruction.p1Register.Remove(0, 1))]) == true && string.IsNullOrEmpty(Qi[Convert.ToInt32(resFExec1.Peek().instruction.p2Register.Remove(0, 1))]) == true)
                    {
                        Qi[Convert.ToInt32(resFExec1.Peek().instruction.sRegister.Remove(0, 1))] = resFExec1.Peek().Name;
                        resFExec1.Peek().instruction.fetch++;
                        fExec1.Enqueue(resFExec1.Dequeue());
                        dynamicFExecTextBox.Text += fExec1.Peek().instruction.InstLit;

                    }
                    else if (Qj == resFExec1.Peek().Qj)
                    {
                        dependenceD++;
                        trueDependenceTextBox.Text = dependenceD.ToString();
                        resFExec1.Peek().Qj = string.Empty;
                        resFExec1.Peek().Vj = setVj(resFExec1.Peek().instruction);
                    }
                    else if (Qk == resFExec1.Peek().Qk)
                    {
                        dependenceD++;
                        trueDependenceTextBox.Text = dependenceD.ToString();
                        resFExec1.Peek().Qk = string.Empty;
                        resFExec1.Peek().Vk = setVk(resFExec1.Peek().instruction);
                    }
                    else
                    {
                        dependenceD++;
                        trueDependenceTextBox.Text = dependenceD.ToString();
                    }
                }

                else
                {
                    resFExec1.Peek().instruction.fetch--;
                }
            }
            if (resIntExec1.Count > 0)
            {
                if (resIntExec1.Peek().instruction.fetch == 0)
                {
                    if ($"{resIntExec1.Peek().instruction.InstLit[0]}{resIntExec1.Peek().instruction.InstLit[1]}{resIntExec1.Peek().instruction.InstLit[2]}{resIntExec1.Peek().instruction.InstLit[3]}" == "STOP")
                    {
                        resIntExec1.Peek().instruction.fetch++;
                        intExec1.Enqueue(resIntExec1.Dequeue());
                        dynamicIntExecuteTextBox.Text += intExec1.Peek().instruction.InstLit;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(resIntExec1.Peek().Qk) == true && string.IsNullOrEmpty(resIntExec1.Peek().Qk) == true && string.IsNullOrEmpty(Qi[Convert.ToInt32(resIntExec1.Peek().instruction.p1Register.Remove(0, 1))]) == true && string.IsNullOrEmpty(Qi[Convert.ToInt32(resIntExec1.Peek().instruction.p2Register.Remove(0, 1))]) == true)
                        {
                            Qi[Convert.ToInt32(resIntExec1.Peek().instruction.sRegister.Remove(0, 1))] = resIntExec1.Peek().Name;
                            resIntExec1.Peek().instruction.fetch++;
                            intExec1.Enqueue(resIntExec1.Dequeue());
                            dynamicIntExecuteTextBox.Text += intExec1.Peek().instruction.InstLit;

                        }
                        else if (Qj == resIntExec1.Peek().Qj)
                        {
                            dependenceD++;
                            trueDependenceTextBox.Text = dependenceD.ToString();
                            resIntExec1.Peek().Qj = string.Empty;
                            resIntExec1.Peek().Vj = setVj(resIntExec1.Peek().instruction);
                        }
                        else if (Qk == resIntExec1.Peek().Qk)
                        {
                            dependenceD++;
                            trueDependenceTextBox.Text = dependenceD.ToString();
                            resIntExec1.Peek().Qk = string.Empty;
                            resIntExec1.Peek().Vk = setVk(resIntExec1.Peek().instruction);
                        }
                        else
                        {
                            dependenceD++;
                            trueDependenceTextBox.Text = dependenceD.ToString();
                        }
                    }
                    
                }
                else
                {
                    resIntExec1.Peek().instruction.fetch--;
                }
            }
            //Memory Read Phase
            if (memExec1.Count > 0)
            {
                if (memExec1.Peek().instruction.fetch == 0)
                {
                    memExec1.Peek().instruction.fetch++;
                    memReadTextBox.Text = memExec1.Peek().instruction.InstLit;
                }
                else
                {
                    memExec1.Peek().instruction.fetch--;
                }
            }


            //Write Phase
            if (memExec1.Count > 0)
            {
                if (memExec1.Peek().instruction.fetch == 0)
                {

                    Qj = memExec1.Peek().Name;
                    Qk = memExec1.Peek().Name;
                    writeTextBox.Text = memExec1.Peek().instruction.InstLit;
                    reorderBuffer.Add(memExec1.Dequeue());

                }
                else
                {
                    memExec1.Peek().instruction.fetch--;
                }
            }
            if (intExec1.Count > 0)
            {
                if (intExec1.Peek().instruction.execute == 0)
                {

                    Qj = intExec1.Peek().Name;
                    Qk = intExec1.Peek().Name;
                    writeTextBox.Text = intExec1.Peek().instruction.InstLit;
                    reorderBuffer.Add(intExec1.Dequeue());
                }
                else
                {
                    intExec1.Peek().instruction.execute--;
                }
            }
            if (fExec1.Count > 0)
            {
                if (fExec1.Peek().instruction.execute == 0)
                {

                    Qj = fExec1.Peek().Name;
                    Qk = fExec1.Peek().Name;
                    writeTextBox.Text = fExec1.Peek().instruction.InstLit;
                    reorderBuffer.Add(fExec1.Dequeue());
                }
                else
                {
                    fExec1.Peek().instruction.execute--;
                }
            }
            if (loadStoreExec1.Count > 0)
            {
                if (loadStoreExec1.Peek().instruction.execute == 0)
                {

                    Qj = loadStoreExec1.Peek().Name;
                    Qk = loadStoreExec1.Peek().Name;
                    writeTextBox.Text = loadStoreExec1.Peek().instruction.InstLit;
                    reorderBuffer.Add(loadStoreExec1.Dequeue());
                }
                else
                {
                    loadStoreExec1.Peek().instruction.execute--;
                }
            }

            //Sort ROB (Reorder Buffer)
            reorderBuffer.Sort((x, y) => x.dest.CompareTo(y.dest));

            //Output Dynamic Pipeline Simulation Final Statistics
            pipeLineOutText.Text = ProgramController.outputDynamicPipelineStats(structHCount,
                                                         dataHCount,
                                                         controlHCount,
                                                         rawCount,
                                                         warCount,
                                                         wawCount,
                                                         bufferD,
                                                         stationD,
                                                         conflictD,
                                                         dependenceD,
                                                         cycleCounter);

        }//end nextDynamicCycle()
        #endregion

        #region returnOp() Method
        /// <summary>
        /// Method for returning the operation to perform on source operands
        /// </summary>
        private string returnOp(Queue<Instruction> ints)
        {
            Instruction temp = ints.Peek();

            return $"{temp.InstLit[0]}{temp.InstLit[1]}{temp.InstLit[2]}{temp.InstLit[3]}";

        }//end returnOp()
        #endregion

        #region setQj() Method
        /// <summary>
        /// Method for setting Qj reservation station
        /// </summary>
        private string setQj(Instruction ints)
        {
            if (stopFF != 1)
            {
                if (ints.P1Register[0] != '&')
                {
                    ints.P1Register.Remove(0, 1);

                    if (string.IsNullOrEmpty(Qi[Convert.ToInt32(ints.P1Register.Remove(0, 1))]) == false)
                    {
                        return Qi[Convert.ToInt32(ints.P1Register.Remove(0, 1))];
                    }
                    else
                    {
                        return "";
                    }
                }
                return "";
            }
            return "";

        }//end setQj()
        #endregion

        #region setQk() Method
        /// <summary>
        /// Method for setting Qk reservation station
        /// </summary> 
        private string setQk(Instruction ints)
        {
            if (string.IsNullOrEmpty(ints.P2Register) == false)
            {
                ints.P2Register.Remove(0, 1);

                if (string.IsNullOrEmpty(Qi[Convert.ToInt32(ints.P2Register.Remove(0, 1))]) == false)
                {
                    return Qi[Convert.ToInt32(ints.P2Register.Remove(0, 1))];
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }//end setQk()
        #endregion

        #region setVj() Method
        /// <summary>
        /// Method for setting Vj 
        /// </summary> 
        private float setVj(Instruction ints)
        {
            if (stopFF != 1)
            {
                if (ints.P1Register[0] != '&')
                {
                    if (string.IsNullOrEmpty(setQj(ints)) == false)
                    {
                        ints.P1Register.Remove(0, 1);
                        return regArray[Convert.ToInt32(ints.P1Register.Remove(0, 1))];
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                    return 0;
            }
            else
                return 0;

        }//end setVj()
        #endregion

        #region setVk() Method
        /// <summary>
        /// Method for setting Vk 
        /// </summary> 
        private float setVk(Instruction ints)
        {
            if (string.IsNullOrEmpty(setQk(ints)) == false)
            {
                ints.P2Register.Remove(0, 1);
                return regArray[Convert.ToInt32(ints.P2Register.Remove(0, 1))];
            }
            else
            {
                return 0;
            }


        }//end setVk()
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
                    pipeLineOutText.Text = ProgramController.outputStaticPipelineStats(structHCount,
                                                                                       dataHCount,
                                                                                       controlHCount,
                                                                                       rawCount,
                                                                                       warCount,
                                                                                       wawCount,
                                                                                       fStall,
                                                                                       dStall,
                                                                                       eStall,
                                                                                       sStall,
                                                                                       cycleCounter);
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
                (pipeFetch, regArray[0], programIndex, stopF) = ProgramController.fetch(instructions, pipeFetch, (int)regArray[0], programIndex);
                r0TextBox.Text = ((int)regArray[0]).ToString();
                start = false;

            }
            if (pipeFetch.Count == 0 && stopF == 0)
            {
                (pipeFetch, regArray[0], programIndex, stopF) = ProgramController.fetch(instructions, pipeFetch, (int)regArray[0], programIndex);
                r0TextBox.Text = ((int)regArray[0]).ToString();
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

        //Register Methods
        #region updateReg Method
        /// <summary>
        /// Method for updating a register to 'update' value
        /// </summary>
        public void updateRegister(string param, float update)
        {
            switch (param)
            {
                case string n when (n == "R0"):
                    regArray[0] = (int)update;
                    r0TextBox.Text = ((int)regArray[0]).ToString();
                    break;

                case string n when (n == "R1"):
                    regArray[1] = (int)update;
                    r1TextBox.Text = ((int)regArray[1]).ToString();
                    break;

                case string n when (n == "R2"):
                    regArray[2] = (int)update;
                    r2TextBox.Text = ((int)regArray[2]).ToString();
                    break;

                case string n when (n == "R3"):
                    regArray[3] = (int)update;
                    r3TextBox.Text = ((int)regArray[3]).ToString();
                    break;

                case string n when (n == "R4"):
                    regArray[4] = (int)update;
                    r4TextBox.Text = ((int)regArray[4]).ToString();
                    break;

                case string n when (n == "R5"):
                    regArray[5] = (int)update;
                    r5TextBox.Text = ((int)regArray[5]).ToString();
                    break;

                case string n when (n == "R6"):
                    regArray[6] = (int)update;
                    r6TextBox.Text = ((int)regArray[6]).ToString();
                    break;

                case string n when (n == "R7"):
                    regArray[7] = (int)update;
                    r7TextBox.Text = ((int)regArray[7]).ToString();
                    break;

                case string n when (n == "R8"):
                    regArray[8] = (int)update;
                    r8TextBox.Text = ((int)regArray[8]).ToString();
                    break;

                case string n when (n == "R9"):
                    regArray[9] = (int)update;
                    r9TextBox.Text = ((int)regArray[9]).ToString();
                    break;

                case string n when (n == "R10"):
                    regArray[10] = (int)update;
                    r10TextBox.Text = ((int)regArray[10]).ToString();
                    break;

                case string n when (n == "R11"):
                    regArray[11] = (int)update;
                    r11TextBox.Text = ((int)regArray[11]).ToString();
                    break;

                case string n when (n == "F12"):
                    regArray[12] = update;
                    f12TextBox.Text = regArray[12].ToString();
                    break;

                case string n when (n == "F13"):
                    regArray[13] = update;
                    f13TextBox.Text = regArray[13].ToString();
                    break;

                case string n when (n == "F14"):
                    regArray[14] = update;
                    f14TextBox.Text = regArray[14].ToString();
                    break;

                case string n when (n == "f15"):
                    regArray[15] = update;
                    f15TextBox.Text = regArray[15].ToString();
                    break;
            }

        }//end updateRegister()
        #endregion

        #region getReg Method
        /// <summary>
        /// Method for getting a register value
        /// </summary>
        public float getReg(string param)
        {
            switch (param)
            {
                case string n when (n == "R0"):
                    return (int)regArray[0];

                case string n when (n == "R1"):
                    return (int)regArray[1];

                case string n when (n == "R2"):
                    return (int)regArray[2];

                case string n when (n == "R3"):
                    return (int)regArray[3];

                case string n when (n == "R4"):
                    return (int)regArray[4];

                case string n when (n == "R5"):
                    return (int)regArray[5];

                case string n when (n == "R6"):
                    return (int)regArray[6];

                case string n when (n == "R7"):
                    return (int)regArray[7];

                case string n when (n == "R8"):
                    return (int)regArray[8];

                case string n when (n == "R9"):
                    return (int)regArray[9];

                case string n when (n == "R10"):
                    return (int)regArray[10];

                case string n when (n == "R11"):
                    return (int)regArray[11];

                case string n when (n == "F12"):
                    return regArray[12];

                case string n when (n == "F13"):
                    return regArray[13];

                case string n when (n == "F14"):
                    return regArray[14];

                case string n when (n == "f15"):
                    return regArray[15];

                default:
                    return 0;
            }

        }//end getReg()
        #endregion


        //Memory Methods
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void assemblyTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region storeMemoryInString() Method
        /// <summary>
        /// Method for storing memory into single string for a textbox
        /// </summary>
        public void storeMemoryInString()
        {
            //Store all memory into single string
            StringBuilder memString = new StringBuilder();
            for (int k = 0; k < 65536; k++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (j == 16)
                    {
                        memString.Append(Memory[k, j]);
                        memString.Append("\r\n");
                    }
                    else
                    {
                        memString.Append(Memory[k, j]);
                    }
                }
            }

            //Output memString to Textbox
            memOutputText.Text = Convert.ToString(memString);

        }//end storeMemoryInString()
        #endregion

        #region storeMemoryInString32nd() Method
        /// <summary>
        /// Method for displaying 1/32 of memory array to reduce lag
        /// </summary>
        public void displayMemoryInString32nd(int sliderValue)
        {
            //Store all 1/32 of memory into memory textbox
            switch (sliderValue)
            {
                case 1:
                    memOutputText.Text = returnMemoryBetweenValues(0, 2047);
                    break;

                case 2:
                    memOutputText.Text = returnMemoryBetweenValues(2048, 4095);
                    break;

                case 3:
                    memOutputText.Text = returnMemoryBetweenValues(4096, 6143);
                    break;

                case 4:
                    memOutputText.Text = returnMemoryBetweenValues(6144, 8191);
                    break;

                case 5:
                    memOutputText.Text = returnMemoryBetweenValues(8192, 10239);
                    break;

                case 6:
                    memOutputText.Text = returnMemoryBetweenValues(10240, 12287);
                    break;

                case 7:
                    memOutputText.Text = returnMemoryBetweenValues(12288, 14335);
                    break;

                case 8:
                    memOutputText.Text = returnMemoryBetweenValues(14336, 16383);
                    break;

                case 9:
                    memOutputText.Text = returnMemoryBetweenValues(16384, 18431);
                    break;

                case 10:
                    memOutputText.Text = returnMemoryBetweenValues(18432, 20479);
                    break;

                case 11:
                    memOutputText.Text = returnMemoryBetweenValues(20480, 22527);
                    break;

                case 12:
                    memOutputText.Text = returnMemoryBetweenValues(22528, 24575);
                    break;

                case 13:
                    memOutputText.Text = returnMemoryBetweenValues(24576, 26623);
                    break;

                case 14:
                    memOutputText.Text = returnMemoryBetweenValues(26624, 28671);
                    break;

                case 15:
                    memOutputText.Text = returnMemoryBetweenValues(28672, 30719);
                    break;

                case 16:
                    memOutputText.Text = returnMemoryBetweenValues(30720, 32767);
                    break;

                case 17:
                    memOutputText.Text = returnMemoryBetweenValues(32768, 34815);
                    break;

                case 18:
                    memOutputText.Text = returnMemoryBetweenValues(34816, 36863);
                    break;

                case 19:
                    memOutputText.Text = returnMemoryBetweenValues(36864, 38911);
                    break;

                case 20:
                    memOutputText.Text = returnMemoryBetweenValues(38912, 40959);
                    break;

                case 21:
                    memOutputText.Text = returnMemoryBetweenValues(40960, 43007);
                    break;

                case 22:
                    memOutputText.Text = returnMemoryBetweenValues(43008, 45055);
                    break;

                case 23:
                    memOutputText.Text = returnMemoryBetweenValues(45056, 47103);
                    break;

                case 24:
                    memOutputText.Text = returnMemoryBetweenValues(47104, 49151);
                    break;

                case 25:
                    memOutputText.Text = returnMemoryBetweenValues(49152, 51199);
                    break;

                case 26:
                    memOutputText.Text = returnMemoryBetweenValues(51200, 53247);
                    break;

                case 27:
                    memOutputText.Text = returnMemoryBetweenValues(53248, 55295);
                    break;

                case 28:
                    memOutputText.Text = returnMemoryBetweenValues(55296, 57343);
                    break;

                case 29:
                    memOutputText.Text = returnMemoryBetweenValues(57344, 59391);
                    break;

                case 30:
                    memOutputText.Text = returnMemoryBetweenValues(59392, 61439);
                    break;

                case 31:
                    memOutputText.Text = returnMemoryBetweenValues(61440, 63487);
                    break;

                case 32:
                    memOutputText.Text = returnMemoryBetweenValues(63488, 65535);
                    break;
            }

        }//end storeMemoryInString32nd()
        #endregion 

        #region returnMemoryBetweenValues() Method
        /// <summary>
        /// Method for returning memory between 2 values
        /// </summary>
        public string returnMemoryBetweenValues(int a, int b)
        {
            StringBuilder memString = new StringBuilder();

            for (int k = a; k <= b; k++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (j == 16)
                    {
                        memString.Append(Memory[k, j]);
                        memString.Append("\r\n");
                    }
                    else
                    {
                        memString.Append(Memory[k, j]);
                    }
                }
            }

            return Convert.ToString(memString);

        }//end returnMemoryBetweenValues()
        #endregion


        //Reset Methods
        #region Reset Methods
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
            dynamicIntExecuteTextBox.Text = string.Empty;
            dynamicMemExecTBox.Text = string.Empty;
            dynamicLSExecTBox.Text = string.Empty;
            dynamicFExecTextBox.Text = string.Empty;
            writeTextBox.Text = string.Empty;
            commitTextBox.Text = string.Empty;

            //Reset Static Phase textboxes
            fetchTextBox.Text = string.Empty;
            decodeTextBox.Text = string.Empty;
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
            nextCycleButton.Enabled = false;
        }

        /// <summary>
        /// Method for resetting all variables in GUIForm to start another simulation without closing program
        /// </summary>
        public void resetAllVariables()
        {
            //Conducting Static or Dynamic Simulation Bool
            isDynamic = false;

            //==Reset Counters==//
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
            controlHCount = 0;

            //Dependency Counters
            rawCount = 0;
            warCount = 0;
            wawCount = 0;

            //Stall Counters
            fStall = 0;
            dStall = 0;
            eStall = 0;
            sStall = 0;


            //==Reset Flags==//
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


            //==Reset Registers==//
            //============================================================//
            regArray = new float[16];
            //R0 - Program Counter
            //R1 - Z (Zero) Flag
            //R2 - C (Carry) Flag
            //R3 - S (Sign) Flag


            //==Reset 1MB Memory Array==//
            //============================================================//
            Memory = new String[65536, 17];


            //Reset list of all assembly instructions
            instructions = new List<string>();

            //Reset currently fetched instructions
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
            stopFF = 0;

            rF1 = true;
            rF2 = true;

            i = 0;

            //==Reset Dynamic Pipeline Variables==//
            //============================================================//
            instructionQueue = new Queue<Instruction>(9);
            reorderBuffer = new List<Station>();
            fExec1 = new Queue<Station>(1);
            intExec1 = new Queue<Station>(1);
            loadStoreExec1 = new Queue<Station>(1);
            memExec1 = new Queue<Station>(1);

            //Reservation Stations
            resFExec1 = new Queue<Station>(2);
            resIntExec1 = new Queue<Station>(2);
            resLoadStoreExec1 = new Queue<Station>(2);
            resMem = new Queue<Station>(2);
            Qi = new String[16];
            destinationCounter = 0;

            commitCounter = 0;

            loadCounter = 0;
            memCounter = 0;
            intCounter = 0;
            fCounter = 0;

        }//end resetAllVariables()
        #endregion
    }
}
