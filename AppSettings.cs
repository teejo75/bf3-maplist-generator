﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace BF3MapListGenerator
{
    /// <summary>
    /// A class for making application settings persistent.
    /// </summary>
    [Serializable()]
    public class AppSettings
    {
        // File to store the settings.
        private string settingsFilePath = Application.StartupPath + @"\" + Application.ProductName + ".dat";
        private bool AppSettingsChanged;
        // Variables used to store the application settings.
        private ArrayList m_lvmapsCheckedItems;
        private ArrayList m_lvmapsItems;
        private ArrayList m_PatternItems;
        private Dictionary<string, bool> m_dropDownItems;
        private int m_OutputSizeH;
        private int m_OutputSizeW;
        private decimal m_RoundsValue;
        private decimal m_RepeatsValue;
        private bool m_InfiniteChecked;
        private bool m_RepeatsEnabled;
        private bool m_DontRepeatChecked;
        private bool m_DontRepeatEnabled;
        // Properties used to access the application settings variables.
        public int OutputSizeW
        {
            get { return m_OutputSizeW; }
            set
            {
                if (value != m_OutputSizeW)
                {
                    m_OutputSizeW = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public int OutputSizeH
        {
            get { return m_OutputSizeH; }
            set
            {
                if (value != m_OutputSizeH)
                {
                    m_OutputSizeH = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public ArrayList lvmapsCheckedItems
        {
            get { return m_lvmapsCheckedItems; }
            set
            {
                if (value != m_lvmapsCheckedItems)
                {
                    m_lvmapsCheckedItems = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public ArrayList lvmapsItems
        {
            get { return m_lvmapsItems; }
            set
            {
                if (value != m_lvmapsItems)
                {
                    m_lvmapsItems = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public ArrayList PatternItems
        {
            get { return m_PatternItems; }
            set
            {
                if (value != m_PatternItems)
                {
                    m_PatternItems = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public Dictionary<string, bool> dropDownItems
        {
            get { return m_dropDownItems; }
            set
            {
                if (value != m_dropDownItems)
                {
                    m_dropDownItems = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public decimal RoundsValue
        {
            get { return m_RoundsValue; }
            set
            {
                if (value != m_RoundsValue)
                {
                    m_RoundsValue = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public decimal RepeatsValue
        {
            get { return m_RepeatsValue; }
            set
            {
                if (value != m_RepeatsValue)
                {
                    m_RepeatsValue = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public bool InfiniteChecked
        {
            get { return m_InfiniteChecked; }
            set
            {
                if (value != m_InfiniteChecked)
                {
                    m_InfiniteChecked = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public bool RepeatsEnabled
        {
            get { return m_RepeatsEnabled; }
            set
            {
                if (value != m_RepeatsEnabled)
                {
                    m_RepeatsEnabled = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public bool DontRepeatChecked
        {
            get { return m_DontRepeatChecked; }
            set
            {
                if (value != m_DontRepeatChecked)
                {
                    m_DontRepeatChecked = value;
                    AppSettingsChanged = true;
                }
            }
        }
        public bool DontRepeatEnabled
        {
            get { return m_DontRepeatEnabled; }
            set
            {
                if (value != m_DontRepeatEnabled)
                {
                    m_DontRepeatEnabled = value;
                    AppSettingsChanged = true;
                }
            }
        }
        // Methods
        // Serializes the class to the file if any settings have changed.
        public bool SaveAppSettings()
        {
            if (this.AppSettingsChanged)
            {
                Stream myWriter = null;
                BinaryFormatter mySerializer = null;
                try
                {
                    mySerializer = new BinaryFormatter();
                    myWriter = File.Open(settingsFilePath,FileMode.Create);
                    mySerializer.Serialize(myWriter, this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    // If the FileStream is open, close it.
                    if (myWriter != null)
                    {
                        myWriter.Close();
                    }
                }
            }
            return AppSettingsChanged;
        }
        //Deserializes the class from the config file.
        public bool LoadAppSettings()
        {
            BinaryFormatter mySerializer = null;
            FileStream myFileStream = null;
            bool fileExists = false;
            try
            {
                mySerializer = new BinaryFormatter();
                FileInfo fi = new FileInfo(settingsFilePath);
                // If the file exists, open it.
                if (fi.Exists)
                {
                    myFileStream = fi.OpenRead();
                    // Create a new instance of the AppSettings by deserializing the config file.
                    AppSettings myAppSettings = (AppSettings)mySerializer.Deserialize(myFileStream);
                    // Assign the property values to this instance of the AppSettings class.
                    this.m_lvmapsCheckedItems = myAppSettings.lvmapsCheckedItems;
                    this.m_lvmapsItems = myAppSettings.lvmapsItems;
                    this.m_InfiniteChecked = myAppSettings.InfiniteChecked;
                    this.m_PatternItems = myAppSettings.PatternItems;
                    this.m_RepeatsValue = myAppSettings.RepeatsValue;
                    this.m_RepeatsEnabled = myAppSettings.RepeatsEnabled;
                    this.m_RoundsValue = myAppSettings.RoundsValue;
                    this.m_DontRepeatChecked = myAppSettings.DontRepeatChecked;
                    this.m_DontRepeatEnabled = myAppSettings.DontRepeatEnabled;
                    this.m_OutputSizeW = myAppSettings.OutputSizeW;
                    this.m_OutputSizeH = myAppSettings.OutputSizeH;
                    this.m_dropDownItems = myAppSettings.dropDownItems;
                    fileExists = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // If the filestream is open, close it.
                if (myFileStream != null)
                {
                    myFileStream.Close();
                }
            }
            return fileExists;
        }
    }
}
