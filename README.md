word-search-maksoodm
====================
This is a console application in C# .NET that implement Word Search Puzzle. 

The user story is: Given a collection of letters which contain hidden words (in the file WORDSEARCH.TXT), 
find all of the words in the word list (in the file WORDLIST.TXT) within the puzzle.  
The words may be hidden left to right, right to left, up, down or diagonally. 
The output should note which word were found, where it was found 
and one of the following designations for the direction the word takes:

LR - Left to right
RL - Right to left
U - Up
D - Down
DUL - Diagonal up left
DUR - Diagonal up right
DDL - Diagonal down left
DDR - Diagonal down right

Output from the Word Search Console Application
================================================
Content of WordSearch.txt:
TPIRCSAVAJLEXIPIGE
LIAMEMORYMMOUSENIL
CRABKSATXINUYHSTFG
DNDIRECTORYETAOEOO
POWERSUPPLYNIRFRLO
UCOASAEVASCCRETNDG
KIROPKTYPSHRUWWEEL
CDDECPREEAHYCAATRM
ANRIMALLTDRPERREAT
BOLENMEIEKETSEEPHH
RCKIPRAFCVRIIRSULM
EEBEIARRIABOOTMBOR
NSTWRAPRGRTNWBINGO
NOOSGNDLOODINTIOIS
ANGMAKAULARAOTEANR
CAEASPTLTAIPONRNDU
SNFIREWALLWREIKOOC
TFDPRDHTOOTEULBYTE

Content of WordList.txt:
Application
Backup
Binary
Bluetooth
Boot
Byte
Chat
Click
Cookie
Cursor
Data
Defragment
Directory
Disk drive
DOS
Drag
Email
Encryption
File
Firewall
Folder
GIF
Google
HTML
Icon
Internet
JavaScript
Kernal
LCD
Login
Memory
Monitor
Mouse
Nanosecond
Network
Partition
Paste
PDF
Pixel
Power Supply
Programmer
Router
Save As
Scanner
Security
ShareWare
Software
Spam
Taskbar
Thumbnail
UNIX
Wallpaper
Wireless

WordSearch Found Words in:
MEMORY found in LR
MOUSE found in LR
DIRECTORY found in LR
POWERSUPPLY found in LR
BOOT found in LR
FIREWALL found in LR
BYTE found in LR
JAVASCRIPT found in RL
PIXEL found in RL
EMAIL found in RL
TASKBAR found in RL
UNIX found in RL
SAVEAS found in RL
COOKIE found in RL
BLUETOOTH found in RL
PDF found in RL
LCD found in D
ENCRYPTION found in D
SHAREWARE found in D
SOFTWARE found in D
INTERNET found in D
FOLDER found in D
GIF found in D
LOGIN found in D
BACKUP found in U
SCANNER found in U
ICON found in U
NANOSECOND found in U
FILE found in U
SECURITY found in U
CURSOR found in U
GOOGLE found in U
HTML found in U
PARTITION found in DDL
APPLICATION found in DDL
KERNAL found in DDL
NETWORK found in DUR
PROGRAMMER found in DUR
WALLPAPER found in DUR
PASTE found in DUR
DRAG found in DUR
DEFRAGMENT found in DUL
CHAT found in DUL
MONITOR found in DUL
THUMBNAIL found in DUL
DATA found in DUL
ROUTER found in DUL
BINARY found in DDR
CLICK found in DDR
DOS found in DDR
SPAM found in DDR
DISKDRIVE found in DDR

WordSearch could not found the following word(s):
WIRELESS

Press any key to exit
