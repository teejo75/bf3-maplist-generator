BF3MapListGenerator
===================

This is a utility to generate RANDOM Battlefield 3 map lists for server
admins. You can generate human readable lists for manual entry into 
admin tools such as procon, or you can generate maplist.txt style lists
to copy and paste directly in to your server's maplist.txt.

Known Issues with current release:
- None

Changelog:
17 April 2014:
 - Fix map selection menu desync when loading settings from disk.
22 January 2014:
 - Add update notifier
13 January 2014:
 - Fix pattern list up/down function
18 December 2013:
 - Cosmetic changes
 - Added button for copy to clipboard on output window.
16 December 2013:
 - Corrected hang caused by large pattern list.
12 September 2013:
 - Added option to exclude comments from output
13 March 2013:
 - Corrected some stupid modal stuff. Should be able to run generate list more than once per session now.
12 March 2013:
 - Updated for End Game
 - Corrected maps corresponding to game types
   - Removed Combined TDM as it is no longer needed
 - Some internal code refactoring
10 January 2013:
 - New feature: Quick Map Selections - Select map groups from various expansions via the menu.
05 December 2012:
 - Updated with Aftermath Gametypes / Maps
02 October 2012:
- Updated with AK Gametypes / Maps
- Added Combined TDM for TDM & CQ TDM maps
12 August 2012:
- Updated with CQ Gametypes / Maps
10 May 2012:
- Removed "Human Readable" map generation. A maplist.txt list will now
  be generated with "Human Readable" comments.
- Fixed resizing of the output window.
4 May 2012:
- Improved list generation slightly. It will now try to have at least
  two different maps before repeating a map (even though game types
  differ).
3 May 2012:
- Persistence: App will now remember your settings.
  Simply delete the generated .dat file and restart the app to reset to
  defaults.
- Removed comments on generated output.
- Added new setting checkbox: ≠∞
  This is only enabled when the ∞ checkbox is set. It tells the 
  generator to not repeat maps when a gametype runs out.
  For example: If you select Assault and Rush in a pattern, there are
  more Rush maps than there are Assault maps, so you will get a
  repeated map list of: Rush1, Assault1, Rush2, Assault2, Rush3, 
  Assault1, Rush4, Assault2, Rush5, Assault 1, etc.
  With the check box selected, it will stop repeating the Assault
  maps and just finish off the pattern with Rush maps.
2 May 2012:
- Fixed combined game types. Can now use B2K Conquest Large and 
  B2K Conquest Small, which are combinations of normal conquest and
  the new Conquest Assault game types in the B2K map pack.
1 May 2012: 
- Initial Release


NOTE: THIS IS BETA SOFTWARE.
DISCLAIMER OF WARRANTIES AND LIMITATION OF LIABILITY

The software is supplied "as is" and all use it as your own risk. The software
author / distributor disclaims all warranties of any kind, either express or 
implied, as to the software, including but not limited to, implied warranties
of fitness for a particular purpose, merchantability or non-infringement of 
proprietary rights. Neither this agreement nor any documentation furnished 
under it is intended to express or imply any warranty that the operation of the
software will be uninterrupted, timely, or error-free.

Under no circumstances shall the software author / distributor be liable to any
user for direct, indirect, incidental, consequential, special, or exemplary 
damages, arising from or relating to this agreement, the software, or user's 
use or misuse of the software or any other services provided by the software 
author / distributor. Such limitation of liability shall apply whether the 
damages arise from the use or misuse of the software or any other services 
supplied by the software author / distributor (including such damages incurred 
by third parties), or errors of the software.


How to use it:
==============
This may not be immediately obvious from the interface layout, but I'll
try to keep it simple.

Unselect the maps that you do NOT want to appear in the generated list.
The generated map list is governed by the pattern list (the right hand 
list). Select the game type pattern that you want to appear in the 
output list (as well as the desired number of rounds).

For example, to generate the typical Conquest 1 round, Rush 2 round map
list that we normally have running on the War Geeks server, you would
simply add a gametype of B2K Conquest, 1 round, and Rush, 2 rounds to
the pattern.

Ensure the infinite checkbox (∞) is selected (very bottom checkbox 
between the lists with the infinity symbol), and on the menu, click 
Generate->maplist.txt or Generate->Human Readable. A random map list 
will then be generated based on the defined pattern until all maps are 
used.

Note that if you select a game type that only has a few maps (like 
assault), and a rush game type, the assault maps will be continually
repeated until there are no more rush maps left, unless you select the
top checkbox (≠∞). In that case, if there are no more maps left for a 
given game type, they will not be repeated, and only maps from the 
remaining game types will be used.

You can unselect the Infinite checkbox and specify how many times
you want the pattern to repeat instead, from once, to 100 times.

I've tried to avoid having the same two maps of different game types
following one another, but it is difficult to test every single 
scenario.

Note that certain game type combinations are invalid and may cause the
server to ignore the offending game types.

There is no checking of game type combinations at this time, so it is 
very easy to generate strange map lists. It is not wise, for example,
to mix Squad Rush and Conquest maps as the number of players are
incompatible.

If you're unhappy with the distribution of a generated list, just 
generate a new one, or edit the generated list.