#!bin/bash
jmeter -n -t /jmeter/test-scripts/TestFileGet.jmx -l /jmeter/results/resultsGetByID.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=https://jmeterclidemo:443/Get/47EE423E-F36B-1410-8E9B-00971AF2703F -Jhttpmethod=GET -Jresponsetime=500
jmeter -n -t /jmeter/test-scripts/TestFileGet.jmx -l /jmeter/results/resultsGetAllData.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=https://jmeterclidemo:443/Get -Jhttpmethod=GET -Jresponsetime=500
jmeter -n -t /jmeter/test-scripts/TestFilePOST.jmx -l /jmeter/results/resultsPost.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=https://jmeterclidemo:443/Post -Jhttpmethod=POST -Jresponsetime=400
jmeter -n -t /jmeter/test-scripts/TestFilePut.jmx -l /jmeter/results/resultsPut.jtl -f -Jusers=1 -Jrampup=1 -Jendpoint=https://jmeterclidemo:443/UpdateByTownCode/823942 -Jhttpmethod=PUT -Jresponsetime=6 -Jloop=100
echo "execution completed successfully."
exit 0