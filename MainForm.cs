using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace BF3MapListGenerator
{
public partial class MainForm : Form
    {
    public MainForm()
        {
            InitializeComponent();
        }
    // Instantiate the class for setting persistence
    private AppSettings appsettings = new AppSettings();
    private int OutH;
    private int OutW;

        private void MainForm_FormClosing (object sender, FormClosingEventArgs e )
        {
            // Specify the settings to save
            Dictionary<string, bool> tempdict = new Dictionary<string, bool>();
            appsettings.InfiniteChecked = cbRepeatAll.Checked;
            appsettings.RepeatsValue = nmRepeats.Value;
            appsettings.RepeatsEnabled = nmRepeats.Enabled;
            appsettings.RoundsValue = nmRounds.Value;
            appsettings.lvmapsCheckedItems = new ArrayList( lvMaps.CheckedItems );
            appsettings.lvmapsItems = new ArrayList( lvMaps.Items );
            appsettings.PatternItems = new ArrayList( lbGameTypesPattern.Items );
            foreach (ToolStripMenuItem item in mapToolStripMenuItem.DropDownItems)
            {
                tempdict.Add(item.Name, item.Checked);
            }
            appsettings.dropDownItems = tempdict;
            appsettings.OutputSizeH = OutH;
            appsettings.OutputSizeW = OutW;
            // Save to file
            appsettings.SaveAppSettings();
        }
        private void MainForm_Load( object sender, EventArgs e )
        {

            // Form startup code
            // Set the form title to the product name (Set in Application properties)
            this.Text = Application.ProductName;
            // Initialize form controls
            // Populate Map Selection menu
            foreach ( string xpItem in Program.BF3XPack )
            {
                mapToolStripMenuItem.DropDownItems.Add( xpItem );
            }
            foreach ( ToolStripMenuItem mItem in mapToolStripMenuItem.DropDownItems )
            {
                mItem.Checked = true;
                mItem.CheckOnClick = false;
                mItem.Name = mItem.Text;
                mItem.Click += new EventHandler( mItem_ChangeSelection );
            }
            // Load game types into the game types control
            foreach ( BF3GameType gameType in Program.bf3GameTypes ) 
            {
                lbGameTypes.Items.Add( gameType.FriendlyName );
            }
            // Add the columns to the lvMaps control (Not visible at runtime)
            lvMaps.Columns.Add( "Map", 117, HorizontalAlignment.Left );
            // Populate the lvMaps control from the class created and populated in Program.cs
            // This causes the listview to not update, prevents flashing when loading data.
            // Not really needed since we're not loading tons of data here.
            lvMaps.BeginUpdate();
            // Load stored settings, if any
            if ( this.appsettings.LoadAppSettings() )
            {
                this.lvMaps.Items.AddRange( ( ListViewItem[] )appsettings.lvmapsItems.ToArray( typeof( ListViewItem ) ) );
                Dictionary<string, bool> tempdict = new Dictionary<string, bool>();
                tempdict = appsettings.dropDownItems;
                foreach (var entry in tempdict)
                {
                    ((ToolStripMenuItem)mapToolStripMenuItem.DropDownItems[entry.Key]).Checked = entry.Value;
                }
                this.lbGameTypesPattern.Items.AddRange( appsettings.PatternItems.ToArray() );
                this.nmRepeats.Value = appsettings.RepeatsValue;
                this.nmRepeats.Enabled = appsettings.RepeatsEnabled;
                this.nmRounds.Value = appsettings.RoundsValue;
                this.cbRepeatAll.Checked = appsettings.InfiniteChecked;
                this.cbDontRepeat.Checked = appsettings.DontRepeatChecked;
                OutH = appsettings.OutputSizeH;
                OutW = appsettings.OutputSizeW;
            }
            else
            {
                // No settings found, set defaults.
                foreach ( BF3Map gameMap in Program.bf3Maps )
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = gameMap.FriendlyName;
                    item.Checked = true;
                    lvMaps.Items.Add( item );
                }
                cbRepeatAll.Checked = true;
            }
            // Let the listview refresh
            lvMaps.EndUpdate();
        }

        // Changes map selection based on what was clicked.
        void mItem_ChangeSelection( object sender, EventArgs e )
        {
            var mItem = sender as ToolStripMenuItem;
            // Expansion Pack index = mapToolStripMenuItem.DropDownItems.IndexOf(mitem)
            foreach ( ListViewItem item in lvMaps.Items )
            {
                int mIndex = Program.bf3Maps.FindIndex( mp => mp.FriendlyName.Equals( item.Text ) );
                if ( mItem.Text == Program.BF3XPack[Program.bf3Maps[mIndex].XPack] )
                {
                    if ( item.Checked ) 
                    { 
                        item.Checked = false;
                    }
                    else item.Checked = true;
                }
            }
            if ( mItem.Checked )
            {
                mItem.Checked = false;
            }
            else mItem.Checked = true;
        }

        #region MENU_PROCESSING_CODE
        private void selectAllToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            // Set all items checked
            for ( int i = 0; i < lvMaps.Items.Count; i++ )
            {
                lvMaps.Items[i].Checked = true;
            }

            foreach ( ToolStripMenuItem mItem in mapToolStripMenuItem.DropDownItems )
            {
                mItem.Checked = true;
            }

        }

        private void clearSelectionToolStripMenuItem1_Click( object sender, EventArgs e )
        {
            // Clear all checked items
            for ( int i = 0; i < lvMaps.Items.Count; i++ )
            {
                lvMaps.Items[i].Checked = false;
            }
            foreach ( ToolStripMenuItem mItem in mapToolStripMenuItem.DropDownItems )
            {
                mItem.Checked = false;
            }
        }

        private void maplisttxtToolStripMenuItem_Click( object sender, EventArgs e )
        {
            GenerateList(true);
        }

        private void cleanMapListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateList(false);
        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click( object sender, EventArgs e )
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }
        #endregion
        #region BUTTON_PROCESSING_CODE
        private void btnClear_Click( object sender, EventArgs e )
        {
            lbGameTypesPattern.Items.Clear();
        }

        private void btnAdd_Click( object sender, EventArgs e )
        {
            lbGameTypesPattern.Items.Add( Program.bf3GameTypes[lbGameTypes.SelectedIndex].FriendlyName + "," + nmRounds.Value.ToString() );
        }

        private void btnRemove_Click( object sender, EventArgs e )
        {
            lbGameTypesPattern.Items.Remove( lbGameTypesPattern.SelectedItem );
        }

        private void btnUp_Click( object sender, EventArgs e )
        {
            if (lbGameTypesPattern.SelectedItems.Count > 0)
            {
                var selectedIndex = lbGameTypesPattern.SelectedIndex;
                if (selectedIndex > 0)
                {
                    var ItemToMoveUp = lbGameTypesPattern.Items[selectedIndex];
                    lbGameTypesPattern.Items.RemoveAt(selectedIndex);
                    lbGameTypesPattern.Items.Insert(selectedIndex - 1, ItemToMoveUp);
                    lbGameTypesPattern.SelectedIndex = selectedIndex - 1;
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lbGameTypesPattern.SelectedItems.Count > 0)
            {
                var selectedIndex = lbGameTypesPattern.SelectedIndex;

                if (selectedIndex + 1 < lbGameTypesPattern.Items.Count)
                {
                    var itemToMoveDown = lbGameTypesPattern.Items[selectedIndex];
                    lbGameTypesPattern.Items.RemoveAt(selectedIndex);
                    lbGameTypesPattern.Items.Insert(selectedIndex + 1, itemToMoveDown);
                    lbGameTypesPattern.SelectedIndex = selectedIndex + 1;
                }
            }
        }
        #endregion

        private void GenerateList(bool Comments)
        {
            int totalMapCount = 0;
            string outputTxt = "";
            Random _rnd = new Random();
            if ( lbGameTypesPattern.Items.Count > 0 )
            {
            #region LISTBOX_PROCESSING
                List<BF3MapListPerGameType> bf3MapList = new List<BF3MapListPerGameType>();
                // Process the patterns
                foreach ( string item in lbGameTypesPattern.Items )
                {
                    // Split the contents of the pattern box into the friendly name and the rounds
                    string[] itemSplit = item.Split(',');
                    // Get the Internal game type name
                    int index = Program.bf3GameTypes.FindIndex( gt => gt.FriendlyName.Equals( itemSplit[0] ) );
                    bf3MapList.Add( new BF3MapListPerGameType( Program.bf3GameTypes[index].GameType,Convert.ToDecimal( itemSplit[1] ) ) );
                }
            #endregion
            #region GAMETYPE_PROCESSING
            // This segment goes through each BF3MapListPerGameType entry and generates a map list for it.
            foreach ( BF3MapListPerGameType mlpgt in bf3MapList )
            {
                foreach ( ListViewItem item in lvMaps.CheckedItems )
                {
                    int mIndex = Program.bf3Maps.FindIndex( mp => mp.FriendlyName.Equals( item.Text ) );
                    switch ( mlpgt.GameTypeEnum )
                    {
                        case GameTypes.CONQUESTASSAULTLARGE0 | GameTypes.CONQUESTLARGE0: // B2KConquestLarge
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.CONQUESTASSAULTLARGE0 ) == GameTypes.CONQUESTASSAULTLARGE0 )
                            {
                                // Calling the clone function defined in the class so that the map lists defined
                                // in Program.cs are not actually modified.
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "ConquestAssaultLarge0" ) )];
                            }
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.CONQUESTLARGE0 ) == GameTypes.CONQUESTLARGE0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals ( "ConquestLarge0" ) )];
                            }
                            break;
                        case GameTypes.CONQUESTASSAULTSMALL0 | GameTypes.CONQUESTASSAULTSMALL1 | GameTypes.CONQUESTSMALL0: // B2KConquestSmall
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.CONQUESTASSAULTSMALL0 ) == GameTypes.CONQUESTASSAULTSMALL0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "ConquestAssaultSmall0" ) )];
                            }
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.CONQUESTASSAULTSMALL1 ) == GameTypes.CONQUESTASSAULTSMALL1 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "ConquestAssaultSmall1" ) )];
                            }
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.CONQUESTSMALL0 ) == GameTypes.CONQUESTSMALL0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "ConquestSmall0" ) )];
                            }
                            break;
                        case GameTypes.CONQUESTASSAULTLARGE0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.CONQUESTASSAULTLARGE0 ) == GameTypes.CONQUESTASSAULTLARGE0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "ConquestAssaultLarge0" ) )];
                            }
                            break;
                        case GameTypes.CONQUESTASSAULTSMALL0:
                            if ( (Program.bf3Maps[mIndex].GameTypeList & GameTypes.CONQUESTASSAULTSMALL0 ) == GameTypes.CONQUESTASSAULTSMALL0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "ConquestAssaultSmall0" ) )];
                            }
                            break;
                        case GameTypes.CONQUESTASSAULTSMALL1:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.CONQUESTASSAULTSMALL1 ) == GameTypes.CONQUESTASSAULTSMALL1 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "ConquestAssaultSmall1" ) )];
                            }
                            break;
                        case GameTypes.CONQUESTLARGE0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.CONQUESTLARGE0 ) == GameTypes.CONQUESTLARGE0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "ConquestLarge0" ) )];
                            }
                            break;
                        case GameTypes.CONQUESTSMALL0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.CONQUESTSMALL0 ) == GameTypes.CONQUESTSMALL0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "ConquestSmall0" ) )];
                            }
                            break;
                        case GameTypes.RUSHLARGE0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.RUSHLARGE0 ) == GameTypes.RUSHLARGE0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "RushLarge0" ) )];
                            }
                            break;
                        case GameTypes.SQUADDEATHMATCH0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.SQUADDEATHMATCH0 ) == GameTypes.SQUADDEATHMATCH0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "SquadDeathMatch0" ) )];
                            }
                            break;
                        case GameTypes.SQUADRUSH0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.SQUADRUSH0 ) == GameTypes.SQUADRUSH0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "SquadRush0" ) )];
                            }
                            break;
                        case GameTypes.TEAMDEATHMATCH0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.TEAMDEATHMATCH0 ) == GameTypes.TEAMDEATHMATCH0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "TeamDeathMatch0" ) )];
                            }
                            break;
                        case GameTypes.DOMINATION0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.DOMINATION0 ) == GameTypes.DOMINATION0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "Domination0" ) )];
                            }
                            break;
                        case GameTypes.GUNMASTER0:
                            if ( (Program.bf3Maps[mIndex].GameTypeList & GameTypes.GUNMASTER0 ) == GameTypes.GUNMASTER0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex ( id => id.InternalName.Equals( "GunMaster0" ) )];
                            }
                            break;
                        case GameTypes.TEAMDEATHMATCHC0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.TEAMDEATHMATCHC0 ) == GameTypes.TEAMDEATHMATCHC0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "TeamDeathMatchC0" ) )];
                            }
                            break;
                        case GameTypes.TANKSUPERIORITY0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.TANKSUPERIORITY0 ) == GameTypes.TANKSUPERIORITY0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "TankSuperiority0" ) )];
                            }
                            break;
                        case GameTypes.SCAVENGER0:
                            if ( ( Program.bf3Maps[mIndex].GameTypeList & GameTypes.SCAVENGER0 ) == GameTypes.SCAVENGER0 )
                            {
                                mlpgt.Maps.Add( ( BF3Map )Program.bf3Maps[mIndex].Clone() );
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex( id => id.InternalName.Equals( "Scavenger0" ) )];
                            }
                            break;
                        case GameTypes.CAPTURETHEFLAG0:
                            if ((Program.bf3Maps[mIndex].GameTypeList & GameTypes.CAPTURETHEFLAG0) == GameTypes.CAPTURETHEFLAG0)
                            {
                                mlpgt.Maps.Add((BF3Map)Program.bf3Maps[mIndex].Clone());
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex(id => id.InternalName.Equals("CaptureTheFlag0"))];
                            }
                            break;
                        case GameTypes.AIRSUPERIORITY0:
                            if ((Program.bf3Maps[mIndex].GameTypeList & GameTypes.AIRSUPERIORITY0) == GameTypes.AIRSUPERIORITY0)
                            {
                                mlpgt.Maps.Add((BF3Map)Program.bf3Maps[mIndex].Clone());
                                mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.bf3GameTypes[Program.bf3GameTypes.FindIndex(id => id.InternalName.Equals("AirSuperiority0"))];
                            }
                            break;

                        default:
                            // This should never be executed, but you never know when I could stuff something up somewhere.
                            MessageBox.Show( "ERROR: Invalid gametype passed while trying to populate map list.\r\n\r\nIn other words, there is a bug.\t>.<" );
                            Application.Exit();
                            break;
                    }
                }
                totalMapCount += mlpgt.Maps.Count;
            }
            #endregion
            #region GENERATE_OUTPUT
            if ( cbRepeatAll.Checked )
            {
                BF3Map previousMap = new BF3Map();
                BF3Map previousPreviousMap = new BF3Map();
                while ( totalMapCount > 0 )
                {
                    foreach ( BF3MapListPerGameType mapListPgt in bf3MapList )
                        // This should get executed once per game type, repeating while there are still maps.
                    {
                        // TotalMapCount loop is almost done, but we still have maps left, so fix TotalMapCount
                        if ( totalMapCount < mapListPgt.Maps.Count ) 
                        {
                            totalMapCount = mapListPgt.Maps.Count;
                        }
                        BF3Map selectedMap = new BF3Map();
                        // string gt = "";
                        // We've run out of maps for this game type, start picking from UsedMaps list.
                        if ( mapListPgt.Maps.Count == 0 ) 
                        {
                            if ( cbDontRepeat.Checked ) { continue; }
                            if ( mapListPgt.UsedMapsCurrentIndex >= mapListPgt.UsedMaps.Count ) // Reset counter to 0 if necessary
                            { 
                                mapListPgt.UsedMapsCurrentIndex = 0; 
                            }
                            selectedMap = mapListPgt.UsedMaps[mapListPgt.UsedMapsCurrentIndex];
                            mapListPgt.UsedMapsCurrentIndex++;
                        }
                        else
                        {
                            selectedMap = Program.SelectMap( mapListPgt.Maps, _rnd.Next(0, mapListPgt.Maps.Count) ); // Randomly select a map from the list
                        }
                        while ( ( selectedMap.InternalName == previousMap.InternalName )|( selectedMap.InternalName == previousPreviousMap.InternalName ) ) // Ensure that two of the same maps don't occur in a row
                        {
                            // Either we don't have any maps left or there is one map left so we cannot resolve the duplicate issue, so force picking from usedmaps.
                            if ( ( mapListPgt.Maps.Count == 0 ) |  ( mapListPgt.Maps.Count == 1 ) ) 
                            {
                                if ( mapListPgt.UsedMapsCurrentIndex >= mapListPgt.UsedMaps.Count ) // Reset counter to 0 if necessary to start the loop over.
                                {
                                    mapListPgt.UsedMapsCurrentIndex = 0;
                                }
                                selectedMap = mapListPgt.UsedMaps[mapListPgt.UsedMapsCurrentIndex];
                                mapListPgt.UsedMapsCurrentIndex++;
                            }
                            else
                            {
                                selectedMap = Program.SelectMap( mapListPgt.Maps, _rnd.Next(0,mapListPgt.Maps.Count) );
                            }
                        }
                        if (Comments)
                        {
                            outputTxt += selectedMap.InternalName + " " + selectedMap.IntendedGameType.InternalName + " " + mapListPgt.Rounds.ToString() + " # Map: " + selectedMap.FriendlyName + ", Game Type: " + selectedMap.IntendedGameType.FriendlyName + "\r\n";
                        }
                        else
                        {
                            outputTxt += selectedMap.InternalName + " " + selectedMap.IntendedGameType.InternalName + " " + mapListPgt.Rounds.ToString() + "\r\n";
                        }
                        if ( mapListPgt.Maps.Count > 0 ) // Only run the below if we haven't run out of maps.
                        {
                            mapListPgt.UsedMaps.Add( selectedMap );
                            mapListPgt.Maps.Remove( selectedMap );
                        }
                        totalMapCount--;
                        previousPreviousMap = previousMap;
                        previousMap = selectedMap;
                    }
                }
            }
            else
            {
                BF3Map previousMap = new BF3Map();
                // else loop the number of nmRepeats.Value
                int Repeats = Convert.ToInt32( nmRepeats.Value );
                while ( Repeats > 0 )
                {
                    foreach ( BF3MapListPerGameType mapListPgt in bf3MapList )
                    // This should get executed once per game type, repeating nmRepeats.Value times.
                    {
                        BF3Map selectedMap = new BF3Map();
                        //string gt = "";
                        if ( mapListPgt.Maps.Count == 0 ) // We've run out of maps, start picking from UsedMaps list.
                        {
                            if ( mapListPgt.UsedMapsCurrentIndex >= mapListPgt.UsedMaps.Count ) // Reset counter to 0 if necessary
                            {
                                mapListPgt.UsedMapsCurrentIndex = 0;
                            }
                            selectedMap = mapListPgt.UsedMaps[mapListPgt.UsedMapsCurrentIndex];
                            mapListPgt.UsedMapsCurrentIndex++;
                        }
                        else
                        {
                            selectedMap = Program.SelectMap( mapListPgt.Maps, _rnd.Next(0,mapListPgt.Maps.Count) ); // Randomly select a map from the list
                        }
                        while  (selectedMap.InternalName == previousMap.InternalName ) // Ensure that two of the same maps don't occur in a row
                        {
                            // Either we don't have any maps left or there is one map left so we cannot resolve the duplicate issue, so force picking from usedmaps.
                            if ( ( mapListPgt.Maps.Count == 0 ) | ( mapListPgt.Maps.Count == 1 ) )
                            {
                                if ( mapListPgt.UsedMapsCurrentIndex >= mapListPgt.UsedMaps.Count ) // Reset counter to 0 if necessary to start the loop over.
                                {
                                    mapListPgt.UsedMapsCurrentIndex = 0;
                                }
                                selectedMap = mapListPgt.UsedMaps[mapListPgt.UsedMapsCurrentIndex];
                                mapListPgt.UsedMapsCurrentIndex++;
                            }
                            else
                            {
                                selectedMap = Program.SelectMap(mapListPgt.Maps, _rnd.Next(0,mapListPgt.Maps.Count));
                            }
                        }
                            outputTxt += selectedMap.InternalName + " " + selectedMap.IntendedGameType.InternalName + " " + mapListPgt.Rounds.ToString() + " # Map: " + selectedMap.FriendlyName + ", Game Type: " + selectedMap.IntendedGameType.FriendlyName + "\r\n";
                        if ( mapListPgt.Maps.Count > 0 ) // Only run the below if we haven't run out of maps.
                        {
                            mapListPgt.UsedMaps.Add( selectedMap );
                            mapListPgt.Maps.Remove( selectedMap );
                        }
                        previousMap = selectedMap;
                    }
                    Repeats--;
                }
            }
            #endregion
            // ---
            // Instancing the output form
            Output outputform = new Output();
            outputform.txOutput.Text = outputTxt;
            if ( ( outputform.Size.Height <= OutH ) | ( outputform.Size.Width <= OutW ) )
            {
                outputform.Size = new System.Drawing.Size( OutW, OutH );
            }
            outputform.ShowDialog();
            // After Show
            if ( outputform.Size.Width != OutW )
            {
                OutW = outputform.Size.Width;
            }
            if ( outputform.Size.Height != OutH )
            {
                OutH = outputform.Size.Height;
            }
            outputform.Dispose();
            }
            else MessageBox.Show( "ERROR: The pattern list is empty" );
        }

        #region DOUBLECLICK_EVENTS
        private void lbGameTypes_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            // Add the game type class and number of rounds to the pattern list
            lbGameTypesPattern.Items.Add( Program.bf3GameTypes[lbGameTypes.SelectedIndex].FriendlyName + "," + nmRounds.Value.ToString() );
        }

        private void lbGameTypesPattern_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            // Removes a pattern from the pattern list
            lbGameTypesPattern.Items.Remove( lbGameTypesPattern.SelectedItem );
        }
        #endregion

        #region EVENTS
        private void cbRepeatAll_CheckedChanged( object sender, EventArgs e )
        {
            if ( cbRepeatAll.Checked )
            {
                nmRepeats.Enabled = false;
                cbDontRepeat.Enabled = true;
            }
            else
            {
                nmRepeats.Enabled = true;
                cbDontRepeat.Enabled = false;
                cbDontRepeat.Checked = false;
            }
        }
        #endregion

    }
}
