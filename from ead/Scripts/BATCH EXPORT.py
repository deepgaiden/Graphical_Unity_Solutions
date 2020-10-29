import maya.cmds as cmds
import maya.mel as mel
import os

# deletes old window and preference, if it still exists
if(cmds.window('uiWindow_fbxLoopExport', q=1, ex=1)):
    cmds.deleteUI('uiWindow_fbxLoopExport')
if(cmds.windowPref('uiWindow_fbxLoopExport', q=1, ex=1)):
    cmds.windowPref('uiWindow_fbxLoopExport', r=1)


def dirPath(filePath, fileType):
    cmds.textFieldButtonGrp('Dir', edit=True, text=str(filePath))
    return 1


def startExport(path):
    list = cmds.ls(sl=1)
    filePathStr = cmds.textFieldButtonGrp('Dir', query=True, text=True)
    for obj in list:
        cmds.spaceLocator(name='gridCenter')
        cmds.select(obj)
        cmds.move('gridCenter')
        cmds.pointConstraint('gridCenter', obj)
        cmds.pointConstraint(remove=1)
        obj_export = filePathStr + obj + '.fbx'
        cmds.delete('gridCenter')
        cmds.select(obj)
        cmds.makeIdentity( apply=True, t=1, r=1, s=1, n=2 )
        try:
            cmds.select(obj, replace=True)
            cmds.delete(ch=1)
            mel.eval('FBXExport -f "{}" -s'.format(obj_export))
        except:
            print "Ignoring object named: '%s'. Export failed, probably not a polygonal object. "%(obj)

    print "Exporting Complete!"



def browseIt():
    cmds.fileBrowserDialog(m=4, fc=dirPath, ft='directory', an='Choose Save Location')
    return




def makeGui():
    uiWindow_fbxLoopExport = cmds.window('uiWindow_fbxLoopExport', title="David P's Batch FBX exporter", iconName='uiWindow_fbxLoopExport', widthHeight=(280, 70))
    cmds.columnLayout('uiColWrapper', w=375, adjustableColumn=False, parent='uiWindow_fbxLoopExport' )
    # cmds.text( label='Settings', align='left', parent = 'uiColWrapper')
    cmds.textFieldButtonGrp('Dir', label='Save Location', cw3=[80,190,50], text='(Choose Save Location)', buttonLabel='Browse', buttonCommand=browseIt, parent='uiColWrapper')
    cmds.button('startExport', label="Export That Sh*t!", parent='uiColWrapper', width = 322, command = startExport)
    cmds.showWindow(uiWindow_fbxLoopExport)


makeGui()