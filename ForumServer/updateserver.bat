@echo off
if "%1"=="" goto :default
set arg=%1
goto :next
:default
set arg=package
:next
pushd D:\git\workspace\ForumServer\
call mvn %arg%
popd
move "D:\git\workspace\ForumServer\target\ForumServer.jar" D:\Users\Sagi\Dropbox\Workshop\Forum\ass2