#!/bin/bash
echo "Execution started..."
while [ ! -e "/jmeter/flag/download_done.txt" ]; do
    :
done;
rm "/jmeter/flag/download_done.txt"
jmeter -n -t /jmeter/test-scripts/TestFileGet.jmx -l /jmeter/results/TestFileGetByID-detail.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=http://jmeterclidemo:80/Get/13F3423E-F36B-1410-80A5-00EF02B43D90 -Jhttpmethod=GET -Jtokenendpoint=http://jmeterclidemo:80/GetToken -Jresponsetime=4500 > /jmeter/results/TestFileGetByID-summary.txt -Dfile.encoding=UTF-8
jmeter -n -t /jmeter/test-scripts/TestFileGet.jmx -l /jmeter/results/TestFileGet-detail.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=http://jmeterclidemo:80/Get -Jhttpmethod=GET -Jtokenendpoint=http://jmeterclidemo:80/GetToken  -Jresponsetime=800 > /jmeter/results/TestFileGet-summary.txt -Dfile.encoding=UTF-8
jmeter -n -t /jmeter/test-scripts/TestFilePOST.jmx -l /jmeter/results/TestFilePOST-detail.jtl -f -Jusers=100 -Jrampup=1 -Jendpoint=http://jmeterclidemo:80/Post -Jhttpmethod=POST -Jtokenendpoint=http://jmeterclidemo:80/GetToken  -Jinputfile=/jmeter/test-scripts/data.csv -Jresponsetime=700 > /jmeter/results/TestFilePOST-summary.txt -Dfile.encoding=UTF-8
jmeter -n -t /jmeter/test-scripts/TestFilePut.jmx -l /jmeter/results/TestFilePut-detail.jtl -f -Jusers=1 -Jrampup=1 -Jendpoint=http://jmeterclidemo:80/UpdateByTownCode/823942 -Jhttpmethod=PUT -Jtokenendpoint=http://jmeterclidemo:80/GetToken  -Jresponsetime=6 -Jloop=100 > /jmeter/results/TestFilePut-summary.txt -Dfile.encoding=UTF-8
echo "Execution completed successfully..."

touch "/jmeter/flag/upload_start.txt"
exit 0
