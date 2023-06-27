#!/bin/bash
echo "Execution started..."
while [ ! -e "/jmeter/flag/download_done.txt" ]; do
    :
done;
rm "/jmeter/flag/download_done.txt"
jmeter -n -t /jmeter/test-scripts/TestFileGet.jmx -l /jmeter/results/TestFileGetByID-detail.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=https://jmeterclidemo:443/Get/13F3423E-F36B-1410-80A5-00EF02B43D90 -Jhttpmethod=GET -Jtokenendpoint=https://jmeterclidemo:443/GetToken -Jresponsetime=4500 > /jmeter/results/TestFileGetByID-summary.txt -Dfile.encoding=UTF-8
jmeter -n -t /jmeter/test-scripts/TestFileGet.jmx -l /jmeter/results/TestFileGet-detail.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=https://jmeterclidemo:443/Get -Jhttpmethod=GET -Jtokenendpoint=https://jmeterclidemo:443/GetToken  -Jresponsetime=800 > /jmeter/results/TestFileGet-summary.txt -Dfile.encoding=UTF-8
jmeter -n -t /jmeter/test-scripts/TestFilePOST.jmx -l /jmeter/results/TestFilePOST-detail.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=https://jmeterclidemo:443/Post -Jhttpmethod=POST -Jtokenendpoint=https://jmeterclidemo:443/GetToken  -Jinputfile=/jmeter/test-scripts/data.csv -Jresponsetime=700 > /jmeter/results/TestFilePOST-summary.txt -Dfile.encoding=UTF-8
jmeter -n -t /jmeter/test-scripts/TestFilePut.jmx -l /jmeter/results/TestFilePut-detail.jtl -f -Jusers=1 -Jrampup=1 -Jendpoint=https://jmeterclidemo:443/UpdateByTownCode/823942 -Jhttpmethod=PUT -Jtokenendpoint=https://jmeterclidemo:443/GetToken  -Jresponsetime=6 -Jloop=100 > /jmeter/results/TestFilePut-summary.txt -Dfile.encoding=UTF-8
echo "Execution completed successfully..."

touch "/jmeter/flag/upload_start.txt"
exit 0
