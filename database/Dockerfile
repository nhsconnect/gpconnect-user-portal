FROM amazon/aws-cli:2.7.16 as aws-cli

FROM flyway/flyway:9.0.1-azure as flyway

FROM library/amazoncorretto:11.0.15

RUN yum install --assumeyes jq-1.5-1.amzn2.0.2.x86_64 && \
    yum clean all && \
    rm -rf /var/cache/yum

COPY --from=aws-cli /usr/local/aws-cli/v2/current/ /usr/local/aws-cli/v2/current/
COPY --from=aws-cli /usr/local/bin/ /usr/local/bin/
COPY --from=flyway /flyway/ /flyway/

WORKDIR /flyway

ENV PATH=/flyway:/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin

COPY . /flyway/sql

ENV FLYWAY_EDITION=community
ENV FLYWAY_PLACEHOLDERS_logger: ndsa

ENTRYPOINT [ "flyway" ]
CMD ["-?"]
