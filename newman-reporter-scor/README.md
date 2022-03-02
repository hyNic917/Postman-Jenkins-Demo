For easy local run/installation

1. Pack
`npm pack`

2. Install the tgz file (since it's not in npm)
`npm i -g newman-reporter-scor-1.0.0.tgz`

3. Run postman
`newman run ..\Petstore.postman_collection.json -r scor`