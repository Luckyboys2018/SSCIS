-- 02_CREATE_SEQUENCES
-- Vytvori sekvence pro generovani ID pri vkladani do tabulek

CREATE SEQUENCE SSCIS.SSCIS_PARAM_SEQ 
  START WITH 1
  INCREMENT BY 1;

CREATE SEQUENCE SSCIS.SSCIS_USER_SEQ 
  START WITH 1
  INCREMENT BY 1;

CREATE SEQUENCE SSCIS.EVENT_SEQ 
  START WITH 1
  INCREMENT BY 1;

CREATE SEQUENCE SSCIS.TUTOR_APPLICATION_SEQ 
  START WITH 1
  INCREMENT BY 1;

CREATE SEQUENCE SSCIS.TUTOR_APPLICATION_SUBJECT_SEQ 
  START WITH 1
  INCREMENT BY 1;

CREATE SEQUENCE SSCIS.APPROVAL_SEQ 
  START WITH 1
  INCREMENT BY 1;

CREATE SEQUENCE SSCIS.SSCIS_CONTENT_SEQ 
  START WITH 1
  INCREMENT BY 1;

CREATE SEQUENCE SSCIS.SSCIS_SESSION_SEQ 
  START WITH 1
  INCREMENT BY 1;

CREATE SEQUENCE SSCIS.ENUM_ROLE_SEQ 
  START WITH 1
  INCREMENT BY 1;

CREATE SEQUENCE SSCIS.ENUM_SUBJECT_SEQ 
  START WITH 1
  INCREMENT BY 1;
  
CREATE SEQUENCE SSCIS.PARTICIPATION_SEQ
  START WITH 1
  INCREMENT BY 1;

CREATE SEQUENCE SSCIS.FEEDBACK_SEQ
  START WITH 1
  INCREMENT BY 1;