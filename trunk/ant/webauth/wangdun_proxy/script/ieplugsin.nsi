Name "WangDunProxy"

SetCompressor lzma

# Defines

!define IE_CONTEXT_MENU_DEFAULT 0x1
!define IE_CONTEXT_MENU_IMAGE 0x2
!define IE_CONTEXT_MENU_CONTROL 0x4
!define IE_CONTEXT_MENU_TABLE 0x8
!define IE_CONTEXT_MENU_TEXTSELECT 0x10
!define IE_CONTEXT_MENU_ANCHOR 0x20
!define IE_CONTEXT_MENU_UNKNOWN 0x40
 
#!define IEMenuItem_KeyName `SOFTWARE\Microsoft\Internet Explorer\MenuExt`
 
#!define IEMenuItem_Add `!insertmacro IEMenuItem_Add`
#!macro IEMenuItem_Add `Name` `Exec` `Contexts` `Flags`
#    WriteRegStr HKCU `${IEMenuItem_KeyName}\${Name}` `` `${Exec}`
#    WriteRegDWORD HKCU `${IEMenuItem_KeyName}\${Name}` `Contexts` ${Contexts}
#   WriteRegDWORD HKCU `${IEMenuItem_KeyName}\${Name}` `Flags` ${Flags}
#!macroend
 
#!define IEMenuItem_Remove `!insertmacro IEMenuItem_Remove`
#!macro IEMenuItem_Remove `Name`
#    DeleteRegKey HKLM `${IEMenuItem_KeyName}\${Name}`
#!macroend


!define REGKEY "SOFTWARE\$(^Name)"
!define VERSION 0.1.0.0
!define COMPANY "Kingsoft Crop."
!define URL http://www.pc120.com

# MUI defines
#!define MUI_ICON ..\resouce\typeease.ico
!define MUI_FINISHPAGE_NOAUTOCLOSE
!define MUI_STARTMENUPAGE_REGISTRY_ROOT HKLM
!define MUI_STARTMENUPAGE_REGISTRY_KEY ${REGKEY}
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME StartMenuGroup
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "WangdunProxy"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\win-uninstall.ico"
!define MUI_UNFINISHPAGE_NOAUTOCLOSE
#!define MUI_WELCOMEFINISHPAGE_BITMAP ..\resouce\back.bmp
#!define MUI_UNWELCOMEFINISHPAGE_BITMAP ..\resouce\back.bmp

# Included files
!include Sections.nsh
!include MUI.nsh

# Variables
Var StartMenuGroup

Function GetIEVersion
  Push $0
  ClearErrors
  ReadRegStr $0 HKLM "Software\Microsoft\Internet Explorer" "Version"
  IfErrors lbl_123 lbl_456

  lbl_456: ; ie 4+
    Strcpy $0 $0 1
  Goto lbl_done

  lbl_123: ; older ie version
    ClearErrors
    ReadRegStr $0 HKLM "Software\Microsoft\Internet Explorer" "IVer"
    IfErrors lbl_error

      StrCpy $0 $0 3
      StrCmp $0 '100' lbl_ie1
      StrCmp $0 '101' lbl_ie2
      StrCmp $0 '102' lbl_ie2

      StrCpy $0 '3' ; default to ie3 if not 100, 101, or 102.
      Goto lbl_done
        lbl_ie1: 
          StrCpy $0 '1'
        Goto lbl_done
        lbl_ie2:
          StrCpy $0 '2'
        Goto lbl_done
    lbl_error:
      StrCpy $0 ''
  lbl_done:
  Exch $0
FunctionEnd


# Installer pages
!insertmacro MUI_PAGE_WELCOME
#!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_STARTMENU Application $StartMenuGroup
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

# Installer languages
!insertmacro MUI_LANGUAGE SimpChinese


