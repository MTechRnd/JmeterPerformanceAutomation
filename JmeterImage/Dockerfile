# FROM amazon/aws-cli AS aws-download
# RUN mkdir -p /app/test-scripts
# WORKDIR /app/test-scripts
# ENV AWS_ACCESS_KEY_ID=AKIA6E3O5Z3XYDFI2D7S
# ENV AWS_SECRET_ACCESS_KEY=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

# RUN aws s3 cp s3://amit-automation/peddlejmeterautomation/test-scripts/ . --recursive

# FROM justb4/jmeter:latest AS jmeter-stage
# RUN mkdir jmeter
# WORKDIR /jmeter
# RUN mkdir test-scripts
# RUN mkdir results
# WORKDIR /jmeter/test-scripts

# COPY --from=aws-download /app/test-scripts/ .
# RUN chmod +x entrypoint.sh
# RUN ./entrypoint.sh

# FROM amazon/aws-cli AS awscli-upload
# ENV AWS_ACCESS_KEY_ID=AKIA6E3O5Z3XYDFI2D7S
# ENV AWS_SECRET_ACCESS_KEY=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

# WORKDIR /app
# RUN mkdir results
# WORKDIR /app/results

# COPY --from=jmeter-stage /jmeter/results/ .
# RUN aws s3 cp /app/results s3://amit-automation/peddlejmeterautomation/results/ --recursive
# ---------------------------------------------------------------------------------------------------------------

# FROM ubuntu:latest

# RUN apt-get install -y openjdk-11-jre-headless wget

# RUN wget -q https://downloads.apache.org/jmeter/binaries/apache-jmeter-5.4.1.tgz && \
#     tar -xzf apache-jmeter-5.4.1.tgz && \
#     rm apache-jmeter-5.4.1.tgz

# ENV JMETER_HOME /apache-jmeter-5.4.1
# ENV PATH $JMETER_HOME/bin:$PATH

# WORKDIR $JMETER_HOME


# FROM alpine 
# WORKDIR /Home
# RUN apk add --no-cache bash

# FROM justb4/jmeter:latest

FROM baseImage