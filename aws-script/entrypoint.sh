#!/bin/bash
aws s3 cp s3://amit-automation/peddlejmeterautomation/test-scripts/ /app/test-scripts/ --recursive
chmod +x /app/test-scripts/entrypoint.sh
echo "Files downloaded successfully..."
touch "/app/flag/download_done.txt"
while [ ! -e "/app/flag/upload_start.txt" ]
do
    :
done
aws s3 cp /app/results/ s3://amit-automation/peddlejmeterautomation/results/  --recursive
echo "Files uploaded successfully..."
rm "/app/flag/upload_start.txt"
rm "/app/flag/download_done.txt"