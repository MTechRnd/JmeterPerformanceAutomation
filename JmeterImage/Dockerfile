# FROM justb4/jmeter:latest
# RUN curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip" && \
#     unzip awscliv2.zip && \
#     ./aws/install && \
#     rm awscliv2.zip 

# ENTRYPOINT ["sh", "-c","/jmeter/test-scripts/entrypoint.sh"]


FROM amazon/aws-cli AS aws-download
RUN mkdir -p /app/test-scripts
WORKDIR /app/test-scripts


RUN aws s3 cp s3://amit-automation/peddlejmeterautomation/test-scripts/ . --recursive

FROM justb4/jmeter:latest AS jmeter-stage
RUN mkdir jmeter
WORKDIR /jmeter
RUN mkdir test-scripts
RUN mkdir results
WORKDIR /jmeter/test-scripts

COPY --from=aws-download /app/test-scripts/ .
RUN chmod +x entrypoint.sh
RUN ./entrypoint.sh

FROM amazon/aws-cli AS awscli-upload

WORKDIR /app
RUN mkdir results
WORKDIR /app/results

# Copy result file from the JMeter stage
COPY --from=jmeter-stage /jmeter/results/ .

# Upload the result file using AWS CLI
RUN aws s3 cp /app/results s3://amit-automation/peddlejmeterautomation/results/ --recursive
