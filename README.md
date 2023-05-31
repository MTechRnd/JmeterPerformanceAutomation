# JmeterPerformanceAutomation
This is the Jmeter performance automation project. The project is dockerized and docker compose is written.   
The database holds the location data of gujarat state and the latest and official mssql image is used to dockerized the database. And the data volume is bind mounted to get the data into the mssql table.  
justb4/jmeter is the unofficial image used to test the API. this is the image which contains .sh file by the help of that file we can run the command.  

### Docker-compose up command:
`docker-compose --env-file secrets.env up`  
This should be the command to run the docker containers. Here the credentials are come from the secrets.env file so we have to specify it in the command.  

### The command for creating database and seeding data into it.
`dotnet ef database update --connection "Server=localhost,<port>;Initial Catalog=<databasename>;User ID=<username>;Password=<Password>;TrustServerCertificate=true"`  
- when the command is run, the migration files are already there in the migration folder, so it will apply those pending migration. So it will create the database and table.  
- Also it will seed the data of gujarat state's district data containing 232 rows.  
* port: this is the local machine port which is binded to the mssql server container. we have binded it to 1436.  

### Options to configure the command for test:
* Jusers : Represents the number of users/threads.
* Jrampup : Represents the rampup time.
* Jendpoint : Represents the endpoint of request.
* Jhttpmethod : Represents the http method.
* Jresponsetime : Represents the expected response time. It is the duration time for the duration assertion. If command takes more than the given Jresponsetime the assertion will fail and it will give error as 500 and response message as Time Limit Exceed. For all the below commands, default response time for duration assertion is 10 milliseconds. 
* Jloop : Represents the loop count for the testing plan to execute.
* Jstartupdelay : Represents the startup delay.
* Jduration : Represents the duration for the thread execution.
* Jinputfile : Define the path of the input data csv file(used in the POST endpoint to define the payload data).
For more information visit: https://www.toolsqa.com/jmeter/thread-group-in-jmeter-test-plan

### commandscript.sh file:
This is the file containing all the commands to run when the jmeter container will run. As it is the shell script, it can containes multiple commands with multiple endpoints and multiple test file(.jmx file).  
You can append any command to this file to test any endpoint.  

### About JWT bearer:
The code for Jwt token generation is there with the /GetToken endpoint. It is secured with password "Amit@123" for preventing it from misclick and unnecessary token generation.  
When we request to this endpoint it will return us the token with all the configuration and the token expiry date is 1 year. which is set in the code.  
Note: All the test files(.jmx files) are containing one token in the http header manager with authorization attribute. You can manually open the test file and change the authorization header value with new token.  
Note: The authorization header value must be like Bearer <token-value>. i.e. Bearer<space><token-value>. If it is not, then it will give error as unauthorized.  

## Tested command for examples
### Get By ID command:
`entrypoint: ["sh", "-c","jmeter -n -t \"/jmeter/test-scripts/TestFileGet.jmx\" -l /jmeter/results/resultsGetByID.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=\"https://jmeterclidemo:443/Get/3\" -JhttpMethod=\"GET\" -Jresponsetime=5000"]`  
Here in above command if we don't pass the method then it will take GET as default method.  
***

### Get All data command:
`entrypoint: ["sh", "-c","jmeter -n -t \"/jmeter/test-scripts/TestFileGet.jmx\" -l /jmeter/results/resultsGetAllData.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=\"https://jmeterclidemo:443/Get\" -JhttpMethod=\"GET\" -Jresponsetime=6000"]`  
Here in above command if we don't pass the method then it will take GET as default method. and response time is 6000 milliseconds for each thread.  
***

### Post Request:
`entrypoint: ["sh", "-c","jmeter -n -t \"/jmeter/test-scripts/TestFilePOSTorPUT.jmx\" -l /jmeter/results/resultsPost.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=\"https://jmeterclidemo:443/Post\" -JhttpMethod=\"POST\" -Jresponsetime=5000"]`  
Here in above command if we don't pass the method then it will take POST as default method. and response time is 5000 milliseconds for each thread.  
***

### Put Request:
`entrypoint: ["sh", "-c","jmeter -n -t \"/jmeter/test-scripts/TestFilePut.jmx\" -l /jmeter/results/resultsPut.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=\"https://jmeterclidemo:443/UpdateByTownCode/823942\" -JhttpMethod=\"PUT\" -Jresponsetime=5000  -Jstartupdelay=1 -Jduration=100"`  
Here in above command if we don't pass the method then it will take PUT as default method. and response time is 5000 milliseconds for each thread.  
***

### Delete Request:
`entrypoint: ["sh", "-c","jmeter -n -t \"/jmeter/test-scripts/TestFileDelete.jmx\" -l /jmeter/results/resultsDelete.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=\"https://jmeterclidemo:443/DeleteByTownCode/823942\" -JhttpMethod=\"DELETE\" -Jresponsetime=4500 -Jstartupdelay=5 -Jduration=100 -Jloop=1"]`  

`entrypoint: ["sh", "-c","jmeter -n -t \"/jmeter/test-scripts/TestFileDelete.jmx\" -l /jmeter/results/resultsDelete.jtl -f -Jusers=1 -Jrampup=1 -Jendpoint=\"https://jmeterclidemo:443/DeleteByTownCode/823942\" -JhttpMethod=\"DELETE\" -Jresponsetime=4500 -Jloop=100"]`  
Here in above command if we don't pass the method then it will take DELETE as default method. and response time is 6000 milliseconds for each thread. here if we try to run the first command then 100 threads are run at the same time and got only few documents to delete. so after deleting those documents, other remaining threads will try to delete those deleted documents. So it will give error.  
***

