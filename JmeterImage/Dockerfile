FROM justb4/jmeter:latest
RUN curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip" && \
    unzip awscliv2.zip && \
    ./aws/install && \
    rm awscliv2.zip 

ENTRYPOINT ["sh", "-c","/jmeter/test-scripts/entrypoint.sh"]