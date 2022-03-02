To run on windows
`node -e "require('./index').runNewman()"`

To run on linux
`node -e 'require("./index").runNewman()'`


To run this in a jenkins pipeline
```
cd $WORKSPACE/scor-tci-writer
npm install
node -e 'require("./index").runNewman()'
```