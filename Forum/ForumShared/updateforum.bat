if "%1"=="" goto :default
set arg=%1
goto :next
:default
set arg=package
:next
pushd D:\workspace\Forum\ForumShared\
call mvn %arg%
popd
pushd D:\workspace\Forum\ForumServer\
call mvn %arg%
popd
pushd D:\workspace\Forum\ForumClient\
call mvn %arg%
popd
move "D:\workspace\Forum\ForumShared\target\ForumShared.jar" D:\Users\Sagi\Dropbox\Public
move "D:\workspace\Forum\ForumServer\target\ForumServer.jar" D:\Users\Sagi\Dropbox\Workshop\Forum\ass2
move "D:\workspace\Forum\ForumClient\target\ForumClient.jar" D:\Users\Sagi\Dropbox\Workshop\Forum\ass2