
ïo
gamemsg.protopb"S
BinBattleData
sceneid (Rsceneid
bsid (Rbsid
mapid (Rmapid"
ExMoney
gold (Rgold"›
BinFrequentRole3
worldbsdata (2.pb.BinBattleDataRworldbsdata
exp (Rexp%
exmoney (2.pb.ExMoneyRexmoney
exmoneys (Rexmoneys"¡
BinNormalRole
name (	Rname(
setting (2.pb.PlySettingRsetting
level (Rlevel
charid (Rcharid$
playerversion (Rplayerversion"×
C2S_DeviceLogin
ip (	Rip
mac (	Rmac
password (	Rpassword
passmd5 (	Rpassmd5
account (	Raccount
deviceid (	Rdeviceid
bindflag (	Rbindflag
platform (Rplatform"‹
S2C_DeviceLogin
number (Rnumber
	usercount (R	usercount
userids (	Ruserids
bindflag (	Rbindflag
	bindemail (	R	bindemail
bindnick (	Rbindnick"
devicefreeze (Rdevicefreeze
passret (Rpassret
errno	 (Rerrno"B

PlySetting
setting (Rsetting
tutorial (Rtutorial"Ž
C2S_CreateUser
ip (	Rip
number (Rnumber
name (	Rname
mac (	Rmac
password (	Rpassword
account (	Raccount
deviceid (	Rdeviceid(
setting (2.pb.PlySettingRsetting
charid	 (Rcharid
platform
 (Rplatform"~
S2C_CreateUser
roleid (Rroleid
name (	Rname
idx (Ridx
errno (Rerrno
passret (Rpassret"ê
C2S_LoadUser
roleid (Rroleid
version (Rversion
sversion (Rsversion
number (Rnumber
deviceid (	Rdeviceid
fversion (Rfversion
nversion (Rnversion 
reconntoken (	Rreconntoken"–
S2C_LoadUser
errno (Rerrno/
frequent (2.pb.BinFrequentRoleRfrequent)
normal (2.pb.BinNormalRoleRnormal 
pubmversion (Rpubmversion
serverid (Rserverid

servertime (R
servertime
fversion (Rfversion
nversion (Rnversion"š

GsRoomInfo
roomid (Rroomid
owner (Rowner
mapid (Rmapid
version (Rversion
maxnum (Rmaxnum
grpmem (Rgrpmem"°
GsRoomMember
roleid (Rroleid
name (	Rname
grpid (Rgrpid
pos (Rpos
ready (Rready
	operation (R	operation
isrobot (Risrobot"l
C2S_CreateRoom
mapid (Rmapid
name (	Rname
version (Rversion
grpmem (Rgrpmem"‡
S2C_CreateRoomRet
errno (Rerrno*
roominfo (2.pb.GsRoomInfoRroominfo0

roommember (2.pb.GsRoomMemberR
roommember"T
C2S_JoinRoom
name (	Rname
version (Rversion
roomid (Rroomid"…
S2C_JoinRoomRet
errno (Rerrno*
roominfo (2.pb.GsRoomInfoRroominfo0

roommember (2.pb.GsRoomMemberR
roommember"
C2S_LeaveRoom"(
S2C_LeaveRoomRet
errno (Rerrno"n
S2C_RoomUpdate*
roominfo (2.pb.GsRoomInfoRroominfo0

roommember (2.pb.GsRoomMemberR
roommember"%
C2S_RoomReady
ready (Rready">
S2C_RoomReadyRet
errno (Rerrno
ready (Rready"%
C2S_RoomChangePos
pos (Rpos"D
S2C_RoomChangePosRet
errno (Rerrno
newpos (Rnewpos")
C2S_RoomChangeMap
mapid (Rmapid"B
S2C_RoomChangeMapRet
errno (Rerrno
mapid (Rmapid"
C2S_StartRoomGame"‹
S2C_StartRoomGame
sceneid (Rsceneid
bsid (Rbsid
mapid (Rmapid
errno (Rerrno
	starttype (R	starttype"n
C2S_AutoJoinRoom
mapid (Rmapid
name (	Rname
version (Rversion
grpmem (Rgrpmem"‰
S2C_AutoJoinRoomRet
errno (Rerrno*
roominfo (2.pb.GsRoomInfoRroominfo0

roommember (2.pb.GsRoomMemberR
roommember"p
C2S_AutoJoinCSRoom
mapid (Rmapid
name (	Rname
version (Rversion
grpnum (Rgrpnum"*
C2S_RoomKickUser
kicked (Rkicked"‰
S2C_RoomKickUserRet
errno (Rerrno*
roominfo (2.pb.GsRoomInfoRroominfo0

roommember (2.pb.GsRoomMemberR
roommember"
S2C_RoomBeKicked"
C2S_RoomAddRobot"@
S2C_RoomAddRobot
errno (Rerrno
roleid (Rroleid"-
C2S_RoomRemoveRobot
roleid (Rroleid"+
S2C_RoomRemoveRobot
errno (Rerrno"
C2S_RoomFullRobot")
S2C_RoomFullRobot
errno (Rerrno"æ
C2S_BatQueMatchEnter
mapid (Rmapid
teammode (Rteammode0
elemtype (2.pb.BatMatchElemTypeRelemtype0
addteamr (2.pb.PUBGAddTeamrTypeRaddteamr
	onlyrobot (R	onlyrobot0
strategy (2.pb.BatMatchStrategyRstrategy
schemeid (Rschemeid
pointnum (Rpointnum
grade	 (Rgrade
sparring
 (Rsparring"d
S2C_BatQueMatchEnter6
	enterinfo (2.pb.C2S_BatQueMatchEnterR	enterinfo
errno (Rerrno"
C2S_BatQueMatchLeave"l
S2C_BatQueMatchLeave
errno (Rerrno
	srcroleid (R	srcroleid 
srcrolename (	Rsrcrolename"
C2S_BatQueMatchInfo"í
S2C_BatQueMatchInfo
inmatch (Rinmatch6
	enterinfo (2.pb.C2S_BatQueMatchEnterR	enterinfo 
queueplynum (Rqueueplynum

needmaxply (R
needmaxply

waitsecond (R
waitsecond"
aiopensecond (Raiopensecond"4
C2S_BatQueMatchPreInfo
teammode (Rteammode".
C2S_BatQueMatchQueInfo
mapid (Rmapid"Ë
S2C_BatQueMatchQueInfo
mapid (Rmapid5
infos (2.pb.S2C_BatQueMatchQueInfo.ModeRinfosd
Mode
teammode (Rteammode 
queueplynum (Rqueueplynum

needmaxply (R
needmaxply"š
C2S_AcctBind
ip (	Rip
email (	Remail
password (	Rpassword
nick (	Rnick
passmd5 (	Rpassmd5
bindtype (	Rbindtype"B
S2C_AcctBind
errno (Rerrno
	dytnumber (R	dytnumber"‰
C2S_GetAcctInfo
ip (	Rip
email (	Remail
password (	Rpassword
passmd5 (	Rpassmd5
bindtype (	Rbindtype"˜
S2C_GetAcctInfo
errno (Rerrno
number (	Rnumber
account (	Raccount
password (	Rpassword!
number_taget (	RnumberTaget":
C2S_UserRename
ip (	Rip
newname (	Rnewname"r
S2C_UserRename
newname (	Rnewname
idx (Ridx

relatename (	R
relatename
errno (Rerrno"a
C2S_GMCommand
cmd (	Rcmd
roleid (Rroleid
eid (Reid
bscmd (Rbscmd"$
S2C_GMCommandRet
ret (Rret"c
S2C_GMReloadRet
errno (Rerrno
filename (	Rfilename

servername (	R
servername"=
C2S_PlayerSetting(
setting (2.pb.PlySettingRsetting",
C2S_ConfirmRecon
confirm (Rconfirm"(
S2C_ConfirmRecon
errno (Rerrno"
C2S_QueryScene"&
S2C_QueryScene
errno (Rerrno"O
	ApplyInfo
roleid (Rroleid
name (	Rname
reason (	Rreason"?
C2S_FriendAdd
roleid (Rroleid
reason (	Rreason"j
S2C_FriendAdd
errno (Rerrno
roleid (Rroleid+
	applyinfo (2.pb.ApplyInfoR	applyinfo"
C2S_Applylist"P
S2C_Applylist
errno (Rerrno)
listdata (2.pb.ApplyInfoRlistdata"*
C2S_FriendAccept
roleid (Rroleid"Ì
S2C_FriendAccept
errno (Rerrno
srcid (Rsrcid
name (	Rname
online (Ronline
destid (Rdestid
roomid (Rroomid
teamid (Rteamid
sceneid (Rsceneid"*
C2S_FriendRefuse
roleid (Rroleid"T
S2C_FriendRefuse
errno (Rerrno
roleid (Rroleid
name (	Rname"'
C2S_FriendDel
roleid (Rroleid"S
S2C_FriendDel
errno (Rerrno
srcid (Rsrcid
destid (Rdestid"7
	QueryInfo
roleid (Rroleid
name (	Rname"<
C2S_FriendFind
roleid (Rroleid
name (	Rname"S
S2C_FriendFind
errno (Rerrno+
	queryinfo (2.pb.QueryInfoR	queryinfo"š

FriendInfo
roleid (Rroleid
name (	Rname
online (Ronline
roomid (Rroomid
teamid (Rteamid
sceneid (Rsceneid"
C2S_FriendList"V
S2C_FriendList
errno (Rerrno.

friendinfo (2.pb.FriendInfoR
friendinfo"B
S2C_FriendUpdate.

friendinfo (2.pb.FriendInfoR
friendinfo"=
C2S_RequestTeam
roleid (Rroleid
name (	Rname"¥
S2C_RequestTeam
errno (Rerrno
request (Rrequest
teamid (Rteamid
name (	Rname
roleid (Rroleid

destcharid (R
destcharid"B
C2S_AcceptTeam
teamid (Rteamid
request (Rrequest"X
S2C_AcceptTeam
errno (Rerrno
roleid (Rroleid
request (Rrequest"
C2S_LeaveTeam"=
S2C_LeaveTeam
errno (Rerrno
roleid (Rroleid"&
C2S_KickTeam
roleid (Rroleid"h
S2C_KickTeam
errno (Rerrno
kicker (Rkicker
name (	Rname
roleid (Rroleid"%
C2S_TeamReady
ready (Rready"%
S2C_TeamReady
errno (Rerrno"B
C2S_TeamRefuse
teamid (Rteamid
request (Rrequest"l
S2C_TeamRefuse
errno (Rerrno
roleid (Rroleid
name (	Rname
request (Rrequest"(
C2S_TeamCancel
roleid (Rroleid"p
S2C_TeamCancel
errno (Rerrno
roleid (Rroleid
teamid (Rteamid
request (Rrequest"p

GsTeamInfo
teamid (Rteamid
owner (Rowner
maxnum (Rmaxnum
	matchmode (R	matchmode"Ä
GsTeamMember
roleid (Rroleid
name (	Rname
pos (Rpos
accept (Raccept
ready (Rready
request (Rrequest
logout (Rlogout
charid (Rcharid"b
S2C_TeamUpdate$
infos (2.pb.GsTeamInfoRinfos*
members (2.pb.GsTeamMemberRmembers"+
C2S_UpgradeLeader
roleid (Rroleid")
S2C_UpgradeLeader
errno (Rerrno"'
S2C_ReloadInter
errno (Rerrno"-
C2S_ChangeCharacter
charid (Rcharid"C
S2C_ChangeCharacter
errno (Rerrno
charid (Rcharid"(
C2S_EchoTest
teststr (Rteststr"(
S2C_EchoTest
teststr (Rteststr"@
C2S_Chat 
type (2.pb.ChatTypeRtype
data (	Rdata"‚
S2C_Chat
sender (Rsender
name (	Rname 
type (2.pb.ChatTypeRtype
data (	Rdata
errno (Rerrno"<
C2S_FriendChat
destid (Rdestid
data (	Rdata"
C2S_PullFriendMsg"Å
S2C_FriendMsg4
allmsg (2.pb.S2C_FriendMsg.OneMessageRallmsg
errno (Rerrnoh

OneMessage
sender (Rsender
name (	Rname
sendtime (Rsendtime
data (	Rdata"ç
CheckPointOneChoice
elemid (Relemid
mapid (Rmapid
grade (Rgrade
quetype (Rquetype
isnext (Risnext 
chooseteamr (Rchooseteamr
	openlimit (R	openlimit
rewardid (Rrewardid"I
CheckPointChoicesRole
roleid (Rroleid
goldnum (Rgoldnum"Õ
S2C_CheckPointChoices1
choices (2.pb.CheckPointOneChoiceRchoices
schemeid (Rschemeid
pointnum (Rpointnum
	endsecond (R	endsecond
collid (Rcollid
fromgs (Rfromgs

maxfailnum (R
maxfailnum

nowfailnum (R
nowfailnum 
maxpointnum	 (Rmaxpointnum"
maxfinishnum
 (Rmaxfinishnum"
nowfinishnum (Rnowfinishnum5
roleinfo (2.pb.CheckPointChoicesRoleRroleinfo"
allfinishnum (Rallfinishnum"^
C2S_CheckPointChoose
elemid (Relemid
collid (Rcollid
fromgs (Rfromgs"¡
S2C_CheckPointResult/
choice (2.pb.CheckPointOneChoiceRchoice
schemeid (Rschemeid
pointnum (Rpointnum 
finalelemid (Rfinalelemid"¢
CheckPointInitChar
charid (Rcharid
hp (Rhp
shield (Rshield
speed (Rspeed"
finefeatures (Rfinefeatures 
badfeatures (Rbadfeatures

initweapon (R
initweapon
defskill (Rdefskill
energy	 (Renergy
	energymax
 (R	energymax"š
S2C_CheckPointCharChoice0
choices (2.pb.CheckPointInitCharRchoices
	endsecond (R	endsecond
collid (Rcollid
fromgs (Rfromgs"b
C2S_CheckPointCharChoice
charid (Rcharid
collid (Rcollid
fromgs (Rfromgs"`
C2S_CheckPointCharScan
charid (Rcharid
collid (Rcollid
fromgs (Rfromgs"v
S2C_CheckPointCharInfo
roleid (Rroleid*
info (2.pb.CheckPointInitCharRinfo
confirm (Rconfirm"H
S2C_ComeBackWorld3
worldbsdata (2.pb.BinBattleDataRworldbsdata*ì
MSG
MSG_None 
MSG_DeviceLoginè
MSG_CreateUseré
MSG_LoadUserê
MSG_UserRenameë
MSG_AcctBindì
MSG_GetAcctInfoí
	MSG_GMCMDî
MSG_PlayerSettingï
MSG_ConfirmReconð
MSG_QuerySceneñ
MSG_GMReloadRetò
MSG_ReloadInteró
MSG_ChangeCharacterô
MSG_EchoTestõ
MSG_Chatö
MSG_CreateRoomÌ
MSG_JoinRoomÍ
MSG_LeaveRoomÎ
MSG_RoomUpdateÏ
MSG_RoomReadyÐ
MSG_RoomChangePosÑ
MSG_StartRoomGameÒ
MSG_AutoJoinRoomÓ
MSG_RoomChangeMapÔ
MSG_RoomKickUserÕ
MSG_RoomBeKickedÖ
MSG_RoomAddRobot×
MSG_RoomRemoveRobotØ
MSG_RoomFullRobotÙ
MSG_AutoJoinCSRoomÚ
MSG_BatQueMatchEnterþ
MSG_BatQueMatchLeaveÿ
MSG_BatQueMatchInfo€	
MSG_BatQueMatchPreInfo	
MSG_BatQueMatchQueInfo‚	
MSG_FriendAdd°	
MSG_ApplyList±	
MSG_FriendAccept²	
MSG_FriendRefuse³	
MSG_FriendDel´	
MSG_FriendFindµ	
MSG_FriendList¶	
MSG_FriendUpdate·	
MSG_FriendChat¸	
MSG_PullFriendMsg¹	
MSG_TeamRequestâ	
MSG_TeamAcceptã	
MSG_LeaveTeamä	
MSG_TeamKickå	
MSG_TeamUpdateæ	
MSG_TeamReadyç	
MSG_TeamRefuseè	
MSG_TeamCancelé	
MSG_UpgradeLeaderê	
MSG_BattleLoginÐ
MSG_BattleFrameÑ
MSG_EntityDisAppearÒ
MSG_GameLoadingÓ
MSG_GameStartÔ
MSG_GameEndÕ
MSG_PlayerQuitGameÖ
MSG_Settlement×
MSG_BattleChatØ
MSG_BattleReconnectÙ
	MSG_WatchÚ
MSG_BattleAllPlayerÛ
MSG_ReplaceEquipÜ
MSG_SupplySelectÝ
MSG_SingleVoteÞ
MSG_CheckPointChoiceß
MSG_CheckPointResultà
MSG_CheckPointCharChoiceá
MSG_CheckPointCharInfoâ
MSG_VoteResultã
MSG_BuyItemä
MSG_EnterPvPVoteå
MSG_ComeBackWorldæ
MSG_PvPCharChoiceç
MSG_ItemErrorè
MSG_TalentChooseé
MSG_AddAudioChannel¸
MSG_EnterAudioChannel¹
MSG_AllDoGM‰'
MSG_Testñ.*J
StartGameType
StartByMatch 
StartByRoom
StartBySupplyMatch*2
BatMatchElemType
BMET_Person 
	BMET_Team*R
PUBGAddTeamrType
PUBG_ATT_CanAdd 
PUBG_ATT_NotAdd
PUBG_ATT_MustAdd*4
BatMatchStrategy
BMS_PUBG 
BMS_CheckPoint*]
ChatType
CT_None 
CT_Team
CT_Room
CT_World
	CT_Battle
	CT_Friendbproto3