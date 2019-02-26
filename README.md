# CradCAD Assignment

This is a real-time browser based math game for up to 10 
concurrent users. The game is structured as a continuous series of rounds, where all connected players compete to submit the correct answer first. The number of rounds is not limited, players can connect at any time and start competing.

## Requirement
Docker 

## Installation
Step 1: Clone repository
```bash
git clone https://github.com/mihara0320/GrabCAD.git
```
Step 2: Initialize sub module 
```bash
git submodule init && git submodule update --remote --merge
```
or run bash script by
```bash
./init.sh (cmd)
```

Step 3: Run app using docker-compose 
```bash
docker-compose up
```

## Useage
Open the link below for the game Fron-tend
```bash
http://localhost:4200
```
Back-end API can is exposed to
```bash
https://localhost:44319/api
```

## Unit Test
Unit tests are automatically executed when docker builds image. However it can also be manually run by command below
```bash
dotnet test GrabCAD.UnitTests
```

## License
[MIT](https://choosealicense.com/licenses/mit/)
