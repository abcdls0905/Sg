
�o


sceneid (
bsid (Rbsid
mapid (
ExMoney
gold (Rgold"�
BinFrequentRole3
worldbsdata (2.pb.BinBattleDataRworldbsdata
exp (Rexp%
exmoney (2.pb.ExMoneyRexmoney
exmoneys (Rexmoneys"�

name (	Rname(
setting (2.pb.PlySettingRsetting
level (
charid (

C2S_DeviceLogin
ip (	Rip
mac (	Rmac
password (	Rpassword
passmd5 (	Rpassmd5
account (	Raccount
deviceid (	Rdeviceid
bindflag (	Rbindflag
platform (Rplatform"�
S2C_DeviceLogin
number (
	usercount (
userids (	Ruserids
bindflag (	Rbindflag
	bindemail (	R	bindemail
bindnick (	Rbindnick"
devicefreeze (
passret (Rpassret
errno	 (Rerrno"B

PlySetting
setting (Rsetting
tutorial (Rtutorial"�
C2S_CreateUser
ip (	Rip
number (
name (	Rname
mac (	Rmac
password (	Rpassword
account (	Raccount
deviceid (	Rdeviceid(
setting (2.pb.PlySettingRsetting
charid	 (
platform
 (Rplatform"~
S2C_CreateUser
roleid (
name (	Rname
idx (
errno (Rerrno
passret (Rpassret"�
C2S_LoadUser
roleid (
version (
sversion (
number (
deviceid (	Rdeviceid
fversion (
nversion (
reconntoken (	Rreconntoken"�
S2C_LoadUser
errno (Rerrno/
frequent (2.pb.BinFrequentRoleRfrequent)
normal (2.pb.BinNormalRoleRnormal 
pubmversion (
serverid (

servertime (R
servertime
fversion (
nversion (

GsRoomInfo
roomid (Rroomid
owner (
mapid (Rmapid
version (Rversion
maxnum (
grpmem (
GsRoomMember
roleid (
name (	Rname
grpid (
pos (
ready (Rready
	operation (
isrobot (Risrobot"l
C2S_CreateRoom
mapid (Rmapid
name (	Rname
version (Rversion
grpmem (
S2C_CreateRoomRet
errno (Rerrno*
roominfo (2.pb.GsRoomInfoRroominfo0

roommember (2.pb.GsRoomMemberR
roommember"T
C2S_JoinRoom
name (	Rname
version (Rversion
roomid (Rroomid"�
S2C_JoinRoomRet
errno (Rerrno*
roominfo (2.pb.GsRoomInfoRroominfo0

roommember (2.pb.GsRoomMemberR
roommember"

S2C_LeaveRoomRet
errno (Rerrno"n
S2C_RoomUpdate*
roominfo (2.pb.GsRoomInfoRroominfo0

roommember (2.pb.GsRoomMemberR
roommember"%

ready (Rready">
S2C_RoomReadyRet
errno (Rerrno
ready (Rready"%
C2S_RoomChangePos
pos (
S2C_RoomChangePosRet
errno (Rerrno
newpos (
C2S_RoomChangeMap
mapid (
S2C_RoomChangeMapRet
errno (Rerrno
mapid (
C2S_StartRoomGame"�
S2C_StartRoomGame
sceneid (
bsid (Rbsid
mapid (
errno (Rerrno
	starttype (
C2S_AutoJoinRoom
mapid (Rmapid
name (	Rname
version (Rversion
grpmem (
S2C_AutoJoinRoomRet
errno (Rerrno*
roominfo (2.pb.GsRoomInfoRroominfo0

roommember (2.pb.GsRoomMemberR
roommember"p
C2S_AutoJoinCSRoom
mapid (Rmapid
name (	Rname
version (Rversion
grpnum (
C2S_RoomKickUser
kicked (
S2C_RoomKickUserRet
errno (Rerrno*
roominfo (2.pb.GsRoomInfoRroominfo0

roommember (2.pb.GsRoomMemberR
roommember"
S2C_RoomBeKicked"
C2S_RoomAddRobot"@
S2C_RoomAddRobot
errno (Rerrno
roleid (
C2S_RoomRemoveRobot
roleid (
S2C_RoomRemoveRobot
errno (Rerrno"
C2S_RoomFullRobot")
S2C_RoomFullRobot
errno (Rerrno"�
C2S_BatQueMatchEnter
mapid (
teammode (
elemtype (2.pb.BatMatchElemTypeRelemtype0
addteamr (2.pb.PUBGAddTeamrTypeRaddteamr
	onlyrobot (R	onlyrobot0
strategy (2.pb.BatMatchStrategyRstrategy
schemeid (
pointnum (
grade	 (
sparring
 (Rsparring"d
S2C_BatQueMatchEnter6
	enterinfo (2.pb.C2S_BatQueMatchEnterR	enterinfo
errno (Rerrno"
C2S_BatQueMatchLeave"l
S2C_BatQueMatchLeave
errno (Rerrno
	srcroleid (
srcrolename (	Rsrcrolename"
C2S_BatQueMatchInfo"�
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
teammode (
C2S_BatQueMatchQueInfo
mapid (
S2C_BatQueMatchQueInfo
mapid (
infos (2.pb.S2C_BatQueMatchQueInfo.ModeRinfosd
Mode
teammode (
queueplynum (Rqueueplynum

needmaxply (R
needmaxply"�
C2S_AcctBind
ip (	Rip
email (	Remail
password (	Rpassword
nick (	Rnick
passmd5 (	Rpassmd5
bindtype (	Rbindtype"B
S2C_AcctBind
errno (Rerrno
	dytnumber (R	dytnumber"�
C2S_GetAcctInfo
ip (	Rip
email (	Remail
password (	Rpassword
passmd5 (	Rpassmd5
bindtype (	Rbindtype"�
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
idx (

relatename (	R
relatename
errno (Rerrno"a

cmd (	Rcmd
roleid (
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
roleid (
name (	Rname
reason (	Rreason"?

roleid (
reason (	Rreason"j

errno (Rerrno
roleid (
	applyinfo (2


errno (Rerrno)
listdata (2
C2S_FriendAccept
roleid (
S2C_FriendAccept
errno (Rerrno
srcid (
name (	Rname
online (Ronline
destid (
roomid (Rroomid
teamid (Rteamid
sceneid (
C2S_FriendRefuse
roleid (
S2C_FriendRefuse
errno (Rerrno
roleid (
name (	Rname"'

roleid (

errno (Rerrno
srcid (
destid (
	QueryInfo
roleid (
name (	Rname"<
C2S_FriendFind
roleid (
name (	Rname"S
S2C_FriendFind
errno (Rerrno+
	queryinfo (2

FriendInfo
roleid (
name (	Rname
online (Ronline
roomid (Rroomid
teamid (Rteamid
sceneid (
C2S_FriendList"V
S2C_FriendList
errno (Rerrno.

friendinfo (2.pb.FriendInfoR
friendinfo"B
S2C_FriendUpdate.

friendinfo (2.pb.FriendInfoR
friendinfo"=
C2S_RequestTeam
roleid (
name (	Rname"�
S2C_RequestTeam
errno (Rerrno
request (
teamid (Rteamid
name (	Rname
roleid (

destcharid (
destcharid"B
C2S_AcceptTeam
teamid (Rteamid
request (
S2C_AcceptTeam
errno (Rerrno
roleid (
request (


errno (Rerrno
roleid (
C2S_KickTeam
roleid (
S2C_KickTeam
errno (Rerrno
kicker (
name (	Rname
roleid (

ready (Rready"%

errno (Rerrno"B
C2S_TeamRefuse
teamid (Rteamid
request (
S2C_TeamRefuse
errno (Rerrno
roleid (
name (	Rname
request (
C2S_TeamCancel
roleid (
S2C_TeamCancel
errno (Rerrno
roleid (
teamid (Rteamid
request (

GsTeamInfo
teamid (Rteamid
owner (
maxnum (
	matchmode (
GsTeamMember
roleid (
name (	Rname
pos (
accept (Raccept
ready (Rready
request (
logout (Rlogout
charid (
S2C_TeamUpdate$
infos (2.pb.GsTeamInfoRinfos*
members (2.pb.GsTeamMemberRmembers"+
C2S_UpgradeLeader
roleid (
S2C_UpgradeLeader
errno (Rerrno"'
S2C_ReloadInter
errno (Rerrno"-
C2S_ChangeCharacter
charid (
S2C_ChangeCharacter
errno (Rerrno
charid (
C2S_EchoTest
teststr (Rteststr"(
S2C_EchoTest
teststr (Rteststr"@
C2S_Chat 
type (2.pb.ChatTypeRtype
data (	Rdata"�
S2C_Chat
sender (
name (	Rname 
type (2.pb.ChatTypeRtype
data (	Rdata
errno (Rerrno"<
C2S_FriendChat
destid (
data (	Rdata"
C2S_PullFriendMsg"�

allmsg (2.pb.S2C_FriendMsg.OneMessageRallmsg
errno (Rerrnoh

OneMessage
sender (
name (	Rname
sendtime (Rsendtime
data (	Rdata"�
CheckPointOneChoice
elemid (
mapid (
grade (
quetype (
isnext (Risnext 
chooseteamr (
	openlimit (
rewardid (
CheckPointChoicesRole
roleid (
goldnum (Rgoldnum"�
S2C_CheckPointChoices1
choices (2.pb.CheckPointOneChoiceRchoices
schemeid (
pointnum (
	endsecond (R	endsecond
collid (Rcollid
fromgs (

maxfailnum (R
maxfailnum

nowfailnum (R
nowfailnum 
maxpointnum	 (Rmaxpointnum"
maxfinishnum
 (Rmaxfinishnum"
nowfinishnum (Rnowfinishnum5
roleinfo (2.pb.CheckPointChoicesRoleRroleinfo"
allfinishnum
C2S_CheckPointChoose
elemid (
collid (Rcollid
fromgs (
S2C_CheckPointResult/
choice (2.pb.CheckPointOneChoiceRchoice
schemeid (
pointnum (
finalelemid (Rfinalelemid"�
CheckPointInitChar
charid (
hp (Rhp
shield (Rshield
speed (Rspeed"
finefeatures (
badfeatures (

initweapon (
initweapon
defskill (
energy	 (Renergy
	energymax
 (R	energymax"�
S2C_CheckPointCharChoice0
choices (2.pb.CheckPointInitCharRchoices
	endsecond (R	endsecond
collid (Rcollid
fromgs (
C2S_CheckPointCharChoice
charid (
collid (Rcollid
fromgs (
C2S_CheckPointCharScan
charid (
collid (Rcollid
fromgs (
S2C_CheckPointCharInfo
roleid (
info (2.pb.CheckPointInitCharRinfo
confirm (Rconfirm"H
S2C_ComeBackWorld3
worldbsdata (2.pb.BinBattleDataRworldbsdata*�
MSG
MSG_None 
MSG_DeviceLogin�
MSG_CreateUser�
MSG_LoadUser�
MSG_UserRename�
MSG_AcctBind�
MSG_GetAcctInfo�
	MSG_GMCMD�
MSG_PlayerSetting�
MSG_ConfirmRecon�
MSG_QueryScene�
MSG_GMReloadRet�
MSG_ReloadInter�
MSG_ChangeCharacter�
MSG_EchoTest�
MSG_Chat�
MSG_CreateRoom�
MSG_JoinRoom�

MSG_RoomUpdate�

MSG_RoomChangePos�
MSG_StartRoomGame�
MSG_AutoJoinRoom�
MSG_RoomChangeMap�
MSG_RoomKickUser�
MSG_RoomBeKicked�
MSG_RoomAddRobot�
MSG_RoomRemoveRobot�
MSG_RoomFullRobot�
MSG_AutoJoinCSRoom�
MSG_BatQueMatchEnter�
MSG_BatQueMatchLeave�
MSG_BatQueMatchInfo�	
MSG_BatQueMatchPreInfo�	
MSG_BatQueMatchQueInfo�	


MSG_FriendAccept�	
MSG_FriendRefuse�	

MSG_FriendFind�	
MSG_FriendList�	
MSG_FriendUpdate�	
MSG_FriendChat�	
MSG_PullFriendMsg�	
MSG_TeamRequest�	
MSG_TeamAccept�	

MSG_TeamKick�	
MSG_TeamUpdate�	

MSG_TeamRefuse�	
MSG_TeamCancel�	
MSG_UpgradeLeader�	
MSG_BattleLogin�
MSG_BattleFrame�
MSG_EntityDisAppear�
MSG_GameLoading�

MSG_GameEnd�
MSG_PlayerQuitGame�
MSG_Settlement�
MSG_BattleChat�
MSG_BattleReconnect�
	MSG_Watch�
MSG_BattleAllPlayer�
MSG_ReplaceEquip�
MSG_SupplySelect�
MSG_SingleVote�
MSG_CheckPointChoice�
MSG_CheckPointResult�
MSG_CheckPointCharChoice�
MSG_CheckPointCharInfo�
MSG_VoteResult�
MSG_BuyItem�
MSG_EnterPvPVote�
MSG_ComeBackWorld�
MSG_PvPCharChoice�

MSG_TalentChoose�
MSG_AddAudioChannel�
MSG_EnterAudioChannel�
MSG_AllDoGM�'
MSG_Test�.*J

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