#!/usr/bin/env bash
SOURCE=src/services
PROXY=src/proxy
ENV=.env

function trap_ctrlc ()
{
    for i in $P1 $P2 $P3 $P4 $P5
    do
        kill -9 -$(ps -o pgid= $i | grep -o '[0-9]*') 
    done
    exit
}

trap "trap_ctrlc" SIGINT

# .NET
dotnet run --project $SOURCE/aspnetcore/aspnetcore.csproj &
P1=$!

# Node
node $SOURCE/expressjs/app.js &
P2=$!

# Python
python3 -m venv $ENV
source $ENV/bin/activate
pip install --upgrade pip
pip install flask
python3 $SOURCE/flask/app.py &
P3=$!

# Go
FILE=go.work
if [ ! -f "$FILE" ]; then
    go work init $SOURCE/gin
fi
go run $SOURCE/gin/main.go &
P4=$!


# Setup the proxy
dotnet run --project $PROXY/proxy.csproj &
P5=$!

wait