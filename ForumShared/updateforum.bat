if "%1"=="" goto :default
set arg=%1
goto :next
:default
set arg=package
:next
pushd D:\git\workspace\ForumShared\
call mvn %arg%
popd
pushd D:\git\workspace\ForumServer\
call mvn %arg%
popd
pushd D:\git\workspace\ForumClient\
call mvn %arg%
popd
move "D:\git\workspace\ForumShared\target\ForumShared.jar" D:\Users\Sagi\Dropbox\Public
move "D:\git\workspace\ForumServer\target\ForumServer.jar" D:\Users\Sagi\Dropbox\Workshop\Forum\ass2
move "D:\git\workspace\ForumClient\target\ForumClient.jar" D:\Users\Sagi\Dropbox\Workshop\Forum\ass2