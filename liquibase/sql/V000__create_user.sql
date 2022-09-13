--liquibase formatted sql
--changeset myname:create-users splitStatements:true endDelimiter:;
CREATE USER easy_surveys IDENTIFIED BY easy_surveys DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
CREATE USER easy_surveys_user IDENTIFIED BY easy_surveys_user DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
GRANT CREATE SESSION TO easy_surveys_user;