﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace CoreKeepersWorkshop
{
    public partial class ItemEditor : Form
    {
        public ItemEditor()
        {
            InitializeComponent();
        }

        // Form closing saving.
        int selectedItemType = 0;
        int selectedItemAmount = 0;
        int selectedItemVariation = 0;
        bool userCancledTask = false;

        // Define closing varibles.
        public int GetItemTypeFromList()
        {
            return selectedItemType;
        }
        public int GetItemAmountFromList()
        {
            return selectedItemAmount;
        }
        public int GetItemVeriationFromList()
        {
            return selectedItemVariation;
        }
        public bool GetUserCancledTask()
        {
            return userCancledTask;
        }

        #region Form Load And Closing Events

        // Do loading events.
        private void ItemEditor_Load(object sender, EventArgs e)
        {
            #region Set Form Locations

            // Set the forms active location based on previous save.
            this.Location = CoreKeepersWorkshop.Properties.Settings.Default.ItemEditorLocation;
            #endregion

            #region Tooltips

            // Create a new tooltip.
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1500;

            // Set tool texts.
            toolTip.SetToolTip(numericUpDown1, "Enter the amount of items to add.");
            toolTip.SetToolTip(numericUpDown2, "Enter a custom ID. Either press enter when done or use the button.");
            toolTip.SetToolTip(numericUpDown3, "Enter a custom variant ID. Either press enter when done or use the button.");

            toolTip.SetToolTip(button1, "Remove the item from this inventory slot.");
            toolTip.SetToolTip(button3, "Spawn in custom item amount + ID.");

            #endregion

            // Load some settings.
            if (CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Length == 8) // Check if item is a food variant.
            {
                // Change some form items.
                numericUpDown3.Visible = false;
                numericUpDown4.Visible = true;
                numericUpDown5.Visible = true;
                button4.Visible = true;

                // Update settings.
                numericUpDown1.Value = CoreKeepersWorkshop.Properties.Settings.Default.InfoID;
                numericUpDown2.Value = CoreKeepersWorkshop.Properties.Settings.Default.InfoAmount;
                numericUpDown4.Value = decimal.Parse(CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Substring(0, CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Length / 2));
                numericUpDown5.Value = decimal.Parse(CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Substring(CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Length / 2));
            }
            else
            {
                // None food variant, keep normal settings.
                numericUpDown1.Value = CoreKeepersWorkshop.Properties.Settings.Default.InfoID;
                numericUpDown2.Value = CoreKeepersWorkshop.Properties.Settings.Default.InfoAmount;
                numericUpDown3.Value = CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation;
            }
        }

        // Do closing events.
        private void ItemEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check if the "X" button was pressed to close form.
            if (!new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close"))
            {
                // User pressed the "X" button cancle task.
                userCancledTask = true;
                this.Close();
            }

            // Save some form settings.
            if (CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Length == 8) // Check if item is a food variant.
            {
                // Check if both entrees are populated.
                if (numericUpDown5.Value != 0)
                {
                    // Combine strings into int.
                    CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation = int.Parse(numericUpDown4.Value.ToString() + numericUpDown5.Value.ToString());
                }
                else
                {
                    // Only single value exists, treat as a unique variant value.
                    CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation = (int)numericUpDown4.Value;
                }
            }
            else
            {
                // Normal item variant.
                CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation = (int)numericUpDown3.Value;
            }
            CoreKeepersWorkshop.Properties.Settings.Default.InfoID = (int)numericUpDown1.Value;
            CoreKeepersWorkshop.Properties.Settings.Default.InfoAmount = (int)numericUpDown2.Value;
            CoreKeepersWorkshop.Properties.Settings.Default.ItemEditorLocation = this.Location;
        }
        #endregion // Form loading and closing events.

        #region Keydown Events

        // Do enter events.
        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectedItemType = (int)numericUpDown1.Value;
                selectedItemAmount = (int)numericUpDown2.Value;
                if (CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Length == 8) // Check if item is a food variant.
                {
                    // Check if both entrees are populated.
                    if (numericUpDown5.Value != 0)
                    {
                        // Combine strings into int.
                        selectedItemVariation = int.Parse(numericUpDown4.Value.ToString() + numericUpDown5.Value.ToString());
                    }
                    else
                    {
                        // Only single value exists, treat as a unique variant value.
                        selectedItemVariation = (int)numericUpDown4.Value;
                    }
                }
                else
                {
                    // Normal item variant.
                    selectedItemVariation = (int)numericUpDown3.Value;
                }
                this.Close();
            }
        }
        private void numericUpDown2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectedItemType = (int)numericUpDown1.Value;
                selectedItemAmount = (int)numericUpDown2.Value;
                if (CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Length == 8) // Check if item is a food variant.
                {
                    // Check if both entrees are populated.
                    if (numericUpDown5.Value != 0)
                    {
                        // Combine strings into int.
                        selectedItemVariation = int.Parse(numericUpDown4.Value.ToString() + numericUpDown5.Value.ToString());
                    }
                    else
                    {
                        // Only single value exists, treat as a unique variant value.
                        selectedItemVariation = (int)numericUpDown4.Value;
                    }
                }
                else
                {
                    // Normal item variant.
                    selectedItemVariation = (int)numericUpDown3.Value;
                }
                this.Close();
            }
        }
        private void numericUpDown3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectedItemType = (int)numericUpDown1.Value;
                selectedItemAmount = (int)numericUpDown2.Value;
                if (CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Length == 8) // Check if item is a food variant.
                {
                    // Check if both entrees are populated.
                    if (numericUpDown5.Value != 0)
                    {
                        // Combine strings into int.
                        selectedItemVariation = int.Parse(numericUpDown4.Value.ToString() + numericUpDown5.Value.ToString());
                    }
                    else
                    {
                        // Only single value exists, treat as a unique variant value.
                        selectedItemVariation = (int)numericUpDown4.Value;
                    }
                }
                else
                {
                    // Normal item variant.
                    selectedItemVariation = (int)numericUpDown3.Value;
                }
                this.Close();
            }
        }
        private void numericUpDown4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectedItemType = (int)numericUpDown1.Value;
                selectedItemAmount = (int)numericUpDown2.Value;
                if (CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Length == 8) // Check if item is a food variant.
                {
                    // Check if both entrees are populated.
                    if (numericUpDown5.Value != 0)
                    {
                        // Combine strings into int.
                        selectedItemVariation = int.Parse(numericUpDown4.Value.ToString() + numericUpDown5.Value.ToString());
                    }
                    else
                    {
                        // Only single value exists, treat as a unique variant value.
                        selectedItemVariation = (int)numericUpDown4.Value;
                    }
                }
                else
                {
                    // Normal item variant.
                    selectedItemVariation = (int)numericUpDown3.Value;
                }
                this.Close();
            }
        }
        private void numericUpDown5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectedItemType = (int)numericUpDown1.Value;
                selectedItemAmount = (int)numericUpDown2.Value;
                if (CoreKeepersWorkshop.Properties.Settings.Default.InfoVariation.ToString().Length == 8) // Check if item is a food variant.
                {
                    // Check if both entrees are populated.
                    if (numericUpDown5.Value != 0)
                    {
                        // Combine strings into int.
                        selectedItemVariation = int.Parse(numericUpDown4.Value.ToString() + numericUpDown5.Value.ToString());
                    }
                    else
                    {
                        // Only single value exists, treat as a unique variant value.
                        selectedItemVariation = (int)numericUpDown4.Value;
                    }
                }
                else
                {
                    // Normal item variant.
                    selectedItemVariation = (int)numericUpDown3.Value;
                }
                this.Close();
            }
        }
        #endregion // Keydown Events.
    }
}