# Installer attributes
OutFile ..\WangDunProxy-Setup.exe
InstallDir "$PROGRAMFILES\Kingsoft\WangDunProxy"
CRCCheck on
XPStyle on
ShowInstDetails show
VIProductVersion "${VERSION}"
VIAddVersionKey ProductName "Web Observer2"
VIAddVersionKey ProductVersion "${VERSION}"
VIAddVersionKey CompanyName "${COMPANY}"
VIAddVersionKey CompanyWebsite "${URL}"
VIAddVersionKey FileVersion "${VERSION}"
VIAddVersionKey FileDescription ""
VIAddVersionKey LegalCopyright ""
InstallDirRegKey HKLM "${REGKEY}" Path
ShowUninstDetails show

#DirText "安装程序将安装 $(^NameDA) 在下列文件夹。要安装到不同文件夹，单击 [浏览(B)] 并选择其他的文件夹。 $_CLICK" 
# Installer sections
Section 程序文件 SEC0001
    SetOutPath $INSTDIR
    SetOverwrite On
    File ..\WangDunProxy\Release\WangDunProxy.dll
    ExecWait 'regsvr32 /s "$INSTDIR\WangDunProxy.dll"'
    WriteRegStr HKLM "${REGKEY}\Components" main 1
SectionEnd

Section -post SEC0002
    WriteRegStr HKLM "${REGKEY}" Path $INSTDIR
    SetOutPath $INSTDIR
    WriteUninstaller $INSTDIR\uninstall.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayName "$(^Name)"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayVersion "${VERSION}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" Publisher "${COMPANY}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" URLInfoAbout "${URL}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayIcon $INSTDIR\uninstall.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" UninstallString $INSTDIR\uninstall.exe
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoModify 1
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoRepair 1
#    ${IEMenuItem_Add} "查看链接详细安全信息" res://$INSTDIR\WebObserver2.dll/213 0x22 0x00
SectionEnd

# Macro for selecting uninstaller sections
!macro SELECT_UNSECTION SECTION_NAME UNSECTION_ID
    Push $R0
    ReadRegStr $R0 HKLM "${REGKEY}\Components" "${SECTION_NAME}"
    StrCmp $R0 1 0 next${UNSECTION_ID}
    !insertmacro SelectSection "${UNSECTION_ID}"
    GoTo done${UNSECTION_ID}
next${UNSECTION_ID}:
    !insertmacro UnselectSection "${UNSECTION_ID}"
done${UNSECTION_ID}:
    Pop $R0
!macroend

# Uninstaller sections
Section /o -un.main UNSEC0000
    ExecWait 'regsvr32 /s /u "$INSTDIR\WangDunProxy.dll"'
    RmDir /r /REBOOTOK $INSTDIR
    DeleteRegValue HKLM "${REGKEY}\Components" main
SectionEnd

Section -un.post UNSEC0001
    DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)"
    Delete /REBOOTOK $INSTDIR\uninstall.exe
    #DeleteRegValue HKLM "${REGKEY}" StartMenuGroup
    DeleteRegValue HKLM "${REGKEY}" Path
    DeleteRegKey /IfEmpty HKLM "${REGKEY}\Components"
    DeleteRegKey /IfEmpty HKLM "${REGKEY}"
    #RmDir /r /REBOOTOK $SMPROGRAMS\$StartMenuGroup
    RmDir /REBOOTOK $INSTDIR
    Push $R0
    StrCpy $R0 $StartMenuGroup 1
    StrCmp $R0 ">" no_smgroup
no_smgroup:
    Pop $R0
SectionEnd

# Installer functions
Function .onInit
		Call GetIEVersion
		Pop $0
		StrCmp $0 '7' lbl_done
		StrCmp $0 '6' lbl_done
		MessageBox MB_OK|MB_ICONEXCLAMATION "你的IE版本暂不支持"
		Abort 你的IE版本暂不支持.
		lbl_done:
    InitPluginsDir
FunctionEnd

# Uninstaller functions
Function un.onInit
    ReadRegStr $INSTDIR HKLM "${REGKEY}" Path
    !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuGroup
    !insertmacro SELECT_UNSECTION main ${UNSEC0000}
FunctionEnd

BrandingText "made by www.kingsoft.com"
BrandingText "made by www.kingsoft.com"
BrandingText "made by www.kingsoft.com"
