FROM openjdk:17.0.2-jdk

ARG JAR_FILE=server/target/*.jar

COPY ${JAR_FILE} app.jar

ENTRYPOINT ["java","-jar","-Dspring.profiles.active=prod", "/app.jar"]