#!/bin/bash
sleep 10
aws s3 cp s3://qa-api-performance-automation/test-scripts/ /app/test-scripts/ --recursive
echo "Files downloaded successfully..."
touch "/app/flag/download_done.txt"
while [ ! -e "/app/flag/upload_start.txt" ]
do
    :
done
aws s3 cp /app/results/ s3://qa-api-performance-automation/results/ --recursive
echo "Files uploaded successfully..."
rm "/app/flag/upload_start.txt"